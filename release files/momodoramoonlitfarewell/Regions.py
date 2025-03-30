from BaseClasses import MultiWorld

def link_momodora_areas(world: MultiWorld, player: int):
    for (exit, region) in mandatory_connections:
        world.get_entrance(exit, player).connect(world.get_region(region, player))

# (Region name, list of exits)
momodora_regions = [
    ("Menu", ["New Game"]),
    ("Springleaf Path", ["SP_SPC"]),
    ("Springleaf Path Continued", ["SPC_LTR", "SPC_FS"]),
    ("Koho Village", ["KV_SP", "KV_OS"]),
    ("Old Sanctuary", ["OS_OSC"]),
    ("Old Sanctuary Continued", []),
    ("Lun Tree Roots", ["LTR_DF", "LTR_FS", "LTR_MR"]),
    ("Demon Frontier", ["DF_AH", "DF_DFC"]),
    ("Demon Frontier Continued", ["DFC_MV"]),
    ("Fairy Springs", ["FS_FV"]),
    ("Fairy Village", []),
    ("Moonlight Repose", []),
    ("Ashen Hinterlands", ["AH_AHC"]),
    ("Ashen Hinterlands Continued", []),
    ("Meikan Village", ["MV_MVW"]),
    ("Meikan Village Windmill", ["MVW_FOR"]),
    ("Fount of Rebirth", ["FOR_SELIN"]),
    ("Selin", ["SELIN_DORA"]),
    ("Dora", [])
]

# (Entrance, region pointed to)
mandatory_connections = [
    ("New Game", "Koho Village"),
    ("SP_SPC", "Springleaf Path Continued"),
    ("SPC_LTR", "Lun Tree Roots"),
    ("SPC_FS", "Fairy Springs"),
    ("KV_SP", "Springleaf Path"),
    ("KV_OS", "Old Sanctuary"),
    ("OS_OSC", "Old Sanctuary Continued"),
    ("LTR_DF", "Demon Frontier"),
    ("LTR_FS", "Fairy Springs"),
    ("LTR_MR", "Moonlight Repose"),
    ("FS_FV", "Fairy Village"),
    ("DF_AH", "Ashen Hinterlands"),
    ("AH_AHC", "Ashen Hinterlands Continued"),
    ("DF_DFC", "Demon Frontier Continued"),
    ("DFC_MV", "Meikan Village"),
    ("MV_MVW", "Meikan Village Windmill"),
    ("MVW_FOR", "Fount of Rebirth"),
    ("FOR_SELIN", "Selin"),
    ("SELIN_DORA", "Dora")
    # ("FOR_DORA", "Dora")
]