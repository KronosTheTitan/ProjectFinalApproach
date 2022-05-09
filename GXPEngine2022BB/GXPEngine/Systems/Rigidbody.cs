using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Rigidbody : Collider
{
    public float radius = 5;
    public float weight = 1;
    public float bounciness = 0f;
    public float inertia = .8f;

    public Vec2 gravity = new Vec2(0, 1);

    public Vec2 velocity = new Vec2();

    public Vec2 position = new Vec2();
    public Vec2 oldPosition = new Vec2();

    public Rigidbody(bool pTrigger):base(pTrigger)
    {
        CollisionManager.Instance.activeRigidbodies.Add(this);

    }
    public override void update()
    {
        oldPosition = position;
        velocity *= inertia;
        velocity += gravity;
        position += velocity;
        entity.x = position.x;
        entity.y = position.y;
    }
}