using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
{
    class APSkillLocationHandler
    {
        static Dictionary<string, int> skillAndScene = new Dictionary<string, int>()
        {
            { "Well26", 20 },
            {"Well29" , 9 },
            {"Bark42" , 10 },
            {"Fairy10" , 194 },
            {"Marsh08" , 131 }
        };

        public static void HandleSkillLocationCheck(string sceneName, Boolean mainMenu)
        {
            if (mainMenu || !skillAndScene.ContainsKey(sceneName) || GameData.current.MomoEvent[skillAndScene[sceneName]] == 0)
            {
                return;
            }

            int index = skillAndScene[sceneName];
            if (index == 9)
            {
                APLocationHandler.ResetDashSkill();
            }
            else if (index == 131)
            {
                if (GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(333)) 
                    && GameData.inventory.HasItem(GameData.itemDatabase.GetItemDef(332))
                    && GameData.current.MomoEvent[255] == 1)
                {
                    APMomoMFRandomizer.session.Locations.CompleteLocationChecks(index);
                } 
            }
            else
            {
                APMomoMFRandomizer.session.Locations.CompleteLocationChecks(index);
            }
        }

    }
}
