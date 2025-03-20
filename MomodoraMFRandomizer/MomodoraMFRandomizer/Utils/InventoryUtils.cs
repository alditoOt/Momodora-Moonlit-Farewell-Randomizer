using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace APMomoMFRandomizer
{
    [HarmonyPatch(typeof(Inventory))]
    class InventoryUtils
    {
        [HarmonyPatch("Add")]
        [HarmonyPrefix]
        private static bool Add(Item item)
        {
            // If item is not in the received items by the multiworld, return false to not assign the item in game, still send the check to the server
            if (item.Type.Equals(ItemType.Sigil))
            {
                return false;
            }
            MelonLogger.Msg($"Sigil {item.Name} with ID {item.itemDef.Index} added to inventory.");
            return true;
        }
    }
}
