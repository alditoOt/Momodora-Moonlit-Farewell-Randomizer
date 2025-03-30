# VERY IMPORTANT, BACKUP YOUR SAVE FILE JUST IN CASE
You can find you save file here:

**C:\Users\Username\AppData\LocalLow\BOMBSERVICE\MomodoraMoonlitFarewell\saves**

You can copy the Save0.sav (or any corresponding slot) into a backup folder (Save0 is slot 1, save4 is slot 5).

When playing in a randomizer, it's recommended to start from a new save file. Hardocre mode is possible, but not recommended.

# Mod Install Notes
This randomizer uses a custom made mod to go along with the AP session. 
1. Download [MelonLoader](https://melonwiki.xyz/#/?id=requirements), execute the file, and when installing for Momodora: Moonlit Farewell, ensure you're using version 0.5.7. 
2. Download the **MomodoraMFRandomizer.zip** file and extract it into the **Mods** folder in the game folder.
3. Before opening the game, open config.json file and edit all fields as necessary. 
# Creating the Session
1. Download **momodoramoonlitfarewell.apworld** and add it into the custom_worlds folder on your Archipelago folder. 
2. Download **Momodora-Moonlit-Farewell.yaml** and edit it as necessary (mainly your player slot). 
3. Add the yaml into the Players folder in the Archipelago folder
4. Open the Archipelago Launcher and hit Generate. Your session should be in the output folder in the Archipelago folder

# Optional Settings in the YAML
## Open Springleaf Path
When this is enabled, the Demon Strands and Wind Barriers on Springleaf Path are removed, so you can explore other areas earlier without needing to have Awakened Sacred Leaf/Sacred Anemone
- Worth noting that you can access anything without defeating the first boss. However, **after** you defeat the first boss and get sent to Koho Village, **the game resets the map as if you're exploring it from scratch**, so be wary of that.
## Deathlink
With this enabled, if you die, every other player in the session (that has deathlink enabled) also dies. The same applies the other way around.
## Bell Hover Generation
With this enabled, the world generation will consider being able to Bell Hover (increase height when jumping and using the Healing Bell) to randomize items in the locations. This can be used to access certain areas without the skill you require in a vanilla game to access them (i.e. Demon Frontier without Wall Jump).

You might need to get creative to access certain areas (i.e. damage boosting to reach a platform to be able to access Ashen Hinterlands without Double Jump, speaking from experience).

With this disabled, areas that can be accessed with Bell Hover will not have the skill needed to access them.

## Randomize Key Items
With this enabled, three key items are added as location checks and to the randomized item pool. These items are: Gold Moonlit Dust, Silver Moonlit Dust and Windmill Key. The Moonlit Dust is required for getting the Lunar Attunement check, and the Windmill Key is an important item to be able to access the final area. If you have the Windmill Key, you don't need to deliver the Wooden Box to be able to activate the Windmill.

## Oracle Sigil
With this setting enabled, the Oracle Sigil is added as a location check and to the randomized item pool. This Sigil is arguibly the hardest to get in the game, but also the best you could have, since it increases crit rate and damage by a great amount. However, in order to get it, you need to free all 30 Lumen Fairies that are all over the map. If you enable this setting, it's very likely one of the last checks you'll have, but it's also possible that you get this Sigil earlier.

## Final Boss Keys
With this setting on, it adds 4 Keys required to open the door to the final boss. This is usually done by defeating 4 bosses right before this door, but with the setting enabled these bosses will only have their regular location checks but won't help to open the door.

If you have this setting on as well as the Oracle Sigil setting, there's a possibility that one of the keys is in the Oracle Sigil check, making completing the game a very long task.

# Items and Locations
## Locations Checks
- All skills* (all 5 main skills and fast travel)
- All Sigils (Save for the last 4 Sigils you can buy from Cereza)
- All Grimoires
- All Bosses (most bosses send the check once their cutscene is over)

## Optional Checks (based on YAML settings)
- Gold Moonlit Dust
- Silver Moonlit Dust
- Windmill Key
- Oracle Sigil

*Due to how the game manages the skills, if you have a skill before checking the room where you'd get it, the location will be disabled. The current workaround for this is:
- Enter the room where you'd get a skill you already received. Your skill will be temporarily disabled
- Leave the room and enter again
- When you check the location, it will be sent to the session, and your original skill will be restored
Places affected by this:
- Springleaf Path where you bathe the Sacred Leaf
- Springleaf Path, fight with the Harpy
- Lun Tree Roots, room after figthing Black Cat
- Fairy Village, talking to the big fairy woman whose name I don't remember right now (yeah, you're gonna have to use the elevators for this one...)
- Ashen Hinterlands, Serpent that gives you the Lunar Attunement

## IMPORTANT
**IF** leaving and entering the room again doesn't give you your skill back, you can go back to the main menu and open your save file. This should give you back your skill, and you will usually enter the rooms where you get these only once or twice. This is an unfortunate oversight on how the game handles these skills so this is the workaround for now.

## Items You Can Receive
- All skills (all 5 main skills and fast travel)
- All Sigils (Save for The Fool, Living Blood and all Sigils you can buy from Cereza. Checking these locations will send a location check as well as giving you their respective Sigil)
- All Grimoires
- 50 Lunar Crystals

## Optional Items You Can Receive (based on YAML settings)
- Gold Moonlit Dust 
- Silver Moonlit Dust
- Windmill Key
- Oracle Sigil
- 4 Final Boss Keys


# Victory Condition
The game is considered finished when you defeat the final boss and finish watching the text after the black screen.

# Small Notes (aka Small Bugs That I'll Fix Later ~~Hopefully~~)
These are things that happen during the game, not detrimental for the randomizer experience but maybe a tad bit annoying... 
- When hosting on the page, when you send a check, the game might freeze for a tiny bit. That's completely normal, I'll look into fixing that later if possible ~~let's hope that's a quick fix~~, ~~no, I'm not mining bitcoin~~
  - The game might freeze for longer if you open it again in a previous session. That's because it's giving you all your unlocked items on the session, so don't worry, the game didn't crash, it's loading (there's probably an implementation that won't cause this, but I'm new to modding......)
- When you load the game again, there's a possibility that if you received a skill without checking the location where you'd get it, the game will send the check as if you just did. While it might be a bit annoying, only 5 locations are affected by this, so at least you're not freely giving away that many items.  
- Due to how the game handles skills, receiving the Fast Travel will always send the check. It doesn't happen the other way around
