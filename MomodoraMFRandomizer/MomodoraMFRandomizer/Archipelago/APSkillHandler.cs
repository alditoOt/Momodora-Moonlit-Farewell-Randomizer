using APMomodoraMoonlitFarewell.Utils;
using Archipelago.MultiClient.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using HarmonyLib;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    [HarmonyPatch(typeof(MomoEventData))]
    class APSkillHandler
    {
        static Dictionary<string, int> skillAndScene = new Dictionary<string, int>()
        {
            { "Well26", 20 },
            {"Well29" , 9 },
            {"Bark42" , 10 },
            {"Fairy10" , 194 },
            {"Marsh08" , 131 },
            {"Cove03", 205 }
        };

        /*
         * Description: This method will handle completing the skill location check in case the player already has the skill when entering the scene where they would receive it in vanilla
         */
        public static void HandleSkillOnSceneLoad(string sceneName, Boolean mainMenu)
        {
            if (mainMenu || !skillAndScene.ContainsKey(sceneName) || GameData.current.MomoEvent[skillAndScene[sceneName]] == 0)
            {
                return;
            }

            int index = skillAndScene[sceneName];

            if (index == 9)
            {
                if (GameData.current.MomoEvent[17] == 0)
                {
                    ResetDashSkill();
                }
            }
            else if (index == 131) //Lunar Attunement
            {
                if (GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(333))
                    && GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(332)) // Gold and Silver Moonlit Dust
                    && GameData.current.MomoEvent[255] == 1) //Tainted Serpent defeated
                {
                    APUtils.CompleteLocation(index);
                }
            }
            else
            {
                APUtils.CompleteLocation(index);
            }
        }

        private static void ResetDashSkill()
        {
            GameData.current.MomoEvent[9] = 0;
        }
    }
}
