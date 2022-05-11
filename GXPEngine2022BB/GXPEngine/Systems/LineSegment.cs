using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;

public class LineSegment : GameObject
{
    public Vec2 start;
    public Vec2 end;
    public LineSegment(Vec2 pStart, Vec2 pEnd)
    {
        start = pStart;
        end = pEnd;
        game.AddChild(this);
    }
    override protected void RenderSelf(GLContext glContext)
    {
        if (game != null)
        {
            Gizmos.RenderLine(start.x, start.y, end.x, end.y, 0xffffffff, 1);
        }
    }
}