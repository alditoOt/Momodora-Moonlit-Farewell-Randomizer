using Archipelago.MultiClient.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APMomodoraMoonlitFarewell.Archipelago;

namespace APMomodoraMoonlitFarewell.Utils
{
    class APUtils
    {
        public static void CompleteLocation(int index)
        {
            if (!APMomodoraMoonlitFarewell.HasSession)
            {
                return;
            }
            // Queued rather than sent: the send happens on APConnectionManager's worker thread so a
            // dead connection can never freeze the game (as it did before), and is retried after a reconnect
            APConnectionManager.QueueLocation(index);
        }

        // Loading a save restores every MomoEvent flag replaying the in-game method set_Item(value: 1) 
        // for checks that were already completed in a previous session. 
        // This lets ReportLocation branches prevent reapplying their effect on that replay of the method,
        // the same way the skill-item branch already guards itself via AllItemsReceived
        public static bool IsLocationChecked(long locationId) =>
            APMomodoraMoonlitFarewell.HasSession &&
            (APConnectionManager.IsPending(locationId) ||
             APMomodoraMoonlitFarewell.session.Locations.AllLocationsChecked.Contains(locationId));
    }
}
