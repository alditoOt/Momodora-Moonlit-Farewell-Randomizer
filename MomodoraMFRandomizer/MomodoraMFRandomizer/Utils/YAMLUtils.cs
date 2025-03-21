using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomodoraMFRandomizer
{
    class YAMLUtils
    {
        public static Boolean OPENSPRINGLEAFPATH;
        public static Boolean DEATHLINK;
        public static void GetSettingsFromYAML()
        {
            try
            {
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("open_springleaf_path", out object openSpringleafPath);
                APMomoMFRandomizer.session.DataStorage.GetSlotData().TryGetValue("deathlink", out object deathlink);
                OPENSPRINGLEAFPATH = (Boolean)openSpringleafPath;
                DEATHLINK = (Boolean)deathlink;
            }
            catch (Exception e)
            {
                MelonLogger.Msg($"error trying to get settings from YAML file: {e.Message}");
            }
        }
    }
}
