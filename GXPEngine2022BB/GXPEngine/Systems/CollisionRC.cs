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
			Vec2 desiredPos = rigidbody.oldPosition + (rigidbody.velocity * t);
			rigidbody.position = desiredPos;
				rigidbody.velocity = rigidbody.velocity.Reflect((line.end - line.start).Normal(), rigidbody.bounciness);
			rigidbody.position = rigidbody.oldPosition + (rigidbody.velocity * (1 - t));
		}
	}
}
