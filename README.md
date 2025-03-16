# THESE NOTES ARE NOT DONE YET, BUT A SMALL GUIDE I SUPPOSE

Download the [MelonLoader Installer](https://github.com/LavaGang/MelonLoader.Installer/releases/latest/download/MelonLoader.Installer.exe) and install version 5.7 (or whichever is the latest 5 version) in the game's folder

Download MomodoraMFRandomizer.dll from the releases tab (still very much a WIP and some things might not even work properly) and move it into the Mods folder in the game files

# Currently Working
- Skills are randomized between each other

# Not Working 
- [FIXED] If a skill is acquired somewhere else, it's location check its locked (since the logic to check the location and the logic to update the proper skills are in the same method, they override each other - easy fix)
- No logic implemented - possible softlocking if the acquired skills aren't useful to go to other areas

# TO-DO
- Implement logic
- Modify code to allow a YAML file for mod configuration (Open World? Starting skills?)
- Remove Healing Bell and add it to the randomizer pool
