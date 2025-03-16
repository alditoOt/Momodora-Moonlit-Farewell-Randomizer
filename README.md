# THESE NOTES ARE NOT DONE YET, BUT A SMALL GUIDE I SUPPOSE

**IMPORTANT: BACKUP YOUR SAVE FILE. RECOMMENDED TO PLAY IN A NEW SAVE FILE**

**ALSO IMPORTANT: THE MOD ONLY WORKS IF YOU OPEN THE GAME AND START A NEW FILE. IF YOU CLOSE THE GAME THE MOD BREAKS. THAT'S DUMB I KNOW BUT THAT'S ALL I GOT FOR NOW. YOU CAN GO BACK TO THE MAIN MENU THOUGH (to take advantage of the auto save feature and loading back in a different place), BUT IF YOU DO THAT DON'T TRY TO OPEN ANOTHER SAVE FILE, JUST IN CASE**

Download the [MelonLoader Installer](https://github.com/LavaGang/MelonLoader.Installer/releases/latest/download/MelonLoader.Installer.exe) and install version 5.7 (or whichever is the latest 5 version) in the game's folder

Download MomodoraMFRandomizer.dll from the releases tab (still very much a WIP and some things might not even work properly) and move it into the Mods folder in the game files

**SINCE THIS IS A WIP THE MOD IS PRONE TO BREAK OR SOMETHING, I'M WORKING ON IT I PROMISE**

# Currently Working
- Skills are randomized between each other

# Not Working 
- **[FIXED]** If a skill is acquired somewhere else, it's location check its locked (since the logic to check the location and the logic to update the proper skills are in the same method, they override each other - easy fix)
- No logic implemented - possible softlocking if the acquired skills aren't useful to go to other areas

# TO-DO
- Modify code to allow a YAML file for mod configuration (Open World? Starting skills?)
- Implement logic (for skills first)
- Add Sigils to the randomization pool (maybe ignoring Cereza's shop and Oracle at first - implement logic later)
- Remove Healing Bell and add it to the randomizer pool

- **EVENTUALLY ADD THIS TO THE ARCHIPELAGO MULTIWORLD WOOOOO**

# POSSIBLE LOCATIONS
- Cereza friendship checks
- Lumen Fairies?
