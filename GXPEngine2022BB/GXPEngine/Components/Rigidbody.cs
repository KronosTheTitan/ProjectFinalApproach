using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Rigidbody : Collider
{
    public float radius=5;
    public float weight = 1;
    public float bounciness = 0f;
    public float inertia = .8f;

    public Vec2 gravity = new Vec2(0, 1);

    public Rigidbody(GameObjectECS gameObjectECS) : base(gameObjectECS)
    {

    }
    public override void UpdateECS()
    {
        base.UpdateECS();
        gameObject.velocity *= inertia;
        gameObject.velocity += gravity;
        //Console.WriteLine(gameObject.velocity.ToString());
    }
    public override void OnChunkChange()
    {
        base.OnChunkChange();
        gameObject.chunk.rigidbodies.Remove(this);
        gameObject.newChunk.rigidbodies.Add(this);
    }
}
