from worlds.generic.Rules import set_rule, add_rule
from BaseClasses import CollectionState
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from . import MomodoraWorld

def set_rules(world: "MomodoraWorld"):
    player = world.player
    multiworld = world.multiworld
    set_rule(multiworld.get_entrance("SP_SPC", player), 
             lambda state: state.has("Awakened Sacred Leaf", player) or
             bool(world.options.open_springleaf_path.value))
    set_rule(multiworld.get_entrance("SPC_LTR", player), 
             lambda state: state.has("Sacred Anemone", player) or
             bool(world.options.open_springleaf_path.value))
    set_rule(multiworld.get_entrance("SPC_FS", player), lambda state: state.has("Sacred Anemone", player))
    set_rule(multiworld.get_entrance("LTR_FS", player), 
             lambda state: state.has("Crescent Moonflower", player) or 
             state.has("Spiral Shell", player))
    set_rule(multiworld.get_entrance("KV_OS", player), lambda state: state.has("Spiral Shell", player))
    set_rule(multiworld.get_entrance("LTR_DF", player), 
             lambda state: state.has("Spiral Shell", player) or
             state.has("Crescent Moonflower", player))
    set_rule(multiworld.get_entrance("LTR_MR", player), lambda state: state.has("Spiral Shell", player))
    set_rule(multiworld.get_entrance("AH_AHC", player), lambda state: state.has("Spiral Shell", player))

def set_completion_rules(world: "MomodoraWorld"):
    player = world.player
    multiworld = world.multiworld
    multiworld.completion_condition[player] = lambda state: state.can_reach("Dora", "Region", player)
    