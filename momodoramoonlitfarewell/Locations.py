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
    "Harpy Archdemon": AdvData(17, "Springleaf Path"),
    "Raging Demon": AdvData(16, "Springleaf Path"),
    "Black Cat": AdvData(278, "Lun Tree Roots"),
    "Viper Archdemon Sorrellia": AdvData(150, "Fairy Springs"),
    "Remnant of an Unknown Phantasm": AdvData(171, "Moonlight Repose"),
    "Accursed Autarch": AdvData(105, "Demon Frontier"),
    "Tainted Serpent": AdvData(255, "Ashen Hinterlands")
}