using APMomodoraMoonlitFarewell;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Patches
{
    [HarmonyPatch(typeof(GameData))]
    class GameDataPatcher
    {
        public static long[] SHOP_ITEM_ID = new long[] { 123, 408, 422, 401, 431, 426, 437, 406 };

        // The in-game shop id that contains every purchasable item.
        private const int fullShopId = 3;

        [HarmonyPatch("GetShop")]
        [HarmonyPrefix]
        public static void GetShop(ref int shop_id)
        {
            shop_id = fullShopId; //Force the shopId to always get the full shop
        }

        private static void UpdateShopNames(Dictionary<long, ScoutedItemInfo> results)
        {
            int index = 0;
            foreach (var pair in results)
            {
                InventoryUtils.AP_SHOP_ITEMS[index] = pair.Value.ItemDisplayName;
                InventoryUtils.AP_SHOP_ITEM_IN_GAME[index] = pair.Value.ItemGame == "Momodora Moonlit Farewell";
                InventoryUtils.AP_SHOP_ITEMS_ID[index] = pair.Value.ItemId;
                InventoryUtils.AP_SHOP_PLAYER_NAME[index] = pair.Value.Player.Alias;
                InventoryUtils.AP_SHOP_ITEM_FLAGS[index] = pair.Value.Flags;
                index++;
            }
        }

        public static void UpdateShopNames()
        {
            try
            {
                // Deliberately synchronous: this runs once during OnLateInitializeMelon, before any
                // gameplay patches can fire, and this MelonLoader mod has no coroutine/async pump to
                // await on instead. Blocking here is acceptable; the try/catch below bounds the risk
                // of a network stall or fault taking down the rest of startup.
                Dictionary<long, ScoutedItemInfo> results = APMomodoraMoonlitFarewell.session.Locations.ScoutLocationsAsync(false, SHOP_ITEM_ID).Result;
                UpdateShopNames(results);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Failed to scout shop item locations: {e.Message}");
            }
        }
    }
}
