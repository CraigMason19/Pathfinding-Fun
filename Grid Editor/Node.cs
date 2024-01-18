using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PathfindingFun
{
    // Needed for a HashSet
    class CustomNodeComparer : IEqualityComparer<SearchNode>
    {
        public bool Equals(SearchNode x, SearchNode y)
        {
            return ((x._Pos.X == y._Pos.X) && (x._Pos.Y == y._Pos.Y));
        }

        public int GetHashCode(SearchNode obj)
        {
            return obj._Pos.GetHashCode();
        }
    }

    class SearchNode : IComparable, IComparable<SearchNode>
    {
        public static SearchNode OutOfIndexNode = new SearchNode(-1, -1);
        public static SearchNode OriginNode = new SearchNode();

        public Point _Pos { get; set; }
        public int _G { get; set; }
        public float _H { get; set; }
        public Point _Parent { get; set; }
        public bool _Walkable { get; set; }   

        public SearchNode()
        {
            _Pos = new Point(0,0);
            _G = 0;
            _H = 0.0f;
            _Parent = _Pos;
            _Walkable = false;
        }

        public SearchNode(Point pos, int g = 0)
        {
            _Pos = pos;
            _G = g;
            _H = 0.0f; 
            _Parent = new Point(0,0);
            _Walkable = true;
        }

        public SearchNode(int x, int y, int g = 0)
        {
            _Pos = new Point(x, y);
            _G = g;
            _H = 0.0f; 
            _Parent = new Point(0, 0);
            _Walkable = true;
        }

        // Useful operators
        public static SearchNode operator+(SearchNode left, SearchNode right)
        {
            return new SearchNode(new Point(left._Pos.X + right._Pos.X, left._Pos.Y + right._Pos.Y), left._G + right._G);
        }

        public static bool operator==(SearchNode a, SearchNode b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a._Pos.X == b._Pos.X && a._Pos.Y == b._Pos.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is SearchNode)
            {
                SearchNode n = obj as SearchNode;
                return n._Pos.X == this._Pos.X && n._Pos.Y == this._Pos.Y;
            }

            return false;
        }

        public static bool operator !=(SearchNode a, SearchNode b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return(String.Format("[{0},{1}]", _Pos.X, _Pos.Y));
        }

        public Point ToPoint()
        {
            return new Point(_Pos.X, _Pos.Y);
        }

        public int CompareTo(SearchNode b)
        {
            return (_H + (float)_G).CompareTo(b._H + (float)b._G);
        }

        public int CompareTo(object obj)
        {
            var other = obj as SearchNode;
            return this.CompareTo(other);
        }

        public override int GetHashCode()
        {
            return _Pos.GetHashCode();
        }
    }

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