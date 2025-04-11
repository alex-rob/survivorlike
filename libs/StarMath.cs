using System;

namespace Survivorlike.libs;

public static class StarMath
{
    public static double Csc(float radAng)
    {
        return 1 / Math.Sin(radAng);
    }

    public static float DegToRad(float deg)
    {
        return (float)(deg * Math.PI / 180);
    }

    public static float RadToDeg(float rad)
    {
        return (float)(rad * 180 / Math.PI);
    }
}