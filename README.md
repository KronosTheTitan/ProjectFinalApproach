# ProjectFinalApproach
<p>this is the readme file for ProjectFinalApproach</p>
<h1>Adjustments to GXPEngine</h1>
this game uses a slightly different version of the gxp engine.
<h2>GameobjectECS</h2> <br>
this game will use a custom gameObject class for the entity component system (ECS). Adding funtionality to this class is done by using the component class.<br>
It contains variables linking to the chunk it is currently in, its position in the form of a Vec2 named transform and a Vec2 representing the objects velocity.

<h2>Component</h2>
this is the basic component class, adding more functions then the default is done by having a new class inherit from it. each component is attached to a GameObjectECS.

<h3>Component_sprite</h3>
this class is the basic component for rendering sprites. the sprite will always be placed on the position of its gameobject. it also contains a variable linking to the Sprite class.
<h3>Rigidbody</h3>
the rigidbody component is used for basic collisions

<h2>Chunk</h2>
the chunk class is used to ensure which objects are currently being simulated, if a GameObjectECS instance is not a part of one of the loaded chunks as defined in the ChunkLoader it will not run any of the update scripts on its components.

<h2>ChunkLoader</h2>
the chunkloader is a singleton that handles which chunks will be loaded and unloaded when the LoadNewChunks() function is called.
