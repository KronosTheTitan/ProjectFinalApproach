using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class PolygonCollider : Collider
{
    public List<LineSegment> lines = new List<LineSegment>();
    public PolygonCollider(Vec2[] points, int[] pLines, bool pTrigger = false):base(pTrigger)
    {
        for (int i = 0; i < pLines.Length; i += 2)
        {
            lines.Add(new LineSegment(points[pLines[i]], points[pLines[i + 1]]));
            //Console.WriteLine("added line");
        }
        trigger = pTrigger;
        CollisionManager.Instance.activeColliders.Add(this);
    }
}