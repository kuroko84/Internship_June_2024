namespace TestWF
{
    partial class TestWF
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
            clickShowText = new Button();
            lblRequire1 = new Label();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // clickShowText
            // 
            clickShowText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            clickShowText.BackColor = SystemColors.InactiveCaption;
            clickShowText.Font = new Font("Segoe UI Emoji", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clickShowText.ForeColor = Color.RoyalBlue;
            clickShowText.ImageAlign = ContentAlignment.TopRight;
            clickShowText.Location = new Point(12, 371);
            clickShowText.Name = "clickShowText";
            clickShowText.Size = new Size(284, 59);
            clickShowText.TabIndex = 0;
            clickShowText.Text = "Click Me";
            clickShowText.UseVisualStyleBackColor = false;
            clickShowText.Click += button1_Click;
            // 
            // lblRequire1
            // 
            lblRequire1.AutoSize = true;
            lblRequire1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRequire1.Location = new Point(12, 9);
            lblRequire1.Name = "lblRequire1";
            lblRequire1.Size = new Size(176, 25);
            lblRequire1.TabIndex = 1;
            lblRequire1.Text = "Nhập gì đó vào đây";
            lblRequire1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(12, 37);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(284, 39);
            textBox1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 82);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(284, 283);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // TestWF
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 192);
            ClientSize = new Size(308, 442);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            Controls.Add(lblRequire1);
            Controls.Add(clickShowText);
            Name = "TestWF";
            Text = "Test Window Form";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button clickShowText;
        private Label lblRequire1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
    }
}
