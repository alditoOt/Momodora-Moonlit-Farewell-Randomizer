using Archipelago.MultiClient.Net.Models;
using HarmonyLib;
using APMomodoraMoonlitFarewell.Archipelago;

namespace APMomodoraMoonlitFarewell.Patches
{
    // Lets every vanilla pickup (berries, sigils, skills sometimes) run completely untouched
    // then adds a separate side ItemNotifications popup whenever the location is part of the AP 
    // exchange (sent to another player, or received back by the player, go you!), and replaces the vanilla 
    // TutorialMessage (top message) popup's text with a short pointer to that side popup 

    [HarmonyPatch(typeof(MainScr), "GetString")]
    class MainScrGetStringSentinelPatcher
    {
        [HarmonyPrefix]
        static bool Prefix(string id, ref string __result)
        {
            if (id == APExchangeNotifier.SentinelLocalizationKey)
            {
                __result = "";
                return false;
            }
            return true;
        }
    }

    // ItemNotifications' own fade animation doesn't apply to colored text, so this re-renders
    // whatever we last put in the corner popup every frame with the current fade alpha baked into its color tags
    [HarmonyPatch(typeof(ItemNotifications), "FixedUpdate")]
    class ItemNotificationsColorFadePatcher
    {
        [HarmonyPostfix]
        static void Postfix(ItemNotifications __instance)
        {
            APExchangeNotifier.ReapplyColorForFrame(__instance);
        }
    }

    [HarmonyPatch(typeof(ItemFruit), "Notif")]
    class ItemFruitNotifPatcher
    {
        [HarmonyPostfix]
        static void Postfix(ItemFruit __instance)
        {
            long locationId = __instance.DestroyFlag * 100;
            if (APLocationScoutCache.TryGetInfo(locationId, out ScoutedItemInfo info))
            {
                APExchangeNotifier.NotifyExchange(info);
                APExchangeNotifier.OverrideTutorialMessageWithPointerPopup(info);
            }
        }
    }

    [HarmonyPatch(typeof(ItemSparkle), "FlowerStuff3")]
    class ItemSparkleFlowerStuff3Patcher
    {
        [HarmonyPostfix]
        static void Postfix(ItemSparkle __instance)
        {
            long locationId = __instance.DestroyFlag * 100;
            if (APLocationScoutCache.TryGetInfo(locationId, out ScoutedItemInfo info))
            {
                APExchangeNotifier.NotifyExchange(info);
                APExchangeNotifier.OverrideTutorialMessageWithPointerPopup(info);
            }
        }
    }

    [HarmonyPatch(typeof(ItemSparkle), "OnTalk")]
    class ItemSparkleSigilOnTalkPatcher
    {
        [HarmonyPostfix]
        static void Postfix(ItemSparkle __instance)
        {
            if (__instance.visualProperty != ItemSparkle.Property.Sigil)
            {
                return;
            }
            long locationId = GameData.itemDatabase.GetItem(__instance.TreasureID).itemDef.Index;
            // The corner popup itself is raised from APSigilHandler (Inventory.Add); only the
            // vanilla TutorialMessage replacement happens here
            if (APLocationScoutCache.TryGetInfo(locationId, out ScoutedItemInfo info))
            {
                APExchangeNotifier.OverrideTutorialMessageWithPointerPopup(info);
            }
        }
    }

    [HarmonyPatch(typeof(MetroidvaniaPickup), "OnTriggerStay")]
    class MetroidvaniaPickupPatcher
    {
        // Dash (9) and Double Jump (10) are the only two skills granted through this generic
        // pickup, and the only two whose vanilla popup is TutorialMessage-based (a genuine popup)
        // rather than a dialogue sequence, so they're the only skills handled here
        [HarmonyPostfix]
        static void Postfix(MetroidvaniaPickup __instance)
        {
            if (__instance.DestroyFlag != 9 && __instance.DestroyFlag != 10)
            {
                return;
            }
            if (APLocationScoutCache.TryGetInfo(__instance.DestroyFlag, out ScoutedItemInfo info))
            {
                APExchangeNotifier.NotifyExchange(info);
                APExchangeNotifier.OverrideTutorialMessageWithPointerPopup(info);
            }
        }
    }
}
