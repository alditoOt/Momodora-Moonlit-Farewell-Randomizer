using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace APMomoMFRandomizer
{
    [HarmonyPatch(typeof(SceneManager))]
    class SceneManagerPatcher
    {
        //[HarmonyPatch("OnSceneWasLoaded")]
        //[HarmonyPrefix]
        private static void RestoreSkillEvent(int index, string name)
        {
            
            MelonLoader.MelonLogger.Msg($"Scene {name} loaded.");
        }
    }
}
