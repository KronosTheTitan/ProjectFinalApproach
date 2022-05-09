using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Collision
{
    public float t;

    public Rigidbody rigidbody;

    public LineSegment line;

    public Collider collider;

    public Collision(float pT,Rigidbody pRigidbody,LineSegment pLine,Collider pCollider)
    {
        t = pT;
        rigidbody = pRigidbody;
        line = pLine;
        collider = pCollider;
    }
}
