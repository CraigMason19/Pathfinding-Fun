using System;

namespace PathfindingFun
{
    class Helper
    {
        static public bool IsInRange<T>(T min, T max, T x) where T : IComparable<T>
        {
            return (x.CompareTo(min) >= 0) && (x.CompareTo(max) <= 0);
        }
    }
}