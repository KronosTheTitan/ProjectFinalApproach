using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Rope : GameObject
    {
        public float x1;
        public float y1;

        public float x2;
        public float y2;

        public Rope(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.y1 = y1;

            this.x2 = x2;
            this.y2 = y2;
        }

        public void Set(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.y1 = y1;

            this.x2 = x2;
            this.y2 = y2;
        }

        override protected void RenderSelf(GLContext glContext)
        {
            if (game != null)
            {
                Gizmos.RenderLine(x1, y1, x2, y2);
            }
        }
    }
}
