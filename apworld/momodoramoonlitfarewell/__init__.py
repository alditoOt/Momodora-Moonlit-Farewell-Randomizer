from .Items import MomodoraItem, item_table, skill_items, extra_skill_items, sigil_items
from .Locations import MomodoraAdvancement, advancement_table
from .Regions import momodora_regions, link_momodora_areas
from worlds.generic.Rules import exclusion_rules
from BaseClasses import Region, Entrance, Tutorial, Item
from .Options import MomodoraOptions
from .Rules import set_rules, set_completion_rules
from worlds.AutoWorld import World, WebWorld
from multiprocessing import Process

class MomodoraWorld(World):
    """
    Momodora Moonlit Farewell is a game
    """
    game = "Momodora Moonlit Farewell"
    options_dataclass = MomodoraOptions
    options: MomodoraOptions

    item_name_to_id = {name: data.code for name, data in item_table.items()}
    location_name_to_id = {name: data.id for name, data in advancement_table.items()}

    def _get_momodora_data(self):
        return {
            "world_seed": self.random.getrandbits(32),
            "seed_name": self.multiworld.seed_name,
            "player_name": self.multiworld.get_player_name(self.player),
            "player_id": self.player,
            "client_version": self.required_client_version,
            "race": self.multiworld.is_race,
            "open_springleaf_path": bool(self.options.open_springleaf_path.value)
        }
    
    def get_filler_item_name(self):
        return "Blessing of Nothing"
    
    def create_items(self):
        # Generate item pool
        itempool = []
        # Add all required progression items
        for name, num in skill_items.items():
            itempool += [name] * num
        #Add useful skill items
        for name, num in extra_skill_items.items():
            itempool += [name] * num
        #Add all sigil items
        for name, num in sigil_items.items():
            itempool += [name] * num
        
        exclusion_pool = set()
        exclusion_checks = set()
        exclusion_rules(self.multiworld, self.player, exclusion_checks)

        # Convert itempool into real items
        itempool = [item for item in map(lambda name: self.create_item(name), itempool)]
        # Fill remaining items with randomly generated junk
        while len(itempool) < len(self.multiworld.get_unfilled_locations(self.player)):
            itempool.append(self.create_filler())

        self.multiworld.itempool += itempool

    def set_rules(self):
        set_rules(self)
        set_completion_rules(self)

    def create_regions(self):
        def MomodoraRegion(region_name: str, exits=[]):
            ret = Region(region_name, self.player, self.multiworld)
            ret.locations += [MomodoraAdvancement(self.player, loc_name, loc_data.id, ret)
                              for loc_name, loc_data in advancement_table.items()
                                if loc_data.region == region_name
                              ]
            for exit in exits:
                ret.exits.append(Entrance(self.player, exit, ret))
            return ret
        
        self.multiworld.regions += [MomodoraRegion(*r) for r in momodora_regions]
        link_momodora_areas(self.multiworld, self.player)

    def fill_slot_data(self):
        return self._get_momodora_data()
    
    def create_item(self, name: str) -> Item:
        item_data = item_table[name]
        item = MomodoraItem(name, item_data.classification, item_data.code, self.player)
        return item