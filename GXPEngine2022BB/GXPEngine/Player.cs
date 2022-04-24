using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Player : Component
{
    public float speed = 5f;
    public float topSpeed = 10f;
    public float jumpForce = 10f;
    public Player(GameObjectECS pGameObjectECS) : base(pGameObjectECS)
    {
        pGameObjectECS.AddComponent(new Component_Sprite(pGameObjectECS,"circle.png"));
        pGameObjectECS.AddComponent(new Rigidbody(pGameObjectECS));
        ChunkLoader.Instance.loadedChunks.Add(gameObject.chunk);
        ChunkLoader.Instance.LoadNewChunks(gameObject.chunk.xPos, gameObject.chunk.yPos);
    }
    public override void UpdateECS()
    {
        Controls();
    }
    void Controls()
    {
        if (Input.GetKey(Key.D))
        {
            gameObject.velocity += new Vec2(speed, 0);
        }
        if (Input.GetKey(Key.A))
        {
            gameObject.velocity += new Vec2(-speed, 0);
        }
        if (Input.GetKey(Key.W)|Input.GetKeyDown(Key.SPACE))
        {
            gameObject.velocity += new Vec2(0, -speed);
        }
        if (Input.GetKey(Key.S) | Input.GetKeyDown(Key.SPACE))
        {
            gameObject.velocity += new Vec2(0, speed);
        }
    }
    public override void OnChunkChange()
    {
        base.OnChunkChange();
        ChunkLoader.Instance.LoadNewChunks(gameObject.newChunk.xPos, gameObject.newChunk.yPos);
    }
}
