using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MomodoraAPRandomizer.Patches
{
    [HarmonyPatch(typeof(SceneManager))]
    class SceneManagerPatch
    {
        //[HarmonyPatch("sceneLoaded")]
        //[HarmonyPostfix]
        static void RemoveSpringleafPathBlocks()
        {
            RemoveWindZones();
            RemoveStrings();
        }

        static void RemoveWindZones()
        {
            FlagDestroy[] extraWindZones = GameObject.FindObjectsOfType<FlagDestroy>();

            if (extraWindZones != null && extraWindZones.Length > 0)
            {
                foreach (FlagDestroy extraWindZone in extraWindZones)
                {
                    if (extraWindZone.gameObject.name.Contains("Extra Wind Barrier"))
                    {
                        extraWindZone.gameObject.SetActive(false);
                    }
                }
            }

            WindMomo2020[] windZones = GameObject.FindObjectsOfType<WindMomo2020>();
            if (windZones != null && windZones.Length > 0)
            {
                foreach (WindMomo2020 windZone in windZones)
                {
                    windZone.gameObject.SetActive(false);
                }
            }
        }

        static void RemoveStrings()
        {
            List<AlrauneThorn> demonStrings = new List<AlrauneThorn>();

            AlrauneThorn[] thorns = GameObject.FindObjectsOfType<AlrauneThorn>();

            if (thorns != null)
            {
                foreach (AlrauneThorn demonString in GameObject.FindObjectsOfType<AlrauneThorn>())
                {
                    if (demonString.gameObject.name.Contains("DemonStrings"))
                    {
                        demonStrings.Add(demonString);
                    }
                }
            }

            if (demonStrings != null && demonStrings.Count > 0)
            {
                foreach (AlrauneThorn demonString in demonStrings)
                {
                    if (demonString.GetComponent<DestroyableObject>() != null && demonString.GetComponent<DestroyableObject>().destroyable)
                    {
                        demonString.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
