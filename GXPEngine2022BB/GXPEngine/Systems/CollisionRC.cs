using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class CollisionRC : Collision
{
	Rigidbody rigidbody;

	LineSegment line;

	PolygonCollider collider;

	public CollisionRC(Rigidbody pRigidBody,PolygonCollider pCollider,LineSegment pLine,float pT) : base(pT)
    {
		rigidbody = pRigidBody;
		collider = pCollider;
		line = pLine;
		t = pT;
    }
    public override void ResolveCollision()
    {
		if (!collider.trigger)
		{
			Vec2 desiredPos = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * t);
			rigidbody.gameObject.transform = desiredPos;
			//if (Mathf.Approximate(t, 0))
			//	rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + rigidbody.gameObject.velocity;
			//else
				rigidbody.gameObject.velocity = rigidbody.gameObject.velocity.Reflect((line.end - line.start).Normal(), rigidbody.bounciness);
			rigidbody.gameObject.transform = rigidbody.gameObject.oldTransform + (rigidbody.gameObject.velocity * (1 - t));
			//rigidbody.gravity = new Vec2(0, 0);
		}

		rigidbody.gameObject.OnCollision(collider.gameObject);
		collider.gameObject.OnCollision(rigidbody.gameObject);
	}
}
