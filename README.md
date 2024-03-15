# BlockBreaker
BlockBreaker Test
Implement a simple block breaker style game written in C# using Unity 2022.

# Preview
![blockbreaker](https://github.com/duckizz1234/BlockBreaker/assets/153803810/f31afba7-2f28-4bcf-8942-b47bbb3a59c8)

# Design Choices
1) State Machines - Choosing to use state machines means individual objects can be autonomous. Statemachine for the game/views means we can show different menus depending on the game state. Statemachine for the projectiles means they can move on their own and react accordingly if their state changes from external input.
      a) BaseView - View class that is attached to UI objects and allows them to be toggled visible/hidden when their related states is active/inactive (i.e. Replay menu)
      b) BaseState - State class that is attached to game objects and controls their behaviors (i.e. Moving for Projectiles)
2) Data in Json - Storing the parameters used in the Json file means we can adjust the values on the fly without needing to recompile the program. This allows another game designer to tweak and playtest without needing an engineer's intervention.
3) Singleton managers - Allows to easily connect objects to important parts of the game.
4) Positional movement - Allows control of where the projectile is specifically. Instead of using just physics and adding directional velocity, I decided to implement moving by specific position so we can have granual control on where the projectiles are and can allow for interesting game design decisions.
5) Object pooling - Reduces the load when it comes to creating and destroying prefabs. Using a pool means the same set of objects are reused thus reducing load on the game itself.
   
# Challenges
1) Starting on juice - I am often tempted to start putting juice and polish in the game even at this early stage. But I do understand that often, an initial prototype or test does not require all the bells and whistles as it may distract testers from testing the actual game mechanism properly

# Potential Improvements
1) Level design - Design a template where level designer can easily indicate where the blocks and their levels are and the game will have a LevelManager which can load the level accordingly.
2) Level seeds - Keep track of level seeds to allow replaying a level with a specific blocks layout again.
3) Editor Debug UI - Debug menu of sorts that designers can tweak to adjust the various parameters like rotational speed, projectile speed etc and then allow saving of the new values.
4) Different Inputs - Allow using keyboard and also touch and hold controls instead of just mouse pointing.
