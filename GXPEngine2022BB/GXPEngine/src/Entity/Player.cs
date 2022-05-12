using GXPEngine.GraplingHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static GXPEngine.ECS.EntityComponent;

namespace GXPEngine
{
    public class Player : Entity
    {
        private bool moveLeft, moveRight, moveUp, moveDown;
        bool grounded = true;

        public bool isGrapple = false;

        public bool ignore = false;

        Vec2 rope;
        Vec2 grapple;

        float ropeAngleVelocity;

        Vec2 grappleOrign;

        float ropeAngle;

        float ropeLength;

        float ropeLengthOld;

        Rope line;

        private float NextGrappleLenght()
        {
            if (grappleOrign.Length() < ropeLength) return grappleOrign.Length();
            if (ropeLengthOld >= ropeLength) return ropeLengthOld *= .97f;
            return ropeLength;
        }

        private float NextGrappleLenghtBox()
        {
            if (grappleOrignBox.Length() < ropeLengthBox) return grappleOrignBox.Length();
            if (ropeLengthOldBox >= ropeLengthBox) return ropeLengthOldBox *= .97f;
            return ropeLengthBox;
        }

        private float NextRopeAccelerationInput()
        {
            //Radians clamp
            if (ropeAngle > -0.872664626) return 0f;
            if (ropeAngle < -2.2689280276) return 0f;
            if (!(Input.GetKey(Key.D) ^ Input.GetKey(Key.A))) return 0f;
            return Input.GetKey(Key.A) ? -0.1f : 0.1f;
        }

        bool AABBTop(Entity a, Entity b)
        {
            float e_bottom = b._position.y + b.height;
            float e1_bottom = a._position.y + a.height;
            float e_right = b._position.x + b.width;
            float e1_right = a._position.x + a.width;

            float b_collision = e1_bottom - b._position.y;
            float t_collision = e_bottom - a._position.y;
            float l_collision = e_right - a._position.x;
            float r_collision = e1_right - b._position.x;

            if (!(e1_right >= b._position.x &&
                e_right >= a._position.x &&
                e1_bottom >= b._position.y &&
                e_bottom >= a._position.y))
            {
                return false;
            }

            if (t_collision < b_collision && t_collision < l_collision && t_collision < r_collision)
            {
                return true;
            }
            else if (b_collision < t_collision && b_collision < l_collision && b_collision < r_collision)
            {
                return true;
            }
            return false;
        }

        Vec2 AABBMax(Entity a, Entity b)
        {
            Vec2 v;
            float e_bottom = b._position.y + b.height;
            float e1_bottom = a._position.y + a.height;
            float e_right = b._position.x + b.width;
            float e1_right = a._position.x + a.width;

            float b_collision = e1_bottom - b._position.y;
            float t_collision = e_bottom - a._position.y;
            float l_collision = e_right - a._position.x;
            float r_collision = e1_right - b._position.x;

            v.x = b._position.x;
            v.y = b._position.y;

            if (!(e1_right >= b._position.x &&
                e_right >= a._position.x &&
                e1_bottom >= b._position.y &&
                e_bottom >= a._position.y))
            {
                return v;
            }

            if (t_collision < b_collision && t_collision < l_collision && t_collision < r_collision)
            {
                v.y = a._position.y - b.height;
            }
            else if (b_collision < t_collision && b_collision < l_collision && b_collision < r_collision)
            {
                v.y = e1_bottom;
            }
            if (l_collision < r_collision && l_collision < t_collision && l_collision < b_collision)
            {
                v.x = a._position.x - b.width;
            }
            else if (r_collision < l_collision && r_collision < t_collision && r_collision < b_collision)
            {
                v.x = e1_right;
            }
            return v;
        }

