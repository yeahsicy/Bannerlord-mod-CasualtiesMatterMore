# Bannerlord-mod-CasualtiesMatterMore
A custom mod changing War progress / score calculation in Mount &amp; Blade II: Bannerlord.

## The idea / change

Default Bannerlord war score logic (v1.3.13) in short: 
```
Total strength = sum (clans strength) 

Kills = casualties caused gap / Declared all men amount * 125

Town Sieges = (successfulTownSieges gap) / (Declared Town number + successfulTownSieges gap) * 1000

Castle Sieges = (SuccessfulSieges gap - successfulTownSieges gap) / (Declared Castle number + SuccessfulSieges gap - successfulTownSieges gap) * 500

Raids = successfulRaids gap / Declared Village number * 250
```

This mod changes the multiplier for "Kills" from 125 to 1000, to better reflect its impact to war progress / score. 

## Compatibility
This mod removed DefaultDiplomacyModel and other models inherited from DiplomacyModel. A custom DiplomacyModel class (CustomDiplomacyModel) was implemented to make the changes. 
Thus, if you're using other mods that rely on DefaultDiplomacyModel / DiplomacyModel, it's not compatible. 

It's developed based on game version 1.3.13. No testing was performed for previous versions. 
However, since this mod only overrides GetWarProgressScore() method, it should work in older versions. 

It's purely in-game logic change that no read / write operation to the game save. Turning this on / off shouldn't impact the save. 

## Installation
* Unzip the downloaded file. Copy `CasualtiesMatterMore` folder to Bannerlord `Modules` folder.
* Open the game launcher and check this mod. 

## Troubleshoot / support
- Like other mods, it's common to see crashes / Unable to start when:
  - Running in mismatched game version (Highly unlikely in this though).
  - Having conflicts with other mods.
- This mod is just for fun and mainly my own personal use, so no promise to maintain this for later versions or support others at this stage.
- Only best efforts to GitHub Issues filed here. 
