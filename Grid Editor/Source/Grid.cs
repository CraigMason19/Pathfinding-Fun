using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathfindingFun
{
    /// <summary>
    /// Class responsible for drawing the grid.
    /// </summary>
    class Grid
    {
        public int HorizontalCells { get; set; }
        public int VerticalCells { get; set; }
        public Point Offset { get; set; }
        public Size CellSize { get; set; }        

        public Grid()
        {
            // Set some defaults
            Offset = new Point(0, 0);
            CellSize = new Size(10, 10);
            HorizontalCells = 1;
            VerticalCells = 1;

        }

        public virtual Point GetSquareScreenPosition(Point square)
        {
            int x = ((CellSize.Width * square.X) + Offset.X);
            int y = ((CellSize.Height * square.Y) + Offset.Y);
            return new Point(x, y);
        }

        public virtual void ColourSquare(Graphics g, Point square, Color colour)
        {
            g.FillRectangle(new SolidBrush(colour), new Rectangle(GetSquareScreenPosition(square), CellSize));
        }

        public virtual void ColourSquare(Graphics g, List<SearchNode> l, Color colour)
        {
            foreach (SearchNode n in l)
            {
                g.FillRectangle(new SolidBrush(colour), new Rectangle(GetSquareScreenPosition(n._Pos), CellSize));
            }
        }

        public virtual void DrawCosts(Graphics graph, Point square, int g, float h)
        {
            Font drawFont = new Font("Arial", 6);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            Point tmp = GetSquareScreenPosition(square);

            // F
            String drawString = (g+(int)h).ToString();
            Point drawPoint = tmp;
            graph.DrawString(drawString, drawFont, drawBrush, drawPoint);

            // G
            drawString = g.ToString();
            drawPoint = tmp;
            drawPoint.Y += CellSize.Height - 10;
            graph.DrawString(drawString, drawFont, drawBrush, drawPoint);

            // H
            drawString = ((int)h).ToString();
            drawPoint = tmp;
            drawPoint.X += CellSize.Width - 15;
            drawPoint.Y += CellSize.Height - 10;
            graph.DrawString(drawString, drawFont, drawBrush, drawPoint);
        }

        public virtual void DrawCosts(Graphics graph, List<SearchNode> l)
        {
            Font drawFont = new Font("Arial", 6);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            foreach (SearchNode n in l)
            {
                int g = n._G;
                int h = (int)n._H;

                Point tmp = GetSquareScreenPosition(n._Pos);

                // F
                String drawString = (g + h).ToString();
                Point drawPoint = tmp;
                graph.DrawString(drawString, drawFont, drawBrush, drawPoint);

                // G
                drawString = g.ToString();
                drawPoint = tmp;
                drawPoint.Y += CellSize.Height - 10;
                graph.DrawString(drawString, drawFont, drawBrush, drawPoint);

                // H
                drawString = h.ToString();
                drawPoint = tmp;
                drawPoint.X += CellSize.Width - 15;
                drawPoint.Y += CellSize.Height - 10;
                graph.DrawString(drawString, drawFont, drawBrush, drawPoint);
            }
        }

        public virtual void Draw(Graphics Graf, Point mouse)
        {
            Pen pencil = new Pen(Color.CornflowerBlue, 2f);
            pencil.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            pencil.DashCap = System.Drawing.Drawing2D.DashCap.Triangle;

            Point startP = new Point();
            Point endP = new Point();
            // Draw horizontals
            startP.X = Offset.X;
            endP.X = Offset.X + HorizontalCells * CellSize.Width;
            for (int i = 0; i <= VerticalCells; i++)
            {
                startP.Y = Offset.Y + i * CellSize.Height;
                endP.Y = startP.Y;
                Graf.DrawLine(pencil, startP, endP);
            }

            // Draw verticals
            startP.Y = Offset.Y;
            endP.Y = Offset.Y + VerticalCells * CellSize.Height;
            for (int i = 0; i <= HorizontalCells; i++)
            {
                startP.X = Offset.X + i * CellSize.Width;
                endP.X = startP.X;
                Graf.DrawLine(pencil, startP, endP);
            }
        }
    }
}




    