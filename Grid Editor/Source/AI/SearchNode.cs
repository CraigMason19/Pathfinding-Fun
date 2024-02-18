using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathfindingFun
{
    /// <summary>
    /// A custom equality comparer for search nodes. Needed because of the 'closed list' HashSet
    /// </summary>
    class SearchNodeComparer : IEqualityComparer<SearchNode>
    {
        public bool Equals(SearchNode a, SearchNode b)
        {
            return (a._Pos.X == b._Pos.X) && (a._Pos.Y == b._Pos.Y);
        }

        public int GetHashCode(SearchNode obj)
        {
            return obj._Pos.GetHashCode();
        }
    }

    /// <summary>
    /// A class representing a node in the grid
    /// </summary>
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
            _Walkable = true;
        }

        public SearchNode(Point pos, int g = 0)
        {
            _Pos = pos;
            _G = g;
            _H = 0.0f; 
            _Parent = new Point(0, 0);
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

        public static SearchNode operator+(SearchNode a, SearchNode b)
        {
            return new SearchNode(new Point(a._Pos.X + b._Pos.X, a._Pos.Y + b._Pos.Y), a._G + b._G);
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
}