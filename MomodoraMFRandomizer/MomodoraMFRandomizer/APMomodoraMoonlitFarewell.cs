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
        
        static DeathLinkService deathLinkService;
        static APDeathLinkHandler deathLinkHandler = new APDeathLinkHandler();

        public static ArchipelagoSession session;
        #endregion
        private static bool loggedIn;

        // True once a login has succeeded, and stays true through a disconnect: the session object
        // keeps its received items / checked locations, so gameplay logic can keep using them
        // while APConnectionManager reconnects
        public static bool HasSession => loggedIn && session != null;

        BlockRemover blockRemover = new BlockRemover();
        private bool mainMenu = true;

        #region Socket Logging
        static void Socket_ErrorReceived(Exception e, string message)
        {
            if (!APConnectionManager.IsConnected)
            {
                // Disconnected so don't write to the console every time
                return;
            }
            MelonLogger.Error($"Socket Error: {message}");
            MelonLogger.Error($"Socket Exception: {e.Message}");

            if (e.StackTrace != null)
                foreach (var line in e.StackTrace.Split('\n'))
                    MelonLogger.Error($"    {line}");
            else
                MelonLogger.Error($"    No stacktrace provided");
        }

        private static void CollectSocketInfo(ArchipelagoSession target)
        {
            target.Socket.ErrorReceived += Socket_ErrorReceived;
            target.Socket.SocketOpened += () => MelonLogger.Msg($"Socket opened to: {target.Socket.Uri}");
            target.Socket.SocketClosed += reason =>
            {
                MelonLogger.Msg($"Socket closed: {reason}");
                // A closed event from a session we've already replaced is stale
                if (target == session)
                {
                    APConnectionManager.MarkDisconnected(reason);
                }
            };
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
                if (!APConnector.Connect(session, server, username, password))
                {
                    return;
                }
                loggedIn = true;
                AttachSession(session);
                GameDataPatcher.UpdateShopNames();
                SlotDataUtils.GetSettingsFromYAML();
                SlotDataUtils.AddItemsToItemPool();
                APLocationScoutCache.Initialize();
                SetupDeathLink();
                APConnectionManager.Start();
            }
            catch (Exception e)
            {
                MelonLogger.Error($"An error occured when trying to create the session: {e}");
            }
        }

        // Sets a newly logged-in session into the mod; used when starting up and reconnecting
        private static void AttachSession(ArchipelagoSession target)
        {
            CollectSocketInfo(target);
            // Items delivered during Connect are already queued; discard them so they don't
            // surface as notifications on the next real item
            while (target.Items.Any())
            {
                target.Items.DequeueItem();
            }
            initialSyncDrained = true;
            // ItemReceived fires on the socket thread, but granting items and showing popups touch
            // Unity objects, so both are handed to the main thread
            target.Items.ItemReceived += itemHandler => APConnectionManager.RunOnMainThread(() =>
            {
                if (target != session)
                {
                    return;
                }
                APLocationHandler.UpdateItemsForTheSession(itemHandler);
                NotifyNewlyReceivedItems(itemHandler);
            });
        }

        private static void SetupDeathLink()
        {
            if (!SlotDataUtils.DEATHLINK)
            {
                return;
            }
            deathLinkService = session.CreateDeathLinkService();
            deathLinkService.EnableDeathLink();
            deathLinkService.OnDeathLinkReceived += deathLinkObject => APConnectionManager.RunOnMainThread(() =>
            {
                Platformer3D.player_hp = 0f;
                deathLinkHandler.SetIsDead(true);
            });
        }

        // Called from APConnectionManager's worker thread (never the game thread) after a connection drop
        // Builds a brand-new session; the old one is left to die (Sadge) and is replaced only on success
        internal static bool TryReconnectSession()
        {
            try
            {
                ArchipelagoSession newSession = ArchipelagoSessionFactory.CreateSession(server);
                if (!APConnector.Connect(newSession, server, ConfigLoader.config.username, ConfigLoader.config.password))
                {
                    return false;
                }
                session = newSession;
                AttachSession(newSession);
                SetupDeathLink();
                return true;
            }
            catch (Exception e)
            {
                MelonLogger.Warning($"Reconnect attempt failed: {e.GetBaseException().Message}");
                return false;
            }
        }

       // Check the history of already received items so that we don't fire a popup notification
       // when updating items for the session after a reconnect
        private static bool initialSyncDrained;

        private static void NotifyNewlyReceivedItems(ReceivedItemsHelper itemHandler)
        {
            bool isInitialSync = !initialSyncDrained;
            initialSyncDrained = true;
            while (itemHandler.Any())
            {
                ItemInfo item = itemHandler.DequeueItem();
                if (!isInitialSync && item.Player.Slot != session.ConnectionInfo.Slot)
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
            if(SlotDataUtils.OPEN_SPRINGLEAF_PATH)
            {
                blockRemover.removeAllBlockers(sceneName);
            }
            blockRemover.RemoveGynBarrier(sceneName);
            APSkillHandler.HandleSkillOnSceneLoad(sceneName, mainMenu);
        }

        public override void OnFixedUpdate()
        {
            APConnectionManager.DrainMainThreadActions();
            deathLinkHandler.CheckDeathLink(deathLinkService, username);
        }
    }
}
