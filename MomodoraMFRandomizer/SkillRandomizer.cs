using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using HarmonyLib;
using System.Collections;

namespace MomodoraMFRandomizer
{
    class SkillRandomizer
    {
        public List<int> skills = new List<int> { 9, 10, 20, 194, 131 };
        public HashSet<int> assignedSkills = new HashSet<int>();
        public HashSet<int> checkedSkills = new HashSet<int>();

        String skillNameCurrent = "";
        String skillNameReplaced = "";

        public void randomizeSkills()
        {
            foreach (int skill in skills)
            {

                if (GameData.current.MomoEvent[skill] == 1 && !assignedSkills.Contains(skill))
                {
                    checkedSkills.Add(skill);
                    SetSkillName(skillNameReplaced, skill);
                    HandleAcquiredSkill(skill);
                    PrintHashSet(checkedSkills);
                    PrintHashSet(assignedSkills);
                    break;
                }
            }   
        }
        private void HandleAcquiredSkill(int acquiredSkill)
        {
            GameData.current.MomoEvent[acquiredSkill] = 0;

            List<int> availableSkills = skills.Except(assignedSkills).ToList();
            
            if (availableSkills.Count > 0)
            {
                int randomSkill = availableSkills[UnityEngine.Random.Range(0, availableSkills.Count)];
                SetSkillName(skillNameCurrent, randomSkill);
                GameData.current.MomoEvent[randomSkill] = 1;
                assignedSkills.Add(randomSkill);
                MelonLogger.Msg("Skill " + skillNameReplaced + " has been replaced with skill " + skillNameCurrent);
            }
        }

        public void loadAcquiredSkills()
        {
            String acquiredSkills = "";
            String skillName = "";
            foreach (int skill in assignedSkills)
            {
                SetSkillName(skillName, skill);
                acquiredSkills += skillName + ", ";
                GameData.current.MomoEvent[skill] = 1;
            }
            MelonLogger.Msg("Acquired skills:" + acquiredSkills);
        }

        private void SetSkillName(String skillName, int skill)
        {
            switch (skill) {
                case 9:
                    skillName = "Sprint";
                    break;
                case 10:
                    skillName = "Double Jump";
                    break;
                case 20:
                    skillName = "Sacred Leaf";
                    break;
                case 194:
                    skillName = "Wall Jump";
                    break;
                case 131:
                    skillName = "Lunar Attunement";
                    break;
                default:
                    break;
            }
        }

        private void PrintHashSet(HashSet<int> set)
        {
            String setString = "";
            foreach (int value in set)
            {
                setString += value + ", ";
            }
        MelonLogger.Msg(setString);
        }
    }
}
