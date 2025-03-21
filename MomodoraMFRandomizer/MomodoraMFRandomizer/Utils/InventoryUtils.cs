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
        public static List<int> SIGIL_ID = new List<int> { 442, 400, 436, 402, 403, 433, 448, 420, 412, 404, 434, 447, 440, 405, 439, 443, 444, 425, 445, 430, 435, 432, 427, 438, 449, 446, 123, 408, 422, 401 };
        public static int ORACLE = 441;
        public static Dictionary<int, int> SKILL_INVENTORY_ID = new Dictionary<int, int>()
        {
            { 9, 345 },
            { 20, 342 },
            { 194, 343 },
            { 131, 351 },
            { 10, 344 }
        };
        public static List<int> KEY_ITEMS_ID = new List<int>();        
    }
}
