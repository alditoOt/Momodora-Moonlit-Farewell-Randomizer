from Options import Choice, Toggle, Range, PerGameCommonOptions
from dataclasses import dataclass

class OpenSpringleafPath(Toggle):
    """Remove Demon Strings and Wind Barriers in Springleaf Path to enable entering other areas before getting Awakened Sacred Leaf/Sacred Anemone"""
    display_name = "Open Springleaf Path"
    default = 1

class DeathLink(Toggle):
    """Link all player deaths together"""
    display_name = "Deathlink"
    default = 1

@dataclass
class MomodoraOptions(PerGameCommonOptions):
    open_springleaf_path: OpenSpringleafPath
    deathlink: DeathLink