# VERY IMPORTANT, BACKUP YOUR SAVE FILE JUST IN CASE
You can find you save file here:

**C:\Users\Username\AppData\LocalLow\BOMBSERVICE\MomodoraMoonlitFarewell\saves**

You can copy the Save0.sav (or any corresponding slot) into a backup folder (Save0 is slot 1, save4 is slot 5).

When playing in a randomizer, it's recommended to start from a new save file. Hardocre mode is possible, but not recommended.

# Mod Install Notes
This randomizer uses a custom made mod to go along with the AP session. 
1. Download [MelonLoader](https://melonwiki.xyz/#/?id=requirements), execute the file, and when installing for Momodora: Moonlit Farewell, **ensure you're installing version 0.5.7**. 
2. Download the **APMomodoraMoonlitFarewell.dll** file and put it into the **Mods** folder in the game's folder.
3. If it's your first time installing this mod, also download **extra-files.zip** and unzip the file in the **Mods** folder in the game's folder.
4. Before opening the game, open config.json file and edit all fields as necessary. 
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

## Progressive Damage Upgrade
With this setting on, all Heavenly Lilies count as location checks, and praying on one will no longer give you the damage upgrade. The damage upgrade is instead now part of the itempool, as Progressive Damage Upgrade, and each upgrade grants 2 additional damage

# Items and Locations
## Locations Checks
- All skills (all 5 main skills and fast travel)
- All Sigils
- All Grimoires
- All Bosses (most bosses send the check once their cutscene is over)

## Optional Checks (based on YAML settings)
- Gold Moonlit Dust
- Silver Moonlit Dust
- Windmill Key
- Oracle Sigil
- Heavenly Lilies

## Skill Location Checks
To send a location check for the skills, you simply need to receive the skill as the vanilla game. This will work **only** if you haven't received the skill from the itempool yet.
If you already have a skill received, but haven't checked its location yet, entering the room where you get the location will automatically send the location check, with a few exceptions:
- Entering the Harpy fight room will remove your Dash until you defeat her and pick up the Sacred Anemone. If you don't get it back, go back to the Title Screen and reload your save.
- For the Lunar Attunement location check, before being able to send this location check, you need to have Golden Moonlit Dust and Silver Moonlit Dust, and have defeated the Tainted Serpent

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
- Progressive Damage Upgrades

# Victory Condition
The game is considered finished when you defeat the final boss and finish watching the text after the black screen.

# Possible Issues
## Stuck in Harpy Room
- For some reason, with the open_springleaf_path setting on, the windzones in the fight with the Harpy, and only in this room, are **not** removed (I'm looking into this). If you find yourself in this situation and can't leave the room, go back to the Title Screen and reload your save file. The red barrier that appears to the right of the room will be gone.
  - If you have **wall jump** or **double jump** you can just use those to leave the room. **If you don't**, the only other way I can think of is using [Bell Hover](https://www.youtube.com/watch?v=wEe-bJFBG_Q) for that particular place. This is a handy guide showcasing Bell Hover, and you can use the healing bell it up to 3 times to gain enough height to leave the room.
## Sending a Check When Reloading the Game
- If you open your file when you have a skill but haven't checked it's location yet, the game will send that location as if it was just checked. Also an oversight from my part, but on the bright side, only 5 locations are affected by it so at least you're not sending too much stuff... (hopefully)

If any issues are found during playing or playtesting, please do let me know in the discord forum so I can look into them. Ultimately, this is still a work in progress so it's not free of bugs, but I'd argue it's very much completable most of the time.

Also, feel free to ask for advice or share what you find! There might be some places that, even with their respective YAML setting, might need some creativity or use of some strats to reach, since considering every possibility of entrance is quite a task, so having information on how to reach certain places can be very useful!
