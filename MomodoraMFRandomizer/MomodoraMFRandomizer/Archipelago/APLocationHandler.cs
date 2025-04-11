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
using System.Reflection;

namespace MomodoraMFRandomizer
{
    [HarmonyPatch(typeof(MomoEventData))]
    class APLocationHandler
    {
        private static int MONEY = 100;
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

        private static int finalBossDoorCount = 0;

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
                !MomoEventUtils.SKILLEVENTS.Contains(index)) &&
                !MomoEventUtils.LILYEVENTS.Contains(index)) {
                return;
            }

            if (MomoEventUtils.SKILLEVENTS.Contains(index))
            {
                ReportSkillLocation(index, value);
            } else if (MomoEventUtils.LILYEVENTS.Contains(index))
            {
                Platformer3D.phys_attack -= 2;
                GameData.current.MomoEvent[MomoEventUtils.LILY_COUNTER_EVENT] --;
                APMomoMFRandomizer.session.Locations.CompleteLocationChecks(index * 100);
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

        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        private static void UpdateFinalBossDoor(int index, int value)
        {
            if (YAMLUtils.FINAL_BOSS_DOOR && index == MomoEventUtils.FINAL_DOOR_EVENT && value != finalBossDoorCount)
            {
                finalBossDoorCount = 0;
                foreach (ItemInfo item in APMomoMFRandomizer.session.Items.AllItemsReceived)
                {
                    if (item.ItemId == 991)
                    {
                        finalBossDoorCount++;
                    }
                }
                GameData.current.MomoEvent[index] = finalBossDoorCount;
            }
        }

        private static void ReportSkillLocation(int index, int value)
        {
            if (!APMomoMFRandomizer.session.Locations.AllLocationsChecked.Contains(index) && (previousEventValue[index] == 0 || index == 205))
            {
                checkedLocation.Add(index);
                if ((index == 205 && !receivedSkill.Contains(index)) || (index != 205 && !GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(InventoryUtils.SKILL_INVENTORY_ID[index]))))
                {
                    GameData.current.MomoEvent[index] = 0;
                }
                APMomoMFRandomizer.session.Locations.CompleteLocationChecks(index);
            }
        }

        public static void GiveItem(int itemId)
        {
            if (itemId == 999)
            {
                Platformer3D.player_money += MONEY;
                return;
            }
            if (MomoEventUtils.SKILLEVENTS.Contains(itemId))
            {
                previousEventValue[itemId] = 1;
                receivedSkill.Add(itemId);
                GameData.current.MomoEvent[itemId] = 1;
            }
            else if (InventoryUtils.ITEM_ID.Contains(itemId))
            {
                GameData.inventory.Add(GameData.itemDatabase.GetItem(itemId), is_new_item: false);
            }
        }

        public static void UpdateItemsForTheSession(ReceivedItemsHelper itemHandler)
        {
            Boolean firstTimeSendingMoney = true;
            finalBossDoorCount = 0;
            int lilyCount = 0;
            foreach (ItemInfo item in APMomoMFRandomizer.session.Items.AllItemsReceived)
            {
                long itemId = item.ItemId;
                if (itemId == InventoryUtils.DAMAGE_ID)
                {
                    lilyCount++;
                    continue;
                }
                if (itemId == InventoryUtils.BOSS_KEY_ID)
                {
                    finalBossDoorCount++;
                    continue;
                }
                if (itemId == InventoryUtils.FILLER_ID) 
                {
                    if (itemHandler == null)
                    {
                        continue;
                    }
                    if (!firstTimeSendingMoney)
                    {
                        continue;
                    }
                    firstTimeSendingMoney = false;
                }
                GiveItem((int)itemId);
            }
            if (lilyCount > 0)
            {
                UpdatePlayerDamage(lilyCount);
            }
            if (finalBossDoorCount > 0)
            {
                GameData.current.MomoEvent[MomoEventUtils.FINAL_DOOR_EVENT] = finalBossDoorCount;
            }
        }

        public void ResetLocationSceneForSkill(string sceneName, Boolean mainMenu)
        {
            if (mainMenu || !skillAndScene.ContainsKey(sceneName))
            {
                return;
            }
            if (skillAndScene[sceneName] == 9)
            {
                GameData.current.MomoEvent[skillAndScene[sceneName]] = 0;
                previousEventValue[skillAndScene[sceneName]] = 0;
                receivedSkill.Remove(skillAndScene[sceneName]);
            }
        }

        public static void ResetDashSkill()
        {
            GameData.current.MomoEvent[9] = 0;
            previousEventValue[9] = 0;
            receivedSkill.Remove(9);
        }

        private static void UpdatePlayerDamage(int lilyCount)
        {
            Platformer3D.phys_attack = 5 + 2 * lilyCount;
            GameData.current.MomoEvent[MomoEventUtils.LILY_COUNTER_EVENT] = lilyCount;
        }
    }
}
