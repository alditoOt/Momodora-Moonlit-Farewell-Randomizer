using APMomodoraMoonlitFarewell.Utils;
using Archipelago.MultiClient.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    [HarmonyPatch(typeof(Inventory))]
    class APSigilHandler
    {
        public static Boolean itemReceived;

        [HarmonyPatch("Add")]
        [HarmonyPrefix]
        public static bool HandleSigilAP(Item item, ref bool is_new_item)
        {
            itemReceived = false;
            if (!InventoryUtils.AP_SIGIL_ITEM_ID.Contains(item.itemDef.Index))
            {
                APUtils.CompleteLocation(item.itemDef.Index);
                return true;
            }
            foreach (ItemInfo apItem in APMomodoraMoonlitFarewell.session.Items.AllItemsReceived)
            {
                long itemId = apItem.ItemId;
                if (item.itemDef.Index == itemId)
                {
                    itemReceived = true;
                    break;
                }
            }
            if (is_new_item)
            {
                APUtils.CompleteLocation(item.itemDef.Index);
            }
            return itemReceived;
        }
    }
}
