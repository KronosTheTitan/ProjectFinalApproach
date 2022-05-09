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

		Vec2 u = ((rigidbody.weight*rigidbody.velocity+other.weight*other.velocity)/totalMass);

		rigidbody.position = rigidbody.oldPosition + (rigidbody.velocity * t);

		rigidbody.velocity = rigidbody.velocity - (1 - rigidbody.bounciness) * (rigidbody.velocity - u);
		other.velocity = other.velocity - (1 - other.bounciness) * (other.velocity - u);
	}
}
