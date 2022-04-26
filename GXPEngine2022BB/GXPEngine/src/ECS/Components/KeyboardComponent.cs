using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class KeyboardComponent : ECS.EntityComponent.Component
    {
        public bool moveLeft, moveRight, moveUp, moveDown;

        public override void init()
        {
            Console.WriteLine("init");
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
            Console.WriteLine("test");
            KeyUpdate();
            if (!((moveLeft ^ moveRight) || (moveUp ^ moveDown))) return;
            if (moveUp ^ moveDown) entity.y += (moveUp ? -3 : 3);
            if (moveRight ^ moveLeft) entity.x += (moveLeft ? -3 : 3);
        }
    }
}
