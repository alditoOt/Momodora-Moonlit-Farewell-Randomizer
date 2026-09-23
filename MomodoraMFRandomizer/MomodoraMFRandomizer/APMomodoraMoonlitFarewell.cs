using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using HarmonyLib;
using UnityEngine.SceneManagement;
using System.Collections;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using System.Reflection;
using APMomodoraMoonlitFarewell.Archipelago;
using APMomodoraMoonlitFarewell.Patches;
using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell
{
    public class APMomodoraMoonlitFarewell : MelonMod
    {
        #region AP variables
        private static string server; 
        private string username; 
        private string password;
        
        DeathLinkService deathLinkService;
        APDeathLinkHandler deathLinkHandler = new APDeathLinkHandler();
        
        public static ArchipelagoSession session;

        // Harmony patches fire on gameplay events independent of whether startup finished connecting,
        // so anything touching `session` should check this first.
        public static bool IsSessionActive => session != null && session.Socket != null && session.Socket.Connected;
        #endregion
        
        BlockRemover blockRemover = new BlockRemover();
        private bool mainMenu = true;

        #region Socket Logging
        static void Socket_ErrorReceived(Exception e, string message)
        {
            MelonLogger.Error($"Socket Error: {message}");
            MelonLogger.Error($"Socket Exception: {e.Message}");

            if (e.StackTrace != null)
                foreach (var line in e.StackTrace.Split('\n'))
                    MelonLogger.Error($"    {line}");
            else
                MelonLogger.Error($"    No stacktrace provided");
        }
        static void Socket_SocketOpened() =>
            MelonLogger.Msg($"Socket opened to: {session.Socket.Uri}");
        static void Socket_SocketClosed(string reason) =>
            MelonLogger.Msg($"Socket closed: {reason}");

        private void CollectSocketInfo()
        {
            session.Socket.ErrorReceived += Socket_ErrorReceived;
            session.Socket.SocketOpened += Socket_SocketOpened;
            session.Socket.SocketClosed += Socket_SocketClosed;
        }

        #endregion
        
        //When starting the game
        public override void OnLateInitializeMelon()
        {
            #region Server Info
            //Load server info from config
            ConfigLoader.LoadConfig();
            if (ConfigLoader.config == null || string.IsNullOrEmpty(ConfigLoader.config.server))
            {
                MelonLogger.Error("Server config is missing or has no server address; aborting Archipelago setup.");
                return;
            }
            server = ConfigLoader.config.server;
            username = ConfigLoader.config.username;
            password = ConfigLoader.config.password;
            #endregion
            try
            {
                session = ArchipelagoSessionFactory.CreateSession(server);
                APConnector.Connect(session, server, username, password);
                session.Items.ItemReceived += APLocationHandler.UpdateItemsForTheSession;
                session.Items.ItemReceived += NotifyNewlyReceivedItems;
                GameDataPatcher.UpdateShopNames();
                CollectSocketInfo();
                SlotDataUtils.GetSettingsFromYAML();
                SlotDataUtils.AddItemsToItemPool();
                APLocationScoutCache.Initialize();
                if (SlotDataUtils.DEATHLINK)
                {
                    deathLinkService = session.CreateDeathLinkService();
                    deathLinkService.EnableDeathLink();
                    deathLinkService.OnDeathLinkReceived += (deathLinkObject) =>
                    {
                        Platformer3D.player_hp = 0f;
                        deathLinkHandler.SetIsDead(true);
                    };
                }
            }
            catch (Exception e)
            {
                MelonLogger.Error($"An error occured when trying to create the session: {e}");
            }
        }

        // Independent of APLocationHandler.UpdateItemsForTheSession, which re-scans the entire
        // received-items history on every resync (scene load, reconnect). Draining the helper's
        // new-item queue here instead ensures a popup fires exactly once per genuinely new item.
        private static void NotifyNewlyReceivedItems(ReceivedItemsHelper itemHandler)
        {
            while (itemHandler.Any())
            {
                ItemInfo item = itemHandler.DequeueItem();
                if (item.Player.Slot != session.ConnectionInfo.Slot)
                {
                    APExchangeNotifier.NotifyReceived(item);
                }
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (SceneManager.sceneCount == 2)
            {
                mainMenu = true;
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (mainMenu && SceneManager.sceneCount >= 2)
            {
                APLocationHandler.UpdateItemsForTheSession(null);
                mainMenu = false;
                MomoEventUtils.DEFAULT_EVENTS_TO_1.ForEach(x => GameData.current.MomoEvent[x] = 1);
                MomoEventUtils.GrowTimedBerries();
            }
            if(SlotDataUtils.OPENSPRINGLEAFPATH)
            {
                blockRemover.removeAllBlockers(sceneName);
            }
            blockRemover.RemoveGynBarrier(sceneName);
            APSkillHandler.HandleSkillOnSceneLoad(sceneName, mainMenu);
        }

        public override void OnFixedUpdate()
        {
            deathLinkHandler.CheckDeathLink(deathLinkService, username);
        }
    }
}
