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
    class InventoryUtils
    {
        public static int BOSS_KEY_ID = 991;
        public static int DAMAGE_ID = 992;
        public static int HEALTH_ID = 993;
        public static int STAMINA_ID = 994;
        public static int MAGIC_ID = 995;
        public static int FAIRY_ID = 996;
        public static int FILLER_ID = 999;

        public static List<int> ITEM_ID = new List<int> { 442, 400, 436, 402, 403, 433, 448, 412, 404, 434, 447, 440, 405, 439, 443, 444, 425, 445, 430, 435, 432, 427, 438, 449, 446, 338, 339, 340 };
        public static List<int> KEY_ITEM_ID = new List<int> { 333, 332, 356 };
        public static int ORACLE = 441;
        public static Dictionary<int, int> SKILL_INVENTORY_ID = new Dictionary<int, int>()
        {
            { 9, 345 },
            { 20, 342 },
            { 194, 343 },
            { 131, 351 },
            { 10, 344 }
        };

        public static string[] AP_SHOP_ITEMS = new string[8];
    }   
}
