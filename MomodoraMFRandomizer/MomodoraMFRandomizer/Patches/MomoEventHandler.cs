using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MomodoraMFRandomizer
{
    class MomoEventHandler
    {
        public void GiveSkill(int eventValue)
        {
            GameData.current.MomoEvent[eventValue] = 1;
        }

        public static void KillPlayer()
        {
            Platformer3D.player_hp = 0f;
        }
    }
}
