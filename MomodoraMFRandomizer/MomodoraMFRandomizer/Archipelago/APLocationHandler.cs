using HarmonyLib;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using MelonLoader;
using APMomodoraMoonlitFarewell.Utils;
using System.Reflection;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    [HarmonyPatch(typeof(MomoEventData))]
    class APLocationHandler
    {
        // Archipelago "filler" item id used for the money/currency pickup, and the amount it grants.
        private const int moneyFillerItemId = 999;
        private const int moneyFillerAmount = 100;

        // Base game stats + per-pickup increment, as measured in-game.
        private const int baseAttack = 5;
        private const int attackPerLily = 2;
        private const int baseMaxHealth = 300;
        private const int healthPerBerry = 50;
        private const int baseMaxMagic = 30;
        private const int magicPerUpgrade = 10;

        private static int finalBossDoorCount = 0;


        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        private static void ReportLocation(int index, int value)
        {
            if (value != 1 || 
                !MomoEventUtils.BOSSEVENTS.Contains(index) && 
                !MomoEventUtils.SKILLEVENTS.Contains(index) &&
                !MomoEventUtils.LILYEVENTS.Contains(index) &&
                !MomoEventUtils.HEALTHBERRYEVENTS.Contains(index) &&
                !MomoEventUtils.STAMINABERRYEVENTS.Contains(index) &&
                !MomoEventUtils.MAGICBERRYEVENTS.Contains(index) &&
                !MomoEventUtils.FAIRYEVENTS.Contains(index)) {
                return;
            }

            if (MomoEventUtils.SKILLEVENTS.Contains(index))
            {
                ReportSkillLocation(index, value);
            } 
            else if (MomoEventUtils.LILYEVENTS.Contains(index))
            {
                Platformer3D.phys_attack -= attackPerLily;
                GameData.current.MomoEvent[MomoEventUtils.LILY_COUNTER_EVENT]--;
                APUtils.CompleteLocation(index * 100);
            }
            else if (MomoEventUtils.HEALTHBERRYEVENTS.Contains(index))
            {
                Platformer3D.player_maxhp -= healthPerBerry;
                Platformer3D.player_hp -= healthPerBerry;
                GameData.current.MomoEvent[MomoEventUtils.HEALTH_COUNTER_EVENT]--;
               APUtils.CompleteLocation(index * 100);
            }
            else if (MomoEventUtils.MAGICBERRYEVENTS.Contains(index))
            {
                Platformer3D.player_maxsp -= magicPerUpgrade;
                Platformer3D.player_sp -= magicPerUpgrade;
                GameData.current.MomoEvent[MomoEventUtils.MAGIC_COUNTER_EVENT]--;
                APUtils.CompleteLocation(index * 100);
            }
            else if (MomoEventUtils.STAMINABERRYEVENTS.Contains(index))
            {
                GameData.current.MomoEvent[MomoEventUtils.STAMINA_COUNTER_EVENT_ONE]--;
                GameData.current.MomoEvent[MomoEventUtils.STAMINA_COUNTER_EVENT_TWO]--;
                APUtils.CompleteLocation(index * 100);
            }
            else if (MomoEventUtils.FAIRYEVENTS.Contains(index))
            {
                GameData.current.MomoEvent[MomoEventUtils.FAIRY_COUNTER_EVENT]--;
                APUtils.CompleteLocation(index * 100);
            }
            else
            {
                APUtils.CompleteLocation(index); //Boss check
            }
        }

        private static void ReportSkillLocation(int index, int value)
        {
            Boolean skillReceived = false;
            foreach (ItemInfo item in APMomodoraMoonlitFarewell.session.Items.AllItemsReceived)
            {
                if (item.ItemId == index)
                {
                    skillReceived = true;
                }
            }
            if (!skillReceived)
            {
                // Reset the skill value if it hasn't been received yet
                GameData.current.MomoEvent[index] = 0;
                if (index == MomoEventUtils.FAST_TRAVEL_EVENT)
                {
                    if (SceneManager.GetActiveScene().name != MomoEventUtils.FAST_TRAVEL_SCENE)
                    {
                        return;
                    }
                }
                APUtils.CompleteLocation(index);
            }
        }

        public static void GiveItem(int itemId)
        {
            if (itemId == moneyFillerItemId)
            {
                Platformer3D.player_money += moneyFillerAmount;
                return;
            }
            if (MomoEventUtils.SKILLEVENTS.Contains(itemId))
            {
                GameData.current.MomoEvent[itemId] = 1;
            }
            else if (InventoryUtils.AP_SIGIL_ITEM_ID.Contains(itemId))
            {
                GameData.inventory.Add(GameData.itemDatabase.GetItem(itemId), is_new_item: false);
            }
        }

        public static void UpdateItemsForTheSession(ReceivedItemsHelper itemHandler)
        {
            List<ItemInfo> items = APMomodoraMoonlitFarewell.session.Items.AllItemsReceived.ToList<ItemInfo>();
            Boolean firstTimeSendingMoney = true;
            finalBossDoorCount = 0;
            int lilyCount = 0;
            int healthCount = 0;
            int staminaCount = 0;
            int magicCount = 0;
            int fairyCount = 0;
            foreach (ItemInfo item in items)
            {
                long itemId = item.ItemId;
                if (itemId == InventoryUtils.DAMAGE_ID)
                {
                    lilyCount++;
                    continue;
                }
                if (itemId == InventoryUtils.HEALTH_ID)
                {
                    healthCount++;
                    continue;
                }
                if (itemId == InventoryUtils.STAMINA_ID)
                {
                    staminaCount++;
                    continue;
                }
                if (itemId == InventoryUtils.MAGIC_ID)
                {
                    magicCount++;
                    continue;
                }
                if (itemId == InventoryUtils.FAIRY_ID)
                {
                    fairyCount++;
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
            UpdatePlayerDamage(lilyCount);
            UpdatePlayerHealth(healthCount);
            UpdatePlayerStamina(staminaCount);
            UpdatePlayerMagic(magicCount);
            UpdateFairies(fairyCount);
            
            if (finalBossDoorCount > 0)
            {
                GameData.current.MomoEvent[MomoEventUtils.FINAL_DOOR_EVENT] = finalBossDoorCount;
            }
        }

        private static void UpdatePlayerDamage(int lilyCount)
        {
            Platformer3D.phys_attack = baseAttack + attackPerLily * lilyCount;
            GameData.current.MomoEvent[MomoEventUtils.LILY_COUNTER_EVENT] = lilyCount;
        }

        private static void UpdatePlayerHealth(int healthCount)
        {
            float prevHP = Platformer3D.player_maxhp;
            Platformer3D.player_maxhp = baseMaxHealth + healthPerBerry * healthCount;
            Platformer3D.player_hp += Platformer3D.player_maxhp > prevHP ? healthPerBerry : 0;
            GameData.current.MomoEvent[MomoEventUtils.HEALTH_COUNTER_EVENT] = healthCount;
        }

        private static void UpdatePlayerMagic(int magicCount)
        {
            float prevMagic = Platformer3D.player_maxsp;
            Platformer3D.player_maxsp = baseMaxMagic + magicPerUpgrade * magicCount;
            Platformer3D.player_sp += Platformer3D.player_maxsp > prevMagic ? magicPerUpgrade : 0;
            GameData.current.MomoEvent[MomoEventUtils.MAGIC_COUNTER_EVENT] = magicCount;
        }

        private static void UpdateFairies(int fairyCount)
        {
            GameData.current.MomoEvent[MomoEventUtils.FAIRY_COUNTER_EVENT] = fairyCount;
        }

        private static void UpdatePlayerStamina(int staminaCount)
        {
            GameData.current.MomoEvent[MomoEventUtils.STAMINA_COUNTER_EVENT_ONE] = staminaCount;
            GameData.current.MomoEvent[MomoEventUtils.STAMINA_COUNTER_EVENT_TWO] = staminaCount;
        }
    }
}
