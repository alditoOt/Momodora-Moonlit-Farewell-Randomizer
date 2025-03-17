using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MomodoraAPRandomizer.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace MomodoraAPRandomizer
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class APRandomizer : BaseUnityPlugin
    {
        #region BepInEx variables
        private const string modGUID = "MomoAPRandomizer";
        private const string modName = "Momodora Moonlit Farewell: AP Randomizer";
        private const string modVersion = "0.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static APRandomizer instance;

        public static ManualLogSource Logger;

        #endregion
        
        #region AP Variables
        private static string server = "localhost:38281";
        private static string user = "alditto";
        private static string password = "";

        private string[] tags = new string[] { "deathlink" };

        private static ArchipelagoSession session;
        DeathLinkService deathLinkService;
        #endregion

        static void Socket_ErrorReceived(Exception e, string message)
        {
            Logger.LogInfo($"Socket Error: {message}");
            Logger.LogInfo($"Socket Exception: {e.Message}");

            if (e.StackTrace != null)
                foreach (var line in e.StackTrace.Split('\n'))
                    Logger.LogInfo($"    {line}");
            else
                Logger.LogInfo($"    No stacktrace provided");
        }
        static void Socket_SocketOpened() =>
            Logger.LogInfo($"Socket opened to: {session.Socket.Uri}");
        static void Socket_SocketClosed(string reason) =>
            Logger.LogInfo($"Socket closed: {reason}");
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            Logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            Logger.LogInfo("Initializing Momodora Moonlit Farewell: AP Randomizer");

            try
            {
                session = ArchipelagoSessionFactory.CreateSession(new Uri("ws://" + server));
            } catch(Exception e)
            {
                Logger.LogInfo($"Error creating session: {e.Message}");
                return;
            }

            session.Socket.ErrorReceived += Socket_ErrorReceived;
            session.Socket.SocketOpened += Socket_SocketOpened;
            session.Socket.SocketClosed += Socket_SocketClosed;
            APConnector.Connect(session, server, user, password);

            if (tags.Contains("deathlink"))
            {
                deathLinkService = session.CreateDeathLinkService();
                deathLinkService.EnableDeathLink();
            }
            harmony.PatchAll();
        }
    }
}
