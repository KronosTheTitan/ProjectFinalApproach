using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GXPEngine.ECS.EntityComponent;

namespace GXPEngine
{
    public class Box : Entity
    {

        private Player p;

        Vec2 player;

        public Box(Player pP) : base("crate.png")
        {
            p = pP;
            _velocity = new Vec2();
            _position = new Vec2();
            _oldVelocity = new Vec2();
            _oldPosition = new Vec2();
            _acceleration = new Vec2(0, 2);
            width = 64;
            height = 64;
        }


        Vec2 grapple;

        float ropeAngleVelocity;

        Vec2 grappleOrign;

        float ropeAngle;

        float ropeLength;

        float ropeLengthOld;

        public float NextGrappleLenght()
        {
            if (grappleOrign.Length() < ropeLength) return grappleOrign.Length();
            if (ropeLengthOld >= ropeLength) return ropeLengthOld *= .97f;
            return ropeLength;
        }

        public void UpdatePosition()
        {
            x = _position.x;
            y = _position.y;
        }

        public override void update()
        {
            _oldPosition.x = x;
            _oldPosition.y = y;
            _velocity += _acceleration;
            _velocity *= .9f;
            _position += _velocity;

            GXPEngine.Level.Level.CheckCollisions(this);
            UpdatePosition();
        }
    }
}
