using APMomoMFRandomizer;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
{
    [HarmonyPatch(typeof(DialogueBox))]
    class ShopPatcher
    {

        [HarmonyPatch("SetOptions")]
        [HarmonyPrefix]
        public static void ChangeItemName(ref string[] options)
        {
            MelonLogger.Msg("Updating shop");
            if (options.Length < 8)
            {
                return;
            }
            string[] apItems = new string[options.Length];
            for (int i = 0; i < apItems.Length; i ++)
            {
                apItems[i] = InventoryUtils.AP_SHOP_ITEMS[i];
                MelonLogger.Msg($"Index: {i}, name: {InventoryUtils.AP_SHOP_ITEMS[i]}");
            }
            options = apItems;
        }
    }

    [HarmonyPatch(typeof(DialogueCharText))]
    class ShopDescriptionPatcher
    {
        private static int currentSelect;
        [HarmonyPatch("SetSelect")]
        [HarmonyPrefix]
        public static void GetCurrentSelect(ref int select)
        {
            currentSelect = select; 
            if (currentSelect >= 8)
            {
                currentSelect = 0;
            } else if (currentSelect < 0)
            {
                currentSelect = 7;
            }
        }
        
        [HarmonyPatch("UpdateSupText")]
        [HarmonyPrefix]
        public static void UpdateItemDescription(ref string text)
        {
            if (currentSelect < 8)
            {
                int itemId = (int)InventoryUtils.AP_SHOP_ITEMS_ID[currentSelect];
                MelonLogger.Msg(itemId);
                if (!InventoryUtils.AP_SHOP_ITEM_IN_GAME[currentSelect])
                {
                    text = "AP Item";
                } else
                {
                    if (itemId == InventoryUtils.DAMAGE_ID)
                    {
                        text = "Increases attack power by 2!";
                    } else if (itemId == InventoryUtils.HEALTH_ID) {
                        text = "Increases Maximum Health by 50!";
                    } else if (itemId == InventoryUtils.MAGIC_ID)
                    {
                        text = "Increases Maximum Magic affinity by 10!";
                    } else if (itemId == InventoryUtils.STAMINA_ID)
                    {
                        text = "increases stamina regeneration speed!";
                    } else if (itemId == InventoryUtils.FAIRY_ID)
                    {
                        text = "Sets free a Lumen Fairy!";
                    } else if (MomoEventUtils.SKILLEVENTS.Contains(itemId))
                    {
                        text = "In game skill";
                    } else if (itemId == InventoryUtils.FILLER_ID)
                    {
                        text = "Money!";
                    }
                    else {
                        text = GameData.itemDatabase.GetItem(itemId).Effect;
                    }
                }

                text += "\nOriginal Sigil: " + GameData.itemDatabase.GetItem((int)GameDataPatcher.SHOP_ITEM_ID[currentSelect]).Name;
            }
        }
    }
}
