using System;
using GXPEngine;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Vec2 Zero()
    {
        this.x = 0;
        this.y = 0;
        return this;
    }

    public Vec2 Reverse()
    {
        this.x *= -1;
        this.y *= -1;
        return this;
    }

    public Vec2 Reversed()
    {
        Vec2 v = new Vec2(this.x, this.y);
        v *= -1;
        return v;
    }

    public Vec2 SetXY(float x, float y)
    {
        this.x = x;
        this.y = y;
        return this;
    }

    public void Normalize()
    {
        if (this == 0) return;
        this /= Length();
    }

    public Vec2 Normalized()
    {
        if (this == 0) return this;
        Vec2 v = new Vec2(this.x, this.y);
        return v /= Length();
    }

    public float Length()
    {
        return (float)Math.Sqrt(this.x * this.x + this.y * this.y);
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

    public static Vec2 operator +(Vec2 v, Vec2 v1)
    {
        v.x += v1.x;
        v.y += v1.y;
        return v;
    }

    public static Vec2 operator +(Vec2 v, float f)
    {
        v.x += f;
        v.y += f;
        return v;
    }

    public static Vec2 operator -(Vec2 v, Vec2 v1)
    {
        v.x -= v1.x;
        v.y -= v1.y;
        return v;
    }

    public static Vec2 operator -(Vec2 v, int i)
    {
        v.x -= i;
        v.y -= i;
        return v;
    }

    public static Vec2 operator *(Vec2 v, float f)
    {
        v.x *= f;
        v.y *= f;
        return v;
    }

    public static Vec2 operator *(Vec2 v, int i)
    {
        v.x *= i;
        v.y *= i;
        return v;
    }

    public static Vec2 operator *(int i, Vec2 v)
    {
        v.x *= i;
        v.y *= i;
        return v;
    }

    public static Vec2 operator *(Vec2 v, Vec2 v1)
    {
        v.x *= v1.x;
        v.y *= v1.y;
        return v;
    }

    public static Vec2 operator /(Vec2 v, float f)
    {
        v.x /= f;
        v.y /= f;
        return v;
    }

    public static Vec2 operator /(Vec2 v, Vec2 v1)
    {
        v.x /= v1.x;
        v.y /= v1.y;
        if (v1.x == 0) v.x = 0;
        if (v1.y == 0) v.y = 0;
        return v;
    }

    public static bool operator ==(Vec2 v, Vec2 v1)
    {
        return v.x == v1.x && v.y == v1.y;
    }

    public static bool operator ==(Vec2 v, float f)
    {
        return v.x == f && v.y == f;
    }

    public static bool operator !=(Vec2 v, float f)
    {
        return v.x != f || v.y != f;
    }

    public static bool operator !=(Vec2 v, Vec2 v1)
    {
        return v.x != v1.x || v.y != v1.y;
    }

    public void SetAngleDegrees(float degrees)
    {
        this = GetUnitVectorDeg(degrees) * Length();
    }

    public void SetAngleRadians(float radians)
    {
        this = GetUnitVectorRad(radians) * Length();
    }

    public void RotateAroundDegrees(Vec2 offset, float degrees)
    {
        this -= offset;
        RotateDegrees(degrees);
        this += offset;
    }
    public void RotateAroundRadians(Vec2 offset, float degrees)
    {
        this -= offset;
        RotateRadians(degrees);
        this += offset;
    }

    public void RotateDegrees(float degrees)
    {
        float radians = Deg2Rad(degrees);
        SetXY(x * Mathf.Cos(radians) - y * Mathf.Sin(radians),
              x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
    }

    public void RotateRadians(float radians)
    {
        SetXY(x * Mathf.Cos(radians) - y * Mathf.Sin(radians),
              x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
    }

    public float GetAngleRadians()
    {
        float radians = Mathf.Atan2(this.y, this.x);
        if (radians < 0) radians += 2 * Mathf.PI;
        return radians;
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }

    public static float Deg2Rad(float degrees)
    {
        return degrees * Mathf.PI / 180;
    }

    public static float Rad2Deg(float degrees)
    {
        return degrees * 180 / Mathf.PI;
    }

    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        degrees = Deg2Rad(degrees);
        return new Vec2(Mathf.Cos(degrees), Mathf.Sin(degrees));
    }

    public static Vec2 GetUnitVectorRad(float radians)
    {
        return new Vec2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public static Vec2 RandomUnitVector()
    {
        return GetUnitVectorDeg(Utils.Random(0, 360));
    }

    public override bool Equals(Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Vec2 v = (Vec2)obj;
            return (x == v.x) && (y == v.y);
        }
    }

    public override int GetHashCode()
    {
        return new { x, y }.GetHashCode();
    }

    override public string ToString()
    {
        return "[Vector2 " + x + ((x % 1) == 0 && (y % 1) == 0 ? ", " : "; ") + y + "]";
    }
}
