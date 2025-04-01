from BaseClasses import Item, ItemClassification
import typing

class ItemData(typing.NamedTuple):
    code: typing.Optional[int]
    classification: any

class MomodoraItem(Item):
    game: str = "MomodoraMoonlitFarewell"

item_table = {
    #Junk
    "100 Lunar Crystals": ItemData(999, ItemClassification.filler),
    #Skills
   "Awakened Sacred Leaf": ItemData(20, ItemClassification.progression),
   "Sacred Anemone": ItemData(9, ItemClassification.progression),
   "Crescent Moonflower": ItemData(10, ItemClassification.progression),
   "Spiral Shell": ItemData(194, ItemClassification.progression),
   "Lunar Attunement": ItemData(131, ItemClassification.progression),
   "Mitchi Fast Travel": ItemData(205, ItemClassification.useful),
   #Sigils
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
   "Welkin Leaf": ItemData(446, ItemClassification.useful),
   "Last Wish": ItemData(123, ItemClassification.useful),
   "Strongfist": ItemData(408, ItemClassification.useful),
   "Fallen Hero": ItemData(422, ItemClassification.useful),
   "The Profiteer": ItemData(401, ItemClassification.useful),
   "Oracle": ItemData(441, ItemClassification.useful),
   "Grimoire": ItemData(338, ItemClassification.useful),
   "Tattered Grimoire": ItemData(339, ItemClassification.useful),
   "Dusty Grimoire": ItemData(340, ItemClassification.useful),
   "Gold Moonlit Dust": ItemData(332, ItemClassification.progression),
   "Silver Moonlit Dust": ItemData(333, ItemClassification.progression),
#    "Wooden Box": ItemData(347, ItemClassification.progression),
   "Windmill Key": ItemData(356, ItemClassification.progression),
   # Selin Door
   "Progressive Final Boss Key": ItemData(991, ItemClassification.progression),
   # Damage Upgrade
   "Progressive Damage Upgrade": ItemData(992, ItemClassification.useful),
   #Health Upgrade
   "Progressive Health Upgrade": ItemData(993, ItemClassification.useful),
   #Stamina Upgrade
   "Progressive Stamina Upgrade": ItemData(994, ItemClassification.useful),
   #Magic Upgrade
   "Progressive Magic Upgrade": ItemData(995, ItemClassification.useful),
   #Fairy count for Oracle
   "Progressive Lumen Fairy": ItemData(996, ItemClassification.useful)
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

cereza_sigil_items = {

   "Last Wish": 1,
   "Strongfist": 1,
   "Fallen Hero": 1,
    "The Profiteer": 1
}

optional_sigil_items = {
    "Oracle": 1
}

grimoire_items = {
    "Grimoire": 1,
    "Tattered Grimoire": 1,
    "Dusty Grimoire": 1
}

key_items = {
    "Gold Moonlit Dust": 1,
    "Silver Moonlit Dust": 1,
    # "Wooden Box": 1
    "Windmill Key": 1
}

selin_door = {
    "Progressive Final Boss Key": 4
}

progressive_upgrade_table = {
    "progressive_damage": {
        "Progressive Damage Upgrade": 25
    },
    "progressive_health": {
        "Progressive Health Upgrade": 24
    },
    "progressive_stamina": {
        "Progressive Stamina Upgrade": 5
    },
    "progressive_magic": {
        "Progressive Magic Upgrade": 7
    },
    "progressive_fairy": {
        "Progressive Lumen Fairy": 30
    }
}