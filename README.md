# Currently Working
- Skills are randomized between each other

# Not Working 
- [FIXED] If a skill is acquired somewhere else, it's location check its locked (since the logic to check the location and the logic to update the proper skills are in the same method, they override each other - easy fix)
- No logic implemented - possible softlocking if the acquired skills aren't useful to go to other areas

# TO-DO
- Implement logic
- Modify code to allow a YAML file for mod configuration (Open World? Starting skills?)
- Remove Healing Bell and add it to the randomizer pool
