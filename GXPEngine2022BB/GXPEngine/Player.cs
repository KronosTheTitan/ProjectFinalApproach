using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Player : Component
{
    public Vec2 velocity = new Vec2();
    public float speed = 5f;
    public float topSpeed = 10f;
    public float jumpForce = 10f;
    public Player(GameObjectECS pGameObjectECS) : base(pGameObjectECS)
    {

    }
    public override void UpdateECS()
    {
        Controls();
    }
    void Controls()
    {
        if (Input.GetKey(Key.D)&&velocity.x<topSpeed)
        {
            velocity += new Vec2(speed, 0);
        }
        if (Input.GetKey(Key.A) && velocity.x < -topSpeed)
        {
            velocity += new Vec2(-speed, 0);
        }
        if (Input.GetKeyDown(Key.W)|Input.GetKeyDown(Key.SPACE))
        {
            velocity += new Vec2(0, jumpForce);
        }
    }
}
