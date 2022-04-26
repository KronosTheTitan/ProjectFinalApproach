using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class PolygonCollider : Collider
{
    public PolygonCollider(GameObjectECS pGameObject, Vec2[] points, int[] pLines, bool pTrigger = false) : base(pGameObject)
    {
        List<LineSegment> lineSegments = new List<LineSegment>();
        for (int i = 0; i < pLines.Length; i += 2)
        {
            lineSegments.Add(new LineSegment(points[pLines[i]], points[pLines[i + 1]]));
            Console.WriteLine("added line");
        }
        lines = lineSegments.ToArray();
        Console.WriteLine(lines.Length);
        trigger = pTrigger;
        if (gameObject.objectStatic)
            CollisionManager.Instance.activeColliders.Add(this);
    }
}
