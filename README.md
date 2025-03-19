# Install Instructions
- Download the .apworld file and add it in the custom_worlds folder of your Archipelago install location.
- Download the example.yaml and edit it to add your player slot name.

# Items and Locations
The randomized Items are as follows:
- All skills
- All Sigils
- Progressive Moonlit Dust (Golden Moonlit Dust and Silver Moonlit Dust)
- Windmill Key
- Blessing of Nothing (filler item)

The locations are as follows:
- Acquiring a skill
- Acquiring a Sigil
- Acquiring any Moonlit Dust
- Acquiring the Windmill Key
- Defeating any Boss

# Manual Deathlink
This is just a fun extra thing, but if doing the multiworld with other players, whenever someone dies they let you know and you go immediately go back to the main menu (to simulate a death).
When you die in your game, let the other players know so they can do the same - if they have deathlink enabled, the player with the fastest game to die in can do so (i.e. Cuphead).

# (Optional) Using Unity Explorer
Using the Unity Explorer mod can be helpful for running this manual randomizer, since it can help you alter the game to grant you the skills indicated by the multiworld instead of having to manually limit yourself to not use them. 
## How to Install Unity Explorer
You can either use BepInEx or MelonLoader to add the mod.
## BepInEx
- Download [BepInEx](https://github.com/BepInEx/BepInEx/releases) (The latest stable version is recommended) and extract all of its contents in the game's folder. Open the game once to let BepInEx configure itself on first start.
- Download [UnityExplorer](https://github.com/sinai-dev/UnityExplorer/releases/tag/4.9.0) (you're looking for **UnityExplorer.BepInEx#.Mono.zip**, where **#** is your BepInEx version), and extract the **sinai-dev-UnityExplorer** folder inside **BepInEx\plugins** folder.

## MelonLoader
- Download [MelonLoader](https://melonwiki.xyz/#/?id=requirements), and when installing, install version 0.5.7. All mods will go into the **Mods** folder in the game folder.
- Download [UnityExplorer](https://github.com/sinai-dev/UnityExplorer/releases/tag/4.9.0) (you're looking for **UnityExplorer.MelonLoader.Mono.zip**), and extract the **Mods** folder inside the game folder
## How to use Unity Explorer for the randomizer
When you open up the game, you should see a bunch of pop ups, that means that Unity Explorer is working.

For modifying the acquired skills, you can access the GameData and update the specific MomoEvent for the specific skill. You can press F7 at any time to open or close the windows.

To access this:
1. Open the Object Explorer, and go to the second tab, Object Search
2. Change the dropdown to search for Class, 
3. Filter for GameData, search and click the first result
4. In the Inspector, look for GameData.current in the left hand column (should be the first option) and hit Inspect
5. In the Inspector, look for GameData.MomoEvent in the left hand column and hit Inspect 
6. In the Inspector, look for MomoEventData.m_events in the left hand oclumn and hit the arrow next to Inspect. Here you can edit manually each MomoEvent value, and then update for the changes to be reflected in your game in real time.
7. If you go back to the Main Menu, when you open your save file once again, follow from step 3 to reload the MomoEvent values

## MomoEvent Value for Each Skill*
- Awakened Sacred Leaf: 20
- Sacred Anemone**: 9
- Crescent Moonflower: 10
- Spiral Shell: 194
- Lunar Attunement: 131

*If you have a skill before going to where you'd get it in the vanilla game (i.e. you got the Awakened Sacred Leaf before going to the place to bathe the Sacred Leaf in the water), that location will be unavailable to check. A workaround would be to enter the screen where you'd get the location and in the Manual Console send the check as if you did it in game.

**If you have the Sacred Anemone before beating the Harpy, when you finish the fight, if you leave the room and enter again, the fight will be triggered once more. You can ignore this room since you only have the Boss check and the Sacred Anemone check, so just send those when you're done with them.

## MomoEvent value for items/sigils
While I was able to get this information, in order to manually add them using Unity Explorer, you'd also need to update the Inventory Database, and I haven't been able to figure out each Inventory Database value yet. Since the Sigils are optional and not required for game completion, you can manually limit yourself to only use the ones you got with the multiworld.
