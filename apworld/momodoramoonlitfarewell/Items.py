from BaseClasses import Item, ItemClassification
import typing

class ItemData(typing.NamedTuple):
    code: typing.Optional[int]
    classification: any

class MomodoraItem(Item):
    game: str = "MomodoraMoonlitFarewell"

item_table = {
    #Junk
    "Blessing of Nothing": ItemData(1, ItemClassification.filler),
    #Skills
   "Awakened Sacred Leaf": ItemData(20, ItemClassification.progression),
   "Sacred Anemone": ItemData(9, ItemClassification.progression),
   "Crescent Moonflower": ItemData(10, ItemClassification.progression),
   "Spiral Shell": ItemData(194, ItemClassification.progression),
   "Lunar Attunement": ItemData(131, ItemClassification.progression),
   "Mitchi Fast Travel": ItemData(205, ItemClassification.useful)
   #Sigils (WIP)
}

skill_items = {
    "Awakened Sacred Leaf": 1,
    "Sacred Anemone": 1,
    "Crescent Moonflower": 1,
    "Spiral Shell": 1,
    "Lunar Attunement": 1
}

extra_skill_items = {
    "Mitchi Fast Travel": 1
}

sigil_items = {
    
}