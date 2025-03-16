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

namespace MomodoraMFRandomizer
{
    public class Main : MelonMod
    {

        BlockRemover demonStringRemover = new BlockRemover();
        SkillRandomizer skillRandomizer = new SkillRandomizer();
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
            if (mainMenu)
            {
                skillRandomizer.PersistAcquiredSkills();
                mainMenu = false;
            }
            if (sceneName == "BrightnessSetup")
            {
                skillRandomizer.InitializeSkills();
            }
            MelonLogger.Msg("Scene loaded: " + sceneName);
            demonStringRemover.removeAllBlockers();
            skillRandomizer.ReloadSceneCheck(sceneName);
            CheckMomoEventValue();
        }
        public override void OnUpdate()
        {
            skillRandomizer.randomizeSkills();
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
