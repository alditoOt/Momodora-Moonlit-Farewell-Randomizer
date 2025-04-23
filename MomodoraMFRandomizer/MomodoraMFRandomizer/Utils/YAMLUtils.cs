using APMomoMFRandomizer;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
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
        public static void GetSettingsFromYAML()
        {
            try
            {
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("open_springleaf_path", out object openSpringleafPath);
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("deathlink", out object deathlink);
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("randomize_key_items", out object keyItems);
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("oracle_sigil", out object oracleSigil);
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("final_boss_keys", out object finalBossKeys);
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("progressive_damage_upgrade", out object damageUpgrade);
                //APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("fast_travel", out object fastTravel);
                OPENSPRINGLEAFPATH = (Boolean)openSpringleafPath;
                DEATHLINK = (Boolean)deathlink;
                KEY_ITEMS = (Boolean)keyItems;
                ADD_ORACLE_SIGIL = (Boolean)oracleSigil;
                FINAL_BOSS_DOOR = (Boolean)finalBossKeys;
                DAMAGE_UPGRADE = (Boolean)damageUpgrade;
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
                InventoryUtils.ITEM_ID.AddRange(InventoryUtils.KEY_ITEM_ID);
            }
            if (ADD_ORACLE_SIGIL)
            {
                InventoryUtils.ITEM_ID.Add(InventoryUtils.ORACLE);
            }
            if (DAMAGE_UPGRADE)
            {
                MomoEventUtils.LILYEVENTS.AddRange(new int[] { 264, 81, 129, 28, 84, 118, 94, 244, 172, 38, 343, 23, 169, 513, 166, 130, 302, 247, 334, 336, 333, 286, 285, 232, 322 });
            }
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
