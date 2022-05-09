using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class CollisionRR : Collision
{
	public readonly Vec2 normal;
	public readonly Rigidbody other;
	public readonly Rigidbody rigidbody;

	public CollisionRR(Vec2 pNormal,Rigidbody pRigidbody, Rigidbody pOther, float pTimeOfImpact) : base(pTimeOfImpact)
	{
		normal = pNormal;
		other = pOther;
		rigidbody = pRigidbody;
	}
	public override void ResolveCollision()
	{
		float totalMass = rigidbody.weight + other.weight;

		Vec2 u = ((rigidbody.weight*rigidbody.gameObject.velocity+other.weight*other.gameObject.velocity)/totalMass);

		rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * t);

		rigidbody.gameObject.velocity = rigidbody.gameObject.velocity - (1 - rigidbody.bounciness) * (rigidbody.gameObject.velocity - u);
		other.gameObject.velocity = other.gameObject.velocity - (1 - other.bounciness) * (other.gameObject.velocity - u);

		Console.WriteLine(rigidbody.gameObject.velocity.ToString());
	}
}
