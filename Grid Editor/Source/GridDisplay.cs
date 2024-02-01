using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathfindingFun
{
    /// <summary>
    /// Class responsible for drawing the grid.
    /// </summary>
    class GridDisplay
    {
        public Size Dimensions { get; set; }
        public Size CellSize { get; set; }
        public Point Offset { get; set; }

        public GridDisplay()
        {
            Dimensions = new Size(1, 1);
            CellSize = new Size(10, 10);
            Offset = new Point(0, 0);
        }

        public Point GetSquareScreenPosition(Point square)
        {
            int x = ((CellSize.Width * square.X) + Offset.X);
            int y = ((CellSize.Height * square.Y) + Offset.Y);
            return new Point(x, y);
        }

        public void ColourSquare(Graphics g, Point square, Color colour)
        {
            g.FillRectangle(new SolidBrush(colour), new Rectangle(GetSquareScreenPosition(square), CellSize));
        }

        public void DrawCosts(Graphics graph, Point square, int g, float h)
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

        public virtual void Draw(Graphics Graf, Point mouse)
        {
            Pen pencil = new Pen(Color.CornflowerBlue, 2f);
            pencil.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            pencil.DashCap = System.Drawing.Drawing2D.DashCap.Triangle;

            Point startP = new Point();
            Point endP = new Point();
            // Draw horizontals
            startP.X = Offset.X;
            endP.X = Offset.X + Dimensions.Width * CellSize.Width;
            for (int i = 0; i <= Dimensions.Height; i++)
            {
                startP.Y = Offset.Y + i * CellSize.Height;
                endP.Y = startP.Y;
                Graf.DrawLine(pencil, startP, endP);
            }

            // Draw verticals
            startP.Y = Offset.Y;
            endP.Y = Offset.Y + Dimensions.Height * CellSize.Height;
            for (int i = 0; i <= Dimensions.Width; i++)
            {
                startP.X = Offset.X + i * CellSize.Width;
                endP.X = startP.X;
                Graf.DrawLine(pencil, startP, endP);
            }
        }
    }
}




    