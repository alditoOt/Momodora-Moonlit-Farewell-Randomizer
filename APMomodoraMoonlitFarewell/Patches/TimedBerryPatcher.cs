using System;
using APMomodoraMoonlitFarewell.Utils;
using HarmonyLib;

namespace APMomodoraMoonlitFarewell.Patches;

[HarmonyPatch(typeof(SoilNPC))]
public class TimedBerryPatcher
{
    [HarmonyPatch("CheckIfFinished")]
    [HarmonyPrefix]
    public static bool CheckIfFinished(SoilNPC __instance)
    {
        if (!SlotDataUtils.BERRYSANITY)
        {
            return true;
        }
        if (GameData.current.MomoEvent[__instance.Flag] != 0)
        {
            __instance.TreeObj.transform.gameObject.SetActive(true);
            __instance.transform.gameObject.SetActive(false);
        }
        return false;
    }
}
