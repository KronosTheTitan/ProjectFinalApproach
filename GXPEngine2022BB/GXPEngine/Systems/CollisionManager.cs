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
		activeRigidbodies.Clear();
		foreach(Chunk chunk in ChunkLoader.Instance.loadedChunks)
        {
			foreach (GameObjectECS gameObject in chunk.gameObjects)
            {
				Component[] components = gameObject.GetComponentsInChilderen(typeof(Rigidbody)).ToArray();
				foreach(Component component in components)
                {
					activeRigidbodies.Add((Rigidbody)component);
                }
            }
		}
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
				Vec2 ltb = rigidbody.gameObject.transform - line.start;
				float ballDistance = ltb.Dot((line.end - line.start).Normal());

				//compare distance with ball radius;
				if (ballDistance < rigidbody.radius)
				{
					float a = (rigidbody.gameObject.oldTransform - line.start).Dot((line.end - line.start).Normal()) - rigidbody.radius;
					float b = -rigidbody.gameObject.velocity.Dot((line.end - line.start).Normal());
					float t = a / b;
					//rigidbody.gameObject.position = rigidbody.gameObject.oldPosition + (rigidbody.gameObject.velocity * t);
					Vec2 desiredPos = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * t);
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
			RtRCheck(rigidbody, rigidbody1);

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
		if ((rigidbody.transform - rigidbody1.transform).Length() < rigidbody.radius + rigidbody1.radius)
		{
			float a = rigidbody.gameObject.velocity.Length() * rigidbody.gameObject.velocity.Length();

			Vec2 u = rigidbody.transform - rigidbody1.transform;

			//float b = 2 * (u.Dot(rigidbody.gameObject.velocity));
			float b = 2 * (rigidbody.gameObject.velocity.Dot(u));

			float c = (u.Length() * u.Length()) - ((rigidbody.radius + rigidbody1.radius) * (rigidbody.radius + rigidbody1.radius));

			Console.WriteLine("a = " + a.ToString() + " : b = " + b.ToString() + " : c = " + c.ToString());

			if (Mathf.Approximate(a,0)) return null;

			float d = (b * b) - (4 * a * c);

			float t = (-b - Mathf.Sqrt(d)) / (2 * a);

			//if (t < 0) return null;

			rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * t);

			//Console.WriteLine("hit : " + t + " : " + rigidbody1.transform.ToString());

			return new CollisionRR(rigidbody, rigidbody1, t);
		}
		return null;
	}
}
