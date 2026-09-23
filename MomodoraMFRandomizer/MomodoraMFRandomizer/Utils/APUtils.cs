using Archipelago.MultiClient.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APMomodoraMoonlitFarewell.Utils
{
    class APUtils
    {
        public static void CompleteLocation(int index)
        {
            if (!APMomodoraMoonlitFarewell.IsSessionActive)
            {
                return;
            }
            APMomodoraMoonlitFarewell.session.Locations.CompleteLocationChecks(index);
        }

        // Loading a save restores every MomoEvent flag through the same indexer setter live gameplay
        // uses, replaying set_Item(value: 1) for checks that were already completed in a previous
        // session. This lets ReportLocation branches guard against reapplying their effect (stat
        // changes, counter decrements, notifications) on that replay, the same way the skill-item
        // branch already guards itself via AllItemsReceived.
        public static bool IsLocationChecked(long locationId) =>
            APMomodoraMoonlitFarewell.IsSessionActive &&
            APMomodoraMoonlitFarewell.session.Locations.AllLocationsChecked.Contains(locationId);
    }
}
