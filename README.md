# Scavenger
Unity Scavenger game based on Unity tutorial [2D Roguelike](https://learn.unity.com/project/2d-roguelike-tutorial?uv=5.x).

Authors: Greg Blodgett and Diego Pina
Class: Game Development 
Instuctor: Dr. McCown
Due Date: 11/20/2020

## Requirements
1. Your game should use a single multi-screen map that you've created in a text file. The map file should reside in your project's Resources directory. The map should have at least 6 different parts. The player can enter each part through an opening in the wall, but bad guys are not allowed through openings. The game ends when the player reaches the exit.

2. The menu screen should display the goal of the game, the game's name, and the developers' names. A start button should start the game. 

3. The player's goal is to reach a single exit, but you can decide how that happens. For example, maybe the player must collect a special key that unlocks the exit. 

4. There should be some way to lose the game. For example, the player could die when they run out of food.

5. In addition to tracking the player's food, the game should have some type of point system. For example, killing a zombie could add points, or collecting some item type could add points. 

6. The game should have an additional zombie that is much more aggressive and dangerous than the current zombies. The zombie should look different than the other zombies. You may duplicate the existing sprite sheet and just change the color of zombie to create the sprites needed for the third zombie.

7. The game should implement some method from the textbook's "Seven Methods of Indirect Guidance" from chapter 12 to help the player find the exit.
The game does not need to save scores.

Greg:
- Reading text file and constructing board
- Creating Key attribute and Locked wall
- Enabled killing zombies
- Point system

Diege:
- Main menu screen
- Third Zombie
- Indirect guidance
