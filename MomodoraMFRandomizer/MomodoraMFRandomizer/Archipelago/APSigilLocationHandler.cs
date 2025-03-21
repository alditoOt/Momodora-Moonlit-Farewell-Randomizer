using Archipelago.MultiClient.Net.Models;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
{
    [HarmonyPatch(typeof(Inventory))]
    class APSigilLocationHandler
    {

        [HarmonyPatch("Add")]
        [HarmonyPrefix]
        private static bool SendSigilLocation(Item item)
        {
            bool itemReceived = false;
            if (!item.Type.Equals(ItemType.Sigil))
            {
                return true;
            }
            foreach (ItemInfo apItem in APMomoMFRandomizer.session.Items.AllItemsReceived)
            {
                long itemId = apItem.ItemId;
                if (!itemReceived && item.itemDef.Index == itemId)
                {
                    itemReceived = true;
                    break;
                }
            }
            APMomoMFRandomizer.session.Locations.CompleteLocationChecks(item.itemDef.Index);
            return itemReceived;
        }

    }
}
