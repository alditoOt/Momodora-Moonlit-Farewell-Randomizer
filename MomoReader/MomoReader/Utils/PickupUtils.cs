using HarmonyLib;
using MelonLoader;
using System;
using System.IO;
using System.Linq;

namespace MomoReader.Utils
{
    // Records companions, black berries and ceiling money holders into PickupData.csv when collected.
    // Lilies, hp/stamina/magic berries and lumen fairies are already covered elsewhere
    public static class PickupUtils
    {
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Data", "PickupData.csv");

        private static string[] ReadAllLinesShared()
        {
            using (StreamReader reader = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                return reader.ReadToEnd().Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public static void Record(string itemName, string itemId, int eventId)
        {
            try
            {
                string location = MomoReader.sceneName;
                string line = $"{itemName.Replace(',', ' ')},{itemId},{eventId},{location}";

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                bool fileExists = File.Exists(filePath);

                if (fileExists && ReadAllLinesShared().Contains(line))
                {
                    MelonLogger.Msg("Pickup already recorded. Skipping...");
                    return;
                }

                using (StreamWriter writer = new StreamWriter(new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)))
                {
                    if (!fileExists)
                    {
                        writer.WriteLine("Item Name,Item ID,Event ID,Location");
                    }
                    writer.WriteLine(line);
                }

                MelonLogger.Msg("Pickup added: " + line);
            }
            catch (IOException)
            {
                MelonLogger.Warning($"Couldn't write to {filePath}, it's probably open in another program (Excel?). Close it and collect the pickup again after reloading the room.");
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error recording pickup: {e}");
            }
        }
    }

    // Companions are ItemSparkle pickups with the Minion visual
    [HarmonyPatch(typeof(ItemSparkle), "OnTalk")]
    class CompanionPickupPatcher
    {
        [HarmonyPostfix]
        static void Postfix(ItemSparkle __instance)
        {
            if (__instance.visualProperty != ItemSparkle.Property.Minion)
            {
                return;
            }
            Item item = GameData.itemDatabase.GetItem(__instance.TreasureID);
            PickupUtils.Record(item.Name, item.itemDef.Index.ToString(), __instance.DestroyFlag);
        }
    }

    // Black berries are assumed to be the ItemFruit with the Lck (luck) property,
    // since the hp/mp/stamina/atk ones are the berries you already track
    [HarmonyPatch(typeof(ItemFruit), "ActivateEffect")]
    class BlackBerryPickupPatcher
    {
        [HarmonyPostfix]
        static void Postfix(ItemFruit __instance)
        {
            if (__instance.specialProperty != ItemFruit.Property.Lck)
            {
                return;
            }
            PickupUtils.Record("Black Berry", "", __instance.DestroyFlag);
        }
    }

    // The money holders hanging from the ceiling are MunnyRocks that aren't the large demon fruit (lumen fairy)
    [HarmonyPatch(typeof(MunnyRocks), "Break")]
    class MunnyHolderPatcher
    {
        [HarmonyPrefix]
        static void Prefix(MunnyRocks __instance, out bool __state)
        {
            __state = __instance.broken;
        }

        [HarmonyPostfix]
        static void Postfix(MunnyRocks __instance, bool __state)
        {
            if (__state || __instance.LargeDemonFruit)
            {
                return;
            }
            PickupUtils.Record("Munny Holder", "", __instance.DestroyFlag);
        }
    }
}
