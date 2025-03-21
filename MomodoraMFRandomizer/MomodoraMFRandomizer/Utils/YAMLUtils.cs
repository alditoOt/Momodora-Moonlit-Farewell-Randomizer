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
        public static string FAST_TRAVEL_CHOICE;
        public static void GetSettingsFromYAML()
        {
            try
            {
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("open_springleaf_path", out object openSpringleafPath);
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("deathlink", out object deathlink);
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("oracle_sigil", out object oracleSigil);
                //APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("fast_travel", out object fastTravel);
                OPENSPRINGLEAFPATH = (Boolean)openSpringleafPath;
                DEATHLINK = (Boolean)deathlink;
                ADD_ORACLE_SIGIL = (Boolean)oracleSigil;
                //FAST_TRAVEL_CHOICE = (string)fastTravel;
            }
            catch (Exception e)
            {
                MelonLogger.Msg($"error trying to get settings from YAML file: {e.Message}");
            }
        }

        public static void AddItemsToItemPool()
        {
            if (ADD_ORACLE_SIGIL)
            {
                InventoryUtils.SIGIL_ID.Add(InventoryUtils.ORACLE);
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
