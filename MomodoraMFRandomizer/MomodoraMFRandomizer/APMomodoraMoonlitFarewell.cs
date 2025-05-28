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
        #endregion
        
        BlockRemover blockRemover = new BlockRemover();
        private bool mainMenu = true;
        private static Boolean manualItem = false;

        #region Socket Logging
        static void Socket_ErrorReceived(Exception e, string message)
        {
            MelonLogger.Msg($"Socket Error: {message}");
            MelonLogger.Msg($"Socket Exception: {e.Message}");

            if (e.StackTrace != null)
                foreach (var line in e.StackTrace.Split('\n'))
                    MelonLogger.Msg($"    {line}");
            else
                MelonLogger.Msg($"    No stacktrace provided");
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
            server = ConfigLoader.config.server;
            username = ConfigLoader.config.username;
            password = ConfigLoader.config.password;
            #endregion
            try
            {
                session = ArchipelagoSessionFactory.CreateSession(server);
                APConnector.Connect(session, server, username, password);
                session.Items.ItemReceived += APLocationHandler.UpdateItemsForTheSession;
                GameDataPatcher.UpdateShopNames();
                CollectSocketInfo();
                YAMLUtils.GetSettingsFromYAML();
                YAMLUtils.AddItemsToItemPool();
                if (YAMLUtils.DEATHLINK)
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
                MelonLogger.Msg($"An error occured when trying to create the session: {e.Message}");
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
            if(YAMLUtils.OPENSPRINGLEAFPATH)
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
