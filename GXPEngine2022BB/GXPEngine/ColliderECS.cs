using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class ColliderECS : Component
{
    LineSegment[] lines;
    public ColliderECS(GameObjectECS pGameObjectECS,Vec2[] points,int[] pLines):base(pGameObjectECS)
    {
        List<LineSegment> lineSegments = new List<LineSegment>();
        for(int i = 0; i > pLines.Length; i += 2)
        {
            lineSegments.Add(new LineSegment(points[pLines[i]], points[pLines[i + 1]]));
        }
    }
    public void CheckCollision(Rigidbody rigidbody)
    {
		foreach(LineSegment line in lines)
		{
			Vec2 ltb = rigidbody.gameObject.transform - line.start;
			float ballDistance = ltb.Dot((line.end - line.start).Normal());   //HINT: it's NOT 10000

			//compare distance with ball radius
			if (ballDistance < rigidbody.radius)
			{
				float a = (rigidbody.gameObject.oldTransform - line.start).Dot((line.end - line.start).Normal()) - rigidbody.radius;
				float b = rigidbody.gameObject.transform.Dot((line.end - line.start).Normal());
				float t = a / b;

				Vec2 desiredPos = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * t);
				Vec2 lineVector = line.end - line.start;
				float lineLength = lineVector.Length();
				Vec2 bulletToLine = line.start - desiredPos;
				float dotProduct = lineVector.Dot(bulletToLine);

				if (dotProduct > 0 && dotProduct < lineLength)
				{
					rigidbody.gameObject.transform = desiredPos;
					rigidbody.gameObject.velocity = rigidbody.gameObject.velocity.Reflect((line.end - line.start).Normal(), rigidbody.bounciness);
					rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * (1 - t));
				}
				else
				{
					rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + rigidbody.gameObject.velocity;
				}
			}
		}
	}
    public override void OnChunkChange()
    {
        base.OnChunkChange();
		gameObject.newChunk.colliders.Add(this);
		gameObject.chunk.colliders.Remove(this);
    }
}
