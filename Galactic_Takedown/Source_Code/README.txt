This folder contains the source code for Galactic Takedown.

Ball.cs = The script which determines the trajectory of the ball upon collision with both the blocks and walls. As well as the behaviour of the ball at 
          the beginning of a level where it is launched from the paddle following a user input. 
        
Block.cs = The script which determines the durability and destruction of each block upon collsion with a ball. It handles which block sprites are displayed,
           as well as the particle effects and audio clips which are played upon destruction of a block. 
           
GameStatus.cs = The script which determines the game speed, auto play mode, and the total number of points which the player accumulates within each level. 

Level.cs = The script which counts the total number of balls and blocks at the start of each level. It loads the next level once all blocks are destroyed, 
           or the start screen if all balls are destroyed. 
           
LoseCollider.cs = The script responsible for returning the player to the start menu if they lose a level. 

Paddle.cs = The script which governs the movement of the player controlled paddle.

SceneLoader.cs = The script responsible for loading the levels and menu screens.





           



           

