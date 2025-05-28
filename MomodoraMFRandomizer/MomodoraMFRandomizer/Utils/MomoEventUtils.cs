using Archipelago.MultiClient.Net;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace APMomodoraMoonlitFarewell.Utils
{
    [HarmonyPatch(typeof(MomoEventData))]
    class MomoEventUtils
    {
        public static List<int> SKILLEVENTS = new List<int> { 9, 10, 20, 194, 131, 205 }; //Sprint, Leaf, Double Jump, Wall Jump, Lunar Attunement, Fast Travel
        public static List<int> BOSSEVENTS = new List<int>() { 15, 17, 16, 278, 150, 171, 114, 105, 188, 255, 213, 259, 260, 261, 262 };
        public static List<int> DEFAULT_EVENTS_TO_1 = new List<int>() { 59, 60, 62 };

        public static List<int> OPTIONALEVENTS = new List<int>();
        public static List<int> LILYEVENTS = new List<int>();
        public static List<int> HEALTHBERRYEVENTS = new List<int>();
        public static List<int> STAMINABERRYEVENTS = new List<int>();
        public static List<int> MAGICBERRYEVENTS = new List<int>();
        public static List<int> FAIRYEVENTS = new List<int>();

        public static int FINAL_DOOR_EVENT = 294;
        public static int FAST_TRAVEL_EVENT = 205;
        public static int LILY_COUNTER_EVENT = 218;
        public static int HEALTH_COUNTER_EVENT = 215;
        public static int MAGIC_COUNTER_EVENT = 216;
        public static int STAMINA_COUNTER_EVENT_ONE = 217;
        public static int STAMINA_COUNTER_EVENT_TWO = 200;
        public static int FAIRY_COUNTER_EVENT = 39;
        public static int VICTORY_EVENT;

        //[HarmonyPatch("set_Item")]
        //[HarmonyPostfix]
        //private static void PrintOnEventChange(int index, int value)
        //{
        //    if (value == 1)
        //    {
        //        MelonLogger.Msg($"Event {index} was set to {value}.");
        //    }
        //}

        public static void GrowTimedBerries()
        {
            if (YAMLUtils.HEALTH_UPGRADE)
            {
                if (GameData.current.MomoEvent[279] != 1)
                {
                    GameData.current.MomoEvent[279] = 1;
                } 
                if (GameData.current.MomoEvent[240] != 1)
                {
                    GameData.current.MomoEvent[240] = 1;
                }
            }
            if (YAMLUtils.STAMINA_UPGRADE && GameData.current.MomoEvent[280] != 1)
            {
                GameData.current.MomoEvent[280] = 1;
            }
        }
    }
}
