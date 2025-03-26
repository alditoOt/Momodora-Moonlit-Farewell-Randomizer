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
With this disabled, areas that can be accessed with Bell Hover will not have the skill needed to access them.

# Items and Locations
## Locations Checks
- All skills* (all 5 main skills and fast travel)
- All Sigils (except for Oracle and the last 4 Sigils you can buy from Cereza)
- All Grimoires
- All Bosses (most bosses send the check once their cutscene is over)

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

## Items You Can Receive
- All skills (all 5 main skills and fast travel)
- All Sigils (except for Oracle, The Fool, Living Blood and all Sigils you can buy from Cereza)
- All Grimoires


# Victory Condition
The game is considered finished when you defeat the final boss and finish watching the text after the black screen. At that point, the server will release all remaining items.

# Small Notes (aka Small Bugs That I'll Fix Later ~~Hopefully~~)
These are things that happen during the game, not detrimental for the randomizer experience but maybe a tad bit annoying... 
- When hosting on the page, when you send a check, the game might freeze for a tiny bit. That's completely normal, I'll look into fixing that later if possible ~~let's hope that's a quick fix~~, ~~no, I'm not mining bitcoin~~
  - The game might freeze for longer if you open it again in a previous session. That's because it's giving you all your unlocked items on the session, so don't worry, the game didn't crash, it's loading (there's probably an implementation that won't cause this, but I'm new to modding......)
- When you load the game again, there's a possibility that if you received a skill without checking the location where you'd get it, the game will send the check as if you just did. Doesn't happen everytime but I saw it happen (and for some reason the Lunar Attunement seems to be the location most affected)
  - Due to how the game handles skills, receiving the Fast Travel will always send the check. It doesn't happen the other way around