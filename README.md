# WORK IN PROGRESS
Currently here's what's happening with the mod:
- I can connect to an archipelago or locally hosted server
- I can send checks whenever I check a skill or defeat a boss
- I can receive a random skill when checked in any game in the multiworld

# MISSING TO BE CONSIDERED PLAYABLE 
- Adding Sigils to the item randomization and location checks
  - I was able to modify the Inventory manually, so I'm now looking into getting the item ID for each Sigil to add them to the randomizer (The Fool Sigil is an exceptional case that might be removed from the pool until I figure out a solution, but 35/36 is pretty good)
  - Cereza Sigils are unlocked later - not entirely sure when, I can see of making them unlocked from the start, depending on this I'll see of adding them to the pool. Apart from the 4 that are available at first, that would move us to 31/36 Sigils.
  - Oracle Sigil is probably the last Sigil you'd usually unlock, since it requires all Lumen Fairies. Will probably remove it from the pool to then add it in the future (30/36 Sigils it is)
- I have some small settings on the YAML. This helps for generating the randomized world, but currently I'm adding the same setting to a config.json file that goes with the mod. If I want to expand the options in the YAML I need to figure out how to get the mod to receive information from the YAML from the multiworld session, so that you don't have to verify that your config.json and your YAML file match in their settings (currently small thing since there are only **two** extra settings for now, but this will be useful for the next point)

# DUMB SMALL ISSUE THAT I WILL (HOPEFULLY) FIX LATER
- If you have a skill received but you haven't checked the location where you get the skill in the vanilla game, when you get there the location won't be there. The current workaround for that is that, when you enter the room where the location is, the mod will remove that specific skill from your skills, but then you will have to exit and re enter the room for the location to appear. Once you do that, when you check the location, it will be sent as normal, and also assign your skill back

# PLANNED SETTINGS FOR THE YAML
- Being able to pick if the victory condition is beating Selin or Dora (turns out dying during the credits skips the entirety of the credits, so I can make something in the mod to manually skip the credits so you can access Dora faster)
- Add Windmill Key to the randomization pool
  - In theory, to finish the game you need: Lunar Attunement, Windmill Key, and Double Jump and Wall Jump to traverse the final area.
  - Extra items are considered. Progressive Moonlit Dust and the two(?) items the rat king thingy gives you that unlocks the Very Big Spider (the item the spider gives could be added as well)
- Add Damage lilies to the randomization pool
- Add Berries to the randomization pool
  - Either separate health, magic and stamina (and black?) berries to add any of them you want, or add all of them to a single randomization pool (berrysanity?)
- Add Lumen Fairies to the randomization pool; doing this would also add the Oracle sigil to the pool
- Red Speech Bubble sanity?
- Add Familiars to the randomization pool (very low on the priority list)
