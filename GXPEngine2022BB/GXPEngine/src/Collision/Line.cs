using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Line : GameObject
    {
        public float x1;
        public float y1;

        public float x2;
        public float y2;

        public bool isHorizontal;
        public bool isVertical;

        public Vec2 midPoint;

        public Line(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.y1 = y1;

            this.x2 = x2;
            this.y2 = y2;

            isHorizontal = y1 == y2;
            isVertical = x1 == x2;

            ensurePointsAreInOrder();

            midPoint = new Vec2((x1 + x2) / 2, (y1 + y2) / 2);
        }

        private void ensurePointsAreInOrder()
        {
            if (x1 > x2 || y1 > y2)
            {
                float temp = x1;
                x1 = x2;
                x2 = temp;

                temp = y1;
                y1 = y2;
                y2 = temp;
            }
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
