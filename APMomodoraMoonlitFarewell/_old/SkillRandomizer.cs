using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using HarmonyLib;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Net;

namespace MomodoraMFRandomizer
{
    class SkillRandomizer
    {
        public List<int> skills = new List<int> { 9, 10, 20, 194, 131 };
        private Dictionary<int, int> previousSkillState = new Dictionary<int, int>();

        private bool doubleJump = true;
        private bool wallJump = false;

        public HashSet<int> assignedSkills = new HashSet<int>() { 205 };
        public HashSet<int> checkedSkills = new HashSet<int>();

        public void InitializeSkills()
        {
            previousSkillState.Add( 9, 0);
            previousSkillState.Add(10, 0);
            previousSkillState.Add(20, 0);
            previousSkillState.Add(194, 0);
            previousSkillState.Add(131, 0);
            GameData.current.MomoEvent[205] = 1;
        }

        public void randomizeSkills()
        {
            foreach (int skill in skills)
            {
                if (GameData.current.MomoEvent[skill] == 1 && previousSkillState[skill] == 0)
                {
                    HandleSkillRandomization(skill);
                    break;
                }
            }
        }

        public void HandleSkillRandomization(int checkedSkill)
        {
            if (!assignedSkills.Contains(checkedSkill))
            {
                GameData.current.MomoEvent[checkedSkill] = 0;
            }
            else
            {
                previousSkillState[checkedSkill] = 1;
            }
            if (checkedSkills.Contains(checkedSkill))
            {
                return;
            }
            checkedSkills.Add(checkedSkill);

            List<int> availableSkills = skills.Except(assignedSkills).ToList();

            if (availableSkills.Count > 0)
            {
                int newSkill = availableSkills[UnityEngine.Random.Range(0, availableSkills.Count)];
                assignedSkills.Add(newSkill);
                GameData.current.MomoEvent[newSkill] = 1;
                previousSkillState[newSkill] = 1;
                MelonLogger.Msg("Skill " + checkedSkill + " was replaced with " + newSkill);
            }
        }


        //Maybe reload the scene once the values have changed?
        public void ReloadSceneCheck(String sceneName)
        {
            foreach (int skill in assignedSkills)
            {
                if (checkedSkills.Contains(skill))
                {
                    continue;
                }
                if (skill == 20 && GameData.current.MomoEvent[20] == 1 && sceneName == "Well26")
                {
                    GameData.current.MomoEvent[20] = 0;
                    previousSkillState[20] = 0;
                }
                if (skill == 9 && GameData.current.MomoEvent[9] == 1 && sceneName == "Well29")
                {
                    GameData.current.MomoEvent[9] = 0;
                    previousSkillState[9] = 0;
                }
                if (skill == 10 && GameData.current.MomoEvent[10] == 1 && sceneName == "Bark42")
                {
                    GameData.current.MomoEvent[10] = 0;
                    previousSkillState[10] = 0;
                }
                if (skill == 194 && GameData.current.MomoEvent[194] == 1 && sceneName == "Fairy10")
                {
                    GameData.current.MomoEvent[194] = 0;
                    previousSkillState[194] = 0;
                }
                if (skill == 131 && GameData.current.MomoEvent[131] == 1 && sceneName == "Marsh08")
                {
                    GameData.current.MomoEvent[131] = 0;
                    previousSkillState[131] = 0;
                }
            }
        }

        public void PersistAcquiredSkills()
        {
            foreach (int skill in assignedSkills)
            {
                MelonLogger.Msg("Persisting skill " + skill);
                GameData.current.MomoEvent[skill] = 1;
            }
        }
    }
}
