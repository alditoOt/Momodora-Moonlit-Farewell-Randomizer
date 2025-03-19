from BaseClasses import Location
import typing

class AdvData(typing.NamedTuple):
    id: typing.Optional[int]
    region: str

class MomodoraAdvancement(Location):
    game: str = "MomodoraMoonlitFarewell"
    
advancement_table = {
    "Awakened Sacred Leaf": AdvData(20, "Springleaf Path"),
    "Sacred Anemone": AdvData(9, "Springleaf Path"),
    "Crescent Moonflower": AdvData(10, "Lun Tree Roots"),
    "Spiral Shell": AdvData(194, "Fairy Village"),
    "Lunar Attunement": AdvData(131, "Ashen Hinterlands"),
    "Gariser Demon": AdvData(15, "Springleaf Path"),
    "Harpy Archdemon": AdvData(17, "Springleaf Path"),
    "Raging Demon": AdvData(16, "Springleaf Path"),
    "Black Cat": AdvData(278, "Lun Tree Roots"),
    "Viper Archdemon Sorrellia": AdvData(150, "Fairy Springs"),
    "Remnant of an Unknown Phantasm": AdvData(171, "Moonlight Repose"),
    "Accursed Autarch": AdvData(105, "Demon Frontier"),
    "Tainted Serpent": AdvData(255, "Ashen Hinterlands Continued"),
    "Very Big Spider": AdvData(114, "Fairy Springs"),
    "Bloodthirsty Siblings": AdvData(188, "Demon Frontier"),
    "Moon Goddess Lineth": AdvData(213, "Meikan Village"),
    "Selin's Fear": AdvData(259, "Fount of Rebirth"),
    "Selin's Envy": AdvData(261, "Fount of Rebirth"),
    "Selin's Mendacity": AdvData(260, "Fount of Rebirth"),
    "Selin's Sorrow": AdvData(262, "Fount of Rebirth"),
    "Moon God Selin": AdvData(364, "Fount of Rebirth"),
    "Mitchi Fast Travel": AdvData(205, "Demon Frontier")
}