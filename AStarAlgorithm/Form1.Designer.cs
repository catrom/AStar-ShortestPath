namespace AStarAlgorithm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudRow = new System.Windows.Forms.NumericUpDown();
            this.nudCol = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPickSource = new System.Windows.Forms.Button();
            this.btnPickDest = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPickBlocked = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnResetSize = new System.Windows.Forms.Button();
            this.btnApplySize = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPick = new System.Windows.Forms.Button();
            this.btnResetCells = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDiagonal = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbShowPath = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbbHeuristic = new System.Windows.Forms.ComboBox();
            this.cbTracePathByStep = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbSolution = new System.Windows.Forms.RichTextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.displayArea = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGoNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Input nums row:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Input nums col:";
            // 
            // nudRow
            // 
            this.nudRow.Location = new System.Drawing.Point(130, 23);
            this.nudRow.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.nudRow.Name = "nudRow";
            this.nudRow.Size = new System.Drawing.Size(45, 20);
            this.nudRow.TabIndex = 1;
            this.nudRow.Tag = "1";
            // 
            // nudCol
            // 
            this.nudCol.Location = new System.Drawing.Point(130, 58);
            this.nudCol.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.nudCol.Name = "nudCol";
            this.nudCol.Size = new System.Drawing.Size(45, 20);
            this.nudCol.TabIndex = 2;
            this.nudCol.Tag = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pick source cell: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Pick destination cell:";
            // 
            // btnPickSource
            // 
            this.btnPickSource.BackColor = System.Drawing.Color.Green;
            this.btnPickSource.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPickSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPickSource.Location = new System.Drawing.Point(130, 23);
            this.btnPickSource.Name = "btnPickSource";
            this.btnPickSource.Size = new System.Drawing.Size(45, 23);
            this.btnPickSource.TabIndex = 2;
            this.btnPickSource.UseVisualStyleBackColor = false;
            this.btnPickSource.Click += new System.EventHandler(this.btnPickSource_Click);
            // 
            // btnPickDest
            // 
            this.btnPickDest.BackColor = System.Drawing.Color.Red;
            this.btnPickDest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPickDest.Location = new System.Drawing.Point(130, 59);
            this.btnPickDest.Name = "btnPickDest";
            this.btnPickDest.Size = new System.Drawing.Size(45, 23);
            this.btnPickDest.TabIndex = 2;
            this.btnPickDest.UseVisualStyleBackColor = false;
            this.btnPickDest.Click += new System.EventHandler(this.btnPickDest_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Select blocked cells:";
            // 
            // btnPickBlocked
            // 
            this.btnPickBlocked.BackColor = System.Drawing.Color.Gray;
            this.btnPickBlocked.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPickBlocked.Location = new System.Drawing.Point(130, 94);
            this.btnPickBlocked.Name = "btnPickBlocked";
            this.btnPickBlocked.Size = new System.Drawing.Size(45, 23);
            this.btnPickBlocked.TabIndex = 2;
            this.btnPickBlocked.UseVisualStyleBackColor = false;
            this.btnPickBlocked.Click += new System.EventHandler(this.BtnPickBlocked_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnResetSize);
            this.groupBox1.Controls.Add(this.btnApplySize);
            this.groupBox1.Controls.Add(this.nudCol);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudRow);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 121);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Board Size";
            // 
            // btnResetSize
            // 
            this.btnResetSize.Location = new System.Drawing.Point(19, 92);
            this.btnResetSize.Name = "btnResetSize";
            this.btnResetSize.Size = new System.Drawing.Size(75, 23);
            this.btnResetSize.TabIndex = 3;
            this.btnResetSize.Text = "Reset";
            this.btnResetSize.UseVisualStyleBackColor = true;
            this.btnResetSize.Click += new System.EventHandler(this.btnResetSize_Click);
            // 
            // btnApplySize
            // 
            this.btnApplySize.Location = new System.Drawing.Point(100, 92);
            this.btnApplySize.Name = "btnApplySize";
            this.btnApplySize.Size = new System.Drawing.Size(75, 23);
            this.btnApplySize.TabIndex = 4;
            this.btnApplySize.Text = "Apply";
            this.btnApplySize.UseVisualStyleBackColor = true;
            this.btnApplySize.Click += new System.EventHandler(this.btnApplySize_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPick);
            this.groupBox2.Controls.Add(this.btnResetCells);
            this.groupBox2.Controls.Add(this.btnPickSource);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnPickBlocked);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnPickDest);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(206, 162);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pick Cells";
            // 
            // btnPick
            // 
            this.btnPick.Location = new System.Drawing.Point(100, 133);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(75, 23);
            this.btnPick.TabIndex = 6;
            this.btnPick.Text = "Start Pick";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // btnResetCells
            // 
            this.btnResetCells.Location = new System.Drawing.Point(19, 133);
            this.btnResetCells.Name = "btnResetCells";
            this.btnResetCells.Size = new System.Drawing.Size(75, 23);
            this.btnResetCells.TabIndex = 5;
            this.btnResetCells.Text = "Reset";
            this.btnResetCells.UseVisualStyleBackColor = true;
            this.btnResetCells.Click += new System.EventHandler(this.btnResetCells_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Allow diagonal move:";
            // 
            // cbDiagonal
            // 
            this.cbDiagonal.AutoSize = true;
            this.cbDiagonal.Checked = true;
            this.cbDiagonal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDiagonal.Location = new System.Drawing.Point(160, 30);
            this.cbDiagonal.Name = "cbDiagonal";
            this.cbDiagonal.Size = new System.Drawing.Size(15, 14);
            this.cbDiagonal.TabIndex = 7;
            this.cbDiagonal.UseVisualStyleBackColor = true;
            this.cbDiagonal.CheckedChanged += new System.EventHandler(this.cbDiagonal_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Show path:";
            // 
            // cbShowPath
            // 
            this.cbShowPath.AutoSize = true;
            this.cbShowPath.Checked = true;
            this.cbShowPath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowPath.Location = new System.Drawing.Point(160, 61);
            this.cbShowPath.Name = "cbShowPath";
            this.cbShowPath.Size = new System.Drawing.Size(15, 14);
            this.cbShowPath.TabIndex = 8;
            this.cbShowPath.UseVisualStyleBackColor = true;
            this.cbShowPath.CheckedChanged += new System.EventHandler(this.cbShowPath_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbbHeuristic);
            this.groupBox3.Controls.Add(this.cbDiagonal);
            this.groupBox3.Controls.Add(this.cbTracePathByStep);
            this.groupBox3.Controls.Add(this.cbShowPath);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(13, 359);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(205, 159);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // cbbHeuristic
            // 
            this.cbbHeuristic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbHeuristic.FormattingEnabled = true;
            this.cbbHeuristic.Location = new System.Drawing.Point(78, 122);
            this.cbbHeuristic.Name = "cbbHeuristic";
            this.cbbHeuristic.Size = new System.Drawing.Size(96, 21);
            this.cbbHeuristic.TabIndex = 10;
            this.cbbHeuristic.SelectedIndexChanged += new System.EventHandler(this.cbbHeuristic_SelectedIndexChanged);
            // 
            // cbTracePathByStep
            // 
            this.cbTracePathByStep.AutoSize = true;
            this.cbTracePathByStep.Location = new System.Drawing.Point(160, 92);
            this.cbTracePathByStep.Name = "cbTracePathByStep";
            this.cbTracePathByStep.Size = new System.Drawing.Size(15, 14);
            this.cbTracePathByStep.TabIndex = 9;
            this.cbTracePathByStep.UseVisualStyleBackColor = true;
            this.cbTracePathByStep.CheckedChanged += new System.EventHandler(this.cbTracePathByStep_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Heuristics:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Step by Steps";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbSolution);
            this.groupBox4.Location = new System.Drawing.Point(265, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(156, 312);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Solution";
            // 
            // tbSolution
            // 
            this.tbSolution.Enabled = false;
            this.tbSolution.Location = new System.Drawing.Point(6, 23);
            this.tbSolution.Name = "tbSolution";
            this.tbSolution.Size = new System.Drawing.Size(144, 283);
            this.tbSolution.TabIndex = 0;
            this.tbSolution.Text = "";
            // 
            // btnCalc
            // 
            this.btnCalc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCalc.Location = new System.Drawing.Point(265, 363);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(156, 45);
            this.btnCalc.TabIndex = 9;
            this.btnCalc.Text = "CALCULATE";
            this.btnCalc.UseVisualStyleBackColor = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // displayArea
            // 
            this.displayArea.Location = new System.Drawing.Point(463, 12);
            this.displayArea.Name = "displayArea";
            this.displayArea.Size = new System.Drawing.Size(507, 506);
            this.displayArea.TabIndex = 9;
            this.displayArea.TabStop = false;
            this.displayArea.Text = "Display";
            this.displayArea.Paint += new System.Windows.Forms.PaintEventHandler(this.PathDraw_Paint);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label11.Location = new System.Drawing.Point(264, 418);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Created by CatRom\r\n";
            // 
            // btnGoNext
            // 
            this.btnGoNext.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGoNext.Location = new System.Drawing.Point(265, 440);
            this.btnGoNext.Name = "btnGoNext";
            this.btnGoNext.Size = new System.Drawing.Size(79, 35);
            this.btnGoNext.TabIndex = 7;
            this.btnGoNext.Text = "Next Step >>";
            this.btnGoNext.UseVisualStyleBackColor = false;
            this.btnGoNext.Click += new System.EventHandler(this.btnGoNext_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(982, 528);
            this.Controls.Add(this.btnGoNext);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.displayArea);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "A* Algorithm - Finding Path";
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudRow;
        private System.Windows.Forms.NumericUpDown nudCol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPickSource;
        private System.Windows.Forms.Button btnPickDest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPickBlocked;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbDiagonal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbShowPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox tbSolution;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.GroupBox displayArea;
        private System.Windows.Forms.Button btnResetSize;
        private System.Windows.Forms.Button btnApplySize;
        private System.Windows.Forms.Button btnResetCells;
        private System.Windows.Forms.Button btnPick;
        private System.Windows.Forms.CheckBox cbTracePathByStep;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbbHeuristic;
        private System.Windows.Forms.Button btnGoNext;
    }
}

