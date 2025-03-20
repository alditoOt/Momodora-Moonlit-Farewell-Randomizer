using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using Archipelago.MultiClient.Net;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
{
    [HarmonyPatch(typeof(MomoEventData))]
    class APSocketHandler
    {
        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        public static void SendCompletion()
        {
            if (GameData.current.MomoEvent[364] == 1)
            {
                var statusUpdatePacket = new StatusUpdatePacket();
                statusUpdatePacket.Status = ArchipelagoClientState.ClientGoal;
                APMomoMFRandomizer.session.Socket.SendPacket(statusUpdatePacket);
            }
        }
    }
}
