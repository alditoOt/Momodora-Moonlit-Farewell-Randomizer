using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MomoReader
{
    public class MomoReader : MelonMod
    {
        public static string sceneName;
        public static float phys_attack;
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MomoReader.sceneName = sceneName;
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F5)) {
                Time.timeScale = 50f;
            }
            if (Input.GetKeyUp(KeyCode.F5))
            {
                Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.F1))
            {
                GameData.inventory.Add(GameData.itemDatabase.GetItem(338));
            }
        }

        public override void OnFixedUpdate()
        {
            phys_attack = Platformer3D.phys_attack;
            
        }
    }
}
