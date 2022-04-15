using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
struct Vec2
{
    float _x;
    public float x
    {
        get
        {
            return _x;
        }
    }
    float _y; 
    public float y
    {
        get
        {
            return _y;
        }
    }
    public Vec2(float pX,float pY)
    {
        _x = pX;
        _y = pY;
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
        return Mathf.Sqrt((_x * _x) + (_y * _y));
    }
    public void SetXY(float pX, float pY)
    {
        _x = pX;
        _y = pY;
    }
    public void Normalize()
    {
        float l = Length();
        _x /= l;
        _y /= l;
    }
    public Vec2 Normalized()
    {
        Vec2 vec2 = this;
        vec2.Normalize();
        return vec2;
    }
}
