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
                if (GameData.current.MomoEvent[skill] == 1)
                {
                    LoadCheck();
                    if (skill == 20 && SceneManager.GetActiveScene().name == "Well26")
                    {
                        checkedSkills.Add(skill);
                        HandleAcquiredSkill(skill);
                        PrintAcquiredSkills();
                        PrintHashSet(checkedSkills);
                        PrintHashSet(assignedSkills);
                    }
                    if (skill == 9 && SceneManager.GetActiveScene().name == "Well29")
                    {
                        checkedSkills.Add(skill);
                        HandleAcquiredSkill(skill);
                        PrintAcquiredSkills();
                        PrintHashSet(checkedSkills);
                        PrintHashSet(assignedSkills);
                    }
                    if (skill == 10 && SceneManager.GetActiveScene().name == "Bark42")
                    {
                        checkedSkills.Add(skill);
                        HandleAcquiredSkill(skill);
                        PrintAcquiredSkills();
                        PrintHashSet(checkedSkills);
                        PrintHashSet(assignedSkills);
                    }
                    if (skill == 194 && SceneManager.GetActiveScene().name == "Fairy10")
                    {
                        checkedSkills.Add(skill);
                        HandleAcquiredSkill(skill);
                        PrintAcquiredSkills();
                        PrintHashSet(checkedSkills);
                        PrintHashSet(assignedSkills);
                    }
                    if (skill == 131 && SceneManager.GetActiveScene().name == "Marsh08")
                    {
                        checkedSkills.Add(skill);
                        HandleAcquiredSkill(skill);
                        PrintAcquiredSkills();
                        PrintHashSet(checkedSkills);
                        PrintHashSet(assignedSkills);
                    }
                }
            }   
        }
        private void HandleAcquiredSkill(int acquiredSkill)
        {
            if (!assignedSkills.Contains(acquiredSkill))
            {
                GameData.current.MomoEvent[acquiredSkill] = 0;
            }

            List<int> availableSkills = skills.Except(assignedSkills).ToList();
            
            if (availableSkills.Count > 0)
            {
                int randomSkill = availableSkills[UnityEngine.Random.Range(0, availableSkills.Count)];
                GameData.current.MomoEvent[randomSkill] = 1;
                assignedSkills.Add(randomSkill);
                MelonLogger.Msg("Skill " + acquiredSkill + " has been replaced with skill " + randomSkill);
            }
        }

        public void PrintAcquiredSkills()
        {
            String acquiredSkills = "";
            foreach (int skill in assignedSkills)
            {
                acquiredSkills += skill + ", ";
            }
            MelonLogger.Msg("Acquired skills:" + acquiredSkills);
        }

        public void LoadCheck()
        {
            if (SceneManager.GetActiveScene().name == "Well26" && assignedSkills.Contains(20) && !checkedSkills.Contains(20))
            {
                GameData.current.MomoEvent[20] = 0;
            }
            if (SceneManager.GetActiveScene().name == "Well29" && assignedSkills.Contains(9) && !checkedSkills.Contains(9))
            {
                GameData.current.MomoEvent[9] = 0;
            }
            if (SceneManager.GetActiveScene().name == "Bark42" && assignedSkills.Contains(10) && !checkedSkills.Contains(10))
            {
                GameData.current.MomoEvent[10] = 0;
            }
            if (SceneManager.GetActiveScene().name == "Fairy10" && assignedSkills.Contains(194) && !checkedSkills.Contains(194))
            {
                GameData.current.MomoEvent[194] = 0;
            }
            if (SceneManager.GetActiveScene().name == "Marsh08" && assignedSkills.Contains(131) && !checkedSkills.Contains(131))
            {
                GameData.current.MomoEvent[131] = 0;
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
