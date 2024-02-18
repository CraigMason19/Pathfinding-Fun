using System;
using System.Linq;
using System.Drawing;

namespace PathfindingFun
{
    class MazeNode
    {
        public int _X { get; set; }
        public int _Y { get; set; }
        public char _Direction { get; set; }

        public MazeNode()
        {
            _X = 0;
            _Y = 0;
            _Direction = '0';
        }

        public MazeNode(int x, int y, char dir = '0')
        {
            _X = x;
            _Y = y;
            _Direction = dir;
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
                return n._X == this._X && n._Y == this._Y;
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
                    if (searchGrid[current._X + _Offset[i].X, current._Y + _Offset[i].Y]._Walkable)
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
            return _X.GetHashCode();
        }
    }
}