using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class CollisionManager
{
    public static CollisionManager Instance = new CollisionManager();

    private CollisionManager()
    {

    }

    public List<PolygonCollider> activeColliders = new List<PolygonCollider>();
    public List<Rigidbody> activeRigidbodies = new List<Rigidbody>();

    public void OnStep()
    {
		foreach (Rigidbody rigidbody in activeRigidbodies)
        {
			Collision collision = FindEarliestCollision(rigidbody);
            if (collision != null)
            {
				collision.ResolveCollision();
            }
        }
	}

	Collision FindEarliestCollision(Rigidbody rigidbody)
    {
		List<Collision> collisions = new List<Collision>();

		Collision collision = null;

		foreach (PolygonCollider collider in activeColliders)
			foreach (LineSegment line in collider.lines)
			{
				Vec2 ltb = rigidbody.position - line.start;
				float ballDistance = ltb.Dot((line.end - line.start).Normal());
                				
				if (ballDistance < rigidbody.radius)
				{
					float a = (rigidbody.oldPosition - line.start).Dot((line.end - line.start).Normal()) - rigidbody.radius;
					float b = -rigidbody.velocity.Dot((line.end - line.start).Normal());
					float t = a / b;

					Vec2 desiredPos = rigidbody.oldPosition + (rigidbody.velocity * t);
					Vec2 lineVector = line.end - line.start;
					float lineLength = lineVector.Length();
					Vec2 bulletToLine = desiredPos - line.start;
					float dotProduct = bulletToLine.Dot(lineVector.Normalized());

                    if (dotProduct > 0-rigidbody.radius && dotProduct < lineLength+rigidbody.radius && (a>0&&b>0))
					{
						collisions.Add(new CollisionRC(rigidbody,collider,line,t));
                    }
				}
			}
		foreach (Rigidbody rigidbody1 in activeRigidbodies)
		{
			if (rigidbody == rigidbody1) continue;
            Collision collision1 = RtRCheck(rigidbody, rigidbody1);
            if (collision1 != null)
                collisions.Add(RtRCheck(rigidbody, rigidbody1));

		}

		foreach (Collision collision1 in collisions)
        {
            if (collision == null || collision1.t < collision.t)
            {
				collision = collision1;
            }
        }

		return collision;
    }
	Collision RtRCheck(Rigidbody rigidbody,Rigidbody rigidbody1)
    {
        Vec2 normal = new Vec2();
        CollisionRR earliestCollision = null;
        float currentTimeOfImpact = 10;

        Vec2 u = rigidbody.oldPosition - (rigidbody1.position);
        float a = Mathf.Pow(rigidbody.velocity.Length(), 2);
        float b = u.Dot(rigidbody.velocity) * 2;
        float c = Mathf.Pow(u.Length(), 2) - Mathf.Pow(rigidbody.radius + rigidbody1.radius, 2);
        float D = Mathf.Pow(b, 2) - (4 * a * c);

        if (c < 0)
        {
            if (b < 0)
                return new CollisionRR(normal, rigidbody, rigidbody1, 0);
            else return null;
        }

        if (rigidbody.velocity.Length() != 0)
        {
            float TOI1 = (-b - Mathf.Sqrt(D)) / (2 * a);
            
            if (TOI1 < 1 && TOI1 >= 0)
            {
                if (currentTimeOfImpact > TOI1)
                {
                    earliestCollision = new CollisionRR(normal, rigidbody, rigidbody1, TOI1);
                    currentTimeOfImpact = TOI1;
                }
            }
            
        }
        return null;
	}
}
