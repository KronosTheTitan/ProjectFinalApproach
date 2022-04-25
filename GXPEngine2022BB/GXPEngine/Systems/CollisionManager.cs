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
			foreach (Collider collider in activeColliders)
			{
				//Console.WriteLine(collider.lines.Length);
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
						Vec2 ObjectToLine = desiredPos - line.start;
						float dotProduct = ObjectToLine.Dot(lineVector.Normalized());

						if (dotProduct > 0 && dotProduct < lineLength)
						{

							if (!collider.trigger)
							{
								rigidbody.gameObject.transform = desiredPos;
								rigidbody.gameObject.velocity = rigidbody.gameObject.velocity.Reflect((line.end - line.start).Normal(), rigidbody.bounciness);
								rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * (1 - t));
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
        }
    }
}
