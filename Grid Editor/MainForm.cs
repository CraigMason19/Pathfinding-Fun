﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PathfindingFun;

// TODO
// right click then left click bug
// private variables have underscore
// search nodes have parent nodes

// Simply struct for simplicity and readability
public struct Mouse
{
    public Point Local;
    public Point Grid;
    public Point LastPoint;
    public bool Moved;

    public string LocalSpaceString()
    {
        return string.Format("X: {0} , Y: {1}", Local.X, Local.Y);
    }

    public string GridSpaceString()
    {
        return string.Format("X: {0} , Y: {1}", Grid.X, Grid.Y);
    }
}

namespace PathfindingFun
{

    public partial class MainForm : Form
    {
        const KnownColor StartNodeColor = KnownColor.LimeGreen;
        const KnownColor EndNodeColor = KnownColor.Red;
        const KnownColor ConsideredNodeColor = KnownColor.PaleGreen;
        const KnownColor PathColor = KnownColor.Yellow;

        Mouse M;

        Grid LevelMap;
        Size SmallGridSize;
        Size LargeGridSize;
        Size SmallPanelSize;
         
        int LastNumericUpDown;

        // Search nodes
        SearchNode StartSearchNode;
        SearchNode EndSearchNode;
        Dictionary<string, SearchNode> OffsetSearchNodes;
        BinaryHeap<SearchNode> OpenHeap; // Faster as we don't constantly sort a list 
        HashSet<SearchNode> ClosedHash;  // Lookup for HashSet is O(1) and List is O(N)
                                         // Could always just add a boolean to the search node to show if it is on the closed list

        SearchNode[,] PathfindingGrid;


