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
        public static Boolean OPEN_SPRINGLEAF_PATH;
        public static Boolean DEATHLINK;
        public static Boolean ORACLE_SIGIL;
        public static Boolean KEY_ITEMS;
        // public static string FAST_TRAVEL_CHOICE;
        public static Boolean FINAL_BOSS_DOOR;
        public static Boolean DAMAGE_UPGRADE;
        public static Boolean HEALTH_UPGRADE;
        public static Boolean STAMINA_UPGRADE;
        public static Boolean MAGIC_UPGRADE;
        public static Boolean LUMEN_FAIRIES;
        public static String VICTORY_CONDITION;

        private const string MOON_GOD_SELIN = "moon_god_selin";
        private const string DORA = "dora";

        // Damage Lily event ids
        private static readonly int[] damageLilyEvents = { 264, 81, 129, 28, 84, 118, 94, 344, 172, 38, 343, 23, 169, 153, 166, 130, 302, 247, 334, 336, 333, 286, 285, 323, 322 };

        // Health Berry event ids
        private static readonly int[] healthBerryEvents = { 30, 27, 29, 361, 26, 152, 24, 174, 116, 35, 31, 89, 143, 249, 356, 246, 165, 359, 332, 360, 348, 349, 219, 282 };

        // Stamina Berry event ids
        private static readonly int[] staminaBerryEvents = { 201, 203, 303, 331, 202 };

        // Magic Berry event ids
        private static readonly int[] magicBerryEvents = { 135, 33, 95, 115, 228, 341, 295 };

        // Lumen Fairy event ids
        private static readonly int[] lumenFairyEvents = { 41, 354, 44, 87, 51, 88, 352, 351, 162, 54, 346, 57, 47, 93, 137, 91, 342, 168, 355, 221, 224, 229, 238, 357, 300, 288, 287, 46, 353, 301 };

        public static void GetSettingsFromYAML()
        {
            var slotData = APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData();

            OPEN_SPRINGLEAF_PATH = GetBoolSetting(slotData, "open_springleaf_path");
            DEATHLINK = GetBoolSetting(slotData, "deathlink");
            KEY_ITEMS = GetBoolSetting(slotData, "randomize_key_items");
            ORACLE_SIGIL = GetBoolSetting(slotData, "oracle_sigil");
            FINAL_BOSS_DOOR = GetBoolSetting(slotData, "final_boss_keys");
            DAMAGE_UPGRADE = GetBoolSetting(slotData, "progressive_damage_upgrade");
            HEALTH_UPGRADE = GetBoolSetting(slotData, "progressive_health_upgrade");
            STAMINA_UPGRADE = GetBoolSetting(slotData, "progressive_stamina_upgrade");
            MAGIC_UPGRADE = GetBoolSetting(slotData, "progressive_magic_upgrade");
            LUMEN_FAIRIES = GetBoolSetting(slotData, "progressive_lumen_fairies");
            VICTORY_CONDITION = GetStringSetting(slotData, "victory_condition");
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
            if (ORACLE_SIGIL)
            {
                InventoryUtils.AP_SIGIL_ITEM_ID.Add(InventoryUtils.ORACLE);
            }
            if (DAMAGE_UPGRADE)
            {
                MomoEventUtils.LILYEVENTS.AddRange(damageLilyEvents);
            }
            if (HEALTH_UPGRADE)
            {
                MomoEventUtils.HEALTHBERRYEVENTS.AddRange(healthBerryEvents);
            }
            if (STAMINA_UPGRADE)
            {
                MomoEventUtils.STAMINABERRYEVENTS.AddRange(staminaBerryEvents);
            }
            if (MAGIC_UPGRADE)
            {
                MomoEventUtils.MAGICBERRYEVENTS.AddRange(magicBerryEvents);
            }
            if (LUMEN_FAIRIES)
            {
                MomoEventUtils.FAIRYEVENTS.AddRange(lumenFairyEvents);
            }
            if (VICTORY_CONDITION == MOON_GOD_SELIN)
            {
                MomoEventUtils.VICTORY_EVENT = 364;
            }
            else if (VICTORY_CONDITION == DORA)
            {
                MomoEventUtils.VICTORY_EVENT = 370;
            }
        }
    }
}
