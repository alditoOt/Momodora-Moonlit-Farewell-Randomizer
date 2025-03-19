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
using MomodoraMFRandomizer.Patches;
using MomodoraMoonlitFarewellAP.Patches;
using MomodoraMoonlitFarewellAP.Utils;

namespace MomodoraMFRandomizer
{
    public class APRandomizer : MelonMod
    {
        #region AP variables
        //All these should be loaded from a config.json file eventually
        //private string localhost = "localhost:38281";
        private static string server; 
        private string username; 
        private string password;
        private bool openSpringleafPath;
        private bool deathlink;
        
        //This should be loaded from the YAML file eventually
        DeathLinkService deathLinkService;
        APDeathLinkHandler deathLinkHandler = new APDeathLinkHandler();
        APLocationHandler locationHandler = new APLocationHandler();

        public static ArchipelagoSession session;
        #endregion
        
        BlockRemover demonStringRemover = new BlockRemover();
        private HashSet<int> previousSceneValues = new HashSet<int>();
        private bool mainMenu = true;

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
        public override void OnInitializeMelon()
        {
            ConfigLoader.LoadConfig();
            server = ConfigLoader.config.server;
            username = ConfigLoader.config.username;
            password = ConfigLoader.config.password;
            deathlink = ConfigLoader.config.deathlink;
            openSpringleafPath = ConfigLoader.config.openSpringleafPath;
            APLocationHandler.fastTravel = ConfigLoader.config.fastTravel;
            MelonLogger.Msg($"Open Springleaf Path: {openSpringleafPath}");
            try
            {
                session = ArchipelagoSessionFactory.CreateSession(server);
                session.Items.ItemReceived += APLocationHandler.ReceiveItem;
                APConnector.Connect(session, server, username, password);
                CollectSocketInfo();
                if (deathlink)
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

        public override void OnLateInitializeMelon()
        {
            locationHandler.InitializeDictionary();
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            MelonLogger.Msg($"Unloading scene. Scene count: {SceneManager.sceneCount}");
            if (SceneManager.sceneCount == 2)
            {
                mainMenu = true;
            }
            MelonLogger.Msg($"Scene {sceneName} unloaded. Main menu: {mainMenu}");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLogger.Msg($"Scene count: {SceneManager.sceneCount}");
            if (mainMenu && SceneManager.sceneCount >= 2)
            {
                locationHandler.UpdateItemsForTheSession(session);
                mainMenu = false;
            }
            locationHandler.ResetLocationSceneForSkill(sceneName);
            MelonLogger.Msg($"Scene {sceneName} loaded. Main menu: {mainMenu}");
            if(openSpringleafPath)
            {
                demonStringRemover.removeAllBlockers();
            }
        }

        public override void OnFixedUpdate()
        {
            deathLinkHandler.CheckDeathLink(deathLinkService, username);
        }

        public void CheckMomoEventValue()
        {
            for (int i = 0; i < GameData.current.MomoEvent.Length; i++)
            {
                if (GameData.current.MomoEvent[i] == 1 && !previousSceneValues.Contains(i))
                {
                    previousSceneValues.Add(i);
                    MelonLogger.Msg("Event value: " + i);
                }
            }
        }
    }
}
