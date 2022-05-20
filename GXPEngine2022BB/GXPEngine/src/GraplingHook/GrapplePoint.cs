using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GXPEngine.GraplingHook
{
    public class GrapplePoint : EasyDraw
    {
        public GrapplePoint(int width = 50, int height = 50) : base(width, height, true)
        {
            Clear(Color.MediumPurple);
            Game.main.AddChild(this);
        }
    }
}
