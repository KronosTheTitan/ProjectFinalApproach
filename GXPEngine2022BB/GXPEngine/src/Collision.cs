using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Collision
{
	public float t;
	public Collision(float pT)
    {
		t = pT;
    }
	public virtual void ResolveCollision()
	{

	}
}
