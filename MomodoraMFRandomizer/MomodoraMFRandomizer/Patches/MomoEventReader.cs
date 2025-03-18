using Archipelago.MultiClient.Net;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MomodoraMFRandomizer
{
    [HarmonyPatch(typeof(MomoEventData))]
    class MomoEventReader
    {
        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        private static void PrintOnEventChange(int index, int value)
        {
            //MelonLogger.Msg($"Event {index} was set to {value}.");
        }
    }
}
