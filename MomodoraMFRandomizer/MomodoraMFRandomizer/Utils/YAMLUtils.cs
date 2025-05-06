using APMomodoraMoonlitFarewell;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMomodoraMoonlitFarewell.Utils
{
    class YAMLUtils
    {
        public static Boolean OPENSPRINGLEAFPATH;
        public static Boolean DEATHLINK;
        public static Boolean ADD_ORACLE_SIGIL;
        public static Boolean KEY_ITEMS;
        public static string FAST_TRAVEL_CHOICE;
        public static Boolean FINAL_BOSS_DOOR;
        public static Boolean DAMAGE_UPGRADE;
        public static Boolean HEALTH_UPGRADE;
        public static Boolean STAMINA_UPGRADE;
        public static Boolean MAGIC_UPGRADE;
        public static Boolean FAIRIES;
        public static String VICTORY;
        public static void GetSettingsFromYAML()
        {
            try
            {
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("open_springleaf_path", out object openSpringleafPath);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("deathlink", out object deathlink);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("randomize_key_items", out object keyItems);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("oracle_sigil", out object oracleSigil);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("final_boss_keys", out object finalBossKeys);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("progressive_damage_upgrade", out object damageUpgrade);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("progressive_health_upgrade", out object healthUpgrade);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("progressive_stamina_upgrade", out object staminaUpgrade);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("progressive_magic_upgrade", out object magicUpgrade);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("progressive_lumen_fairies", out object fairies);
                APMomodoraMoonlitFarewell.session.DataStorage.GetSlotData().TryGetValue("victory_condition", out object victory);
                //APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("fast_travel", out object fastTravel);
                OPENSPRINGLEAFPATH = (Boolean)openSpringleafPath;
                DEATHLINK = (Boolean)deathlink;
                KEY_ITEMS = (Boolean)keyItems;
                ADD_ORACLE_SIGIL = (Boolean)oracleSigil;
                FINAL_BOSS_DOOR = (Boolean)finalBossKeys;
                DAMAGE_UPGRADE = (Boolean)damageUpgrade;
                HEALTH_UPGRADE = (Boolean)healthUpgrade;
                STAMINA_UPGRADE = (Boolean)staminaUpgrade;
                MAGIC_UPGRADE = (Boolean)magicUpgrade;
                FAIRIES = (Boolean)fairies;
                VICTORY = (string)victory;
                //FAST_TRAVEL_CHOICE = (string)fastTravel;
            }
            catch (Exception e)
            {
                MelonLogger.Msg($"error trying to get settings from YAML file: {e.Message}");
            }
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
                MomoEventUtils.LILYEVENTS.AddRange(new int[] { 264, 81, 129, 28, 84, 118, 94, 344, 172, 38, 343, 23, 169, 153, 166, 130, 302, 247, 334, 336, 333, 286, 285, 323, 322 });
            }
            if (HEALTH_UPGRADE)
            {
                MomoEventUtils.HEALTHBERRYEVENTS.AddRange(new int[] { 30, 27, 29, 361, 26, 152, 24, 174, 116, 35, 31, 89, 143, 149, 356, 246, 165, 359, 332, 360, 348, 349, 218, 282});
            }
            if (STAMINA_UPGRADE)
            {
                MomoEventUtils.STAMINABERRYEVENTS.AddRange(new int[] { 201, 203, 303, 331, 202});
            }
            if (MAGIC_UPGRADE)
            {
                MomoEventUtils.MAGICBERRYEVENTS.AddRange(new int[] { 135, 33, 95, 115, 228, 341, 295});
            }
            if (FAIRIES)
            {
                MomoEventUtils.FAIRYEVENTS.AddRange(new int[] { 41, 354, 44, 87, 51, 88, 352, 351, 162, 54, 346, 57, 47, 93, 137, 91, 342, 168, 355, 221, 224, 229, 238, 357, 300, 288, 287, 46, 353, 301});
            }
            if (VICTORY == "moon_god_selin")
            {
                MomoEventUtils.VICTORY_EVENT = 364;
            } 
            else if (VICTORY == "dora")
            {
                MomoEventUtils.VICTORY_EVENT = 370;
            }
            MelonLogger.Msg(MomoEventUtils.VICTORY_EVENT);
            //WIP
            //if (FAST_TRAVEL_CHOICE == StringUtils.VANILLA)
            //{
            //    MomoEventUtils.SKILLEVENTS.Remove(MomoEventUtils.FAST_TRAVEL_EVENT);
            //}
            //else if (FAST_TRAVEL_CHOICE == StringUtils.START_WITH)
            //{
            //    APLocationHandler.GiveItem(MomoEventUtils.FAST_TRAVEL_EVENT);
            //}
        }
    }
}
