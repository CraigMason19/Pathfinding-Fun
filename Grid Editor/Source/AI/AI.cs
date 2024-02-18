using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PathfindingFun
{
    class AI
    {
        // for square
        public static Point[] _CardinalOffset = { new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) };
        public static Point[] _OrdinalOffset = { new Point(1, 0), new Point(1, 1), new Point(0, 1), new Point(-1, 1),
                                                 new Point(-1, 0), new Point(-1, -1), new Point(0, -1), new Point(1, -1)};

        // Cardinal = N E S W
        static public string[] GetCardinalKeys(SearchNode[,] searchGrid, SearchNode current)
        {
            string[] keys = new string[] { "N", "E", "S", "W" };
 
            foreach(int i in Enumerable.Range(0,4))
            {
                try
                {
                    if (!searchGrid[current.Pos.X + _CardinalOffset[i].X, current.Pos.Y + _CardinalOffset[i].Y].Walkable)
                    {
                        keys[i] = "";
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    keys[i] = "";
                }
            }

            return keys.Where(key => key != "").ToArray();
        }

        // Cardinal = N E S W, Ordinal = NE SE SW NE
        static public string[] GetCardinalAndOrdinalKeys(SearchNode[,] searchGrid, SearchNode current)
        {
            string[] keys = new string[] { "E", "SE", "S", "SW", "W", "NW", "N", "NE" };
            Point[] _OrdinalOffset = { new Point(1, 0), new Point(1, 1), new Point(0, 1), new Point(-1, 1),
                                       new Point(-1, 0), new Point(-1, -1), new Point(0, -1), new Point(1, -1)};

            foreach (int i in Enumerable.Range(0, 8))
            {
                try
                {
                    if (!searchGrid[current.Pos.X + _OrdinalOffset[i].X, current.Pos.Y + _OrdinalOffset[i].Y].Walkable)
                    {
                        keys[i] = "";
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    keys[i] = "";
                }
            }

            // Don't cut corners
            if(keys[0] != "E")
            {
                keys[0] = keys[1] = keys[7] = "";
            }
            if (keys[2] != "S")
            {
                keys[2] = keys[1] = keys[3] = "";
            }
            if (keys[4] != "W")
            {
                keys[4] = keys[3] = keys[5] = "";
            }
            if (keys[6] != "N")
            {
                keys[6] = keys[5] = keys[7] = "";
            }

            return keys.Where(key => key != "").ToArray();
        }

        static public float ManhattanDistance(Point node, Point end, float d1)
        {
            float dx = (float)Math.Abs(node.X - end.X);
            float dy = (float)Math.Abs(node.Y - end.Y);
            return d1 * (dx + dy);
        }

        static public float DiagonalDistance(Point node, Point end, float d1, float d2)
        {
            float dx = (float)Math.Abs(node.X - end.X);
            float dy = (float)Math.Abs(node.Y - end.Y);
            return d1 * (dx + dy) + (d2 - 2.0f * d1) * Math.Min(dx, dy);
        }

        static public float EuclideanDistance(Point node, Point end, float d1)
        {
            float dx = (float)Math.Abs(node.X - end.X);
            float dy = (float)Math.Abs(node.Y - end.Y);
            return d1 * (float)Math.Sqrt(dx * dx + dy * dy);
        }

        // Tie breaker that using a vector cross product; prefers paths that are along the straight line from
        // the starting point to the goal
        static public float GetTieBreakerScale(Point node, Point start, Point end)
        {
            float dx1 = (float)node.X - end.X;
            float dy1 = (float)node.Y - end.Y;
            float dx2 = (float)start.X - end.X;
            float dy2 = (float)start.Y - end.Y;
            float cross = Math.Abs(dx1 * dy2 - dx2 * dy1);
            return cross * 0.001f;
        }
    }
}
