from BaseClasses import Location
import typing

class AdvData(typing.NamedTuple):
    id: typing.Optional[int]
    region: str

class MomodoraAdvancement(Location):
    game: str = "MomodoraMoonlitFarewell"
    
advancement_table = {
    #Skills
    "Awakened Sacred Leaf": AdvData(20, "Springleaf Path"),
    "Sacred Anemone": AdvData(9, "Springleaf Path"),
    "Crescent Moonflower": AdvData(10, "Lun Tree Roots"),
    "Spiral Shell": AdvData(194, "Fairy Village"),
    "Lunar Attunement": AdvData(131, "Ashen Hinterlands Continued"),
    #Bosses
    "Gariser Demon": AdvData(15, "Springleaf Path"),
    "Harpy Archdemon": AdvData(17, "Springleaf Path Continued"),
    "Raging Demon": AdvData(16, "Springleaf Path Continued"),
    "Black Cat": AdvData(278, "Lun Tree Roots"),
    "Viper Archdemon Sorrellia": AdvData(150, "Fairy Springs"),
    "Remnant of an Unknown Phantasm": AdvData(171, "Moonlight Repose"),
    "Accursed Autarch": AdvData(105, "Demon Frontier"),
    "Tainted Serpent": AdvData(255, "Ashen Hinterlands Continued"),
    "Very Big Spider": AdvData(114, "Fairy Springs"),
    "Bloodthirsty Siblings": AdvData(188, "Demon Frontier Continued"),
    "Moon Goddess Lineth": AdvData(213, "Meikan Village"),
    "Selin's Fear": AdvData(259, "Fount of Rebirth"),
    "Selin's Envy": AdvData(261, "Fount of Rebirth"),
    "Selin's Mendacity": AdvData(260, "Fount of Rebirth"),
    "Selin's Sorrow": AdvData(262, "Fount of Rebirth"),
    "Moon God Selin": AdvData(364, "Selin"),
    #Extra
    "Mitchi Fast Travel": AdvData(205, "Demon Frontier"),
    #Sigils
    "Ascended Slash": AdvData(442, "Fount of Rebirth"),
    "Cloudy Blood": AdvData(400, "Springleaf Path"),
    "Companionship Pact": AdvData(436, "Lun Tree Roots"),
    "Dark Healer": AdvData(402, "Demon Frontier Continued"),
    "Demilune Whisper": AdvData(403, "Fount of Rebirth"),
    "Glazed Aegis": AdvData(433, "Fairy Springs"),
    "Hare": AdvData(448, "Ashen Hinterlands Continued"),
    "Living Blood": AdvData(420, "Ashen Hinterlands Continued"),
    "Living Edge": AdvData(412, "Springleaf Path"),
    "Magic Blade": AdvData(404, "Demon Frontier Continued"),
    "Mending Resonance": AdvData(434, "Demon Frontier Continued"),
    "Mudwalker": AdvData(447, "Ashen Hinterlands Continued"),
    "Pawn": AdvData(440, "Springleaf Path Continued"),
    "Perfect Chime": AdvData(405, "Meikan Village"),
    "Phantasm Blade": AdvData(439, "Moonlight Repose"),
    "Quintessence": AdvData(443, "Meikan Village"),
    "Resolve": AdvData(444, "Old Sanctuary Continued"),
    "Resonance of Ifriya": AdvData(425, "Fairy Springs"),
    "Serval": AdvData(445, "Ashen Hinterlands"),
    "The Arsonist": AdvData(430, "Lun Tree Roots"),
    "The Blessed": AdvData(435, "Fairy Springs"),
    "The Fortunate": AdvData(432, "Lun Tree Roots"),
    "The Hunter": AdvData(427, "Koho Village"),
    "The Sharpshooter": AdvData(438, "Old Sanctuary"),
    "Trinary": AdvData(449, "Meikan Village Windmill"),
    "Welkin Leaf": AdvData(446, "Koho Village"),
    "The Fool": AdvData(419, "Springleaf Path"),
   "Last Wish": AdvData(123, "Springleaf Path"),
   "Strongfist": AdvData(408, "Springleaf Path"),
   "Fallen Hero": AdvData(422, "Springleaf Path"),
   "The Profiteer": AdvData(401, "Springleaf Path"),
   "Chrysanth": AdvData(431, "Springleaf Path"),
   "Queen of Dusk": AdvData(437, "Springleaf Path"),
   "Queen of Light": AdvData(406, "Springleaf Path"),
    "The Collector": AdvData(426, "Springleaf Path"),
   "Oracle": AdvData(441, "Fairy Village"),
    #Grimoires
   "Grimoire": AdvData(338, "Fairy Springs"),
   "Tattered Grimoire": AdvData(339, "Meikan Village Windmill"),
   "Dusty Grimoire": AdvData(340, "Fount of Rebirth"),
   #Key Items
   "Gold Moonlit Dust": AdvData(332, "Old Sanctuary Continued"),
   "Silver Moonlit Dust": AdvData(333, "Moonlight Repose"),
   "Wooden Box": AdvData(347, "Meikan Village"),
   "Windmill Key": AdvData(356, "Meikan Village"),
    #Heavenly Lilies
   "Heavenly Lily - Koho Village": AdvData(26400, "Koho Village"),
   "Heavenly Lily 1 - Old Sanctuary": AdvData(8100, "Old Sanctuary"),
   "Heavenly Lily 2 - Old Sanctuary": AdvData(12900, "Old Sanctuary"),
   "Heavenly Lily 1 - Springleaf Path": AdvData(2800, "Springleaf Path"),
   "Heavenly Lily 2 - Springleaf Path": AdvData(8400, "Springleaf Path"),
   "Heavenly Lily 1 - Lun Tree Roots": AdvData(11800, "Lun Tree Roots"),
   "Heavenly Lily 2 - Lun Tree Roots": AdvData(9400, "Lun Tree Roots"),
   "Heavenly Lily 1 - Moonlight Repose": AdvData(34400, "Moonlight Repose"),
   "Heavenly Lily 2 - Moonlight Repose": AdvData(17200, "Moonlight Repose"),
   "Heavenly Lily 1 - Fairy Springs": AdvData(3800, "Fairy Springs"),
   "Heavenly Lily 2 - Fairy Springs": AdvData(34300, "Fairy Springs"),
   "Heavenly Lily - Fairy Village": AdvData(2300, "Fairy Village"),
   "Heavenly Lily 1 - Demon Frontier": AdvData(16900, "Demon Frontier"),
   "Heavenly Lily 2 - Demon Frontier": AdvData(15300, "Demon Frontier Continued"),
   "Heavenly Lily 3 - Demon Frontier": AdvData(16600, "Demon Frontier Continued"),
   "Heavenly Lily 1 - Ashen Hinterlands": AdvData(13000, "Ashen Hinterlands"),
   "Heavenly Lily 2 - Ashen Hinterlands": AdvData(30200, "Ashen Hinterlands Continued"),
   "Heavenly Lily 3 - Ashen Hinterlands": AdvData(24700, "Ashen Hinterlands Continued"),
   "Heavenly Lily 1 - Meikan Village": AdvData(33400, "Meikan Village"),
   "Heavenly Lily 2 - Meikan Village": AdvData(33600, "Meikan Village"),
   "Heavenly Lily 3 - Meikan Village": AdvData(33300, "Fount of Rebirth"), #Logic says it's in FOR, but in game the area is still MV
   "Heavenly Lily 1 - Fount of Rebirth": AdvData(28600, "Fount of Rebirth"),
   "Heavenly Lily 2 - Fount of Rebirth": AdvData(28500, "Fount of Rebirth"),
   "Heavenly Lily 3 - Fount of Rebirth": AdvData(32300, "Fount of Rebirth"),
   "Heavenly Lily 4 - Fount of Rebirth": AdvData(32200, "Fount of Rebirth")
}

