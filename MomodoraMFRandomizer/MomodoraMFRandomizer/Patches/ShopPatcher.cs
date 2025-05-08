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
            MelonLogger.Msg("Updating shop");
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
            //for (int i = 0; i < apItems.Length; i ++) 
            //{
                //if (decors[i] != MainScr.GetString("ui_sold"))
            //    {
            //        decors[i] = SetItemPrices()[i];
            //    }
            //}
        }

        private static string[] SetItemPrices()
        {
            string[] prices = new string[InventoryUtils.AP_SHOP_ITEMS.Length];
            for (int i = 0; i < InventoryUtils.AP_SHOP_ITEMS.Length; i++)
            {
                ItemFlags flag = InventoryUtils.AP_SHOP_ITEM_FLAGS[i];
                switch (flag)
                {
                    case var flagEnum when flagEnum == ItemFlags.None:
                        prices[i] = "400";
                        break;
                    case var flagEnum when flagEnum == ItemFlags.Advancement:
                        prices[i] = "250";
                        break;
                    case var flagEnum when flagEnum == ItemFlags.NeverExclude:
                        prices[i] = "200";
                        break;
                    case var flagEnum when flagEnum == ItemFlags.Trap:
                        prices[i] = "1";
                        break;
                    default:
                        prices[i] = "100";
                        break;
                }
            }
            return prices;
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

                if (!InventoryUtils.AP_SHOP_ITEM_IN_GAME[currentSelect])
                {
                    text = $"AP Item for {InventoryUtils.AP_SHOP_PLAYER_NAME[currentSelect]} --- {ClassificationText()[currentSelect]}";
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

                text += "\nOriginal Sigil: " + GameData.itemDatabase.GetItem((int)GameDataPatcher.SHOP_ITEM_ID[currentSelect]).Name;
            }
        }

        private static string[] ClassificationText()
        {
            string[] classText = new string[InventoryUtils.AP_SHOP_ITEMS.Length];
            for (int index = 0; index < InventoryUtils.AP_SHOP_ITEMS.Length; index++)
            {
                ItemFlags flag = InventoryUtils.AP_SHOP_ITEM_FLAGS[index];
                switch (flag)
                {
                    case var flagEnum when flagEnum == ItemFlags.None:
                        classText[index] = "Doesn't seem special";
                        break;
                    case var flagEnum when flagEnum == ItemFlags.Advancement:
                        classText[index] = "Looks important";
                        break;
                    case var flagEnum when flagEnum == ItemFlags.NeverExclude:
                        classText[index] = "Looks useful";
                        break;
                    case var flagEnum when flagEnum == ItemFlags.Trap:
                        classText[index] = "Seems like a trap...";
                        break;
                    default:
                        classText[index] = "";
                        break;
                }
            }
            return classText;
        }
    }
}
