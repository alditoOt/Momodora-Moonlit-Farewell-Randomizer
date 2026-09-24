using APMomodoraMoonlitFarewell;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMomodoraMoonlitFarewell.Utils;
using Archipelago.MultiClient.Net.Enums;
using System.Reflection;

namespace APMomodoraMoonlitFarewell.Patches
{
    [HarmonyPatch(typeof(DialogueBox))]
    class ShopPatcher
    {
        [HarmonyPatch("SetOptions")]
        [HarmonyPrefix]
        public static void ChangeItemName(ref string[] options, ref string[] decors)
        {
            if (options.Length < 8)
            {
                return;
            }
            string[] apItems = new string[options.Length];
            for (int i = 0; i < apItems.Length; i ++)
            {
                apItems[i] = InventoryUtils.AP_SHOP_ITEMS[i];
            }
            options = apItems;
        }

        private static string[] SetItemPrices()
        {
            string[] prices = new string[InventoryUtils.AP_SHOP_ITEMS.Length];
            for (int i = 0; i < InventoryUtils.AP_SHOP_ITEMS.Length; i++)
            {
                prices[i] = ItemFlagText.PriceFor(InventoryUtils.AP_SHOP_ITEM_FLAGS[i]);
            }
            return prices;
        }
    }

    static class ItemFlagText
    {
        private static readonly Dictionary<ItemFlags, (string price, string classification)> flagText = new Dictionary<ItemFlags, (string price, string classification)>()
        {
            { ItemFlags.None, ("100", "Doesn't seem special") },
            { ItemFlags.Advancement, ("400", "Looks important") },
            { ItemFlags.NeverExclude, ("250", "Looks useful") },
            { ItemFlags.Trap, ("1", "Looks like...?") },
        };

        private static readonly (string price, string classification) defaultText = ("100", "No clue");

        public static string PriceFor(ItemFlags flag) =>
            flagText.TryGetValue(flag, out var text) ? text.price : defaultText.price;

        public static string ClassificationFor(ItemFlags flag) =>
            flagText.TryGetValue(flag, out var text) ? text.classification : defaultText.classification;
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

                if (!InventoryUtils.AP_SHOP_ITEM_IN_GAME[currentSelect])
                {
                    text = $"AP Item for {InventoryUtils.AP_SHOP_PLAYER_NAME[currentSelect]} - {ClassificationText()[currentSelect]}";
                }
                else
                {
                    switch (itemId)
                    {
                        case var id when id == InventoryUtils.BOSS_KEY_ID:
                            text = StringUtils.BOSS_KEY_DESCRIPTION;
                            break;

                        case var id when id == InventoryUtils.DAMAGE_ID:
                            text = StringUtils.DAMAGE_DESCRIPTION;
                            break;

                        case var id when id == InventoryUtils.HEALTH_ID:
                            text = StringUtils.HEALTH_DESCRPTION;
                            break;

                        case var id when id == InventoryUtils.MAGIC_ID:
                            text = StringUtils.MAGIC_DESCRIPTION;
                            break;

                        case var id when id == InventoryUtils.STAMINA_ID:
                            text = StringUtils.STAMINA_DESCRIPTION;
                            break;

                        case var id when id == InventoryUtils.FAIRY_ID:
                            text = StringUtils.FAIRY_DESCRIPTION;
                            break;

                        case var id when id == InventoryUtils.FILLER_ID:
                            text = StringUtils.GARBAGE_DESCRIPTION;
                            break;
                        case 9:
                            text = StringUtils.DASH_DESCRIPTION;
                            break;
                        case 10:
                            text = StringUtils.DOUBLE_JUMP_DESCRIPTION;
                            break;
                        case 20:
                            text = StringUtils.LEAF_DESCRIPTION;
                            break;
                        case 194:
                            text = StringUtils.WALL_JUMP_DESCRIPTION;
                            break;
                        case 131:
                            text = StringUtils.LUNAR_ATTUNEMENT_DESCRIPTION;
                            break;
                        case 205:
                            text = StringUtils.FAST_TRAVEL_DESCRIPTION;
                            break;
                        default:
                            var item = GameData.itemDatabase.GetItem(itemId);
                            text = item.Effect ?? item.Description;
                            break;
                    }
                }

                text += "\nOriginal Sigil: " + GameData.itemDatabase.GetItem((int)GameDataPatcher.shopItemIDs[currentSelect]).Name;
            }
        }

        private static string[] ClassificationText()
        {
            string[] classText = new string[InventoryUtils.AP_SHOP_ITEMS.Length];
            for (int index = 0; index < InventoryUtils.AP_SHOP_ITEMS.Length; index++)
            {
                classText[index] = ItemFlagText.ClassificationFor(InventoryUtils.AP_SHOP_ITEM_FLAGS[index]);
            }
            return classText;
        }
    }
}