exclusion_table = {
    "random_key_items": {
        "Gold Moonlit Dust",
        "Silver Moonlit Dust",
        "Windmill Key"
    },
    "oracle_sigil": {
        "Oracle"
    },
    "progressive_damage": {
        "Heavenly Lily - Koho Village",
        "Heavenly Lily 1 - Old Sanctuary",
        "Heavenly Lily 2 - Old Sanctuary",
        "Heavenly Lily 1 - Springleaf Path",
        "Heavenly Lily 2 - Springleaf Path",
        "Heavenly Lily 1 - Lun Tree Roots",
        "Heavenly Lily 2 - Lun Tree Roots",
        "Heavenly Lily 1 - Moonlight Repose",
        "Heavenly Lily 2 - Moonlight Repose",
        "Heavenly Lily 1 - Fairy Springs",
        "Heavenly Lily 2 - Fairy Springs",
        "Heavenly Lily - Fairy Village",
        "Heavenly Lily 1 - Demon Frontier",
        "Heavenly Lily 2 - Demon Frontier",
        "Heavenly Lily 3 - Demon Frontier",
        "Heavenly Lily 1 - Ashen Hinterlands",
        "Heavenly Lily 2 - Ashen Hinterlands",
        "Heavenly Lily 3 - Ashen Hinterlands",
        "Heavenly Lily 1 - Meikan Village",
        "Heavenly Lily 2 - Meikan Village",
        "Heavenly Lily 3 - Meikan Village",
        "Heavenly Lily 1 - Fount of Rebirth",
        "Heavenly Lily 2 - Fount of Rebirth",
        "Heavenly Lily 3 - Fount of Rebirth",
        "Heavenly Lily 4 - Fount of Rebirth"
    },
    "progressive_health": {

    },
    "progressive_magic": {

    },
    "progressive_stamina": {

    }
}