using System;
using System.Linq;
using System.Drawing;

namespace PathfindingFun
{
    class MazeNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Direction { get; set; }

        public MazeNode()
        {
            X = 0;
            Y = 0;
            Direction = '0';
        }

        public MazeNode(int x, int y, char dir = '0')
        {
            X = x;
            Y = y;
            Direction = dir;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is MazeNode)
            {
                MazeNode n = obj as MazeNode;
                return n.X == this.X && n.Y == this.Y;
            }

            return false;
        }

        public string[] GetNeighbours(SearchNode[,] searchGrid, MazeNode current)
        {
            string[] keys = new string[] { "N", "E", "S", "W" };

            Point[] _Offset = { new Point(0, -2), new Point(2, 0), new Point(0, 2), new Point(-2, 0) };

            foreach (int i in Enumerable.Range(0, 4))
            {
                try
                {
                    if (searchGrid[current.X + _Offset[i].X, current.Y + _Offset[i].Y].Walkable)
                    {
                        keys[i] = "";
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    keys[i] = "";
                }
            }

            return keys;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode();
        }
    }
}