using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomoReader.Utils
{
    [HarmonyPatch(typeof(MomoEventData))]
    class MomoEventUtils
    {
        public static int index;
        private static string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Data", "LilyData.csv");

        [HarmonyPatch("set_Item")]
        [HarmonyPrefix]
        public static void GetEventIndex(int index, int value)
        {
            MomoEventUtils.index = index;
            if (MomoReader.phys_attack != Platformer3D.phys_attack)
            {
                UpdateItemData(index, MomoReader.sceneName);
            }
            MomoReader.phys_attack = Platformer3D.phys_attack;
        }


        public static void UpdateItemData(int eventId, string location)
        {
            string itemData = $"{eventId},{location}";

            // Ensure the file exists before checking
            if (File.Exists(filePath))
            {
                // Read all lines and check if the item already exists
                bool exists = File.ReadLines(filePath).Any(line => line.Contains($"{location}"));

                if (exists)
                {
                    MelonLogger.Msg("Item already exists. Skipping...");
                    return; // Exit without adding duplicate entry
                }
            }

            bool fileExists = File.Exists(filePath);

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Item Name,Item ID,Event ID,Location"); // Write headers if new file
                }
                writer.WriteLine(itemData);
            }

            MelonLogger.Msg("Item added: " + itemData);
        }
    }
}
