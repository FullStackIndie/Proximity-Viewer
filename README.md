# Proximity-Viewer
Proximity Viewer - Disables the renders of any gameobject outside the Cameras scan radius.
By close proximity i tested this with  20-30f radius.
Works good with under 300 gameobjects in close proximity.
Slight Lag with over 500 gameobjects in close proximity .
Noticiable lag with over 900 gameobjects in close proximity - Dont run in update .
Major lag with over 1500 game objects in close promity - Dont use in update or use a faster algorithm.

Doesnt work with Tasks but may work with Unity DOTS Stack/ECS.
