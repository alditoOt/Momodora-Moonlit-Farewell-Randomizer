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
    "Lunar Attunement": AdvData(131, "Ashen Hinterlands"),
    #Bosses
    "Gariser Demon": AdvData(15, "Springleaf Path"),
    "Harpy Archdemon": AdvData(17, "Springleaf Path"),
    "Raging Demon": AdvData(16, "Springleaf Path"),
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
    "Moon God Selin": AdvData(364, "Fount of Rebirth"),
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
    "Mudwalker": AdvData(447, "Ashen Hinterlands"),
    "Pawn": AdvData(440, "Springleaf Path Continued"),
    "Perfect Chime": AdvData(405, "Meikan Village"),
    "Phantasm Blade": AdvData(439, "Moonlight Repose"),
    "Quintessence": AdvData(443, "Meikan Village"),
    "Resolve": AdvData(444, "Old Sanctuary"),
    "Resonance of Ifriya": AdvData(425, "Fairy Springs"),
    "Serval": AdvData(445, "Ashen Hinterlands"),
    "The Arsonist": AdvData(430, "Lun Tree Roots"),
    "The Blessed": AdvData(435, "Fairy Springs"),
    "The Fortunate": AdvData(432, "Lun Tree Roots"),
    "The Hunter": AdvData(427, "Koho Village"),
    "The Sharpshooter": AdvData(438, "Old Sanctuary"),
    "Trinary": AdvData(449, "Meikan Village"),
    "Welkin Leaf": AdvData(446, "Koho Village"),
    "The Fool": AdvData(419, "Springleaf Path"),
   "Last Wish": AdvData(123, "Cereza"),
   "Strongfist": AdvData(408, "Cereza"),
   "Fallen Hero": AdvData(422, "Cereza"),
   "The Profiteer": AdvData(401, "Cereza")
#    "Oracle": AdvData(441, "Fairy Village")
    #Grimoires
}