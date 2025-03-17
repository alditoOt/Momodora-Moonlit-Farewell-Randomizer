from worlds.generic.Rules import set_rule, add_rule
from BaseClasses import CollectionState
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from . import MomodoraWorld

def set_rules(world: "MomodoraWorld"):
    player = world.player
    multiworld = world.multiworld

def set_completion_rules(world: "MomodoraWorld"):
    player = world.player
    multiworld = world.multiworld
    multiworld.completion_condition[player] = lambda state: state.can_reach("Dora", "Region", player)