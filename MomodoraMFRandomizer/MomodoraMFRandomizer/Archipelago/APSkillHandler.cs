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
        // Sprint, Leaf, Double Jump, Wall Jump, Lunar Attunement, Fast Travel event indices, as measured in-game.
        private const int dashSkillEvent = 9;
        private const int leafSkillEvent = 20;
        private const int doubleJumpSkillEvent = 10;
        private const int wallJumpSkillEvent = 194;
        private const int lunarAttunementSkillEvent = 131;

        // Boss event that must be cleared before the dash skill can be reset/re-granted in this scene.
        private const int dashResetGateEvent = 17;

        static Dictionary<string, int> skillAndScene = new Dictionary<string, int>()
        {
            { "Well26", leafSkillEvent },
            { "Well29", dashSkillEvent },
            { "Bark42", doubleJumpSkillEvent },
            { "Fairy10", wallJumpSkillEvent },
            { "Marsh08", lunarAttunementSkillEvent },
            { MomoEventUtils.FAST_TRAVEL_SCENE, MomoEventUtils.FAST_TRAVEL_EVENT }
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

            if (index == dashSkillEvent)
            {
                if (GameData.current.MomoEvent[dashResetGateEvent] == 0)
                {
                    ResetDashSkill();
                    // Check what happens if you get the dash skill back while in the scene
                }
            }
            else if (index == lunarAttunementSkillEvent)
            {
                if (GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(InventoryUtils.GOLD_MOONLIT_DUST_ID))
                    && GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(InventoryUtils.SILVER_MOONLIT_DUST_ID))
                    && GameData.current.MomoEvent[MomoEventUtils.TAINTED_SERPENT_DEFEATED_EVENT] == 1) //Tainted Serpent defeated
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
            GameData.current.MomoEvent[dashSkillEvent] = 0;
        }
    }
}
