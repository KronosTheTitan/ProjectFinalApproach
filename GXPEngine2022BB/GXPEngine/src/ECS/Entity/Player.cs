using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GXPEngine.ECS.EntityComponent;

namespace GXPEngine
{
    public class Player : Entity
    {
        Camera _camera;
        Rigidbody rigidbody;
        public bool moveLeft, moveRight, moveUp, moveDown;
        public Player(Camera camera,string filename, int cols = 1, int rows = 1) : base(filename, cols, rows)
        {
            _camera = camera;
            rigidbody = addComponent<Rigidbody>();
            width = 32;
            height = 32;
            rigidbody.radius = width / 2;
            rigidbody.weight = 100;
            SetOrigin(width / 2, height / 2);
        }

        private void KeyUpdate()
        {
            moveLeft = Input.GetKey(Key.A);
            moveUp = Input.GetKey(Key.W);
            moveRight = Input.GetKey(Key.D);
            moveDown = Input.GetKey(Key.S);
        }
        public override void update()
        {
            KeyUpdate();
            _camera.SetXY(x, y);/*
            if (!((moveLeft ^ moveRight) || (moveUp ^ moveDown))) return;
            if (moveUp ^ moveDown) rigidbody.velocity.y = (moveUp ? -3 : 3);
            if (moveRight ^ moveLeft) rigidbody.velocity.x = (moveLeft ? -3 : 3);*/
            rigidbody.velocity.x += ((Input.GetKey(Key.D) ? 3: 0) - (Input.GetKey(Key.A) ? 3 : 0));
            rigidbody.velocity.y += ((Input.GetKey(Key.S) ? 3 : 0) - (Input.GetKey(Key.W) ? 3 : 0));
        }
    }
}
