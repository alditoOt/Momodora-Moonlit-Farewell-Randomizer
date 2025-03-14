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
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (SceneManager.GetActiveScene().name == "BrightnessSetup")
            {
                SceneManager.LoadScene("Well01");
            }
            demonStringRemover.removeAllBlockers();
            skillRandomizer.loadAcquiredSkills();
        }
        public override void OnUpdate()
        {
            GameData.current.MomoEvent[20] = 1;
           skillRandomizer.randomizeSkills();
        }
    }
}
