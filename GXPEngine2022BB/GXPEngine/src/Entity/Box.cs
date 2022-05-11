using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GXPEngine.ECS.EntityComponent;

namespace GXPEngine
{
    public class Box : Entity
    {
        private Rope rope;

        private Player p;

        Vec2 player;

        public Box(Player pP) : base("checkers.png")
        {
            p = pP;
            _velocity = new Vec2();
            _position = new Vec2();
            _oldVelocity = new Vec2();
            _oldPosition = new Vec2();
            _acceleration = new Vec2(0, 2);
            rope = new Rope(0, 0, 0, 0);
            game.AddChild(rope);
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
            if (Input.GetMouseButtonUp(0))
            {
                _velocity.Zero();
            }
            if (Input.GetMouseButtonDown(0))
            {
                grapple = new Vec2(p._position.x + p.width / 2, p._position.y + p.height / 2);
                player = new Vec2(_position.x + width / 2, _position.y + height / 2);

                ropeAngleVelocity = -.005f * _velocity.x;

                grappleOrign = player - grapple;

                ropeAngle = grappleOrign.GetAngleRadians() - Mathf.PI;

                ropeLength = 0;

                ropeLengthOld = grappleOrign.Length();
            }
            if (!Input.GetMouseButton(0)) return;


            Vec2 v = Vec2.GetUnitVectorRad(ropeAngle - Mathf.PI);

            v *= NextGrappleLenght();

            //Console.WriteLine(ropeAngleVelocity);

            player = grapple + v;

            Vec2 speed = player - new Vec2(_position.x + width / 2, _position.y + height / 2);
            _velocity = speed;

            rope.Set(grapple.x, grapple.y, player.x, player.y);
        }
    }
}
