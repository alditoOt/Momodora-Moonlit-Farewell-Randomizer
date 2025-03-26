from Options import Choice, Toggle, Range, PerGameCommonOptions
from dataclasses import dataclass

class OpenSpringleafPath(Toggle):
    """Remove Demon Strings and Wind Barriers in Springleaf Path to enable entering other areas before getting Awakened Sacred Leaf/Sacred Anemone."""
    display_name = "Open Springleaf Path"
    default = 1

class DeathLink(Toggle):
    """Link all player deaths together. If you die, all other players in the session (with deathlink enabled) also die. The same works the other way around."""
    display_name = "Deathlink"
    default = 1

class BellHover(Toggle):
    """Consider Bell Hover as a possible strat for world generation. Bell Hover is used to access certain areas without having the correct skill (i.e. Demon Frontier without Wall Jump)"""
    display_name = "Bell Hover Generation"
    default: 0

class RandomizeKeyItems(Toggle):
    """Add Key Items needed for progression to the randomization pool. These items are: Gold Moonlit Dust, Silver Moonlit Dust, Wooden Box and Windmill Key"""
    display_name = "Randomize Key items"
    default: 0

class OracleSigil(Toggle):
    """Add the Oracle Sigil to the item pool"""
    display_name = "Add Oracle Sigil"
    default = 1

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
    bell_hover_generation: BellHover
    randomize_key_items: RandomizeKeyItems
    oracle_sigil: OracleSigil
    # fast_travel: FastTravel