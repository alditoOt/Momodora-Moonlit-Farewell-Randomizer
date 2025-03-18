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

namespace MomodoraMFRandomizer
{
    public class APRandomizer : MelonMod
    {
        #region AP variables
        //All these should be loaded from a config.json file eventually
        private static string localhost = "localhost:38281";
        private static string server = "archipelago.gg:56527";
        private string username = "alditto";
        private string password = "";

        //This should be loaded from the YAML file eventually
        private string[] tags = new string[] { "deathlink" };
        DeathLinkService deathLinkService;
        APDeathLinkHandler deathLinkHandler = new APDeathLinkHandler();
        APLocationHandler locationHandler = new APLocationHandler();

        public static ArchipelagoSession session = ArchipelagoSessionFactory.CreateSession(localhost);
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

        #endregion
        //When starting the game
        public override void OnLateInitializeMelon()
        {
            //Attempt connection to the server

            MelonLogger.Msg("Attempting connection");
            APConnector.Connect(session, server, username, password);
            locationHandler.UpdateItemsForTheSession(session);
            session.Items.ItemReceived += APLocationHandler.ReceiveItem;
            session.Socket.ErrorReceived += Socket_ErrorReceived;
            session.Socket.SocketOpened += Socket_SocketOpened;
            session.Socket.SocketClosed += Socket_SocketClosed;

            if (tags.Contains("deathlink"))
            {
                deathLinkService = session.CreateDeathLinkService();
                deathLinkService.EnableDeathLink();
            }
            locationHandler.InitializeDictionary();
        }



        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (SceneManager.sceneCount <= 2)
            {
                mainMenu = true;
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (SceneManager.sceneCount >= 3)
            {
                mainMenu = false;
            }
            demonStringRemover.removeAllBlockers(); // This should happen if the YAML file has enabled opening up the first area
            locationHandler.ResetLocationSceneForSkill(sceneName);
        }

        public override void OnUpdate()
        {
            if (tags.Contains("deathlink"))
            {
                deathLinkHandler.CheckDeathLink(deathLinkService, username);
            }
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
