## INTRODUCTION

FROGGY PANG is a Pang-inspired game with a frog who can also jump and climb walls. As in Pang, the player's goal is to destroy all bubbles by touching them with the frog's tongue before the time is up. As an extra twist, the scenario rotates 90º every few seconds, reorganizing platforms. 

![Screenshot](/Images/Picture8.png)
![Screenshot](/Images/Picture9.png)

There are different types of tongues (simple, double, spit projectiles), as well as hidden pickups that give extra points. The game has a total of 7 levels, which unlock progressively as the player beats them. There is a save system to keep track of the unlocked levels.

![Screenshot](/Images/Picture2.png)


## HOW TO PLAY

The game is better played with a controller, but it can also be played with a keyboard and mouse. 

Controls:

- Movement: WASD / joystick 
- Jump: space bar / button south gamepad (A on xbox controller)
- Shoot: left button mouse / button north gamepad (Y on xbox controller)
- Dashing: B key / Button east (B on xbox controller)

To play in Unity Editor, open the project with Unity version 2021.3.10f1 and start playing from the SplashScreen. Alternatively, you can play making a local build.
To reset the PlayerPrefs (which store the unlocked/locked levels), go to the script LevelLockController.cs and delete the comment on line LINENUMBER, which removes the current PlayerPrefs.

Below you can see a gameplay video:
https://youtu.be/JaXMrNwA1vQ


## DEVELOPMENT

The game has been developed using Unity Version 021.3.10f1 and Visual Studio 2022. The project uses the New Input System to facilitate playing both with a controller (such as xbox or PlayStation) and also with a keyboard and mouse. Moreover, it has been designed to be played with a resolution of 1920x1080 and locally, as it incorporates a local save system. 


## ASSETS USED

The game characters and animations (frog, insects, pickups) have been developed by me. The rest of the assets have been developed by third parties (references below).

### Backgrounds:
- Upkylak, a https://www.freepik.com/
- Craftpix: https://craftpix.net/freebies/free-cartoon-forest-game-backgrounds/

### Tiles:
- Game Art 2D: https://www.gameart2d.com/free-desert-platformer-tileset.html
- Game Art 2D: https://www.gameart2d.com/winter-platformer-game-tileset.html

### Music and sfx:
- Frog sfx: AntumDeluge, https://opengameart.org/content/mutant-frog
- Antoinemax: https://opengameart.org/content/nature-sounds-pack
- TinyWorlds: https://opengameart.org/content/we-are-prophet-happy-energetic-tune
- SketchyLogic: https://opengameart.org/content/hungry-dino-9-chiptune-tracks-10-sfx
- Juhani Junkala: https://juhanijunkala.com/ 
- 3xBlast, Harm-Jan Wiechers: https://opengameart.org/content/bleeps-and-synths
- Bart Kelsey: https://opengameart.org/content/8-bit-platformer-sfx
- Rubberduck: https://opengameart.org/content/80-cc0-creature-sfx 

Some sounds were modified using free software Audacity (https://aduducity.org/)

### Font:
- Ahmad Zulfikar Ali: ilarakifluzdamha@gmail.com (https://www.dafont.com/es/cartoonic.font)
- Letterna studios, Balloon Dreams: https://letterena.com/product/ballon-dreams/
- OMAR MOGOLLON-ALAGUNNA (c) 2015, Personal website: http://www.alagunna.com
- Tokokoo studios (https://www.dafont.com/es/fun-blob.font)

### Asset cleaner:
https://github.com/unity-cn/Tool-UnityAssetCleaner











