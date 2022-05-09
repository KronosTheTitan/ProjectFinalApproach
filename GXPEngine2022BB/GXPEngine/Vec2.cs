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
    public static float Deg2Rad(float f)
    {
        return f * (Mathf.PI / 180f);

    }
    public static float Rad2Deg(float f)
    {
        return f * (180f / Mathf.PI);
    }
    public static Vec2 GetUnitVectorDeg(float f)
    {
        float radian = Deg2Rad(f);
        Vec2 output = new Vec2(Mathf.Cos(radian), Mathf.Sin(radian));
        return output;
    }
    public static Vec2 GetUnitVectorRad(float f)
    {
        Vec2 output = new Vec2(Mathf.Cos(f), Mathf.Sin(f));
        return output;
    }
    public void SetAngleDegrees(float f)
    {
        f = Deg2Rad(f);
        float l = Length();
        x = Mathf.Cos(f);
        y = Mathf.Sin(f);
        Normalize();
        this = this * l;
    }
    public void SetAngleRadians(float f)
    {
        float l = Length();
        x = Mathf.Cos(f);
        y = Mathf.Sin(f);
        Normalize();
        this = this * l;
    }
    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }
    public float GetAngleDegrees()
    {
        return Rad2Deg(Mathf.Atan2(y, x));
    }
    public void RotateDegrees(float f)
    {
        f = Deg2Rad(f);
        float xo = x;
        x = x * Mathf.Cos(f) - y * Mathf.Sin(f);
        y = xo * Mathf.Sin(f) + y * Mathf.Cos(f);
    }
    public void RotateRadians(float f)
    {
        float xo = x;
        x = x * Mathf.Cos(f) - y * Mathf.Sin(f);
        y = xo * Mathf.Sin(f) + y * Mathf.Cos(f);
    }
    public void RotateAroundDegrees(Vec2 point, float angle)
    {
        Vec2 dist = this - point;
        dist.RotateDegrees(angle);
        dist += point;
        this = dist;
    }
    public void RotateAroundRadians(Vec2 point, float angle)
    {
        RotateAroundDegrees(point, Rad2Deg(angle));
    }
}
