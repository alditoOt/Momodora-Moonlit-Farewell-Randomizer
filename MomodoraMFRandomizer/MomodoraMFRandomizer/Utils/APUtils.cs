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
    }
}
