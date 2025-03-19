using HarmonyLib;
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
using MomodoraMoonlitFarewellAP.Utils;

namespace MomodoraMoonlitFarewellAP.Patches
{
    [HarmonyPatch(typeof(MomoEventData))]
    class APLocationHandler
    {
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
            {"Lunar Attunement", 131 },
            {"Mitchi Fast Travel", 205 }
        };

        public void InitializeDictionary()
        {
            foreach (int skill in MomoEventUtils.SKILLEVENTS)
            {
                previousEventValue[skill] = GameData.current.MomoEvent[skill];
            }
        }

        [HarmonyPatch("set_Item")]
        [HarmonyPrefix]
        private static void ReportLocation(int index, int value)
        {
            if (value != 1 || 
                (!MomoEventUtils.BOSSEVENTS.Contains(index) && 
                !MomoEventUtils.SKILLEVENTS.Contains(index) && 
                !MomoEventUtils.SIGILEVENTS.Contains(index))) {
                return;
            }

            if (MomoEventUtils.SKILLEVENTS.Contains(index))
            {
                ReportSkillLocation(index, value);
            } 
            else if (MomoEventUtils.SIGILEVENTS.Contains(index))
            {
                ReportSigilLocation(index, value);
            } 
            else
            {
                MomodoraMFRandomizer.APMomoMFRandomizer.session.Locations.CompleteLocationChecks(index); //Boss check
            }

        }

        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        private static void RemoveNonReceivedSkill(int index, int value)
        {
            if (value != 1 || !MomoEventUtils.SKILLEVENTS.Contains(index))
            {
                return;
            }

            if (previousEventValue[index] == 0)
            {
                if (!receivedSkill.Contains(index))
                {
                    MelonLogger.Msg("Skill not received yet");
                    GameData.current.MomoEvent[index] = 0;
                }
                else
                {
                    GiveSkill(index);
                }
            }
        }

        private static void ReportSkillLocation(int index, int value)
        {
            MelonLogger.Msg($"Attempting to report skill {index}");
            if (!checkedLocation.Contains(index) && previousEventValue[index] == 0)
            {
                MelonLogger.Msg($"Actually reporting skill {index}");
                checkedLocation.Add(index);
                if (!receivedSkill.Contains(index))
                {
                    GameData.current.MomoEvent[index] = 0;
                }
                APMomoMFRandomizer.session.Locations.CompleteLocationChecks(index);
            }
        }

        private static void ReportSigilLocation(int index, int value)
        {
            //TO-DO
        }

        private static void GiveSkill(int skillId)
        {
            if (MomoEventUtils.SKILLEVENTS.Contains(skillId))
            {
                MelonLogger.Msg($"Assigning skill {skillId}");
                previousEventValue[skillId] = 1;
                receivedSkill.Add(skillId);
                GameData.current.MomoEvent[skillId] = 1;
            }
        }

        public static void ReceiveItem(ReceivedItemsHelper itemHandler)
        {
            var receivedItem = itemHandler.PeekItem().ItemName;
            if (nameAndSkill.ContainsKey(receivedItem))
            {
                MelonLogger.Msg($"Receiving {receivedItem}");
                GiveSkill(nameAndSkill[receivedItem]);
            }
            itemHandler.DequeueItem();
        }

        public static void UpdateItemsForTheSession(ReceivedItemsHelper itemHandler)
        {
            List<int> receivedItems = new List<int>();
            foreach (ItemInfo item in APMomoMFRandomizer.session.Items.AllItemsReceived)
            {
                long itemId = item.ItemId;
                MelonLogger.Msg($"Item with name {itemHandler.PeekItem().ItemName} and ID {itemId} received");
                var itemReceivedName = item.ItemName ?? $"Item: {item.ItemId}";
                if (nameAndSkill.ContainsKey(itemReceivedName)) 
                {
                    receivedItems.Add(nameAndSkill[itemReceivedName]);
                }
                MelonLogger.Msg(itemReceivedName);
            }
            foreach (int itemReceived in receivedItems)
            {
                //if (!receivedSkill.Contains(itemReceived))
                //{
                //    GameData.current.MomoEvent[itemReceived] = 0;
                //}
                GiveSkill(itemReceived);
            }
        }

        public void UpdateItemsForTheSaveFile()
        {
            List<int> receivedItems = new List<int>();
            foreach (ItemInfo item in APMomoMFRandomizer.session.Items.AllItemsReceived)
            {
                long itemId = item.ItemId;
                //MelonLogger.Msg($"Item with name {itemHandler.PeekItem().ItemName} and ID {itemId} received");
                var itemReceivedName = item.ItemName ?? $"Item: {item.ItemId}";
                if (nameAndSkill.ContainsKey(itemReceivedName))
                {
                    receivedItems.Add(nameAndSkill[itemReceivedName]);
                }
                MelonLogger.Msg(itemReceivedName);
            }
            foreach (int itemReceived in receivedItems)
            {
                //if (!receivedSkill.Contains(itemReceived))
                //{
                //    GameData.current.MomoEvent[itemReceived] = 0;
                //}
                GiveSkill(itemReceived);
            }
        }

        public void ResetLocationSceneForSkill(string sceneName)
        {
            foreach (int skillId in MomoEventUtils.SKILLEVENTS)
            {
                if (!receivedSkill.Contains(skillId) || checkedLocation.Contains(skillId))
                {
                    return;
                }
                if (GameData.current.MomoEvent[skillId] == 1 && skillAndScene[skillId] == sceneName)
                {
                    GameData.current.MomoEvent[skillId] = 0;
                    previousEventValue[skillId] = 0;
                    SceneManager.LoadScene(sceneName);
                    break;
                }
            }
        }
    }
}
