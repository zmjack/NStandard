﻿// <auto-generated/>
namespace NStandard;

public static partial class MathEx
{
    /// <summary>
    /// Rounds a number up, to the nearest multiple of significance.
    /// </summary>
    /// <param name="n"> The value that you want to round. </param>
    /// <param name="significance"> The multiple to which you want to round. </param>
    /// <param name="mode"> When given a nonzero number, this function will round towards zero. </param>
    /// <returns></returns>
    public static int Ceiling(int n, int significance, bool mode = false)
    {
        if (n == 0) return 0;
        if (significance == 0) return 0;
        if (significance < 0) significance -= significance;
        
        var mod = n % significance;
        if (mod == 0) return n;

        var times = (int)(n / significance);
        if (n > 0) return significance * (times + 1);
        else
        {
            if (mode) return significance * (times - 1);
            else return significance * times;
        }
    }

    /// <summary>
    /// Rounds a number up, to the nearest multiple of significance.
    /// </summary>
    /// <param name="n"> The value that you want to round. </param>
    /// <param name="significance"> The multiple to which you want to round. </param>
    /// <param name="mode"> When given a nonzero number, this function will round towards zero. </param>
    /// <returns></returns>
    public static decimal Ceiling(decimal n, decimal significance, bool mode = false)
    {
        if (n == 0m) return 0m;
        if (significance == 0m) return 0m;
        if (significance < 0m) significance -= significance;
        
        var mod = n % significance;
        if (mod == 0) return n;

        var times = (int)(n / significance);
        if (n > 0m) return significance * (times + 1m);
        else
        {
            if (mode) return significance * (times - 1m);
            else return significance * times;
        }
    }

    /// <summary>
    /// Rounds a number up, to the nearest multiple of significance.
    /// </summary>
    /// <param name="n"> The value that you want to round. </param>
    /// <param name="significance"> The multiple to which you want to round. </param>
    /// <param name="mode"> When given a nonzero number, this function will round towards zero. </param>
    /// <returns></returns>
    public static double Ceiling(double n, double significance, bool mode = false)
    {        
        if (double.IsInfinity(n) || double.IsNaN(n)) return double.NaN;
        if (double.IsInfinity(significance) || double.IsNaN(significance)) return double.NaN;
        
        return (double)Ceiling((decimal)n, (decimal)significance, mode);
    }
}

