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
                    if (itemId == InventoryUtils.BOSS_KEY_ID)
                    {
                        text = StringUtils.BOSS_KEY_DESCRIPTION;
                    }
                    else if (itemId == InventoryUtils.DAMAGE_ID)
                    {
                        text = StringUtils.DAMAGE_DESCRIPTION;
                    }
                    else if (itemId == InventoryUtils.HEALTH_ID)
                    {
                        text = StringUtils.HEALTH_DESCRPTION;
                    }
                    else if (itemId == InventoryUtils.MAGIC_ID)
                    {
                        text = StringUtils.MAGIC_DESCRIPTION;
                    }
                    else if (itemId == InventoryUtils.STAMINA_ID)
                    {
                        text = StringUtils.STAMINA_DESCRIPTION;
                    }
                    else if (itemId == InventoryUtils.FAIRY_ID)
                    {
                        text = StringUtils.FAIRY_DESCRIPTION;
                    }
                    else if (MomoEventUtils.SKILLEVENTS.Contains(itemId))
                    {
                        text = StringUtils.GENERAL_SKILL_DESCRIPTION;
                    }
                    else if (itemId == InventoryUtils.FILLER_ID)
                    {
                        text = StringUtils.GARBAGE_DESCRIPTION;
                    }
                    else
                    {
                        text = string.Format("{0}\n{1}", GameData.itemDatabase.GetItem(itemId).Effect, GameData.itemDatabase.GetItem(itemId).Description);
                    }
                }

                text += "\nOriginal Sigil: " + GameData.itemDatabase.GetItem((int)GameDataPatcher.SHOP_ITEM_ID[currentSelect]).Name;
            }
        }
    }
}
