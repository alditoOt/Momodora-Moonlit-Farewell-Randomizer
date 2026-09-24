using APMomodoraMoonlitFarewell;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APMomodoraMoonlitFarewell.Utils
{
    class SlotDataUtils
    {
        public static Boolean OPENSPRINGLEAFPATH;
        public static Boolean DEATHLINK;
        public static Boolean ADD_ORACLE_SIGIL;
        public static Boolean KEY_ITEMS;
        // public static string FAST_TRAVEL_CHOICE;
        public static Boolean FINAL_BOSS_DOOR;
        public static Boolean DAMAGE_UPGRADE;
        public static Boolean HEALTH_UPGRADE;
        public static Boolean STAMINA_UPGRADE;
        public static Boolean MAGIC_UPGRADE;
        public static Boolean FAIRIES;
        public static String VICTORY;

        private const string victoryConditionMoonGodSelin = "moon_god_selin";
        private const string victoryConditionDora = "dora";

        // Additional lily/damage-upgrade berry event ids unlocked when the damage upgrade option is enabled.
        private static readonly int[] damageUpgradeLilyEvents = { 264, 81, 129, 28, 84, 118, 94, 344, 172, 38, 343, 23, 169, 153, 166, 130, 302, 247, 334, 336, 333, 286, 285, 323, 322 };

        // Additional health-berry event ids unlocked when the health upgrade option is enabled.
        private static readonly int[] healthUpgradeBerryEvents = { 30, 27, 29, 361, 26, 152, 24, 174, 116, 35, 31, 89, 143, 249, 356, 246, 165, 359, 332, 360, 348, 349, 219, 282 };

        // Additional stamina-berry event ids unlocked when the stamina upgrade option is enabled.
        private static readonly int[] staminaUpgradeBerryEvents = { 201, 203, 303, 331, 202 };

        // Additional magic-berry event ids unlocked when the magic upgrade option is enabled.
        private static readonly int[] magicUpgradeBerryEvents = { 135, 33, 95, 115, 228, 341, 295 };

        // Lumen Fairy event ids unlocked when the fairies option is enabled.
        private static readonly int[] fairyEvents = { 41, 354, 44, 87, 51, 88, 352, 351, 162, 54, 346, 57, 47, 93, 137, 91, 342, 168, 355, 221, 224, 229, 238, 357, 300, 288, 287, 46, 353, 301 };

        public static void GetSettingsFromYAML()
        {
            var slotData = APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData();

            OPENSPRINGLEAFPATH = GetBoolSetting(slotData, "open_springleaf_path");
            DEATHLINK = GetBoolSetting(slotData, "deathlink");
            KEY_ITEMS = GetBoolSetting(slotData, "randomize_key_items");
            ADD_ORACLE_SIGIL = GetBoolSetting(slotData, "oracle_sigil");
            FINAL_BOSS_DOOR = GetBoolSetting(slotData, "final_boss_keys");
            DAMAGE_UPGRADE = GetBoolSetting(slotData, "progressive_damage_upgrade");
            HEALTH_UPGRADE = GetBoolSetting(slotData, "progressive_health_upgrade");
            STAMINA_UPGRADE = GetBoolSetting(slotData, "progressive_stamina_upgrade");
            MAGIC_UPGRADE = GetBoolSetting(slotData, "progressive_magic_upgrade");
            FAIRIES = GetBoolSetting(slotData, "progressive_lumen_fairies");
            VICTORY = GetStringSetting(slotData, "victory_condition");
        }

        private static bool GetBoolSetting(Dictionary<string, object> slotData, string key)
        {
            if (slotData.TryGetValue(key, out object value) && value is Boolean boolValue)
            {
                return boolValue;
            }
            MelonLogger.Error($"Slot data key '{key}' was missing or not a boolean; defaulting to false.");
            return false;
        }

        private static string GetStringSetting(Dictionary<string, object> slotData, string key)
        {
            if (slotData.TryGetValue(key, out object value) && value is string stringValue)
            {
                return stringValue;
            }
            MelonLogger.Error($"Slot data key '{key}' was missing or not a string; defaulting to null.");
            return null;
        }

        public static void AddItemsToItemPool()
        {
            if (KEY_ITEMS)
            {
                InventoryUtils.AP_SIGIL_ITEM_ID.AddRange(InventoryUtils.KEY_ITEM_ID);
            }
            if (ADD_ORACLE_SIGIL)
            {
                InventoryUtils.AP_SIGIL_ITEM_ID.Add(InventoryUtils.ORACLE);
            }
            if (DAMAGE_UPGRADE)
            {
                MomoEventUtils.LILYEVENTS.AddRange(damageUpgradeLilyEvents);
            }
            if (HEALTH_UPGRADE)
            {
                MomoEventUtils.HEALTHBERRYEVENTS.AddRange(healthUpgradeBerryEvents);
            }
            if (STAMINA_UPGRADE)
            {
                MomoEventUtils.STAMINABERRYEVENTS.AddRange(staminaUpgradeBerryEvents);
            }
            if (MAGIC_UPGRADE)
            {
                MomoEventUtils.MAGICBERRYEVENTS.AddRange(magicUpgradeBerryEvents);
            }
            if (FAIRIES)
            {
                MomoEventUtils.FAIRYEVENTS.AddRange(fairyEvents);
            }
            if (VICTORY == victoryConditionMoonGodSelin)
            {
                MomoEventUtils.VICTORY_EVENT = 364;
            }
            else if (VICTORY == victoryConditionDora)
            {
                MomoEventUtils.VICTORY_EVENT = 370;
            }
        }
    }
}
