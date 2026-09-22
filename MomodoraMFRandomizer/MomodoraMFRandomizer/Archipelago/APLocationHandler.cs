using HarmonyLib;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    // Detects in-game events that correspond to Archipelago location checks and reports them,
    // and rebuilds player state from the full set of received items when the session's item list changes.
    [HarmonyPatch(typeof(MomoEventData))]
    class APLocationHandler
    {
        private static int finalBossDoorCount = 0;

        [HarmonyPatch("set_Item")]
        [HarmonyPostfix]
        private static void ReportLocation(int index, int value)
        {
            if (!APMomodoraMoonlitFarewell.IsSessionActive)
            {
                return;
            }

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
                Platformer3D.phys_attack -= APPlayerStatUpdater.attackPerLily;
                GameData.current.MomoEvent[MomoEventUtils.LILY_COUNTER_EVENT]--;
                APUtils.CompleteLocation(index * 100);
            }
            else if (MomoEventUtils.HEALTHBERRYEVENTS.Contains(index))
            {
                Platformer3D.player_maxhp -= APPlayerStatUpdater.healthPerBerry;
                Platformer3D.player_hp -= APPlayerStatUpdater.healthPerBerry;
                GameData.current.MomoEvent[MomoEventUtils.HEALTH_COUNTER_EVENT]--;
               APUtils.CompleteLocation(index * 100);
            }
            else if (MomoEventUtils.MAGICBERRYEVENTS.Contains(index))
            {
                Platformer3D.player_maxsp -= APPlayerStatUpdater.magicPerUpgrade;
                Platformer3D.player_sp -= APPlayerStatUpdater.magicPerUpgrade;
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

        public static void UpdateItemsForTheSession(ReceivedItemsHelper itemHandler)
        {
            if (!APMomodoraMoonlitFarewell.IsSessionActive)
            {
                return;
            }

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
                APItemGranter.GiveItem((int)itemId);
            }
            APPlayerStatUpdater.UpdatePlayerDamage(lilyCount);
            APPlayerStatUpdater.UpdatePlayerHealth(healthCount);
            APPlayerStatUpdater.UpdatePlayerStamina(staminaCount);
            APPlayerStatUpdater.UpdatePlayerMagic(magicCount);
            APPlayerStatUpdater.UpdateFairies(fairyCount);

            if (finalBossDoorCount > 0)
            {
                GameData.current.MomoEvent[MomoEventUtils.FINAL_DOOR_EVENT] = finalBossDoorCount;
            }
        }
    }
}
