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
using APMomoMFRandomizer;

namespace MomodoraMFRandomizer
{
    [HarmonyPatch(typeof(MomoEventData))]
    class APLocationHandler
    {
        static HashSet<int> receivedSkill = new HashSet<int>();
        static HashSet<int> checkedLocation = new HashSet<int>();
        static Dictionary<int, int> previousEventValue = new Dictionary<int, int>();
        static Dictionary<string, int> skillAndScene = new Dictionary<string, int>()
        {
            { "Well26", 20 },
            {"Well29" , 9 },
            {"Bark42" , 10 },
            {"Fairy10" , 194 },
            {"Marsh08" , 131 }
        };

        public void InitializeDictionary()
        {
            foreach (int skill in MomoEventUtils.SKILLEVENTS)
            {
                previousEventValue[skill] = GameData.current.MomoEvent[skill];
            }
        }

        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
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
            else
            {
                APMomoMFRandomizer.session.Locations.CompleteLocationChecks(index); //Boss check
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
                    GameData.current.MomoEvent[index] = 0;
                }
                else
                {
                    GiveItem(index);
                }
            }
        }

        private static void ReportSkillLocation(int index, int value)
        {
            if (!APMomoMFRandomizer.session.Locations.AllLocationsChecked.Contains(index) && previousEventValue[index] == 0)
            {
                checkedLocation.Add(index);
                if (!GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(InventoryUtils.SKILL_INVENTORY_ID[index])))
                {
                    GameData.current.MomoEvent[index] = 0;
                }
                APMomoMFRandomizer.session.Locations.CompleteLocationChecks(index);
            }
        }

        private static void GiveItem(int itemId)
        {
            if (MomoEventUtils.SKILLEVENTS.Contains(itemId))
            {
                previousEventValue[itemId] = 1;
                receivedSkill.Add(itemId);
                GameData.current.MomoEvent[itemId] = 1;
            }
            else if (InventoryUtils.SIGILID.Contains(itemId))
            {
                GameData.inventory.Add(GameData.itemDatabase.GetItem(itemId), is_new_item: false);
            }
        }

        public static void UpdateItemsForTheSession(ReceivedItemsHelper itemHandler)
        {
            foreach (ItemInfo item in APMomoMFRandomizer.session.Items.AllItemsReceived)
            {
                long itemId = item.ItemId;
                GiveItem((int)itemId);
            }
        }

        public void ResetLocationSceneForSkill(string sceneName, Boolean mainMenu)
        {
            if (mainMenu || !InventoryUtils.SKILL_INVENTORY_ID.ContainsKey(skillAndScene[sceneName]))
            {
                return;
            }
            MelonLogger.Msg($"I'm in scene {sceneName}");
            int itemId = InventoryUtils.SKILL_INVENTORY_ID[skillAndScene[sceneName]];

            if (GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(itemId)))
            {
                MelonLogger.Msg(itemId + " is in the inventory.");
                return;
            }

            MelonLogger.Msg($"Resetting skill {skillAndScene[sceneName]}.");
            GameData.current.MomoEvent[skillAndScene[sceneName]] = 0;
            previousEventValue[skillAndScene[sceneName]] = 0;
            receivedSkill.Remove(skillAndScene[sceneName]);
        }
    }
}
