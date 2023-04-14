namespace djvu
{
    partial class Template
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
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            button2 = new Button();
            groupBox2 = new GroupBox();
            TempliteLable = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.HighlightText;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.djvuLogo;
            pictureBox1.Location = new Point(11, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(383, 425);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.HighlightText;
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(401, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(386, 427);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Template";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Highlight;
            button2.Location = new Point(57, 338);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 2;
            button2.Text = "Back";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click_1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(TempliteLable);
            groupBox2.Controls.Add(comboBox2);
            groupBox2.Controls.Add(comboBox1);
            groupBox2.Location = new Point(46, 73);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(300, 242);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // TempliteLable
            // 
            TempliteLable.AutoSize = true;
            TempliteLable.Font = new Font("Stencil", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TempliteLable.Location = new Point(34, 27);
            TempliteLable.Name = "TempliteLable";
            TempliteLable.Size = new Size(206, 21);
            TempliteLable.TabIndex = 4;
            TempliteLable.Text = "Select Pos Template";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "A4", "88mm", "54mm" });
            comboBox2.Location = new Point(20, 165);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(258, 33);
            comboBox2.TabIndex = 3;
            comboBox2.Text = "Select Reciept Size";
            // 
            // comboBox1
            // 
            comboBox1.DropDownHeight = 100;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.ItemHeight = 25;
            comboBox1.Items.AddRange(new object[] { "QuickBooks", "QuickBooks POS", "Zoho Books", "Zero", "Odo", "Misoo Pos", "Crystal", "Samba Pos", "Hotel Wise", "Wave 2 system Pos", "Mango Pos", "Robisearch Pos", "Retailman", "Retailware", "Sage", "Fumes Pos", "Pastel", "", "" });
            comboBox1.Location = new Point(20, 92);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(258, 33);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "select a template";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Highlight;
            button1.Location = new Point(233, 338);
            button1.Name = "button1";
            button1.Size = new Size(111, 33);
            button1.TabIndex = 0;
            button1.Text = "Next";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Template
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            MaximumSize = new Size(822, 506);
            MinimumSize = new Size(822, 506);
            Name = "Template";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pos Template";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ComboBox comboBox1;
        private Button button1;
        private Label TempliteLable;
        private ComboBox comboBox2;
        private Button button2;
    }
}