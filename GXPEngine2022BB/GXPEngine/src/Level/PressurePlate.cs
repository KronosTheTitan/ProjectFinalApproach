using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GXPEngine.Level
{
    public class PressurePlate : EasyDraw
    {
        List<Door> doors;

        public PressurePlate(int width = 50, int height = 20) : base(width, height, true)
        {
            doors = new List<Door>();
            Clear(Color.Red);
            Game.main.AddChild(this);
        }

        public void AddDoor(Door d)
        {
            doors.Add(d);
        }

        public void Update()
        {
            GameObject[] cs = GetCollisions();
            if (cs.OfType<Box>().Any())
            {
                foreach (Door d in doors)
                {
                    d.open = false;
                }
            }
            else
            {
                foreach (Door d in doors)
                {
                    d.open = true;
                }
            }
        }
    }
}
