using Archipelago.MultiClient.Net.Models;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    // Pre-scouts every AP-trackable (non-shop) location once at startup so ownership of a
    // pickup (ours vs. another player's) is known synchronously, in time to intercept the
    // same-frame vanilla popup rather than waiting on a live server round-trip.
    static class APLocationScoutCache
    {
        private static Dictionary<long, ScoutedItemInfo> locationInfo = new Dictionary<long, ScoutedItemInfo>();

        public static void Initialize()
        {
            if (!APMomodoraMoonlitFarewell.HasSession)
            {
                return;
            }

            try
            {
                long[] ids = BuildTrackedLocationIds();
                locationInfo = APMomodoraMoonlitFarewell.session.Locations.ScoutLocationsAsync(false, ids).Result;
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Failed to pre-scout AP locations for send notifications: {e.Message}");
            }
        }

        private static long[] BuildTrackedLocationIds()
        {
            List<long> ids = new List<long>();
            ids.AddRange(MomoEventUtils.BOSSEVENTS.Select(i => (long)i));
            ids.AddRange(MomoEventUtils.SKILLEVENTS.Select(i => (long)i));
            ids.AddRange(MomoEventUtils.LILYEVENTS.Select(i => (long)i * 100));
            ids.AddRange(MomoEventUtils.HEALTHBERRYEVENTS.Select(i => (long)i * 100));
            ids.AddRange(MomoEventUtils.STAMINABERRYEVENTS.Select(i => (long)i * 100));
            ids.AddRange(MomoEventUtils.MAGICBERRYEVENTS.Select(i => (long)i * 100));
            ids.AddRange(MomoEventUtils.FAIRYEVENTS.Select(i => (long)i * 100));
            ids.AddRange(InventoryUtils.AP_SIGIL_ITEM_ID.Select(i => (long)i));
            ids.AddRange(InventoryUtils.NON_AP_SIGIL_ITEM_ID.Select(i => (long)i));
            ids.Add(InventoryUtils.livingBloodId);
            ids.Add(InventoryUtils.woodenBoxId);
            return ids.Distinct().ToArray();
        }

        public static bool TryGetInfo(long locationId, out ScoutedItemInfo info)
        {
            return locationInfo.TryGetValue(locationId, out info);
        }
    }
}
