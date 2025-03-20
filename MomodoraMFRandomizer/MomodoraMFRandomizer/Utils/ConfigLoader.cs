using MelonLoader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
{
    public class ServerConfig
    {
        public string server { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool deathlink { get; set; }
        public bool openSpringleafPath { get; set; }
    
    }

    public static class ConfigLoader
    {

        private static string configPath = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "config.json");
        public static ServerConfig config { get; private set; } 

        public static void LoadConfig()
        {
            try
            {
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    config = JsonConvert.DeserializeObject<ServerConfig>(json);
                    MelonLogger.Msg("Config loaded successfully");
                }
                else
                {
                    MelonLogger.Error("Config file not found!");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Failed to load config: {ex.Message}");
                config = new ServerConfig();
            }
        }
    }
}
