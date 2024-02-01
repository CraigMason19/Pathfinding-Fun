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
        public int CellSize { get; set; }
        public Point Offset { get; set; }

        public GridDisplay()
        {
            Dimensions = new Size(1, 1);
            CellSize = 10;
            Offset = new Point(0, 0);
        }

        public Point GetSquareScreenPosition(Point square)
        {
            int x = ((CellSize * square.X) + Offset.X);
            int y = ((CellSize * square.Y) + Offset.Y);
            return new Point(x, y);
        }

        public void ColourSquare(Graphics g, Point square, Color colour)
        {
            g.FillRectangle(new SolidBrush(colour), new Rectangle(GetSquareScreenPosition(square), new Size(CellSize, CellSize)));
        }

        public virtual void Draw(Graphics Graf, Point mouse)
        {
            Pen pencil = new Pen(ProjectColors.GridLines, 2f);
            pencil.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            pencil.DashCap = System.Drawing.Drawing2D.DashCap.Triangle;

            Point startP = new Point();
            Point endP = new Point();

            // Draw horizontals
            startP.X = Offset.X;
            endP.X = Offset.X + Dimensions.Width * CellSize;
            for (int i = 0; i <= Dimensions.Height; i++)
            {
                startP.Y = Offset.Y + i * CellSize;
                endP.Y = startP.Y;
                Graf.DrawLine(pencil, startP, endP);
            }

            // Draw verticals
            startP.Y = Offset.Y;
            endP.Y = Offset.Y + Dimensions.Height * CellSize;
            for (int i = 0; i <= Dimensions.Width; i++)
            {
                startP.X = Offset.X + i * CellSize;
                endP.X = startP.X;
                Graf.DrawLine(pencil, startP, endP);
            }
        }

        public void DrawCosts(Graphics graph, Point square, int g, float h)
        {
            Font drawFont = new Font("Arial", 6, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(ProjectColors.Text);

            Point tmp = GetSquareScreenPosition(square);

            // F
            String drawString = (g+(int)h).ToString();
            Point drawPoint = tmp;
            graph.DrawString(drawString, drawFont, drawBrush, drawPoint);

            // G
            drawString = g.ToString();
            drawPoint = tmp;
            drawPoint.Y += CellSize - 10;
            graph.DrawString(drawString, drawFont, drawBrush, drawPoint);

            // H
            drawString = ((int)h).ToString();
            drawPoint = tmp;
            drawPoint.X += CellSize - 15;
            drawPoint.Y += CellSize - 10;
            graph.DrawString(drawString, drawFont, drawBrush, drawPoint);
        }
    }
}