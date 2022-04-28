using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public struct Vec2
{
    public float x;
    public float y;
    public Vec2(float pX,float pY)
    {
        x = pX;
        y = pY;
    }
    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }
    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }
    public static Vec2 operator *(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x * right.x, left.y * right.y);
    }
    public static Vec2 operator *(Vec2 left, float right)
    {
        return new Vec2(left.x * right, left.y * right);
    }
    public static Vec2 operator *(float left, Vec2 right)
    {
        return new Vec2(left * right.x, left * right.y);
    }
    public static Vec2 operator /(float left, Vec2 right)
    {
        return new Vec2(left / right.x, left / right.y);
    }
    public static Vec2 operator /(Vec2 left, float right)
    {
        return new Vec2(left.x / right, left.y / right);
    }
    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }
    public float Length()
    {
        return Mathf.Sqrt((x * x) + (y * y));
    }
    public void SetXY(float pX, float pY)
    {
        x = pX;
        y = pY;
    }
    public void Normalize()
    {
        float l = Length();
        x /= l;
        y /= l;
    }
    public Vec2 Normalized()
    {
        Vec2 vec2 = this;
        vec2.Normalize();
        return vec2;
    }
    public float Dot(Vec2 other)
    {
        return x * other.x + y * other.y;
    }
    public Vec2 Normal()
    {
        Vec2 output = new Vec2(x * 0 - y * 1, x * 1 + y * 0);
        output.Normalize();
        return output;
    }
    public Vec2 Reflect(Vec2 pNormal, float pBounciness = 1)
    {
        return this - (1 + pBounciness) * Dot(pNormal) * pNormal;
    }
}
