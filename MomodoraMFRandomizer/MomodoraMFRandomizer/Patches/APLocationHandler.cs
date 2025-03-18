﻿using HarmonyLib;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using MomodoraMFRandomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using MelonLoader;

namespace MomodoraMoonlitFarewellAP.Patches
{
    [HarmonyPatch(typeof(MomoEventData))]
    class APLocationHandler
    {
        static List<int> skillEvents = new List<int> { 9, 10, 20, 194, 131 };
        static List<int> bossEvents = new List<int>(); // { 17, 16, 278, 150, 171, 105, 255};
        static List<int> sigilEvents = new List<int>();
        static HashSet<int> receivedSkill = new HashSet<int>();
        static HashSet<int> checkedLocation = new HashSet<int>();
        static Dictionary<int, int> previousEventValue = new Dictionary<int, int>();
        static Dictionary<int, string> skillAndScene = new Dictionary<int, string>()
        {
            { 20, "Well26" },
            {9, "Well29" },
            {10, "Bark42" },
            {194, "Fairy10" },
            {131, "Marsh08" }
        };

        static Dictionary<string, int> nameAndSkill = new Dictionary<string, int>()
        {
            {"Awakened Sacred Leaf", 20 },
            {"Sacred Anemone", 9 },
            {"Spiral Shell", 194 },
            {"Crescent Moonflower", 10 },
            {"Lunar Attunement", 131 }
        };

        private static int index = 0;

        public void InitializeDictionary()
        {
            foreach (int skill in skillEvents)
            {
                previousEventValue[skill] = GameData.current.MomoEvent[skill];
            }
        }

        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        private static void ReportLocation(int index, int value)
        {
            if (value == 0 || (!bossEvents.Contains(index) && !skillEvents.Contains(index) && !sigilEvents.Contains(index))) {
                return;
            }

            if (skillEvents.Contains(index))
            {
                ReportSkillLocation(index, value);
            } 
            else if (sigilEvents.Contains(index))
            {
                ReportSigilLocation(index, value);
            }

        }

        private static void ReportSkillLocation(int index, int value)
        {
            if (!checkedLocation.Contains(index) && previousEventValue[index] == 0 && value == 1)
            {
                checkedLocation.Add(index);
                if (!receivedSkill.Contains(index))
                {
                    MelonLogger.Msg("Skill not received yet");
                    GameData.current.MomoEvent[index] = 0;
                }
                else
                {
                    GiveSkill(index);
                }
                APRandomizer.session.Locations.CompleteLocationChecks(index);
            }
        }

        private static void ReportSigilLocation(int index, int value)
        {
            //TO-DO
        }

        private static void GiveSkill(int skillId)
        {
            MelonLogger.Msg($"{skillId}");
            previousEventValue[skillId] = 1;
            receivedSkill.Add(skillId);
            GameData.current.MomoEvent[skillId] = 1;
        }

        public static void ReceiveItem(ReceivedItemsHelper itemHandler)
        {
            var receivedItem = itemHandler.PeekItem().ItemName;
            GiveSkill(nameAndSkill[receivedItem]);
            itemHandler.DequeueItem();
        }

        public void UpdateItemsForTheSession(ArchipelagoSession session)
        {
            foreach (ItemInfo item in session.Items.AllItemsReceived)
            {
                long itemId = item.ItemId;
                session.Items.ItemReceived += (receivedItemsHelper) =>
                {
                    MelonLogger.Msg($"Item with name {receivedItemsHelper.PeekItem().ItemName} and ID {itemId} received");
                    var itemReceivedName = receivedItemsHelper.PeekItem().ItemName ?? $"Item: {itemId}";
                    GiveSkill((int)itemId);
                    receivedItemsHelper.DequeueItem();
                };
            }
            
        }

        public void ResetLocationSceneForSkill(string sceneName)
        {
            foreach (int skillId in skillEvents)
            {
                if (!receivedSkill.Contains(skillId) || checkedLocation.Contains(skillId))
                {
                    return;
                }
                if (skillId == 1 && skillAndScene[skillId] == sceneName)
                {
                    GameData.current.MomoEvent[skillId] = 0;
                    previousEventValue[skillId] = 0;
                    //SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}
