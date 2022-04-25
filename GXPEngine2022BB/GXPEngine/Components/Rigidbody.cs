using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Rigidbody : Component
{
    public float radius=5;
    public float weight;
    public float bounciness = 1;
    public float inertia = 0.75f;
    public Rigidbody(GameObjectECS gameObjectECS) : base(gameObjectECS)
    {

    }
    public override void UpdateECS()
    {
        base.UpdateECS();
        gameObject.velocity *= inertia;
        gameObject.velocity += new Vec2(0, 1);
    }
    public override void OnChunkChange()
    {
        base.OnChunkChange();
        gameObject.chunk.rigidbodies.Remove(this);
        gameObject.newChunk.rigidbodies.Add(this);
    }
}
