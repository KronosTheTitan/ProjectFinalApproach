using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Rigidbody : Component
{
    public float radius;
    public float weight;
    public float bounciness;
    public float inertia = 0.75f;
    public Rigidbody(GameObjectECS gameObjectECS) : base(gameObjectECS)
    {

    }
    public override void UpdateECS()
    {
        base.UpdateECS();
        gameObject.velocity *= inertia;
        gameObject.velocity += new Vec2(0, 0);
    }
}
