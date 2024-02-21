using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PathfindingFun
{
    public partial class MainForm
    {
        public void GenerateMaze()
        {
            _startSearchNode = SearchNode.OutOfIndexNode;
            _endSearchNode = SearchNode.OutOfIndexNode;

            _openHeap.Clear();
            _closedHash.Clear();

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
            foreach (SearchNode n in _pathfindingGrid)
            {
                n.Walkable = false;
            }

            Random rnd = new Random();
            List<MazeNode> wallList = new List<MazeNode>();

            // 2. Pick a cell, mark it as part of the maze. Add the walls of the cell
            // to the wall list
            // opposite = (1,1);
            int a = rnd.Next(_gridDisplay.Dimensions.Width / 2 - 1);
            int b = rnd.Next(_gridDisplay.Dimensions.Height / 2 - 1);
            MazeNode passage = new MazeNode(a, b);
            MazeNode opposite = new MazeNode(a, b);
            _pathfindingGrid[opposite.X, opposite.Y].Walkable = true;

            // Add neighbour nodes (north and south not walkable)
            wallList.Add(new MazeNode(passage.X + 1, passage.Y, 'E')); // add the passage
            wallList.Add(new MazeNode(passage.X, passage.Y + 1, 'S'));

            while (wallList.Any())// for (int i = 0; i < 20; i++)
            {
                // 3. While there are walls in the list
                // pick a random wall from the list ( 0 )
                passage = wallList[rnd.Next(wallList.Count)];

                switch (passage.Direction)
                {
                    case 'E':
                        opposite = new MazeNode(passage.X + 1, passage.Y);
                        break;

                    case 'S':
                        opposite = new MazeNode(passage.X, passage.Y + 1);
                        break;

                    case 'W':
                        opposite = new MazeNode(passage.X - 1, passage.Y);
                        break;

                    case 'N':
                        opposite = new MazeNode(passage.X, passage.Y - 1);
                        break;
                }

                // if the cell on the opposite side isn't in the maze yet
                //var match = wallList.FindIndex(x => x.Equals(passage));
                //if (match == -1)
                if (_pathfindingGrid[opposite.X, opposite.Y].Walkable == false)
                {
                    // make the wall a passage and mark the cell on the opposite side as part of the maze
                    _pathfindingGrid[passage.X, passage.Y].Walkable = true;
                    _pathfindingGrid[opposite.X, opposite.Y].Walkable = true;

                    // add the neighbouring walls of the cell to the wall list
                    string[] keys = opposite.GetNeighbours(_pathfindingGrid, opposite);
                    if (keys[0] == "N")
                    {
                        wallList.Add(new MazeNode(opposite.X, opposite.Y - 1, 'N')); // add the passage
                    }
                    if (keys[1] == "E")
                    {
                        wallList.Add(new MazeNode(opposite.X + 1, opposite.Y, 'E')); // add the passage
                    }
                    if (keys[2] == "S")
                    {
                        wallList.Add(new MazeNode(opposite.X, opposite.Y + 1, 'S')); // add the passage
                    }
                    if (keys[3] == "W")
                    {
                        wallList.Add(new MazeNode(opposite.X - 1, opposite.Y, 'W')); // add the passage
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

            MazeLoadingForm?.Dispose();

            // Finally draw
            foreach (SearchNode n in _pathfindingGrid)
            {
                if (!n.Walkable)
                {
                    _gridDisplay.ColourSquare(Panel1.CreateGraphics(), n.Pos, ProjectColors.Wall);
                }
            }
        }
    }
}