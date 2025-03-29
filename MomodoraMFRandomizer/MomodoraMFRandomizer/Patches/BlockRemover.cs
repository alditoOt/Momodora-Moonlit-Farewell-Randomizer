using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace MomodoraMFRandomizer
{
    class BlockRemover
    {

        public void removeAllBlockers(string sceneName)
        {
            RemoveStrings();
            RemoveWindZones(sceneName);
            //RemoveGynBarrier(sceneName);
        }

        public void RemoveWindZones(string sceneName)
        {
            if (sceneName == "Well29")
            {
                MelonLogger.Msg("We're in Well29");
                GameObject windZoneOne = GameObject.Find("Momo2020WindZone");
                GameObject windZoneTwo = GameObject.Find("Momo2020WindZone (1)");

                if (windZoneOne != null)
                {
                    windZoneTwo.SetActive(false);
                }
                if (windZoneTwo != null)
                {
                    windZoneOne.SetActive(false);
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
                        MelonLogger.Msg("Removing extra windzone");
                        extraWindZone.gameObject.SetActive(false);
                    }
                }
            }

            WindMomo2020[] windZones = GameObject.FindObjectsOfType<WindMomo2020>();
            if (windZones != null && windZones.Length > 0)
            {
                foreach (WindMomo2020 windZone in windZones)
                {
                    MelonLogger.Msg("Removing windzone");
                    windZone.gameObject.SetActive(false);
                }
            }
        }

        public void RemoveGynBarrier(string sceneName)
        {
            if (sceneName == "Bark21")
            {
                GameObject gynBarrier = GameObject.Find("GynBarrier");
                if (gynBarrier != null)
                {
                    gynBarrier.SetActive(false);
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
