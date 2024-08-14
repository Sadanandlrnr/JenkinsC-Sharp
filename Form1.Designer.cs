namespace Single_Click
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            JenkinsURL = new ComboBox();
            Jobs = new ComboBox();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            button2 = new Button();
            Parameter = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // JenkinsURL
            // 
            JenkinsURL.FormattingEnabled = true;
            JenkinsURL.Location = new Point(45, 48);
            JenkinsURL.Name = "JenkinsURL";
            JenkinsURL.Size = new Size(393, 33);
            JenkinsURL.TabIndex = 0;
            JenkinsURL.SelectedIndexChanged += JenkinsURL_SelectedIndexChanged;
            JenkinsURL.Leave += JenkinsURL_Leave;
            // 
            // Jobs
            // 
            Jobs.FormattingEnabled = true;
            Jobs.Location = new Point(497, 48);
            Jobs.Name = "Jobs";
            Jobs.Size = new Size(393, 33);
            Jobs.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(965, 52);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 2;
            button1.Text = "Fetch";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 34;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Parameter, Value });
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.GridColor = SystemColors.ButtonFace;
            dataGridView1.ImeMode = ImeMode.On;
            dataGridView1.Location = new Point(232, 44);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(727, 344);
            dataGridView1.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top;
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(45, 108);
            groupBox1.Name = "groupBox1";
            groupBox1.RightToLeft = RightToLeft.No;
            groupBox1.Size = new Size(1176, 465);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Build Parameters";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // button2
            // 
            button2.Location = new Point(491, 410);
            button2.Name = "button2";
            button2.Size = new Size(230, 34);
            button2.TabIndex = 4;
            button2.Text = "Update Build Parameters";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Parameter
            // 
            Parameter.HeaderText = "Parameter";
            Parameter.MinimumWidth = 8;
            Parameter.Name = "Parameter";
            Parameter.Width = 400;
            // 
            // Value
            // 
            Value.HeaderText = "Value";
            Value.MinimumWidth = 8;
            Value.Name = "Value";
            Value.Width = 400;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1421, 750);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(Jobs);
            Controls.Add(JenkinsURL);
            Name = "Form1";
            Text = "Single Click";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox JenkinsURL;
        private ComboBox Jobs;
        private Button button1;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Button button2;
        private DataGridViewTextBoxColumn Parameter;
        private DataGridViewTextBoxColumn Value;
    }
}