        bool AABB(Entity a, Entity b)
        {
            float e_bottom = b._position.y + b.height;
            float e1_bottom = a._position.y + a.height;
            float e_right = b._position.x + b.width;
            float e1_right = a._position.x + a.width;

            float b_collision = e1_bottom - b._position.y;
            float t_collision = e_bottom - a._position.y;
            float l_collision = e_right - a._position.x;
            float r_collision = e1_right - b._position.x;

            if (!(e1_right >= b._position.x &&
                e_right >= a._position.x &&
                e1_bottom >= b._position.y &&
                e_bottom >= a._position.y))
            {
                return true;
            }
            return false;
        }
        GrapplePoint gPoint = null;
        void UpdateGrapple()
        {
            if (!isGrapple)
            {
                gPoint = null;
                foreach (GrapplePoint point in GXPEngine.Level.Level.grapplePoints)
                {
                    if (!point.HitTestPoint(Input.mouseX, Input.mouseY)) continue;
                    //Console.WriteLine("test");
                    gPoint = point;
                    break;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isGrapple = false;
                line.Set(0, 0, 0, 0);
            }
            if (!Input.GetMouseButton(0)) return;
            if (gPoint == null) return;
            if (Input.GetMouseButtonDown(0) && grounded)
            {
                isGrapple = true;
                rope = new Vec2(_position.x + width / 2, _position.y + height / 2);
                grapple = new Vec2(gPoint.x + gPoint.width / 2, gPoint.y + gPoint.height / 2);

                ropeAngleVelocity = -.005f * _velocity.x;

                grappleOrign = rope - grapple;

                ropeAngle = grappleOrign.GetAngleRadians() - Mathf.PI;

                ropeLength = 200;

                ropeLengthOld = grappleOrign.Length();
            }

            float ropeAcceleration = -.005f * (Mathf.Cos(ropeAngle) + NextRopeAccelerationInput());
            ropeAngleVelocity += ropeAcceleration;
            ropeAngle += ropeAngleVelocity;

            ropeAngleVelocity *= .99f;

            Vec2 v = Vec2.GetUnitVectorRad(ropeAngle - Mathf.PI);

            v *= NextGrappleLenght();

            //left = ropeAngleVelocity > 0;

            //Console.WriteLine(ropeAngleVelocity);

            rope = grapple + v;

            Vec2 speed = rope - new Vec2(_position.x + width / 2, _position.y + height / 2);
            _velocity = speed;

            line.Set(grapple.x, grapple.y, rope.x, rope.y);

        }

        Vec2 player;
        Vec2 grappleBox;

        float ropeAngleVelocityBox;

        Vec2 grappleOrignBox;

        float ropeAngleBox;

        float ropeLengthBox;

        float ropeLengthOldBox;

        bool BoxGrapple = false;

        Box box;

        void UpdateBoxGrapple()
        {
            if (!BoxGrapple)
            {
                foreach (Box b in GXPEngine.Level.Level.boxes)
                {
                    if (!b.HitTestPoint(Input.mouseX, Input.mouseY)) continue;
                    box = b;
                    break;
                }
            }

            //box = GXPEngine.Level.Level.boxes[0];

            if (box == null) return;

            if (Input.GetMouseButtonUp(0))
            {
                BoxGrapple = false;
                box._velocity.Zero();
                box = null;
                line.Set(0, 0, 0, 0);
            }
            if (!Input.GetMouseButton(0)) return;

            if (!grounded) return;
            if (Input.GetMouseButtonDown(0))
            {
                BoxGrapple = true;
                grappleBox = new Vec2(_position.x + width / 2, _position.y + height / 2);
                player = new Vec2(box._position.x + box.width / 2, box._position.y + box.height / 2);

                ropeAngleVelocityBox = -.005f * box._velocity.x;

                grappleOrignBox = player - grappleBox;

                ropeAngleBox = grappleOrignBox.GetAngleRadians() - Mathf.PI;

                ropeLengthBox = 0;

                ropeLengthOldBox = grappleOrignBox.Length();
            }

            if (!BoxGrapple) return;

            Vec2 v = Vec2.GetUnitVectorRad(ropeAngleBox - Mathf.PI);

            v *= NextGrappleLenghtBox();

            //Console.WriteLine(ropeAngleVelocity);

            player = grappleBox + v;

            Vec2 speed = player - new Vec2(box._position.x + box.width / 2, box._position.y + box.height / 2);
            box._velocity = speed;

            line.Set(_position.x + width / 2, _position.y + height / 2, player.x, player.y);
        }

        public Player() : base("checkers.png")
        {
            line = new Rope(0, 0, 0, 0);
            Game.main.AddChild(line);
            _velocity = new Vec2();
            _position = new Vec2();
            _oldVelocity = new Vec2();
            _oldPosition = new Vec2();
            _acceleration = new Vec2(0, 2);
        }

        public void UpdatePosition()
        {
            x = _position.x;
            y = _position.y;
        }


        private void KeyUpdate()
        {
            moveLeft = Input.GetKey(Key.A);
            moveUp = Input.GetKey(Key.W);
            moveRight = Input.GetKey(Key.D);
            moveDown = Input.GetKey(Key.S);

            if (Input.GetKey(Key.SPACE) && grounded)
            {
                _velocity.y = -30;
            }
        }

        private void Controlls()
        {
            KeyUpdate();
            _acceleration.x = 0;
            if (isGrapple || BoxGrapple) return;
            if (moveRight ^ moveLeft) _acceleration.x = (moveLeft ? -1 : 1);
        }

        public override void update()
        {
            _oldPosition.x = _position.x;
            _oldPosition.y = _position.y;
            _oldVelocity = _velocity;

            UpdateGrapple();
            UpdateBoxGrapple();

            //onsole.WriteLine(isGrapple);

            Controlls();
            Move();

            foreach (Box box in GXPEngine.Level.Level.boxes)
            {
                bool v = AABB(this, box);
                if (!v)
                {
                    float totalMass = 1 + 1;

                    Vec2 u = ((1*this._velocity + 1*box._velocity) / totalMass);

                    bool isTopOrBottom = AABBTop(this, box);

                    Console.ForegroundColor = ConsoleColor.Green;

                    this._position = AABBMax(box, this);
                    if (isTopOrBottom) continue;

                    box._velocity.x = this._velocity.x - (1 - 0) * (box._velocity.x - u.x);
                }
            }


            if (GXPEngine.Level.Level.CheckCollisions(this))
            {
                grounded = true;
                if (isGrapple)
                {
                    ropeAngleVelocity = 0;
                    rope = new Vec2(_position.x + width / 2, _position.y + height / 2);
                    grappleOrign = rope - grapple;

                    ropeAngle = grappleOrign.GetAngleRadians() - Mathf.PI;
                    ropeLengthOld = grappleOrign.Length();
                }
            }
            else
            {
                grounded = false;
            }

            UpdatePosition();
        }

        void Move()
        {
            if (!ignore)
            {
                _velocity += _acceleration;
                _velocity *= .9f;
            }
            _position += _velocity;
        }
    }
}
