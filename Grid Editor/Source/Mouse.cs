using System.Drawing;

namespace PathfindingFun
{
    /// <summary>
    /// Simple struct for simplicity and readability
    /// </summary>
    public class Mouse
    {
        public Point Local;
        public Point Grid;
        public Point LastPoint;
        public bool Moved;

        public Mouse()
        {
            Local = new Point(0, 0);
            Grid = new Point(0, 0);
            LastPoint = new Point(-1, -1);
            Moved = false;
        }

        public string LocalSpaceString()
        {
            return string.Format("X: {0} , Y: {1}", Local.X, Local.Y);
        }

        public string GridSpaceString()
        {
            return string.Format("X: {0} , Y: {1}", Grid.X, Grid.Y);
        }
    }
}