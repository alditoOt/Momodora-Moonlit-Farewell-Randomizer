using Archipelago.MultiClient.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMomodoraMoonlitFarewell.Utils
{
    class APUtils
    {
        public static void CompleteLocation(int index)
        {
            APMomodoraMoonlitFarewell.session.Locations.CompleteLocationChecks(index);
        }
    }
}
