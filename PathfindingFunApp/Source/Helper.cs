using System;

namespace PathfindingFun
{
    /// <summary>
    /// Custom functions definitions across the project 
    /// </summary>
    class Helper
    {
        static public bool IsInRange<T>(T min, T max, T x) where T : IComparable<T>
        {
            return (x.CompareTo(min) >= 0) && (x.CompareTo(max) <= 0);
        }
    }
}