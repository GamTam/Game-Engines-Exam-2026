The Object Oriented principles that I implemented were inheritance and encapsulation. 
I utilised inheritance by creating a parent class FallingObject.cs and gave it the functionality to make an object fall down unless it collides with the floor. I then had the player controller inherit from it, and I also planned to have the enemy objects inherit from it to allow them both to fall to the floor.
Encapsulation was used to make sure that all the objects only had access to what was strictly needed for each other. An example is in FallingObject.cs, where all the variables are protected with the exception of _floorDistance. _floorDistance doesn't need to be accessed by child classes, so it is made private.
I had originally intended on using polymorphism to create an IDamagable interfaces and have the player and enemy classes derive it. This would have been done in order to ensure that both types of object can be damaged and that any object can make them take damage.
I had also originally planned to create three types of enemies: A static one that just fell to the ground, one that would walk back and forth on a platform, and a flying one that would chase the player. The three of them would derive from a parent enemy class that contained HP, Score, OnBubble, and OnDeath data to be inherited. This would better fit inheritance than what ended up being used in the submitted build.
The same goes for the player's bubbles, but I had only intended to create two that derived from a base bubble class. What I had in mind for the base bubble class was what is seen in Bubble.cs, and I had wanted to create a variation that flew up as well as out to catch any enemies that were to high to jump to.

The way that Singleton was implemented in the game was by using a game manager that kept track of various stats while playing. The total time played and the total amount of enemies killed were both elements that were tracked. 
I had also intended to use the singleton pattern on each of the enemy types to make sure that only one instance of each enemy would be on screen at any time without needing the enemy spawner to keep track of what types of enemies it's already spawned in. This would be to make sure that the play area doesn't become too overwhelming
while the player's running around and defeating enemies.  

The way that the Factory pattern was used was as an enemy spawner. The Factory has a list of all possible objects of type EnemyController that it can spawn, and randomly picks one from the list when it's time to spawn one in. This was done because I wanted to have a sense of unpredictability when spawning in the enemies.
It helps to keep the player on their toes when they don't know what's going to come next. I had also planned on using the Factory pattern when the player fired out a bubble to randomize which type of bubble spawned.

PlayerController.cs and EnemyController.cs were taken from the in-class assignments and modified to fit this midterm.
Singleton.cs was taken from the lecture slides.
