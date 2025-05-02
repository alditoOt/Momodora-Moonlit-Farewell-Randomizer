using APMomoMFRandomizer;
using HarmonyLib;
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
    }
}
