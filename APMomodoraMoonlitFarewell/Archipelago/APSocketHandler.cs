using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using Archipelago.MultiClient.Net;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    [HarmonyPatch(typeof(MomoEventData))]
    class APSocketHandler
    {
        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        public static void SendCompletion()
        {
            if (!APMomodoraMoonlitFarewell.HasSession)
            {
                return;
            }
            if (GameData.current.MomoEvent[MomoEventUtils.VICTORY_EVENT] == 1)
            {
                APConnectionManager.QueueGoal();
            }
        }
    }
}
