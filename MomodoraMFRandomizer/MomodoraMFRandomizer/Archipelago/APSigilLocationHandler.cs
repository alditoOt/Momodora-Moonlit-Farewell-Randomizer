using Archipelago.MultiClient.Net.Models;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
{
    [HarmonyPatch(typeof(Inventory))]
    class APSigilLocationHandler
    {
        public static Boolean itemReceived;
        [HarmonyPatch("Add")]
        [HarmonyPrefix]
        public static bool CheckSigilReceived(Item item, ref bool is_new_item)
        {
            itemReceived = false;
            if (!item.Type.Equals(ItemType.Sigil))
            {
                return true;
            }
            foreach (ItemInfo apItem in APMomoMFRandomizer.session.Items.AllItemsReceived)
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
                APMomoMFRandomizer.session.Locations.CompleteLocationChecks(item.itemDef.Index);
            }
            return itemReceived || item.itemDef.Index == 419;
        }
    }
}
