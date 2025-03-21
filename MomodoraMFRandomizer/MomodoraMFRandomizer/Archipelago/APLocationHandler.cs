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
        static Dictionary<int, string> skillAndScene = new Dictionary<int, string>()
        {
            { 20, "Well26" },
            {9, "Well29" },
            {10, "Bark42" },
            {194, "Fairy10" },
            {131, "Marsh08" }
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
                    //MelonLogger.Msg("Skill not received yet");
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
            //MelonLogger.Msg($"Attempting to report skill {index}");
            if (!APMomoMFRandomizer.session.Locations.AllLocationsChecked.Contains(index) && previousEventValue[index] == 0)
            {
                //MelonLogger.Msg($"Actually reporting skill {index}");
                checkedLocation.Add(index);
                if (!receivedSkill.Contains(index))
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
                MelonLogger.Msg($"Assigning skill {itemId}");
                previousEventValue[itemId] = 1;
                receivedSkill.Add(itemId);
                GameData.current.MomoEvent[itemId] = 1;
            }
            else if (InventoryUtils.SIGILID.Contains(itemId))
            {
                MelonLogger.Msg($"Giving item with id: {itemId}");
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

        public void HandleItemsReceived()
        {
            foreach (ItemInfo item in APMomoMFRandomizer.session.Items.AllItemsReceived)
            {
                long itemId = item.ItemId;
                GiveItem((int)itemId);
            }
        }

        public void ResetLocationSceneForSkill(string sceneName, Boolean mainMenu)
        {
            if (mainMenu)
            {
                return;
            }
            foreach (int skillId in MomoEventUtils.SKILLEVENTS)
            {
                if (!receivedSkill.Contains(skillId) || APMomoMFRandomizer.session.Locations.AllLocationsChecked.Contains(skillId))
                {
                    continue;
                }
                if (GameData.current.MomoEvent[skillId] == 1 && skillAndScene[skillId] == sceneName)
                {
                    GameData.current.MomoEvent[skillId] = 0;
                    previousEventValue[skillId] = 0;
                    receivedSkill.Remove(skillId);
                    break;
                }
            }
        }
    }
}
