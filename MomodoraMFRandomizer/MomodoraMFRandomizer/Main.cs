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

namespace MomodoraMFRandomizer
{
    public class Main : MelonMod
    {
        private static string server = "archipelago.gg:45419";
        static ArchipelagoSession session = ArchipelagoSessionFactory.CreateSession(server);
        public override void OnInitializeMelon()
        {
            //APConnector.Connect(session, server, "alditto", "");
        }
        #region Randomizer

        BlockRemover demonStringRemover = new BlockRemover();
        Platformer3D p3d = GameObject.FindObjectOfType<Platformer3D>(); //Could be useful?

        private HashSet<int> previousSceneValues = new HashSet<int>();
        private bool mainMenu = false;

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (SceneManager.sceneCount <= 2)
            {
                mainMenu = true;
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            // This should happen if the settings file has this enabled
            demonStringRemover.removeAllBlockers();
            CheckMomoEventValue();
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
        #endregion

    }
}