        public MainForm()
        {
            InitializeComponent();
            SmallGridSize = new Size(30, 30);
            LastNumericUpDown = Convert.ToInt32(GridSizeUpDown.Value);
            LargeGridSize = new Size(Convert.ToInt32(GridSizeUpDown.Value), Convert.ToInt32(GridSizeUpDown.Value));
            SmallPanelSize = new Size(600, 600);

            LevelMap = new Grid();
            LevelMap.CellSize = SmallGridSize;
            LevelMap.HorizontalCells = 20;
            LevelMap.VerticalCells = 20;

            M = new Mouse();
            M.Local = new Point(0, 0);
            M.Grid = new Point(0, 0);
            M.LastPoint = new Point(-1, -1);
            M.Moved = false;

            HeuristicComboBox.SelectedIndex = 0;

            // SearchNode setup
            StartSearchNode = SearchNode.OutOfIndexNode;
            EndSearchNode = SearchNode.OutOfIndexNode;

            OffsetSearchNodes = new Dictionary<string, SearchNode>();
            OffsetSearchNodes["S"] = new SearchNode(0, 1, 10);    // Down
            OffsetSearchNodes["SE"] = new SearchNode(1, 1, 14);   // Down Right
            OffsetSearchNodes["E"] = new SearchNode(1, 0, 10);    // Right
            OffsetSearchNodes["NE"] = new SearchNode(1, -1, 14);  // Up Right
            OffsetSearchNodes["N"] = new SearchNode(0, -1, 10);   // Up
            OffsetSearchNodes["NW"] = new SearchNode(-1, -1, 14); // Up Left
            OffsetSearchNodes["W"] = new SearchNode(-1, 0, 10);   // Left
            OffsetSearchNodes["SW"] = new SearchNode(-1, 1, 14);  // Down Left

            OpenHeap = new BinaryHeap<SearchNode>();
            ClosedHash = new HashSet<SearchNode>(new CustomNodeComparer());

            PathfindingGrid = new SearchNode[LevelMap.HorizontalCells, LevelMap.VerticalCells];
            for (int x = 0; x < LevelMap.HorizontalCells; x++)
            {
                for (int y = 0; y < LevelMap.VerticalCells; y++)
                {
                    PathfindingGrid[x, y] = new SearchNode(x, y);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (SmallGridButton.Checked)
            {
                LevelMap.Draw(e.Graphics, M.Local);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //MouseLocal = Panel1.PointToClient(Cursor.Position);
            //MouseScreenTextBox.Text = string.Format("X: {0} , Y: {1}", MouseLocal.X, MouseLocal.Y);

            M.Local = Panel1.PointToClient(Cursor.Position);
            MouseScreenTextBox.Text = M.LocalSpaceString();

            //MouseGrid.X = (MouseLocal.X / LevelMap.CellSize.Width);
            //MouseGrid.Y = (MouseLocal.Y / LevelMap.CellSize.Height);
            //MouseGridTextBox.Text = string.Format("X: {0} , Y: {1}", MouseGrid.X, MouseGrid.Y);

            M.Grid.X = (M.Local.X / LevelMap.CellSize.Width);
            M.Grid.Y = (M.Local.Y / LevelMap.CellSize.Height);
            MouseGridTextBox.Text = M.GridSpaceString();

            //if (MouseGrid != LastPoint)
            //{
            //    MouseMoved = true;
            //}
            //else
            //{
            //    MouseMoved = false;
            //}
            //LastPoint = MouseGrid;

            if (M.Grid != M.LastPoint)
            {
                M.Moved = true;
            }
            else
            {
                M.Moved = false;
            }
            M.LastPoint = M.Grid;

            //if (MouseMoved && e.Button == MouseButtons.Left)
            //{
            //    if (Helper.IsInRange(0, LevelMap.HorizontalCells - 1, MouseGrid.X) &&
            //        Helper.IsInRange(0, LevelMap.VerticalCells - 1, MouseGrid.Y))
            //    {
            //        LevelMap.ColourSquare(Panel1.CreateGraphics(), MouseGrid, Color.CornflowerBlue);
            //        PathfindingGrid[MouseGrid.X, MouseGrid.Y]._Walkable = false;
            //    }
            //}
            if (M.Moved && e.Button == MouseButtons.Left)
            {
                if (Helper.IsInRange(0, LevelMap.HorizontalCells - 1, M.Grid.X) &&
                    Helper.IsInRange(0, LevelMap.VerticalCells - 1, M.Grid.Y))
                {
                    LevelMap.ColourSquare(Panel1.CreateGraphics(), M.Grid, Color.CornflowerBlue);
                    PathfindingGrid[M.Grid.X, M.Grid.Y]._Walkable = false;
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    // Need to draw the first square         
            //    if (Helper.IsInRange(0, LevelMap.HorizontalCells - 1, MouseGrid.X) &&
            //        Helper.IsInRange(0, LevelMap.VerticalCells - 1, MouseGrid.Y))
            //    {
            //        LevelMap.ColourSquare(Panel1.CreateGraphics(), MouseGrid, Color.CornflowerBlue);
            //        PathfindingGrid[MouseGrid.X, MouseGrid.Y]._Walkable = false;
            //    }
            //}
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Need to draw the first square         
                if (Helper.IsInRange(0, LevelMap.HorizontalCells - 1, M.Grid.X) &&
                    Helper.IsInRange(0, LevelMap.VerticalCells - 1, M.Grid.Y))
                {
                    LevelMap.ColourSquare(Panel1.CreateGraphics(), M.Grid, Color.CornflowerBlue);
                    PathfindingGrid[M.Grid.X, M.Grid.Y]._Walkable = false;
                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Add Start SearchNode"));
                m.MenuItems.Add(new MenuItem("Add End SearchNode"));
                m.MenuItems[0].Click += new System.EventHandler(this.StartSearchNode_Click);
                m.MenuItems[1].Click += new System.EventHandler(this.EndSearchNode_Click);
                //m.Show(Panel1, MouseLocal);
                m.Show(Panel1, M.Local);
            }
        }

        private void StartSearchNode_Click(object sender, System.EventArgs e)
        {
            LevelMap.ColourSquare(Panel1.CreateGraphics(), StartSearchNode.ToPoint(), Color.White);
            LevelMap.ColourSquare(Panel1.CreateGraphics(), M.Grid, Color.FromKnownColor(StartNodeColor));
            StartSearchNode = new SearchNode(M.Grid, 0);
            OpenHeap.Clear();
            OpenHeap.Insert(StartSearchNode);
            PathfindingGrid[StartSearchNode._Pos.X, StartSearchNode._Pos.Y]._Walkable = true;
            // Redraw grid otherwise it is ugly
            if (SmallGridButton.Checked)
            {
                LevelMap.Draw(Panel1.CreateGraphics(), M.Local);
            }
        }

        private void EndSearchNode_Click(object sender, System.EventArgs e)
        {
            LevelMap.ColourSquare(Panel1.CreateGraphics(), EndSearchNode.ToPoint(), Color.White);
            LevelMap.ColourSquare(Panel1.CreateGraphics(), M.Grid, Color.FromKnownColor(EndNodeColor));
            EndSearchNode = new SearchNode(M.Grid, 0);
            PathfindingGrid[EndSearchNode._Pos.X, EndSearchNode._Pos.Y]._Walkable = true;
            // Redraw grid otherwise it is ugly TODO
            if (SmallGridButton.Checked)
            {
                LevelMap.Draw(Panel1.CreateGraphics(), M.Local);
            }
        }

        private void RunAI_Click(object sender, EventArgs e)
        {
            SearchNode current = SearchNode.OutOfIndexNode;

            // Have we got a start and a destination?
            if (StartSearchNode != SearchNode.OutOfIndexNode && EndSearchNode != SearchNode.OutOfIndexNode)
            {
                while (!OpenHeap.IsEmpty())
                {
                    current = OpenHeap.Peek(); // Just look at lowest f

                    // Finished!?
                    if (current == EndSearchNode)
                    {
                        // TODO double check why need draw end
                        Point p = PathfindingGrid[current._Pos.X, current._Pos.Y]._Parent;// _Pos;
                        while (!(p == StartSearchNode._Pos))
                        {
                            LevelMap.ColourSquare(Panel1.CreateGraphics(), p, Color.FromKnownColor(PathColor));
                            p = PathfindingGrid[p.X, p.Y]._Parent;
                        }

                        LevelMap.ColourSquare(Panel1.CreateGraphics(), EndSearchNode._Pos, Color.FromKnownColor(EndNodeColor));
                        
                        break;
                    }

                    // Begin pathfinding
                    current = OpenHeap.PopMin(); // Now grab lowest f
                    ClosedHash.Add(current);

                    // Find neighbours we can travel to
                    string[] neighbourKeys = new string[] { };
                    if (!DiagonalCheckBox.Checked)
                    {
                        neighbourKeys = AI.GetCardinalKeys(PathfindingGrid, current);
                    }
                    else
                    {
                        neighbourKeys = AI.GetCardinalAndOrdinalKeys(PathfindingGrid, current);
                    }

                    foreach (string key in neighbourKeys)
                    {
                        SearchNode neighbour = OffsetSearchNodes[key];
                        SearchNode tmp = current + neighbour;

                        int tentativeG = current._G + neighbour._G;

                        if (ClosedHash.Contains(tmp) && tentativeG >= neighbour._G)
                        {
                            continue;
                        }

                        if (!OpenHeap.Contains(tmp) || tentativeG < neighbour._G)
                        {
                            //tmp._Parent = current._Pos;
                            PathfindingGrid[tmp._Pos.X, tmp._Pos.Y]._Parent = current._Pos;
                            tmp._G = tentativeG;

                            // Decide which hueristic to use. We use 10.0f for cardinal directions and 14.0f for ordinal 
                            // directions; the cost to move diagonaly is square 2. 10, 14 is about the right ratio and 
                            // avoids lots of square roots. 
                            switch (HeuristicComboBox.SelectedItem.ToString())
                            {
                                case "Manhatten":
                                    tmp._H = AI.ManhattanDistance(tmp._Pos, EndSearchNode._Pos, 10.0f);
                                    break;

                                case "Diagonal Distance":
                                    tmp._H = AI.DiagonalDistance(tmp._Pos, EndSearchNode._Pos, 10.0f, 14.0f);
                                    break;

                                case "Euclidean Distance":
                                    tmp._H = AI.EuclideanDistance(tmp._Pos, EndSearchNode._Pos, 14.0f);
                                    break;

                                case "None":
                                    tmp._H = 0;
                                    break;

                                default:
                                    break;
                            }

                            // Apply a tie breaker? In some maps there are many equal-length paths and A * may 
                            // explore all of these instead of one. This tie breaker preferes paths that are on the 
                            // straight line from the start to the goal 
                            if (TieBreakerCheckBox.Checked)
                            {
                                tmp._H += AI.GetTieBreakerScale(tmp._Pos, StartSearchNode._Pos, EndSearchNode._Pos);
                            }

                            // Draw considered nodes
                            if (DrawOpenlistCheckBox.Checked)
                            {
                                LevelMap.ColourSquare(Panel1.CreateGraphics(), tmp._Pos, Color.FromKnownColor(ConsideredNodeColor));
                                if (SmallGridButton.Checked) // Drawing costs on large map would be pointless,
                                {                            // also costs are drawn as ints (because it is easier to see)
                                    LevelMap.DrawCosts(Panel1.CreateGraphics(), tmp._Pos, tmp._G, tmp._H);
                                }
                            }

                            if (!OpenHeap.Contains(tmp))
                            {
                                OpenHeap.Insert(tmp);
                            }
                        }
                    }
                }
                
                if (current != EndSearchNode)
                {
                    // No solution found   
                }
            }
        }

        public void Clear()
        {
            StartSearchNode = SearchNode.OutOfIndexNode;
            EndSearchNode = SearchNode.OutOfIndexNode;

            OpenHeap.Clear();
            ClosedHash.Clear();
            Panel1.Invalidate();

            foreach (SearchNode n in PathfindingGrid)
            {
                n._Walkable = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void SmallGridButton_CheckedChanged(object sender, EventArgs e)
        {
            LevelMap = new Grid();
            LevelMap.CellSize = SmallGridSize;
            LevelMap.HorizontalCells = Panel1.Width / SmallGridSize.Width;
            LevelMap.VerticalCells = Panel1.Height / SmallGridSize.Height;

            PathfindingGrid = new SearchNode[LevelMap.HorizontalCells, LevelMap.VerticalCells];
            for (int x = 0; x < LevelMap.HorizontalCells; x++)
            {
                for (int y = 0; y < LevelMap.VerticalCells; y++)
                {
                    PathfindingGrid[x, y] = new SearchNode(x, y);
                }
            }

            Clear();
        }

        private void LargeGridButton_CheckedChanged(object sender, EventArgs e)
        {
            LevelMap = new Grid();
            LevelMap.CellSize = new Size(LargeGridSize.Width, LargeGridSize.Height);
            LevelMap.HorizontalCells = Panel1.Width / LargeGridSize.Width;
            LevelMap.VerticalCells = Panel1.Height / LargeGridSize.Height;

            PathfindingGrid = new SearchNode[LevelMap.HorizontalCells, LevelMap.VerticalCells];
            for (int x = 0; x < LevelMap.HorizontalCells; x++)
            {
                for (int y = 0; y < LevelMap.VerticalCells; y++)
                {
                    PathfindingGrid[x, y] = new SearchNode(x, y);
                }
            }

            Clear();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            Panel1.Refresh();
            LevelMap.ColourSquare(Panel1.CreateGraphics(), StartSearchNode._Pos, Color.FromKnownColor(StartNodeColor));
            LevelMap.ColourSquare(Panel1.CreateGraphics(), EndSearchNode._Pos, Color.FromKnownColor(EndNodeColor));

            for (int x = 0; x < LevelMap.HorizontalCells; x++)
            {
                for (int y = 0; y < LevelMap.VerticalCells; y++)
                {
                    SearchNode tmp = PathfindingGrid[x, y];
                    if (tmp._Walkable == false)
                    {
                        LevelMap.ColourSquare(Panel1.CreateGraphics(), tmp._Pos, Color.CornflowerBlue);
                    }
                }
            }

            OpenHeap.Clear();
            OpenHeap.Insert(StartSearchNode);
            ClosedHash.Clear();
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GridSizeUpDown.Value) != LastNumericUpDown && LargeGridButton.Checked)
            {
                LastNumericUpDown = Convert.ToInt32(GridSizeUpDown.Value);
                LargeGridButton_CheckedChanged(sender, e);
            }

            Panel1.Refresh();

            StartSearchNode = SearchNode.OutOfIndexNode;
            EndSearchNode = SearchNode.OutOfIndexNode;

            OpenHeap.Clear();
            ClosedHash.Clear();

            Random r = new Random();
            foreach (SearchNode n in PathfindingGrid)
            {
                n._Walkable = (r.Next(100) > RandomnessBar.Value);
                if (!n._Walkable)
                {
                    LevelMap.ColourSquare(Panel1.CreateGraphics(), n._Pos, Color.CornflowerBlue);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    //handle maximizing
                    int w = this.Size.Width - Panel1.Left - 30;
                    int h = this.Size.Height - Panel1.Top - 50;
                    Panel1.Size = new Size(w, h);

                    if (!LargeGridButton.Checked)
                    {
                        SmallGridButton_CheckedChanged(sender, e);
                    }
                    else
                    {
                        LargeGridButton_CheckedChanged(sender, e);
                    }
                    break;

                case FormWindowState.Minimized:
                    // Handle minimizing
                    break;

                case FormWindowState.Normal:
                    Panel1.Size = SmallPanelSize;
                    if (!LargeGridButton.Checked)
                    {
                        SmallGridButton_CheckedChanged(sender, e);
                    }
                    else
                    {
                        LargeGridButton_CheckedChanged(sender, e);
                    }
                    break;

                default:
                    break;
            }
        }

        private void LargeGridButton_Click(object sender, EventArgs e)
        {
            LargeGridButton_CheckedChanged(sender, e);
        }

        private void GridSizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            LargeGridSize.Width = Convert.ToInt32(GridSizeUpDown.Value);
            LargeGridSize.Height = Convert.ToInt32(GridSizeUpDown.Value);
        }

        // TODO - index out range
        private void RandomMazeButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GridSizeUpDown.Value) != LastNumericUpDown && LargeGridButton.Checked)
            {
                LastNumericUpDown = Convert.ToInt32(GridSizeUpDown.Value);
                LargeGridButton_CheckedChanged(sender, e);                
            }

            Panel1.Refresh();

            StartSearchNode = SearchNode.OutOfIndexNode;
            EndSearchNode = SearchNode.OutOfIndexNode;

            OpenHeap.Clear();
            ClosedHash.Clear();

            Form MazeLoadingForm = null;

            // Small maze generation is fairly quick, no need to show the form
            if (LargeGridButton.Checked)
            {
                MazeLoadingForm = new MazeLoadingForm();
                int x = this.Location.X + Panel1.Location.X + 100;
                int y = this.Location.Y + Panel1.Location.Y + 100;
                MazeLoadingForm.Location = new Point(x, y);
                MazeLoadingForm.StartPosition = FormStartPosition.Manual;
                MazeLoadingForm.ShowInTaskbar = false;
                MazeLoadingForm.Show();
                MazeLoadingForm.Update();
            }

            // Primm
            // 1. Start with grid full of walls
            foreach (SearchNode n in PathfindingGrid)
            {
                n._Walkable = false;
            }

            Random rnd = new Random();
            List<MazeNode> wallList = new List<MazeNode>();

            // 2. Pick a cell, mark it as part of the maze. Add the walls of the cell
            // to the wall list
            // opposite = (1,1);
            int a = rnd.Next(LevelMap.HorizontalCells / 2 - 1);
            int b = rnd.Next(LevelMap.VerticalCells / 2 - 1);
            MazeNode passage = new MazeNode(a, b);
            MazeNode opposite = new MazeNode(a, b);
            PathfindingGrid[opposite._X, opposite._Y]._Walkable = true;

            // Add neighbour nodes (north and south not walkable)
            wallList.Add(new MazeNode(passage._X + 1, passage._Y, 'E')); // add the passage
            wallList.Add(new MazeNode(passage._X, passage._Y + 1, 'S'));
            
            while(wallList.Any())// for (int i = 0; i < 20; i++)
            {
                // 3. While there are walls in the list
                // pick a random wall from the list ( 0 )
                passage = wallList[rnd.Next(wallList.Count)];

                switch (passage._Direction)
                {
                    case 'E':
                        opposite = new MazeNode(passage._X + 1, passage._Y);
                        break;

                    case 'S':
                        opposite = new MazeNode(passage._X, passage._Y + 1);
                        break;

                    case 'W':
                        opposite = new MazeNode(passage._X - 1, passage._Y);
                        break;

                    case 'N':
                        opposite = new MazeNode(passage._X, passage._Y - 1);
                        break;
                }

                // if the cell on the opposite side isn't in the maze yet
                //var match = wallList.FindIndex(x => x.Equals(passage));
                //if (match == -1)
                if(PathfindingGrid[opposite._X, opposite._Y]._Walkable == false)
                {
                    // make the wall a passage and mark the cell on the opposite side as part of the maze
                    PathfindingGrid[passage._X, passage._Y]._Walkable = true;
                    PathfindingGrid[opposite._X, opposite._Y]._Walkable = true;
                    
                    // add the neighbouring walls of the cell to the wall list
                    string[] keys = opposite.GetNeighbours(PathfindingGrid, opposite);
                    if (keys[0] == "N")
                    {
                        wallList.Add(new MazeNode(opposite._X, opposite._Y-1, 'N')); // add the passage
                    }
                    if (keys[1] == "E")
                    {
                        wallList.Add(new MazeNode(opposite._X + 1, opposite._Y, 'E')); // add the passage
                    }
                    if (keys[2] == "S")
                    {
                        wallList.Add(new MazeNode(opposite._X, opposite._Y+1, 'S')); // add the passage
                    }
                    if (keys[3] == "W")
                    {
                        wallList.Add(new MazeNode(opposite._X- 1, opposite._Y, 'W')); // add the passage
                    }

                   // wallList.Remove(passage);

                }
                // if the cell on the opposite side already was in the maze, remove the wall from the list
                else
                {
                    wallList.Remove(passage);
                    //wallList.RemoveAt(match);
                }
            }

            if (MazeLoadingForm != null)
            {
                MazeLoadingForm.Dispose();
            } 

            // Finally draw
            foreach (SearchNode n in PathfindingGrid)
            {
                if (!n._Walkable)
                {
                    LevelMap.ColourSquare(Panel1.CreateGraphics(), n._Pos, Color.CornflowerBlue);
                }
            }            
        }

        private void HeuristicComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            switch ((sender as ComboBox).SelectedItem.ToString())
            {
                case "Manhatten":
                    DiagonalCheckBox.Checked = false;
                    UseGCheckBox.Checked = true;
                    TieBreakerCheckBox.Checked = true;
                    break;

                case "Diagonal Distance":
                    DiagonalCheckBox.Checked = true;
                    UseGCheckBox.Checked = true;
                    TieBreakerCheckBox.Checked = true;
                    break;                    

                case "Euclidean Distance":
                    DiagonalCheckBox.Checked = true;
                    UseGCheckBox.Checked = true;
                    TieBreakerCheckBox.Checked = true;
                    break;

                case "None":
                    DiagonalCheckBox.Checked = false;
                    UseGCheckBox.Checked = true;
                    TieBreakerCheckBox.Checked = true;
                    break;

                default:
                    break;
            }
        }

        private void SmallGridButton_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
