using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Packets;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
{
    class APConnector
    {
        private static LoginResult TryToLogin(ArchipelagoSession session, string user, string password)
        {
            var result = session.TryConnectAndLogin(StringUtils.GAME, user, ItemsHandlingFlags.AllItems, password: password);
            return result;
        }

        public static void Connect(ArchipelagoSession session, string server, string user, string pass)
        {
            LoginResult result;
            try
            {
                result = TryToLogin(session, user, pass);
            }
            catch (Exception e)
            {
                result = new LoginFailure(e.GetBaseException().Message);
                MelonLogger.Msg("Error connecting to Archipelago: " + e.Message);
            }

            if (!result.Successful)
            {
                LoginFailure failure = (LoginFailure)result;
                string errorMessage = $"Failed to Connect to {server} as {user}:";
                foreach (string error in failure.Errors)
                {
                    errorMessage += $"\n    {error}";
                }
                foreach (ConnectionRefusedError error in failure.ErrorCodes)
                {
                    errorMessage += $"\n    {error}";
                }

                MelonLogger.Msg(errorMessage);
                return; // Did not connect, show the user the contents of `errorMessage`
            }

            var loginSuccess = (LoginSuccessful)result;
            MelonLogger.Msg("soy fans");
        }
    }
}
