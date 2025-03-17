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
    "Lunar Attunement": AdvData(131, "Ashen Hinterlands")
}