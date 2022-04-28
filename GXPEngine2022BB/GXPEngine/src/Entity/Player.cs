using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GXPEngine.ECS.EntityComponent;

namespace GXPEngine.ECS
{
    public class Player : Entity
    {
        private bool moveLeft, moveRight, moveUp, moveDown;

        public Vec2 _velocity, _position;

        public Vec2 _oldVelocity, _oldPosition;

        private Vec2 _acceleration, _input;
        bool grounded = true;

        public Player() : base("checkers.png")
        {
            _velocity = new Vec2();
            _position = new Vec2();
            _oldVelocity = new Vec2();
            _oldPosition = new Vec2();
            _acceleration = new Vec2(0, 2);
            _input = new Vec2(0, 0);
        }

        private void UpdatePosition()
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

            if (Input.GetKey(Key.SPACE))
            {
                _velocity.y = -20;
            }
        }

        private void Controlls()
        {
            KeyUpdate();
            if (!((moveLeft ^ moveRight) || (moveUp ^ moveDown))) _acceleration.x = 0;
            if (moveRight ^ moveLeft) _acceleration.x = (moveLeft ? -1 : 1);
        }

        public override void update()
        {
            _oldPosition = _position;
            _oldVelocity = _velocity;

            Controlls();

            Move();

            //UpdatePosition();
        }

        void Move()
        {
            _velocity += _acceleration;
            _velocity *= .9f;
            //_position += _velocity;


            MoveUntilCollision(_velocity.x, 0);
            MoveUntilCollision(0, _velocity.y);
        }
    }
}
