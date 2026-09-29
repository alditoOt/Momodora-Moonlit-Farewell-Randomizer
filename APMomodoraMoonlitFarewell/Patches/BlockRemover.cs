using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace APMomodoraMoonlitFarewell.Patches
{
    class BlockRemover
    {

        private static readonly string[] SPRINGLEAF_PATH_STRINGS = { "Well05", "Well12", "Well26", "Well20"};
        private static readonly string[] GYNBARRIER_SCENES = { "Bark09", "Bark08", "Bark16", "Bark21" };
        public void removeAllBlockers(string sceneName)
        {
            if (SPRINGLEAF_PATH_STRINGS.Contains(sceneName))
            {
                RemoveStrings();
            }
            RemoveWindZones(sceneName);
        }

        public void RemoveWindZones(string sceneName)
        {
            if (sceneName == "Well29")
            {
                GameObject windZoneOne = GameObject.Find("Momo2020WindZone");
                GameObject windZoneTwo = GameObject.Find("Momo2020WindZone (1)");

                if (windZoneOne != null)
                {
                    windZoneOne.SetActive(false);
                }
                if (windZoneTwo != null)
                {
                    windZoneTwo.SetActive(false);
                }
                return;
            }

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

        public void RemoveGynBarrier(string sceneName)
        {
            if (GYNBARRIER_SCENES.Contains(sceneName))
            {
                GameObject gynBarrier = GameObject.Find("GynBarrier");
                GameObject gynBarrier1 = GameObject.Find("GynBarrier (1)");
                GameObject gynBarrier2 = GameObject.Find("GynBarrier (2)");
                if (gynBarrier != null)
                {
                    gynBarrier.SetActive(false);
                }
                if (gynBarrier1 != null)
                {
                    gynBarrier1.SetActive(false);
                }
                if (gynBarrier2 != null)
                {
                    gynBarrier2.SetActive(false);
                }
            }
        }

        public void RemoveStrings()
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
