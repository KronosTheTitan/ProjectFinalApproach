using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GXPEngine.ECS.EntityComponent;

namespace GXPEngine
{
    public class Player : Entity
    {
        Rigidbody rigidbody;
        public bool moveLeft, moveRight, moveUp, moveDown;
        public Player(string filename, int cols = 1, int rows = 1) : base(filename, cols, rows)
        {
            rigidbody = addComponent<Rigidbody>();
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
            if (!((moveLeft ^ moveRight) || (moveUp ^ moveDown))) return;
            if (moveUp ^ moveDown) rigidbody.velocity.y = (moveUp ? -3 : 3);
            if (moveRight ^ moveLeft) rigidbody.velocity.x = (moveLeft ? -3 : 3);
        }
    }
}
