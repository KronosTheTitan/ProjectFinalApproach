using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Collider : Component
{
    public LineSegment[] lines;
    public bool trigger;
    public Collider(GameObjectECS pGameObjectECS,Vec2[] points,int[] pLines,bool pTrigger = false):base(pGameObjectECS)
    {
        List<LineSegment> lineSegments = new List<LineSegment>();
        for(int i = 0; i < pLines.Length; i += 2)
        {
            lineSegments.Add(new LineSegment(points[pLines[i]], points[pLines[i + 1]]));
            Console.WriteLine("added line");
        }
        lines = lineSegments.ToArray();
        Console.WriteLine(lines.Length);
        trigger = pTrigger;
        if(gameObject.objectStatic)
            CollisionManager.Instance.activeColliders.Add(this);
    }
    public void CheckCollision(Rigidbody rigidbody)
    {
		
	}
    public override void OnChunkChange()
    {
        base.OnChunkChange();
		gameObject.newChunk.colliders.Add(this);
		gameObject.chunk.colliders.Remove(this);
    }
}
