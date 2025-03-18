using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using MomodoraMFRandomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace MomodoraMoonlitFarewellAP.Patches
{
    class APLocationHandler
    {
        List<int> skillEvents = new List<int> { 9, 10, 20, 194, 1313 };
        HashSet<int> receivedSkill = new HashSet<int>();
        HashSet<int> checkedLocation = new HashSet<int>();
        Dictionary<int, int> previousEventValue = new Dictionary<int, int>();
        Dictionary<int, string> skillAndScene = new Dictionary<int, string>()
        {
            { 20, "Well26" },
            {9, "Well29" },
            {10, "Bark42" },
            {194, "Fairy10" },
            {131, "Marsh08" }
        };

        public void InitializeDictionary()
        {
            foreach (int skill in skillEvents)
            {
                previousEventValue[skill] = GameData.current.MomoEvent[skill];
            }
        }

        public void ReportLocation(ArchipelagoSession session)
        {
            if (session == null) {
                return;
            }
            foreach(int locationId in skillEvents)
            {
                if (!checkedLocation.Contains(locationId) && previousEventValue[locationId] == 0 && GameData.current.MomoEvent[locationId] == 1)
                {
                    if (!receivedSkill.Contains(locationId))
                    {
                        GameData.current.MomoEvent[locationId] = 0;
                    }
                    else
                    {
                        GiveSkill(locationId);
                    }
                    checkedLocation.Add(locationId);
                    session.Locations.CompleteLocationChecks(locationId);
                    break;
                }
            }
        }

        private void GiveSkill(int skillId)
        {
            previousEventValue[skillId] = 1;
            GameData.current.MomoEvent[skillId] = 1;
            receivedSkill.Add(skillId);
        }

        public void UpdateItems(ArchipelagoSession session)
        {
            if (session == null)
            {
                return;
            }
            foreach (ItemInfo item in session.Items.AllItemsReceived)
            {
                session.Items.ItemReceived += (receivedItemsHelper) =>
                {
                    var itemReceivedName = receivedItemsHelper.PeekItem().ItemName ?? $"Item: {item.ItemId}";
                    GiveSkill((int)item.ItemId);
                    receivedItemsHelper.DequeueItem();
                };
            }
            
        }

        public void ResetLocationSceneForSkill(string sceneName)
        {
            foreach (int skillId in skillEvents)
            {
                if (!receivedSkill.Contains(skillId) || checkedLocation.Contains(skillId))
                {
                    return;
                }
                if (skillId == 1 && skillAndScene[skillId] == sceneName)
                {
                    GameData.current.MomoEvent[skillId] = 0;
                    previousEventValue[skillId] = 0;
                    //SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}
