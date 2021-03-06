﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarAlgorithm
{
    public partial class Form1 : Form
    {
        // object-algorithm
        AStar aStar;

        // cell color
        public Color colorCellNone = Color.White;
        public Color colorCellSource = Color.Green;
        public Color colorCellDestination = Color.Red;
        public Color colorCellBlock = Color.Gray;
        public Color colorCellInOpenList = Color.LimeGreen;
        public Color colorCellInCloseList = Color.Orange;

        // path color
        public Color colorPathBlue = Color.Blue;
        public Color colorPathNone = Color.FromArgb(0, 0, 0, 0);

        public enum cellType:int {NONE, SOURCE, DESTINATION, BLOCK};
        int selectedOptionCellPicker;

        // deternmine user can pick cell on board or not
        bool enableToPickCellOnBoard;

        // pen to draw path
        Pen pen;

        // Source is the left-most bottom-most corner 
        KeyValuePair<int, int> src;

        // Destination is the left-most top-most corner 
        KeyValuePair<int, int> dest;

        // bool 
        bool letMagicHappen;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        #region Utilites

        private void Init()
        {
            aStar = new AStar();
            pen = new Pen(Color.Blue, 2);
            letMagicHappen = false;

            // if user have not pick any cell option yet, this will be set to false
            btnPick.Enabled = false;

            selectedOptionCellPicker = (int)cellType.NONE;
            enableToPickCellOnBoard = false;

            // init combobox Heuristic Model
            cbbHeuristic.DataSource = new object[] { "Manhattan", "Diagonal", "Euclidean" };
            cbbHeuristic.DisplayMember = "Manhattan";
            aStar.HeuristicModel = (int)AStar.Heuristics.Manhattan; // set to default

            ResetLocation();
            DoItAtOnce(); // default
        }

        private void GoStepByStep()
        {
            aStar.GoStepByStep = true;
            btnCalc.Enabled = false;
            btnGoNext.Enabled = true;
            ResetAStar();
            ResetSolutionTextbox();
        }

        private void DoItAtOnce()
        {
            aStar.GoStepByStep = false;
            btnCalc.Enabled = true;
            btnGoNext.Enabled = false;
            ResetAStar();
            ResetSolutionTextbox();
        }

        private void resetSourceLocation()
        {
            src = new KeyValuePair<int, int>(-1, -1);
        }

        private void resetDestinationLocation()
        {
            dest = new KeyValuePair<int, int>(-1, -1);
        }

        private void ResetLocation()
        {
            resetDestinationLocation();
            resetSourceLocation();
        }

        private void ResetDisplayArea()
        {
            displayArea.Controls.Clear();
        }

        private void ResetCellsColor()
        {
            foreach (Control c in displayArea.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    Button btn = (Button)c;
                    int[] index = GetButtonIndex(btn);

                    if (IsSourcePick(index[0], index[1]) || IsDestinationPick(index[0], index[1])) 
                        continue;
                    if (btn.BackColor != colorCellBlock) btn.BackColor = colorCellNone;
                }
            }

            // redraw
            ReDrawDisplayBoard();
        }

        private void ResetAStar()
        {
            aStar.FoundDest = false;
            aStar.Result.Clear();
            aStar.IsInitAstar = false;
            aStar.ResetOpenList();
            aStar.ResetCloseList();
            aStar.ResetCellTracking();
        }

        private void ResetAStarWithoutResetCellTracking()
        {
            aStar.FoundDest = false;
            aStar.Result.Clear();
            aStar.IsInitAstar = false;
            aStar.ResetOpenList();
            aStar.ResetCloseList();
        }

        private void ResetSolutionTextbox()
        {
            tbSolution.Text = "";
        }

        private void ReDrawDisplayBoard()
        {
            // redraw
            displayArea.Refresh();
        }

        private void ApplySizeToAStarGrid()
        {
            aStar.Rows = (int)nudRow.Value;
            aStar.Cols = (int)nudCol.Value;
        }

        private void InitIndexOnDisplayArea()
        {
            int startX = 20, startY = 20;

            // init index row
            for (int i = 0; i < aStar.Rows; i++)
            {
                Label lb = new Label();
                lb.AutoSize = true;
                lb.Text = i.ToString();
                lb.Location = new System.Drawing.Point(startX, 60 + i * 40);

                // add to display
                displayArea.Controls.Add(lb);
            }

            // init index col
            for (int j = 0; j < aStar.Cols; j++)
            {
                Label lb = new Label();
                lb.AutoSize = true;
                lb.Text = j.ToString();
                lb.Location = new System.Drawing.Point(60 + j * 40, startY);

                // add to display
                lb.BringToFront();
                displayArea.Controls.Add(lb);
            }
        }

        private void InitCellsOnDisplayArea()
        {
            int startX = 40, startY = 40;

            // init board here
            for (int i = 0; i < aStar.Rows; i++)
            {
                for (int j = 0; j < aStar.Cols; j++)
                {
                    Button btn = new Button();
                    btn.BackColor = colorCellNone;
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btn.Location = new System.Drawing.Point(startY + j * 40, startX + i * 40); // a little confuse here, location = (col, row) 
                    btn.Name = InitIndexForButton(i, j); // name using to store index (row, col) of a cell
                    btn.Size = new System.Drawing.Size(40, 40);
                    btn.UseVisualStyleBackColor = false;
                    btn.Click += new System.EventHandler(this.btnCell_Click);
                    btn.Paint += PathDraw_Paint;

                    // add to display
                    btn.BringToFront();
                    displayArea.Controls.Add(btn);

                }
            }
        }

        private void HandleAfterPickOption()
        {
            btnPick.Text = "Start Pick";

            // enable the button so user can click it
            btnPick.Enabled = true;

            // just when user click the button, they can pick on board
            enableToPickCellOnBoard = false;
        }

        private string InitIndexForButton(int row, int col)
        {
            return row.ToString() + "," + col.ToString();
        }

        private int[] GetButtonIndex(Button btn)
        {
            string[] index = btn.Name.Split(',');
            int row = Int32.Parse(index[0]), col = Int32.Parse(index[1]);

            return new int[] { row, col };
        }

        private bool IsSourcePick(int row, int col)
        {
            return row == src.Key && col == src.Value;
        }

        private bool IsDestinationPick(int row, int col)
        {
            return row == dest.Key && col == dest.Value;
        }

        private void renderSelectPickSourceButton()
        {
            btnPickSource.FlatAppearance.BorderColor = Color.Blue;
            btnPickSource.FlatAppearance.BorderSize = 2;
        }

        private void renderUnselectPickSourceButton()
        {
            btnPickSource.FlatAppearance.BorderColor = Color.Black;
            btnPickSource.FlatAppearance.BorderSize = 1;
        }

        private void renderSelectPickDestinationButton()
        {
            btnPickDest.FlatAppearance.BorderColor = Color.Blue;
            btnPickDest.FlatAppearance.BorderSize = 2;
        }

        private void renderUnselectPickDestinationButton()
        {
            btnPickDest.FlatAppearance.BorderColor = Color.Black;
            btnPickDest.FlatAppearance.BorderSize = 1;
        }

        private void renderSelectPickBlockedButton()
        {
            btnPickBlocked.FlatAppearance.BorderColor = Color.Blue;
            btnPickBlocked.FlatAppearance.BorderSize = 2;
        }

        private void renderUnselectPickBlockedButton()
        {
            btnPickBlocked.FlatAppearance.BorderColor = Color.Black;
            btnPickBlocked.FlatAppearance.BorderSize = 1;
        }

        private void ShowInSolutionTextBox(Stack<KeyValuePair<int, int>> result)
        {
            // clear the textbox
            tbSolution.Text = "";

            if (result.Count() > 0)
            {
                tbSolution.Text = "Summary: " + result.Count().ToString() + " steps";
                foreach (var i in result)
                {
                    tbSolution.Text = "(" + i.Key.ToString() + ", " + i.Value.ToString() + ")" + '\n' + tbSolution.Text;
                }
            }
            else
            {
                tbSolution.Text = "No Path!";
            }
        }

        // get grid from board for A Star Search, using defined color 
        private int[,] getGrid()
        {
            int[,] grid = new int[aStar.Rows, aStar.Cols];

            foreach (Control c in displayArea.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    int[] index = GetButtonIndex((Button)c);
                    grid[index[0], index[1]] = (c.BackColor == colorCellBlock ? 0 : 1); // 0: blocked, 1: can move
                }
            }

            return grid;
        }

        #endregion

        #region Event Handlers

        private void btnResetSize_Click(object sender, EventArgs e)
        {
            nudRow.Value = 0;
            nudCol.Value = 0;

            ResetDisplayArea();
            ResetAStar();
            ResetSolutionTextbox();
        }

        private void btnApplySize_Click(object sender, EventArgs e)
        {
            ResetDisplayArea();
            ResetAStar();
            ResetLocation();

            ApplySizeToAStarGrid();
            InitIndexOnDisplayArea();
            InitCellsOnDisplayArea();
        }

        private void btnPickSource_Click(object sender, EventArgs e)
        {
            selectedOptionCellPicker = (int)cellType.SOURCE;
            HandleAfterPickOption();

            // render
            renderSelectPickSourceButton();
            renderUnselectPickBlockedButton();
            renderUnselectPickDestinationButton();
        }

        private void btnPickDest_Click(object sender, EventArgs e)
        {
            selectedOptionCellPicker = (int)cellType.DESTINATION;
            HandleAfterPickOption();

            // render
            renderSelectPickDestinationButton();
            renderUnselectPickSourceButton();
            renderUnselectPickBlockedButton();
        }

        private void BtnPickBlocked_Click(object sender, EventArgs e)
        {
            selectedOptionCellPicker = (int)cellType.BLOCK;
            HandleAfterPickOption();

            // render
            renderSelectPickBlockedButton();
            renderUnselectPickSourceButton();
            renderUnselectPickDestinationButton();
        }

        // Handle the cell on board click event
        private void btnCell_Click(object sender, EventArgs e)
        {
            // cannot pick when this set to false
            if (!enableToPickCellOnBoard) return;

            // detect which option was be selected
            Button btn = sender as Button;
            bool isSourcePicker = (selectedOptionCellPicker == (int)cellType.SOURCE);
            bool isDestinationPicker = (selectedOptionCellPicker == (int)cellType.DESTINATION);
            bool isBlockedPicker = (selectedOptionCellPicker == (int)cellType.BLOCK);

            // get index from chosen button
            int[] index = GetButtonIndex(btn);
            int row = index[0], col = index[1];

            // if that cell is picked before, unpick it
            if (btn.BackColor != colorCellNone)
            {
                // if it is a src/dest spot, notice to user and ignore the picker
                if (isBlockedPicker && 
                    (IsSourcePick(row, col) || IsDestinationPick(row, col)))
                {
                    MessageBox.Show("Source or destination can not be blocked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                // set color to none
                btn.BackColor = colorCellNone;

                // release the storage of src and dest
                if (isSourcePicker) resetSourceLocation();
                if (isDestinationPicker) resetDestinationLocation();
            }
            else
            {
                if (isSourcePicker)
                {
                    btn.BackColor = colorCellSource;

                    // if it is another source, remove the color old
                    if (src.Key != -1)
                    {
                        string old_name = InitIndexForButton(src.Key, src.Value);
                        displayArea.Controls[old_name].BackColor = colorCellNone;
                    }

                    // update new src
                    src = new KeyValuePair<int, int>(row, col);

                }
                else if (isDestinationPicker)
                {
                    btn.BackColor = colorCellDestination;

                    // if it is another destination, remove the color old
                    if (dest.Key != -1)
                    {
                        string old_name = InitIndexForButton(dest.Key, dest.Value);
                        displayArea.Controls[old_name].BackColor = colorCellNone;
                    }

                    // update new dest
                    dest = new KeyValuePair<int, int>(row, col);
                }
                else if (isBlockedPicker)
                {
                    btn.BackColor = colorCellBlock;
                }
            }
        }

        private void btnResetCells_Click(object sender, EventArgs e)
        {
            // reset all color in display area
            foreach (Control c in displayArea.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    c.BackColor = colorCellNone;
                }
            }

            // reset properties of A star
            resetSourceLocation();
            resetDestinationLocation();
            ResetAStar();
            ResetSolutionTextbox();

        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            if (btnPick.Text == "Start Pick")
            {
                enableToPickCellOnBoard = true;
                btnPick.Text = "Stop Pick";
            }
            else // btnPick.Text == "Stop Pick", now is the time user chosing the cell on board and want to stop.
            {
                enableToPickCellOnBoard = false;
                btnPick.Enabled = false;
                btnPick.Text = "Start Pick";

                // render
                renderUnselectPickSourceButton();
                renderUnselectPickBlockedButton();
                renderUnselectPickDestinationButton();
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            aStar.IsInitAstar = false;

            int[,] grid = getGrid();

            if (aStar.HandleBeforeSearching(grid, src, dest))
            {
                aStar.aStarSearch(grid, src, dest);
            }
            
            ShowInSolutionTextBox(aStar.Result);
            ReDrawDisplayBoard();
        }

        private void cbDiagonal_CheckedChanged(object sender, EventArgs e)
        {
            aStar.AllowDiagonalMove = cbDiagonal.Checked;
            ResetAStar();
            ResetSolutionTextbox();
        }

        private void cbShowPath_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPath.Checked)
            {
                pen.Color = colorPathBlue;
            }
            else
            {
                pen.Color = colorPathNone;
            }

            ReDrawDisplayBoard();
        }

        private void PathDraw_Paint(object sender, PaintEventArgs e)
        {

            if (aStar.GoStepByStep) // draw the list tracked cells
            {
                List<KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>>> listTrackedCells = aStar.CellTracking;
                int len = listTrackedCells.Count();

                for (int i = 0; i < len; i++)
                {
                    string btn1 = listTrackedCells[i].Key.Key.ToString() + "," + listTrackedCells[i].Key.Value.ToString();
                    string btn2 = listTrackedCells[i].Value.Key.ToString() + "," + listTrackedCells[i].Value.Value.ToString();

                    Point pt1 = new Point(displayArea.Controls[btn1].Left + (displayArea.Controls[btn1].Width / 2),
                            displayArea.Controls[btn1].Top + (displayArea.Controls[btn1].Height / 2));
                    Point pt2 = new Point(displayArea.Controls[btn2].Left + (displayArea.Controls[btn2].Width / 2),
                            displayArea.Controls[btn2].Top + (displayArea.Controls[btn2].Height / 2));

                    if (sender is Button)
                    {
                        // offset line so it's drawn over the button where
                        // the line on the panel is drawn
                        Button btn = (Button)sender;
                        pt1.X -= btn.Left;
                        pt1.Y -= btn.Top;
                        pt2.X -= btn.Left;
                        pt2.Y -= btn.Top;
                    }

                    e.Graphics.DrawLine(pen, pt1, pt2);
                }

                // coloring all cells in open list and close list for more visualize :)
                HashSet<KeyValuePair<double, KeyValuePair<int, int>>> openList
                    = new HashSet<KeyValuePair<double, KeyValuePair<int, int>>>(aStar.OpenList);
                bool[,] closeList = aStar.ClosedList;

                // color open list
                while (openList.Count() > 0)
                {
                    KeyValuePair<double, KeyValuePair<int, int>> cell = openList.First();
                    openList.Remove(openList.First());

                    // not change the color of src/dest cell
                    if (IsSourcePick(cell.Value.Key, cell.Value.Value) ||
                        IsDestinationPick(cell.Value.Key, cell.Value.Value))
                        continue;

                    string cellIndex = InitIndexForButton(cell.Value.Key, cell.Value.Value);
                    displayArea.Controls[cellIndex].BackColor = colorCellInOpenList;
                }

                // color close list 
                int numsRowCloseList = closeList.GetLength(0);

                if (numsRowCloseList > 0)
                {
                    int numsColCloseList = aStar.Cols;

                    for (int i = 0; i < numsRowCloseList; i++)
                    {
                        for (int j = 0; j < numsColCloseList; j++)
                        {
                            if (closeList[i, j] && !(IsSourcePick(i, j) || IsDestinationPick(i, j)))
                            {
                                string cellIndex = InitIndexForButton(i, j);
                                displayArea.Controls[cellIndex].BackColor = colorCellInCloseList;
                            }
                        }
                    }
                }
            }
            else // drawa the result at once
            {
                KeyValuePair<int, int>[] result = aStar.Result.ToArray();
                int len = result.Count();
                if (len > 0)
                {
                    for (int i = 0; i < len - 1; i++)
                    {
                        string btn1 = result[i].Key.ToString() + "," + result[i].Value.ToString();
                        string btn2 = result[i + 1].Key.ToString() + "," + result[i + 1].Value.ToString();

                        Point pt1 = new Point(displayArea.Controls[btn1].Left + (displayArea.Controls[btn1].Width / 2),
                                displayArea.Controls[btn1].Top + (displayArea.Controls[btn1].Height / 2));
                        Point pt2 = new Point(displayArea.Controls[btn2].Left + (displayArea.Controls[btn2].Width / 2),
                                displayArea.Controls[btn2].Top + (displayArea.Controls[btn2].Height / 2));

                        if (sender is Button)
                        {
                            // offset line so it's drawn over the button where
                            // the line on the panel is drawn
                            Button btn = (Button)sender;
                            pt1.X -= btn.Left;
                            pt1.Y -= btn.Top;
                            pt2.X -= btn.Left;
                            pt2.Y -= btn.Top;
                        }

                        e.Graphics.DrawLine(pen, pt1, pt2);
                    }
                }
            }
        }

        private void cbbHeuristic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbHeuristic.Text == "Manhattan")
            {
                aStar.HeuristicModel = (int)AStar.Heuristics.Manhattan;
            }
            else if (cbbHeuristic.Text == "Diagonal")
            {
                aStar.HeuristicModel = (int)AStar.Heuristics.Diagonal;
            }
            else if (cbbHeuristic.Text == "Euclidean")
            {
                aStar.HeuristicModel = (int)AStar.Heuristics.Euclidean;
            }

            // reset
            ResetAStar();
            ResetSolutionTextbox();
        }

        private void cbTracePathByStep_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTracePathByStep.Checked)
            {
                // reset pen
                // pen.Color = colorPathBlue;
                GoStepByStep();
                ResetCellsColor();
            }
            else
            {
                // reset pen
                // pen.Color = colorPathNone;
                DoItAtOnce();
                ResetCellsColor();
            }
        }

        private void btnGoNext_Click(object sender, EventArgs e)
        {
            if (aStar.FoundDest)
            {
                ShowInSolutionTextBox(aStar.Result);
                ResetAStarWithoutResetCellTracking(); // keep cell tracking list to render
                letMagicHappen = true;
                return;
            }

            if (letMagicHappen)
            {
                letMagicHappen = false;
                aStar.ResetCellTracking(); 
            }

            ResetCellsColor();

            int[,] grid = getGrid();

            if (aStar.HandleBeforeSearching(grid, src, dest))
            {
                aStar.aStarSearch(grid, src, dest);
            }

            ReDrawDisplayBoard();
        }

        #endregion


    }
}
