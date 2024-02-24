using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PathfindingFun
{
    public partial class MainForm : Form
    {
        // General
        Mouse _mouse;

        // Grid
        GridDisplay _gridDisplay;
        int _smallGridPixelSize; 
        int _largeGridPixelSize;
         
        // Search nodes
        SearchNode _startSearchNode;
        SearchNode _endSearchNode;
        SearchNode[,] _pathfindingGrid;

        Dictionary<string, SearchNode> OffsetSearchNodes;
        BinaryHeap<SearchNode> _openHeap; // Faster as we don't constantly sort a list 
        HashSet<SearchNode> _closedHash;  // Lookup for HashSet is O(1) and List is O(N). Also prevents duplicates

        public MainForm()
        {
            InitializeComponent();

            FormSetup();
            GridSetup(_smallGridPixelSize);
            NodeSetup();
        }

        #region Setup

        private void FormSetup()
        {
            HeuristicComboBox.SelectedIndex = 0;
                        
            this.MinimumSize = this.Size;
            this.BackColor = ProjectColors.FormColor;

            RandomnessBar.BackColor = ProjectColors.FormColor;

            Panel1.MinimumSize = Panel1.Size;
            Panel1.BackColor = ProjectColors.Clear;

            _mouse = new Mouse();

            _smallGridPixelSize = 30;
            _largeGridPixelSize = Convert.ToInt32(GridSizeUpDown.Value);
        }

        private void GridSetup(int pixelSize)
        {
            _gridDisplay = new GridDisplay
            {
                CellSize = pixelSize,
                Dimensions = new Size(Panel1.Width / pixelSize, Panel1.Height / pixelSize)
            };

            UpdateGridLabels();
        }

        private void NodeSetup()
        {
            // SearchNode setup
            _startSearchNode = SearchNode.OutOfIndexNode;
            _endSearchNode = SearchNode.OutOfIndexNode;

            OffsetSearchNodes = new Dictionary<string, SearchNode>
            {
                ["S"] = new SearchNode(0, 1, 10),    // Down
                ["SE"] = new SearchNode(1, 1, 14),   // Down Right
                ["E"] = new SearchNode(1, 0, 10),    // Right
                ["NE"] = new SearchNode(1, -1, 14),  // Up Right
                ["N"] = new SearchNode(0, -1, 10),   // Up
                ["NW"] = new SearchNode(-1, -1, 14), // Up Left
                ["W"] = new SearchNode(-1, 0, 10),   // Left
                ["SW"] = new SearchNode(-1, 1, 14)  // Down Left
            };

            _openHeap = new BinaryHeap<SearchNode>();
            _closedHash = new HashSet<SearchNode>(new SearchNodeComparer());

            _pathfindingGrid = new SearchNode[_gridDisplay.Dimensions.Width, _gridDisplay.Dimensions.Height];
            for (int x = 0; x < _gridDisplay.Dimensions.Width; x++)
            {
                for (int y = 0; y < _gridDisplay.Dimensions.Height; y++)
                {
                    _pathfindingGrid[x, y] = new SearchNode(x, y);
                }
            }

            ResetAILabels();
        }

        #endregion

        #region Labels

        private void UpdateGridLabels()
        {
            PixelSizeTextBox.Text = string.Format("{0} x {1}", Panel1.Size.Width, Panel1.Size.Height);
            GridSizeTextBox.Text = string.Format("{0} x {1}", _gridDisplay.Dimensions.Width, _gridDisplay.Dimensions.Height);
            CellCountTextBox.Text = (_gridDisplay.Dimensions.Width * _gridDisplay.Dimensions.Height).ToString();
        }

        public void UpdateMouseLabels()
        {
            _mouse.Local = Panel1.PointToClient(Cursor.Position);
            MouseScreenTextBox.Text = _mouse.LocalSpaceString();

            _mouse.Grid = new Point(_mouse.Local.X / _gridDisplay.CellSize, _mouse.Local.Y / _gridDisplay.CellSize);
            MouseGridTextBox.Text = _mouse.GridSpaceString();
        }

        private void ResetAILabels()
        {
            PathLengthTextBox.Text = "N/A";
            ConsideredNodeLengthTextBox.Text = "N/A";
        }

        #endregion

        #region Mouse

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateMouseLabels();

            _mouse.Moved = (_mouse.Grid != _mouse.LastPoint);
            _mouse.LastPoint = _mouse.Grid;

            // If the left mouse button is clicked, can draw extra walls.
            if (_mouse.Moved && e.Button == MouseButtons.Left)
            {
                if (Helper.IsInRange(0, _gridDisplay.Dimensions.Width - 1, _mouse.Grid.X) &&
                    Helper.IsInRange(0, _gridDisplay.Dimensions.Height - 1, _mouse.Grid.Y))
                {
                    _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _mouse.Grid, ProjectColors.Wall);
                    _pathfindingGrid[_mouse.Grid.X, _mouse.Grid.Y].Walkable = false;
                }
            }

            if (SmallGridButton.Checked)
            {
                _gridDisplay.DrawGridLines(Panel1.CreateGraphics());
            }
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Need to draw the first square on mouse down        
                if (Helper.IsInRange(0, _gridDisplay.Dimensions.Width - 1, _mouse.Grid.X) &&
                    Helper.IsInRange(0, _gridDisplay.Dimensions.Height - 1, _mouse.Grid.Y))
                {
                    _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _mouse.Grid, ProjectColors.Wall);
                    _pathfindingGrid[_mouse.Grid.X, _mouse.Grid.Y].Walkable = false;
                }
            }
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Add Start SearchNode"));
                m.MenuItems.Add(new MenuItem("Add End SearchNode"));
                m.MenuItems[0].Click += new System.EventHandler(this.StartSearchNode_Click);
                m.MenuItems[1].Click += new System.EventHandler(this.EndSearchNode_Click);
                m.Show(Panel1, _mouse.Local);
            }
        }

        private void StartSearchNode_Click(object sender, System.EventArgs e)
        {
            _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _startSearchNode.Pos, ProjectColors.Clear);
            _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _mouse.Grid, ProjectColors.StartNode);
            if (SmallGridButton.Checked)
            {
                _gridDisplay.DrawGridLines(Panel1.CreateGraphics());
            }

            _startSearchNode = new SearchNode(_mouse.Grid, 0);
            _openHeap.Clear();
            _openHeap.Insert(_startSearchNode);
            _pathfindingGrid[_startSearchNode.Pos.X, _startSearchNode.Pos.Y].Walkable = true;
        }

        private void EndSearchNode_Click(object sender, System.EventArgs e)
        {
            _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _endSearchNode.Pos, ProjectColors.Clear);
            _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _mouse.Grid, ProjectColors.EndNode);
            if (SmallGridButton.Checked)
            {
                _gridDisplay.DrawGridLines(Panel1.CreateGraphics());
            }

            _endSearchNode = new SearchNode(_mouse.Grid, 0);
            _pathfindingGrid[_endSearchNode.Pos.X, _endSearchNode.Pos.Y].Walkable = true;
        }

        #endregion

        #region GUI

        private void SmallGridButton_Click(object sender, EventArgs e)
        {
            SmallGridButton_CheckedChanged(sender, e);
        }

        private void LargeGridButton_Click(object sender, EventArgs e)
        {
            LargeGridButton_CheckedChanged(sender, e);
        }

        private void GridSizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            _largeGridPixelSize = Convert.ToInt32(GridSizeUpDown.Value);
            SmallGridButton.Checked = false;
            LargeGridButton.Checked = true;
            LargeGridButton_CheckedChanged(sender, e);
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

                case "None (Dijkstra)":
                    DiagonalCheckBox.Checked = false;
                    UseGCheckBox.Checked = true;
                    TieBreakerCheckBox.Checked = true;
                    break;

                default:
                    break;
            }
        }

        #endregion

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (SmallGridButton.Checked)
            {
                _gridDisplay.DrawGridLines(e.Graphics);
            }
        }




        public void Clear()
        {
            _startSearchNode = SearchNode.OutOfIndexNode;
            _endSearchNode = SearchNode.OutOfIndexNode;

            _openHeap.Clear();
            _closedHash.Clear();

            ResetAILabels();

            Panel1.Invalidate();

            _pathfindingGrid = new SearchNode[_gridDisplay.Dimensions.Width, _gridDisplay.Dimensions.Height];
            for (int x = 0; x < _gridDisplay.Dimensions.Width; x++)
            {
                for (int y = 0; y < _gridDisplay.Dimensions.Height; y++)
                {
                    _pathfindingGrid[x, y] = new SearchNode(x, y);
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void SmallGridButton_CheckedChanged(object sender, EventArgs e)
        {
            GridSetup(_smallGridPixelSize);
            Clear();
        }

        private void LargeGridButton_CheckedChanged(object sender, EventArgs e)
        {
            GridSetup(_largeGridPixelSize);
            Clear();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            Panel1.Refresh();
            _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _startSearchNode.Pos, ProjectColors.StartNode);
            _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _endSearchNode.Pos, ProjectColors.EndNode);

            ResetAILabels();

            // Keep any walls that exists so we can run it again
            for (int x = 0; x < _gridDisplay.Dimensions.Width; x++)
            {
                for (int y = 0; y < _gridDisplay.Dimensions.Height; y++)
                {
                    SearchNode tmp = _pathfindingGrid[x, y];
                    if (tmp.Walkable == false)
                    {
                        _gridDisplay.ColourSquare(Panel1.CreateGraphics(), tmp.Pos, ProjectColors.Wall);
                    }
                }
            }

            _openHeap.Clear();
            _openHeap.Insert(_startSearchNode);
            _closedHash.Clear();

            if (SmallGridButton.Checked)
            {
                _gridDisplay.DrawGridLines(Panel1.CreateGraphics());
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResetAILabels();

            // Update the resized panel with the correct pixel size
            if (SmallGridButton.Checked)
            {
                SmallGridButton_CheckedChanged(sender, e);
            }
            else
            {
                LargeGridButton_CheckedChanged(sender, e);
            }

            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    int w = this.Size.Width - Panel1.Left - 30;
                    int h = this.Size.Height - Panel1.Top - 50;
                    Panel1.Size = new Size(w, h);

                    break;

                case FormWindowState.Minimized:
                    break;

                case FormWindowState.Normal:
                    break;

                default:
                    break;
            }
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            Panel1.Refresh();

            _startSearchNode = SearchNode.OutOfIndexNode;
            _endSearchNode = SearchNode.OutOfIndexNode;

            _openHeap.Clear();
            _closedHash.Clear();

            Random r = new Random();
            foreach (SearchNode n in _pathfindingGrid)
            {
                n.Walkable = (r.Next(100) > RandomnessBar.Value);
                if (!n.Walkable)
                {
                    _gridDisplay.ColourSquare(Panel1.CreateGraphics(), n.Pos, ProjectColors.Wall);
                }
            }
        }

        private void RandomMazeButton_Click(object sender, EventArgs e)
        {
            Panel1.Refresh();
            GenerateMaze();
        }

        private void RunAI_Click(object sender, EventArgs e)
        {
            int pathLength = 0;
            int consideredNodesCount = 0;

            SearchNode current = SearchNode.OutOfIndexNode;

            // Have we got a start and a destination?
            if (_startSearchNode != SearchNode.OutOfIndexNode && _endSearchNode != SearchNode.OutOfIndexNode)
            {
                while (!_openHeap.IsEmpty())
                {
                    current = _openHeap.Peek(); // Just look at lowest f

                    // Finished!?
                    if (current == _endSearchNode)
                    {
                        Point p = _pathfindingGrid[current.Pos.X, current.Pos.Y].Parent;// _Pos;
                        while (!(p == _startSearchNode.Pos))
                        {
                            _gridDisplay.ColourSquare(Panel1.CreateGraphics(), p, ProjectColors.Path);
                            p = _pathfindingGrid[p.X, p.Y].Parent;
                            pathLength++;
                        }

                        // Redraw the end node as it was put onto the considered nodes list 
                        _gridDisplay.ColourSquare(Panel1.CreateGraphics(), _endSearchNode.Pos, ProjectColors.EndNode);

                        // Need to add one to the pathLength because the end node counts as part of the path
                        PathLengthTextBox.Text = (pathLength + 1).ToString();
                        ConsideredNodeLengthTextBox.Text = (consideredNodesCount - (pathLength + 1)).ToString();

                        break;
                    }

                    // Begin pathfinding
                    current = _openHeap.PopMin(); // Now grab lowest f
                    _closedHash.Add(current);

                    // Find neighbours we can travel to
                    string[] neighbourKeys;
                    if (!DiagonalCheckBox.Checked)
                    {
                        neighbourKeys = AI.GetCardinalKeys(_pathfindingGrid, current);
                    }
                    else
                    {
                        neighbourKeys = AI.GetCardinalAndOrdinalKeys(_pathfindingGrid, current);
                    }

                    foreach (string key in neighbourKeys)
                    {
                        SearchNode neighbour = OffsetSearchNodes[key];
                        SearchNode tmp = current + neighbour;

                        int tentativeG = current.G + neighbour.G;

                        if (_closedHash.Contains(tmp) && tentativeG >= neighbour.G)
                        {
                            continue;
                        }

                        if (!_openHeap.Contains(tmp) || tentativeG < neighbour.G)
                        {
                            //tmp._Parent = current._Pos;
                            _pathfindingGrid[tmp.Pos.X, tmp.Pos.Y].Parent = current.Pos;
                            tmp.G = tentativeG;

                            // Decide which hueristic to use. We use 10.0f for cardinal directions and 14.0f for ordinal 
                            // directions; the cost to move diagonaly is square 2. 10, 14 is about the right ratio and 
                            // avoids lots of square roots for speed. 
                            switch (HeuristicComboBox.SelectedItem.ToString())
                            {
                                case "Manhatten":
                                    tmp.H = AI.ManhattanDistance(tmp.Pos, _endSearchNode.Pos, 10.0f);
                                    break;

                                case "Diagonal Distance":
                                    tmp.H = AI.DiagonalDistance(tmp.Pos, _endSearchNode.Pos, 10.0f, 14.0f);
                                    break;

                                case "Euclidean Distance":
                                    tmp.H = AI.EuclideanDistance(tmp.Pos, _endSearchNode.Pos, 14.0f);
                                    break;

                                case "None":
                                    tmp.H = 0;
                                    break;

                                default:
                                    break;
                            }

                            // Apply a tie breaker? In some maps there are many equal-length paths and A * may 
                            // explore all of these instead of one. This tie breaker preferes paths that are on the 
                            // straight line from the start to the goal 
                            if (TieBreakerCheckBox.Checked)
                            {
                                tmp.H += AI.GetTieBreakerScale(tmp.Pos, _startSearchNode.Pos, _endSearchNode.Pos);
                            }

                            consideredNodesCount++;

                            // Draw considered nodes
                            if (DrawOpenlistCheckBox.Checked)
                            {
                                _gridDisplay.ColourSquare(Panel1.CreateGraphics(), tmp.Pos, ProjectColors.ConsideredNode);
                                if (SmallGridButton.Checked) // Drawing costs on large map would be pointless,
                                {                            // also costs are drawn as ints (because it is easier to see)
                                    _gridDisplay.DrawCosts(Panel1.CreateGraphics(), tmp.Pos, tmp.G, tmp.H);
                                }
                            }

                            if (!_openHeap.Contains(tmp))
                            {
                                _openHeap.Insert(tmp);
                            }
                        }
                    }
                }

                if (SmallGridButton.Checked)
                {
                    _gridDisplay.DrawGridLines(Panel1.CreateGraphics());
                }

                // No solution found   
                if (current != _endSearchNode)
                {
                    string message = "All potential nodes have been exhausted and no path could be found.";
                    string title = "No solution found";
                    MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
        }
    }
}