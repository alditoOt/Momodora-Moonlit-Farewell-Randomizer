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
    set_rule(multiworld.get_entrance("SPC_FS", player), 
             lambda state: (state.has("Crescent Moonflower", player) or
                            state.has("Spiral Shell", player)) and
             (bool(world.options.open_springleaf_path.value) or 
             (state.has("Sacred Anemone", player))))
    set_rule(multiworld.get_entrance("LTR_FS", player), lambda state: state.has("Crescent Moonflower", player))
    set_rule(multiworld.get_entrance("KV_OS", player), 
             lambda state: state.has("Spiral Shell", player) or 
             (state.has("Crescent Moonflower", player) and
             (world.options.bell_hover_generation.value or
              state.has("Lunar Attunement", player))))
    set_rule(multiworld.get_entrance("OS_OSC", player), 
             lambda state: state.has("Spiral Shell", player) or
                            (world.options.bell_hover_generation.value and state.has("Lunar Attunement", player) and state.has("Crescent Moonflower", player)))
    set_rule(multiworld.get_entrance("LTR_DF", player), 
             lambda state: state.has("Spiral Shell", player) or
             (state.has("Crescent Moonflower", player) and world.options.bell_hover_generation.value))
    set_rule(multiworld.get_entrance("DF_AH", player),
             lambda state: (world.options.bell_hover_generation.value and state.has("Spiral Shell", player) and
                            (state.has("Sacred Anemone", player) or
                            state.has("Perfect Chime", player))) or
                            state.has("Crescent Moonflower", player)),
    set_rule(multiworld.get_entrance("LTR_MR", player), 
             lambda state: state.has("Spiral Shell", player) and
             (state.has("Awakened Sacred Leaf", player) or
              world.options.open_springleaf_path.value))
    set_rule(multiworld.get_entrance("AH_AHC", player), lambda state: state.has("Spiral Shell", player))
    set_rule(multiworld.get_entrance("DF_DFC", player), 
             lambda state: state.has("Crescent Moonflower", player) and
             (state.has("Lunar Attunement", player) or
              (state.has("Spiral Shell", player) and
               (state.has("Perfect Chime", player) or state.has("Sacred Anemone", player)))))
    set_rule(multiworld.get_entrance("DFC_MV", player), lambda state: state.has("Lunar Attunement", player)),
    set_rule(multiworld.get_entrance("MV_MVW", player), 
             lambda state: (state.has("Windmill Key", player) if world.options.randomize_key_items.value else True) and 
             (state.has("Crescent Moonflower", player) or
             (state.has("Spiral Shell", player) and
              world.options.bell_hover_generation.value))),
    set_rule(multiworld.get_entrance("MVW_FOR", player), lambda state: 
             state.has("Crescent Moonflower", player) and 
              (state.has("Windmill Key", player) if world.options.randomize_key_items.value else True)),
    set_rule(multiworld.get_entrance("FOR_SELIN", player), lambda state: state.has("Progressive Final Boss Key", player, 4) if world.options.final_boss_keys.value else True),
    set_rule(multiworld.get_location("Perfect Chime", player), 
             lambda state: state.has("Spiral Shell", player) and 
             (world.options.bell_hover_generation.value or 
              state.has("Crescent Moonflower", player)))
    set_rule(multiworld.get_location("Mending Resonance", player), lambda state: state.has("Lunar Attunement", player)),
    set_rule(multiworld.get_location("Resolve", player), lambda state: state.has("Lunar Attunement", player))
    set_rule(multiworld.get_location("Welkin Leaf", player), 
             lambda state: state.has("Crescent Moonflower", player) and
             state.has("Spiral Shell", player))
    if world.options.randomize_key_items:
        set_rule(multiworld.get_location("Gold Moonlit Dust", player), 
                 lambda state: state.has("Crescent Moonflower", player) or 
                 (state.has("Spiral Shell", player) and
                  (state.has("Sacred Anemone", player) or
                   state.has("Perfect Chime", player))))
    set_rule(multiworld.get_location("Lunar Attunement", player),
             lambda state:
             not world.options.randomize_key_items.value or
             (state.has("Gold Moonlit Dust", player) and
             state.has("Silver Moonlit Dust", player)))
    if world.options.oracle_sigil:
        set_rule(multiworld.get_location("Oracle", player), 
                 lambda state: state.can_reach("Fount of Rebirth", "Region", player))

def set_completion_rules(world: "MomodoraWorld"):
    player = world.player
    multiworld = world.multiworld
    multiworld.completion_condition[player] = lambda state: state.can_reach("Dora", "Region", player)
    