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

    public List<Collider> activeColliders = new List<Collider>();
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
				ResolveCollision(collision);
				Console.WriteLine(collision.t);
                if (Approximate(collision.t,0))
                {
					rigidbody.gameObject.transform += (collision.line.end-collision.line.start).Normal(); 
                }
            }
        }
	}

	Collision FindEarliestCollision(Rigidbody rigidbody)
    {
		List<Collision> collisions = new List<Collision>();

		Collision collision = null;

		foreach (Collider collider in activeColliders)
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

					if (dotProduct > 0 && dotProduct < lineLength)
					{
						collisions.Add(new Collision(t,rigidbody,line,collider));
					}
				}
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

	void ResolveCollision(Collision collision)
	{
		if (!collision.collider.trigger)
		{
			Vec2 desiredPos = collision.rigidbody.gameObject.oldTransform + (collision.rigidbody.gameObject.velocity * collision.t);
			collision.rigidbody.gameObject.transform = desiredPos;
			if (Approximate(collision.t, 0))
				collision.rigidbody.gameObject.transform = collision.rigidbody.gameObject.oldTransform + collision.rigidbody.gameObject.velocity;
			else
				collision.rigidbody.gameObject.velocity = collision.rigidbody.gameObject.velocity.Reflect((collision.line.end - collision.line.start).Normal(), collision.rigidbody.bounciness);
			collision.rigidbody.gameObject.transform = collision.rigidbody.gameObject.oldTransform + (collision.rigidbody.gameObject.velocity * (1 - collision.t));
			//rigidbody.gravity = new Vec2(0, 0);
		}

		collision.rigidbody.gameObject.OnCollision(collision.collider.gameObject);
		collision.collider.gameObject.OnCollision(collision.rigidbody.gameObject);
	}
	public static bool Approximate(Vec2 a, Vec2 b, float errorMargin = 0.01f)
	{
		return Approximate(a.x, b.x, errorMargin) && Approximate(a.y, b.y, errorMargin);
	}

	/// <summary>
	/// A helper method for unit testing:
	/// Returns true if and only if [a] and [b] differ by at most [errorMargin].
	/// </summary>
	public static bool Approximate(float a, float b, float errorMargin = 0.01f)
	{
		return Math.Abs(a - b) < errorMargin;
	}
}
