using Microsoft.Xna.Framework;
using System;

namespace RapidMonoDesktop.Helpers;

static class MHelper
{
    public static double Vector2Distance(Vector2 a, Vector2 b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }

    internal static double Vector2Distance(Rectangle a, Vector2 b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}