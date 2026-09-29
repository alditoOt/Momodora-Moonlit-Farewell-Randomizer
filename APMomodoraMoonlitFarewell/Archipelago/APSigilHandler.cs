using APMomodoraMoonlitFarewell.Utils;
using Archipelago.MultiClient.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                NotifyIfNew(item.itemDef.Index, is_new_item);
                return true;
            }
            if (!APMomodoraMoonlitFarewell.HasSession)
            {
                // Session isn't ready yet; let the vanilla pickup through rather than blocking the player
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
                NotifyIfNew(item.itemDef.Index, is_new_item);
            }
            return itemReceived;
        }

        // Inventory.Add is the one place every sigil, grimoire, key item and The Fool/Living Blood
        // pickup passes through, so the send popup for all of them is raised here
        private static void NotifyIfNew(int locationId, bool isNewItem)
        {
            if (isNewItem && APLocationScoutCache.TryGetInfo(locationId, out ScoutedItemInfo info))
            {
                APExchangeNotifier.NotifyExchange(info);
            }
        }
    }
}
