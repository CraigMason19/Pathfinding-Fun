﻿using System.Drawing;

namespace PathfindingFun
{
    class ProjectColors
    {
        public static readonly Color GridLines = Color.LightGray;
        public static readonly Color Text = Color.Black;
        public static readonly Color Wall = Color.CornflowerBlue;
        public static readonly Color Clear = Color.White;

        // Nodes
        public static readonly Color StartNode = Color.LimeGreen;
        public static readonly Color EndNode = Color.Red;
        public static readonly Color ConsideredNode = Color.PaleGreen;
        public static readonly Color Path = Color.Yellow;
    }
}