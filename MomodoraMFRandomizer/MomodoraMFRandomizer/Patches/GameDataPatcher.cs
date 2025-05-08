using APMomodoraMoonlitFarewell;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Patches
{
    [HarmonyPatch(typeof(GameData))]
    class GameDataPatcher
    {
        public static long[] SHOP_ITEM_ID = new long[] { 123, 408, 422, 401, 431, 426, 437, 406 };
        
        [HarmonyPatch("GetShop")]
        [HarmonyPrefix]
        public static void GetShop(ref int shop_id)
        {
            shop_id = 3; //Force the shopId to always get the full shop
        }

        public static void AddAllItemsToShop()
        {
            // Blatantly copied from GameData.DefineShops() method
            ItemShop maxItemShop = new ItemShop(4, "shop_dia34", "shop_dia01", "shop_dia02", "cereza_dia12");
            maxItemShop.AddItem(GameData.itemDatabase.GetItem("fatalsurvive"), 480);
            maxItemShop.AddItem(GameData.itemDatabase.GetItem("hyperarmor"), 110);
            maxItemShop.AddItem(GameData.itemDatabase.GetItem("purehealplus"), 120);
            maxItemShop.AddItem(GameData.itemDatabase.GetItem("moremunny"), 570);
            maxItemShop.AddItem(GameData.itemDatabase.GetItem("nostatus"), 110);
            maxItemShop.AddItem(GameData.itemDatabase.GetItem("magnet"), 190);
            maxItemShop.AddItem(GameData.itemDatabase.GetItem("healmoney"), 280);
            maxItemShop.AddItem(GameData.itemDatabase.GetItem("hpregen"), 800);
            try
            {
            GameData.itemShops.Add(maxItemShop);
            } catch (Exception e)
            {
                MelonLogger.Msg("An error occured when trying to max out the shop: " + e.Message);
            } 
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
            APMomodoraMoonlitFarewell.session.Locations.ScoutLocationsAsync(UpdateShopNames, SHOP_ITEM_ID);
        }
    }
}
