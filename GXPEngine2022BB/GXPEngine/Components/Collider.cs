using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Collider : Component
{
    public LineSegment[] lines;
    public bool trigger;
    public Collider(GameObjectECS pGameObjectECS):base(pGameObjectECS)
    {

    }
    public void CheckCollision(Rigidbody rigidbody)
    {
		
	}
    public override void OnChunkChange()
    {
        base.OnChunkChange();
		//gameObject.newChunk.colliders.Add(this);
		//gameObject.chunk.colliders.Remove(this);
    }
}
