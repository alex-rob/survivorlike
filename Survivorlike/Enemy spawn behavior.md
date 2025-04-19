Summary:
A Path3D node will be added as a child of the Player node, ensuring that the spawn area will follow with the player's movement. 
The Path3D shall have vertices that create a closed loop, with each point firmly outside of the range of the Camera3D's field-of-view. 
Enemy spawn location shall be determined by obtaining a random float from 0 to 1 denoting the "progress" along the Path3D from the first vertex at the beginning of the loop to the end of the closed loop, again at the first vertex.

Enemy spawn rate shall be determined by the [[Scaling Difficulty]] as a factor over time. The lowest and base spawn rate for enemies shall be 2 seconds. 
A maximum count for enemies shall be establishes which shall also scale with [[Scaling Difficulty]] 