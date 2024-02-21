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
            return (a.Pos.X == b.Pos.X) && (a.Pos.Y == b.Pos.Y);
        }

        public int GetHashCode(SearchNode obj)
        {
            return obj.Pos.GetHashCode();
        }
    }

    /// <summary>
    /// A class representing a node in the grid
    /// </summary>
    class SearchNode : IComparable, IComparable<SearchNode>
    {
        public static SearchNode OutOfIndexNode = new SearchNode(-1, -1);
        public static SearchNode OriginNode = new SearchNode();

        public Point Pos { get; set; }
        public int G { get; set; }
        public float H { get; set; }
        public Point Parent { get; set; }
        public bool Walkable { get; set; }   

        public SearchNode()
        {
            Pos = new Point(0, 0);
            G = 0;
            H = 0.0f;
            Parent = new Point(0, 0);
            Walkable = true;
        }

        public SearchNode(Point pos, int g = 0)
        {
            Pos = pos;
            G = g;
            H = 0.0f; 
            Parent = new Point(0, 0);
            Walkable = true;
        }

        public SearchNode(int x, int y, int g = 0)
        {
            Pos = new Point(x, y);
            G = g;
            H = 0.0f; 
            Parent = new Point(0, 0);
            Walkable = true;
        }

        public static SearchNode operator+(SearchNode a, SearchNode b)
        {
            return new SearchNode(new Point(a.Pos.X + b.Pos.X, a.Pos.Y + b.Pos.Y), a.G + b.G);
        }

        public static bool operator==(SearchNode a, SearchNode b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (a is null || b is null)
            {
                return false;
            }

            return a.Pos.X == b.Pos.X && a.Pos.Y == b.Pos.Y;
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
                return n.Pos.X == this.Pos.X && n.Pos.Y == this.Pos.Y;
            }

            return false;
        }

        public static bool operator !=(SearchNode a, SearchNode b)
        {
            return !(a == b);
        }

        public int CompareTo(SearchNode b)
        {
            return (H + (float)G).CompareTo(b.H + (float)b.G);
        }

        public int CompareTo(object obj)
        {
            var other = obj as SearchNode;
            return this.CompareTo(other);
        }

        public override int GetHashCode()
        {
            return Pos.GetHashCode();
        }

        public override string ToString()
        {
            return (String.Format("[{0},{1}]", Pos.X, Pos.Y));
        }
    }
}