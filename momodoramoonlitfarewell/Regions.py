from BaseClasses import MultiWorld

def link_momodora_areas(world: MultiWorld, player: int):
    for (exit, region) in mandatory_connections:
        world.get_entrance(exit, player).connect(world.get_region(region, player))

# (Region name, list of exits)
momodora_regions = [
    ("Menu", ["New Game"]),
    ("Springleaf Path", ["SP_LTR", "SP_FS"]),
    ("Koho Village", ["KV_SP", "KV_OS"]),
    ("Old Sanctuary", []),
    ("Lun Tree Roots", ["LTR_DF", "LTR_FS", "LTR_MR"]),
    ("Demon Frontier", ["DF_AH", "DF_MV"]),
    ("Fairy Springs", ["FS_FV"]),
    ("Fairy Village", []),
    ("Moonlight Repose", []),
    ("Ashen Hinterlands", []),
    ("Meikan Village", ["MV_FOR"]),
    ("Fount of Rebirth", ["FOR_DORA"]),
    ("Dora", [])
]

# (Entrance, region pointed to)
mandatory_connections = [
    ("New Game", "Springleaf Path"),
    ("SP_LTR", "Lun Tree Roots"),
    ("SP_FS", "Fairy Springs"),
    ("KV_SP", "Springleaf Path"),
    ("KV_OS", "Old Sanctuary"),
    ("LTR_DF", "Demon Frontier"),
    ("LTR_FS", "Fairy Springs"),
    ("LTR_MR", "Moonlight Repose"),
    ("FS_FV", "Fairy Village"),
    ("DF_AH", "Ashen Hinterlands"),
    ("DF_MV", "Meikan Village"),
    ("MV_FOR", "Fount of Rebirth"),
    ("FOR_DORA", "Dora")
]