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
        private HashSet<int> previousSceneValues = new HashSet<int>();
        private Dictionary<int, int> previousSkillState = new Dictionary<int, int>()
        {
            {9, 0 },
            {10, 0},
            {20, 0},
            {194, 0},
            {131, 0}
        };
        public HashSet<int> assignedSkills = new HashSet<int>() { 205 };
        public HashSet<int> checkedSkills = new HashSet<int>();

        public bool twentyStarted = false;


        public void CheckMomoEventValue()
        {
            for (int i = 0; i < GameData.current.MomoEvent.Length; i ++)
            {
                if (GameData.current.MomoEvent[i] == 1 && !previousSceneValues.Contains(i))
                {
                    previousSceneValues.Add(i);
                    MelonLogger.Msg("Event value: " + i);
                }
            }
        }

        public void randomizeSkills()
        {
            foreach (int skill in skills)
            {
                if (GameData.current.MomoEvent[skill] == 1 && previousSkillState[skill] == 0)
                {
                    if (assignedSkills.Contains(skill))
                    {
                        continue;
                    }
                    if (skill == 10)
                    {
                        GameData.current.MomoEvent[10] = 1;
                        assignedSkills.Add(10);
                        continue;
                    }
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

        public void ReloadSceneCheck()
        {
            foreach (int skill in assignedSkills)
            {
                if (checkedSkills.Contains(skill))
                {
                    continue;
                }
                if (skill == 20 && SceneManager.GetActiveScene().name == "Well26")
                {
                    GameData.current.MomoEvent[20] = 0;
                }
                if (skill == 9 && SceneManager.GetActiveScene().name == "Well29")
                {
                    GameData.current.MomoEvent[9] = 0;
                }
                if (skill == 10 && SceneManager.GetActiveScene().name == "Bark42")
                {
                    GameData.current.MomoEvent[10] = 0;
                }
                if (skill == 194 && SceneManager.GetActiveScene().name == "Fairy10")
                {
                    GameData.current.MomoEvent[194] = 0;
                }
                if (skill == 131 && SceneManager.GetActiveScene().name == "Marsh08")
                {
                    GameData.current.MomoEvent[131] = 0;
                }
            }
        }

        public void PersistAcquiredSkills()
        {
            foreach (int skill in assignedSkills)
            {
                GameData.current.MomoEvent[skill] = 1;
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
    }
}
