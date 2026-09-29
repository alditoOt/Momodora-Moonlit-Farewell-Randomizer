using HarmonyLib;
using UnityEngine;
using APMomodoraMoonlitFarewell.Archipelago;

namespace APMomodoraMoonlitFarewell.Patches
{
	[HarmonyPatch(typeof(Platformer3D))]
	class Platformer3DPatcher
	{
        [HarmonyPatch("GameOver")]
        [HarmonyPostfix]
		public static void ModifyGameOverMessage(Platformer3D __instance)
		{
            if (APDeathLinkHandler.getDeathLinkSource() != null)
            __instance.GameOverText.text = $"Deathlink received from {APDeathLinkHandler.getDeathLinkSource()}...";
                APDeathLinkHandler.setDeathLinkSource(null);
        }
	}
}
