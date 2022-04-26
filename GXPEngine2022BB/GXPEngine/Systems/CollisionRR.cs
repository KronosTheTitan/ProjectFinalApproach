using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class CollisionRR : Collision
{
	Rigidbody rigidbody1;
	Rigidbody rigidbody2;

	public CollisionRR(Rigidbody pRigidBody1, Rigidbody pRigidBody2, float pT) : base(pT)
	{
		rigidbody1 = pRigidBody1;
		rigidbody2 = pRigidBody2;
	}
	public override void ResolveCollision()
	{
	}
}
