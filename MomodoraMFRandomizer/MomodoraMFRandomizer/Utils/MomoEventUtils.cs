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
    class MomoEventUtils
    {
        public static List<int> SKILLEVENTS = new List<int> { 9, 10, 20, 194, 131, 205 };
        public static List<int> BOSSEVENTS = new List<int>() { 15, 17, 16, 278, 150, 171, 114, 105, 188, 255, 213, 259, 260, 261, 262, 364 };
        public static List<int> SIGILEVENTS = new List<int>();

        public static int FAST_TRAVEL_EVENT = 205;

        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        private static void PrintOnEventChange(int index, int value)
        {
            if (value == 1)
            {
                MelonLogger.Msg($"Event {index} was set to {value}.");
            }
        }
    }
}
