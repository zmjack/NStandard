using System;
using System.ComponentModel;

namespace NStandard;

public static partial class MathEx
{
    /// <summary>
    /// Compares two floats if they are similar.
    /// </summary>
    /// <param name="number">One of the numbers to compare.</param>
    /// <param name="other">The other number to compare.</param>
    /// <returns></returns>
    public static bool Approximately(float number, float other)
    {
        return Math.Abs(other - number) < Math.Max(1E-06f * Math.Max(Math.Abs(number), Math.Abs(other)), float.Epsilon * 8);
    }

    /// <summary>
    /// Compares two doubles if they are similar.
    /// </summary>
    /// <param name="number">One of the numbers to compare.</param>
    /// <param name="other">The other number to compare.</param>
    public static bool Approximately(double number, double other)
    {
        return Math.Abs(other - number) < Math.Max(1E-15d * Math.Max(Math.Abs(number), Math.Abs(other)), double.Epsilon * 8);
    }

    /// <summary>
    /// Compares two floats if they are similar.
    /// </summary>
    /// <param name="number">One of the numbers to compare.</param>
    /// <param name="other">The other number to compare.</param>
    /// <param name="tolerance">The amount of tolerance to allow while still considering the numbers approximately equal.</param>
    /// <returns></returns>
    public static bool Approximately(float number, float other, float tolerance)
    {
        if (tolerance < 0) throw new ArgumentException("The tolerance can not be negative.", nameof(tolerance));
        return Math.Abs(other - number) < tolerance;
    }

    /// <summary>
    /// Compares two doubles if they are similar.
    /// </summary>
    /// <param name="number">One of the numbers to compare.</param>
    /// <param name="other">The other number to compare.</param>
    /// <param name="tolerance">The amount of tolerance to allow while still considering the numbers approximately equal.</param>
    /// <returns></returns>
    public static bool Approximately(double number, double other, double tolerance)
    {
        if (tolerance < 0) throw new ArgumentException("The tolerance can not be negative.", nameof(tolerance));
        return Math.Abs(other - number) < tolerance;
    }
}
