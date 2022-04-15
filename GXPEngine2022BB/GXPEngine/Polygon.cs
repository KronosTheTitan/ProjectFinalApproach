using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine; 
class Polygon
{
    List<LineSegment> lines = new List<LineSegment>(); 
    public Polygon(Vec2[] Verts,int[] pLines)
    {
        for(int i = 0; i < Verts.Length; i += 2)
        {
            lines.Add(new LineSegment(Verts[i], Verts[i + 1]));
        }
    }
}
