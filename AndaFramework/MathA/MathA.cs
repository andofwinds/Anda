using OpenTK.Mathematics;

using System;

namespace AndaFramework.MathA;

public class MathA
{
    public static double Magnitude(Vector3 v)
    {
        return Math.Sqrt(
            Math.Pow(v.X, 2) + Math.Pow(v.Y, 2) + Math.Pow(v.Z, 2));
    }
}