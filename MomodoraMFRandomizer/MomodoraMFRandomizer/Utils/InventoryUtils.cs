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
        public static List<int> SIGILID = new List<int> { 442, 400, 436, 402, 403, 433, 448, 420, 412, 404, 434, 447, 440, 405, 439, 443, 444, 425, 445, 430, 435, 419, 432, 427, 438, 449, 446 };

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
