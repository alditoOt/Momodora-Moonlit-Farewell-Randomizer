using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    class APDeathLinkHandler
    {

        private Boolean isDead = false;
        public void CheckDeathLink(DeathLinkService deathLinkService, String username)
        {           
            if (!isDead && Platformer3D.player_hp <= 0f)
            {
                isDead = true;
                if (deathLinkService != null && APConnectionManager.IsConnected)
                {
                    MelonLogger.Msg("deathlink sent");
                    APConnectionManager.RunInBackground(() => deathLinkService.SendDeathLink(new DeathLink(username)));
                }
            }
            if (Platformer3D.player_hp >= 1)
            {
                isDead = false;
            }
        }

        public void SetIsDead(Boolean isDead)
        {
            this.isDead = isDead;
        }
    }
}
