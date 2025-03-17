using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraAPRandomizer.Patches
{
    [HarmonyPatch(typeof(GameData))]
    class MomoEventPatch
    {
        [HarmonyPatch("SetFlag")]
        [HarmonyPostfix]
        static void AssignSprint()
        {
            if (GameData.current.MomoEvent[9] == 0)
            {
                APRandomizer.Logger.LogInfo("MomoEventPatch: Assigning Sprint");
                GameData.current.MomoEvent[9] = 1;
            }
        }
    }
}
