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
   "Mitchi Fast Travel": ItemData(205, ItemClassification.useful),
   #Sigils (WIP)
   "Ascended Slash": ItemData(442, ItemClassification.useful),
   "Cloudy Blood": ItemData(400, ItemClassification.useful),
   "Companionship Pact": ItemData(436, ItemClassification.useful),
   "Dark Healer": ItemData(402, ItemClassification.useful),
   "Demilune Whisper": ItemData(403, ItemClassification.useful),
   "Glazed Aegis": ItemData(433, ItemClassification.useful),
   "Hare": ItemData(448, ItemClassification.useful),
   "Living Blood": ItemData(420, ItemClassification.useful),
   "Living Edge": ItemData(412, ItemClassification.useful),
   "Magic Blade": ItemData(404, ItemClassification.useful),
   "Mending Resonance": ItemData(434, ItemClassification.useful),
   "Mudwalker": ItemData(447, ItemClassification.useful),
   "Pawn": ItemData(440, ItemClassification.useful),
   "Perfect Chime": ItemData(405, ItemClassification.useful),
   "Phantasm Blade": ItemData(439, ItemClassification.useful),
   "Quintessence": ItemData(443, ItemClassification.useful),
   "Resolve": ItemData(444, ItemClassification.useful),
   "Resonance of Ifriya": ItemData(425, ItemClassification.useful),
   "Serval": ItemData(445, ItemClassification.useful),
   "The Arsonist": ItemData(430, ItemClassification.useful),
   "The Blessed": ItemData(435, ItemClassification.useful),
   "The Fortunate": ItemData(432, ItemClassification.useful),
   "The Hunter": ItemData(427, ItemClassification.useful),
   "The Sharpshooter": ItemData(438, ItemClassification.useful),
   "Trinary": ItemData(449, ItemClassification.useful),
   "Welkin Leaf": ItemData(446, ItemClassification.useful)
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
    "Ascended Slash": 1,
   "Cloudy Blood": 1,
   "Companionship Pact": 1,
   "Dark Healer": 1,
   "Demilune Whisper": 1,
   "Glazed Aegis": 1,
   "Hare": 1,
   "Living Blood": 1,
   "Living Edge": 1,
   "Magic Blade": 1,
   "Mending Resonance": 1,
   "Mudwalker": 1,
   "Pawn": 1,
   "Perfect Chime": 1,
   "Phantasm Blade": 1,
   "Quintessence": 1,
   "Resolve": 1,
   "Resonance of Ifriya": 1,
   "Serval": 1,
   "The Arsonist": 1,
   "The Blessed": 1,
   "The Fortunate": 1,
   "The Hunter": 1,
   "The Sharpshooter": 1,
   "Trinary": 1,
   "Welkin Leaf": 1
}