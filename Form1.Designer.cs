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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            JenkinsURL = new ComboBox();
            Jobs = new ComboBox();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            Parameter = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // JenkinsURL
            // 
            JenkinsURL.FormattingEnabled = true;
            JenkinsURL.Location = new Point(54, 64);
            JenkinsURL.Name = "JenkinsURL";
            JenkinsURL.Size = new Size(393, 33);
            JenkinsURL.TabIndex = 0;
            JenkinsURL.SelectedIndexChanged += JenkinsURL_SelectedIndexChanged;
            JenkinsURL.DataContextChanged += JenkinsURL_SelectedIndexChanged;
            JenkinsURL.Leave += JenkinsURL_Leave;
            // 
            // Jobs
            // 
            Jobs.FormattingEnabled = true;
            Jobs.Location = new Point(559, 64);
            Jobs.Name = "Jobs";
            Jobs.Size = new Size(393, 33);
            Jobs.TabIndex = 1;
            Jobs.SelectedIndexChanged += Jobs_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(1079, 64);
            button1.Name = "button1";
            button1.Size = new Size(112, 35);
            button1.TabIndex = 2;
            button1.Text = "Fetch";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 1, true);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 34;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Parameter, Value });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.GridColor = SystemColors.ButtonFace;
            dataGridView1.ImeMode = ImeMode.On;
            dataGridView1.Location = new Point(195, 42);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(863, 344);
            dataGridView1.TabIndex = 3;
            // 
            // Parameter
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Parameter.DefaultCellStyle = dataGridViewCellStyle2;
            Parameter.HeaderText = "Parameter";
            Parameter.MinimumWidth = 8;
            Parameter.Name = "Parameter";
            Parameter.Width = 400;
            // 
            // Value
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Value.DefaultCellStyle = dataGridViewCellStyle3;
            Value.HeaderText = "Value";
            Value.MinimumWidth = 8;
            Value.Name = "Value";
            Value.Width = 400;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top;
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 1, true);
            groupBox1.Location = new Point(54, 154);
            groupBox1.Name = "groupBox1";
            groupBox1.RightToLeft = RightToLeft.No;
            groupBox1.Size = new Size(1146, 465);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Build Parameters";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // button2
            // 
            button2.Location = new Point(491, 410);
            button2.Name = "button2";
            button2.Size = new Size(252, 34);
            button2.TabIndex = 4;
            button2.Text = "Update Default Parameters";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(579, 625);
            button3.Name = "button3";
            button3.Size = new Size(187, 42);
            button3.TabIndex = 5;
            button3.Text = "Run All-In-One";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 24);
            label1.Name = "label1";
            label1.Size = new Size(104, 25);
            label1.TabIndex = 6;
            label1.Text = "Jenkins URL";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(559, 24);
            label2.Name = "label2";
            label2.Size = new Size(101, 25);
            label2.TabIndex = 7;
            label2.Text = "Jenkins Job";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1421, 750);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button3);
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
            PerformLayout();
        }

        #endregion

        private ComboBox JenkinsURL;
        private ComboBox Jobs;
        private Button button1;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Button button2;
        private Button button3;
        private DataGridViewTextBoxColumn Parameter;
        private DataGridViewTextBoxColumn Value;
        private Label label1;
        private Label label2;
    }
}
