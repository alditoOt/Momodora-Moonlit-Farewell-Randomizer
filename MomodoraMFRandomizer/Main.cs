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
            //skillRandomizer.randomizeSkills();
            demonStringRemover.removeAllBlockers();
        }
        public override void OnUpdate()
        {
            //GameData.current.MomoEvent[10] = 1;
            skillRandomizer.randomizeSkills();
        }
    }
}
