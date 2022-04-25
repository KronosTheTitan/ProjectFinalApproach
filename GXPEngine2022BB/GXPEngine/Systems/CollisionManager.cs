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
			foreach (Collider collider in activeColliders)
				foreach (LineSegment line in collider.lines)
				{
					Vec2 ltb = rigidbody.gameObject.transform - line.start;
					float ballDistance = ltb.Dot((line.end - line.start).Normal());   //HINT: it's NOT 10000

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

							if (!collider.trigger)
							{
								rigidbody.gameObject.transform = desiredPos;
								if (Approximate(t, 0))
									rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + rigidbody.gameObject.velocity;
								else
									rigidbody.gameObject.velocity = rigidbody.gameObject.velocity.Reflect((line.end - line.start).Normal(), rigidbody.bounciness);
								rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * (1 - t));
								//rigidbody.gravity = new Vec2(0, 0);
							}

							rigidbody.gameObject.OnCollision(collider.gameObject);
							collider.gameObject.OnCollision(rigidbody.gameObject);
						}
						else
						{
							rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + rigidbody.gameObject.velocity;
							//rigidbody.gameObject.velocity *= 0;
						}
					}
				}
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
