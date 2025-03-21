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

class OracleSigil(Toggle):
    """Add the Oracle Sigil to the item pool (setting currently not implemented)"""
    display_name = "Add Oracle Sigil"
    default = 0

# class FastTravel(Choice):
#     """Whether to start with Fast Travel, add it to the randomization pool, or keep it vanilla (Unlocking Fast Travel is still a location check)"""
#     display_name = "Fast Travel Choice"
#     option_vanilla = 0
#     option_start_with = 1
#     option_add_to_item_pool = 2
#     default = 2

@dataclass
class MomodoraOptions(PerGameCommonOptions):
    open_springleaf_path: OpenSpringleafPath
    deathlink: DeathLink
    oracle_sigil: OracleSigil
    # fast_travel: FastTravel