using System;
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
        AStar aStar;

        /// <summary>
        /// -1: is not selected
        /// 0: pick source cell
        /// 1: pick destination cell
        /// 2: pick blocked cells
        /// </summary>
        int selectedOptionCellPicker = -1;
        bool enableToPick = false;

        // pen to draw path
        PaintEventArgs peargs;
        Pen pen;

        // Source is the left-most bottom-most corner 
        KeyValuePair<int, int> src = new KeyValuePair<int, int>(-1, -1);

        // Destination is the left-most top-most corner 
        KeyValuePair<int, int> dest = new KeyValuePair<int, int>(-1, -1);
        public Form1()
        {
            InitializeComponent();
            aStar = new AStar();
            btnPick.Enabled = enableToPick;
            pen = new Pen(Color.Blue, 2);
        }

        private void btnResetSize_Click(object sender, EventArgs e)
        {
            nudRow.Value = 0;
            nudCol.Value = 0;

            // reset display area
            displayArea.Controls.Clear();
            aStar.Result.Clear();
            resetDestinationLocation();
            resetSourceLocation();
        }

        private void btnApplySize_Click(object sender, EventArgs e)
        {
            // reset display area
            displayArea.Controls.Clear();
            aStar.Result.Clear();

            aStar.Rows = (int)nudRow.Value;
            aStar.Cols = (int)nudCol.Value;

            int startX = 40, startY = 40;

            // init index
            for (int i = 0; i < aStar.Rows; i++)
            {
                Label lb = new Label();
                lb.AutoSize = true;
                lb.Text = i.ToString();
                lb.Location = new System.Drawing.Point(20, 60 + i * 40);

                // add to display
                displayArea.Controls.Add(lb);
            }

            for (int j = 0; j < aStar.Cols; j++)
            {
                Label lb = new Label();
                lb.AutoSize = true;
                lb.Text = j.ToString();
                lb.Location = new System.Drawing.Point(60 + j * 40, 20);

                // add to display
                lb.BringToFront();
                displayArea.Controls.Add(lb);
            }

            // init board here
            for (int i = 0; i < aStar.Rows; i++)
            {
                for (int j = 0; j < aStar.Cols; j++)
                {
                    Button btn = new Button();
                    btn.BackColor = System.Drawing.Color.White;
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btn.Location = new System.Drawing.Point(startY + j * 40, startX + i * 40); // a little confuse here, location = (col, row) 
                    btn.Name = i.ToString() + "," + j.ToString(); // name using to store index (row, col) of a cell
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

        private void btnPickSource_Click(object sender, EventArgs e)
        {
            selectedOptionCellPicker = 0;
            btnPick.Text = "Start Pick";
            btnPick.Enabled = true;
            enableToPick = false;
            selectPickSourceButton();
            unselectPickDestinationButton();
            unselectPickBlockedButton();
        }

        private void btnPickDest_Click(object sender, EventArgs e)
        {
            selectedOptionCellPicker = 1;
            btnPick.Text = "Start Pick";
            btnPick.Enabled = true;
            enableToPick = false;
            selectPickDestinationButton();
            unselectPickBlockedButton();
            unselectPickSourceButton();
        }

        private void btnPickBlocked_Click(object sender, EventArgs e)
        {
            selectedOptionCellPicker = 2;
            btnPick.Text = "Start Pick";
            btnPick.Enabled = true;
            enableToPick = false;
            selectPickBlockedButton();
            unselectPickSourceButton();
            unselectPickDestinationButton();
        }

        private void btnPickCell_Leave(object sender, EventArgs e)
        {
            enableToPick = false;
        }

        private void btnCell_Click(object sender, EventArgs e)
        {
            if (!enableToPick) return;

            Button btn = sender as Button;
            bool isSourcePicker = (selectedOptionCellPicker == 0);
            bool isDestinationPicker = (selectedOptionCellPicker == 1);
            bool isBlockedPicker = (selectedOptionCellPicker == 2);

            // if that cell is picked before, unpick it
            if (btn.BackColor != Color.White)
            {
                string[] index = btn.Name.Split(',');
                int row = Int32.Parse(index[0]), col = Int32.Parse(index[1]);

                // if it is a src/dest spot, notice to user and ignore the picker
                if ((row == src.Key && col == src.Value) || (row == dest.Key && col == dest.Value))
                {
                    MessageBox.Show("Source or destination can not be blocked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                btn.BackColor = Color.White;

                // release the storage
                if (isSourcePicker) resetSourceLocation();
                else if (isDestinationPicker) resetDestinationLocation();
            }
            else
            {
                if (isSourcePicker)
                {
                    btn.BackColor = Color.Lime;

                    // if it is another source, remove the color old
                    if (src.Key != -1)
                    {
                        string old_name = src.Key.ToString() + "," + src.Value.ToString();
                        displayArea.Controls[old_name].BackColor = Color.White;
                    }


                    // update src
                    string[] index = btn.Name.Split(',');
                    src = new KeyValuePair<int, int>(Int32.Parse(index[0]), Int32.Parse(index[1]));

                }
                else if (isDestinationPicker)
                {
                    btn.BackColor = Color.OrangeRed;

                    // if it is another destination, remove the color old
                    if (dest.Key != -1)
                    {
                        string old_name = dest.Key.ToString() + "," + dest.Value.ToString();
                        displayArea.Controls[old_name].BackColor = Color.White;
                    }

                    // update src
                    string[] index = btn.Name.Split(',');
                    dest = new KeyValuePair<int, int>(Int32.Parse(index[0]), Int32.Parse(index[1]));
                }
                else if (isBlockedPicker)
                {
                    btn.BackColor = SystemColors.AppWorkspace;
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
                    c.BackColor = Color.White;
                }
            }

            // reset properties of A star
            resetSourceLocation();
            resetDestinationLocation();
            aStar.Result.Clear();

        }

        private void resetSourceLocation()
        {
            src = new KeyValuePair<int, int>(-1, -1);
        }

        private void resetDestinationLocation()
        {
            dest = new KeyValuePair<int, int>(-1, -1);
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            if (btnPick.Text == "Start Pick")
            {
                enableToPick = true;
                btnPick.Text = "Stop Pick";
            }
            else
            {
                enableToPick = false;
                btnPick.Enabled = false;
                btnPick.Text = "Start Pick";
                unselectPickBlockedButton();
                unselectPickDestinationButton();
                unselectPickSourceButton();
            }
        }

        private void selectPickSourceButton()
        {
            btnPickSource.FlatAppearance.BorderColor = Color.Blue;
            btnPickSource.FlatAppearance.BorderSize = 2;
        }

        private void unselectPickSourceButton()
        {
            btnPickSource.FlatAppearance.BorderColor = Color.Black;
            btnPickSource.FlatAppearance.BorderSize = 1;
        }

        private void selectPickDestinationButton()
        {
            btnPickDest.FlatAppearance.BorderColor = Color.Blue;
            btnPickDest.FlatAppearance.BorderSize = 2;
        }

        private void unselectPickDestinationButton()
        {
            btnPickDest.FlatAppearance.BorderColor = Color.Black;
            btnPickDest.FlatAppearance.BorderSize = 1;
        }

        private void selectPickBlockedButton()
        {
            btnPickBlocked.FlatAppearance.BorderColor = Color.Blue;
            btnPickBlocked.FlatAppearance.BorderSize = 2;
        }

        private void unselectPickBlockedButton()
        {
            btnPickBlocked.FlatAppearance.BorderColor = Color.Black;
            btnPickBlocked.FlatAppearance.BorderSize = 1;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            aStar.aStarSearch(getGrid(), src, dest);

            Stack<KeyValuePair<int, int>> result = aStar.Result;

            // show in solution
            tbSolution.Text = "";
            if (aStar.Result.Count() > 0)
            {
                tbSolution.Text = "Summary: " + aStar.Result.Count().ToString() + " steps";
                foreach (var i in result)
                {
                    tbSolution.Text = "(" + i.Key.ToString() + ", " + i.Value.ToString() + ")" + '\n' + tbSolution.Text;
                }
            }
            else
            {
                tbSolution.Text = "No Path!";
            }

            // refresh display
            displayArea.Refresh();
        }

        private int[,] getGrid()
        {
            int[,] grid = new int[aStar.Rows, aStar.Cols];

            foreach (Control c in displayArea.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    string[] index = c.Name.Split(',');
                    int row = Int32.Parse(index[0]), col = Int32.Parse(index[1]);
                    grid[row, col] = (c.BackColor == SystemColors.AppWorkspace ? 0 : 1); // 0: blocked, 1: can move
                }
            }

            return grid;
        }

        private void cbDiagonal_CheckedChanged(object sender, EventArgs e)
        {
            aStar.AllowDiagonalMove = cbDiagonal.Checked;
        }

        private void cbShowPath_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPath.Checked)
            {
                pen.Color = Color.Blue;
            }
            else
            {
                pen.Color = Color.FromArgb(0, 0, 0, 0);
            }

            // redraw
            displayArea.Refresh();
        }

        private void PathDraw_Paint(object sender, PaintEventArgs e)
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
}
