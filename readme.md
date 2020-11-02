# Unity AI workshop

This repository contains a (fairly basic) pacman-like game and a simplisitc behaviour tree implementation adapted from an online example (located in scripts/ai/bt).
This has been extended to include a simplistic blackboard and a basic controller (btController) which can be used for storing tree nodes.

## Current Implementation
In it's current state:

* The "pacman" can collect pills
* The "ghosts" start in an off-limits region of the level
* The pacman can collide with ghosts and lose lives.
* You can switch between the perspective of the ghosts and pacman by pressing space (for debugging)
* Pressing tab will cycle between ghosts (if currently viewing a ghost).

The behaviour of the AI in this game is currently very simplistic:

* Select a pill
 * check if we are currently do not have a pill targeted, or the targeted pill is close to us
 * if we don't have one selected, select a random one
* Move towards the currently selected target

## Today's Tasks

### Following the player
Open the bt controller, and find the `createTree` method. This contains the logic used to build the agent's behaviour tree.
The _composite_ nodes are designed to take their children as arguments to their contructor. The root node is the one that returned from the method.

In the method, you will find commented out code. Replace the behaviour of the ghost by using the commented out code as the tree. If you move close to the ghost it should head towards the pacman.
Else, it should remain still.

When the pacman is caught, the game is over. There is no end screen, I built the game to play with AI techniques so it's a little rough around the edges :).

### Combining behaviours
Note, if you try to combine the two techniques ( chase the player and patrol ), the agent won't behave as you expect.

What causes this?

*Hint*: how does the code detect if the player is close?

Attempt to fix this by implementing a new action or condition. You can use the example code as a base.

### Extending the behaviours
The AI behaviour is fairly simplistic. Implement additional behaviours by writing new behaviour tree nodes.

Some intresting changes might be:

* Only chase the pacman if it's in front of the ghost, or was 'seen' a short while ago
* Give up chasing the pacman after an amount of time (you might need to alter the ghost speed or pacman's speed to make this work)
* Use a fixed patrol path rather than a random one
* There are 3 disabled ghosts within the game, add them and give them unique behaviours
* There is logic for power pills within the game, but there aren't any within the 'maze', add some to the maze and alter the AI behaviour based on if the ghost is edible or not.