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
    [HarmonyPatch(typeof(Inventory))]
    class InventoryUtils
    {

        private static string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Data", "SigilData.csv");

        public static void UpdateItemData(string itemName, int itemId, int eventId)
        {
            string itemData = $"{itemName},{itemId},{eventId}";

            // Ensure the file exists before checking
            if (File.Exists(filePath))
            {
                // Read all lines and check if the item already exists
                bool exists = File.ReadLines(filePath).Any(line => line.StartsWith($"{itemName},{itemId}"));

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

        [HarmonyPatch("Add")]
        [HarmonyPrefix]
        private static void PrintSigilInfo(Item item)
        {
            if (!item.itemDef.IsSigil)
            {
                return;
            }
            try
            {
                UpdateItemData(item.Name, item.itemDef.Index, MomoEventUtils.index);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error updating data: {e}");
            }
        }
    }
}
