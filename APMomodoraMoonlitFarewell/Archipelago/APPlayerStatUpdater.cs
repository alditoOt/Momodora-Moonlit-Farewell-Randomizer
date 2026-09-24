using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    // Applies the player-stat side effects (attack/health/magic/stamina/fairy not a stat but here either way) of received Archipelago items
    static class APPlayerStatUpdater
    {
        // Base game stats + per-pickup increment
        public const int baseAttack = 5;
        public const int attackPerLily = 2;
        public const int baseMaxHealth = 300;
        public const int healthPerBerry = 50;
        public const int baseMaxMagic = 30;
        public const int magicPerUpgrade = 10;

        public static void UpdatePlayerDamage(int lilyCount)
        {
            Platformer3D.phys_attack = baseAttack + attackPerLily * lilyCount;
            GameData.current.MomoEvent[MomoEventUtils.LILY_COUNTER_EVENT] = lilyCount;
        }

        public static void UpdatePlayerHealth(int healthCount)
        {
            float prevHP = Platformer3D.player_maxhp;
            Platformer3D.player_maxhp = baseMaxHealth + healthPerBerry * healthCount;
            Platformer3D.player_hp += Platformer3D.player_maxhp > prevHP ? healthPerBerry : 0;
            GameData.current.MomoEvent[MomoEventUtils.HEALTH_COUNTER_EVENT] = healthCount;
        }

        public static void UpdatePlayerMagic(int magicCount)
        {
            float prevMagic = Platformer3D.player_maxsp;
            Platformer3D.player_maxsp = baseMaxMagic + magicPerUpgrade * magicCount;
            Platformer3D.player_sp += Platformer3D.player_maxsp > prevMagic ? magicPerUpgrade : 0;
            GameData.current.MomoEvent[MomoEventUtils.MAGIC_COUNTER_EVENT] = magicCount;
        }

        public static void UpdateFairies(int fairyCount)
        {
            GameData.current.MomoEvent[MomoEventUtils.FAIRY_COUNTER_EVENT] = fairyCount;
        }

        public static void UpdatePlayerStamina(int staminaCount)
        {
            GameData.current.MomoEvent[MomoEventUtils.STAMINA_COUNTER_EVENT_ONE] = staminaCount;
            GameData.current.MomoEvent[MomoEventUtils.STAMINA_COUNTER_EVENT_TWO] = staminaCount;
        }
    }
}
