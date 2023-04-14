namespace djvu
{
    partial class Initiolize
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
            comboBox1 = new ComboBox();
            label5 = new Label();
            vscuipText = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.djvuLogo;
            pictureBox1.Location = new Point(10, 96);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(364, 233);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(vscuipText);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(380, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(396, 433);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Initiolization";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Highlight;
            button2.Location = new Point(30, 353);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 12;
            button2.Text = "Back";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "OSCU", "VSCU(EMBEDED)", "VSCU" });
            comboBox1.Location = new Point(156, 57);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(220, 33);
            comboBox1.TabIndex = 11;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 303);
            label5.Name = "label5";
            label5.Size = new Size(76, 25);
            label5.TabIndex = 10;
            label5.Text = "VSCU IP";
            label5.Visible = false;
            // 
            // vscuipText
            // 
            vscuipText.Location = new Point(156, 297);
            vscuipText.Name = "vscuipText";
            vscuipText.Size = new Size(224, 31);
            vscuipText.TabIndex = 9;
            vscuipText.Visible = false;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(156, 223);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(220, 31);
            textBox4.TabIndex = 8;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(156, 162);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(220, 31);
            textBox3.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(156, 111);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(220, 31);
            textBox2.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 229);
            label4.Name = "label4";
            label4.Size = new Size(86, 25);
            label4.TabIndex = 4;
            label4.Text = "DvcSrlNo";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 168);
            label3.Name = "label3";
            label3.Size = new Size(56, 25);
            label3.TabIndex = 3;
            label3.Text = "BhfID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 117);
            label2.Name = "label2";
            label2.Size = new Size(35, 25);
            label2.TabIndex = 2;
            label2.Text = "Tin";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 60);
            label1.Name = "label1";
            label1.Size = new Size(102, 25);
            label1.TabIndex = 1;
            label1.Text = "Connection";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Highlight;
            button1.Location = new Point(264, 353);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 0;
            button1.Text = "Next";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Initiolize
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HighlightText;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            MaximumSize = new Size(822, 506);
            MinimumSize = new Size(822, 506);
            Name = "Initiolize";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Initiolization";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private GroupBox groupBox1;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button button1;
        private ComboBox comboBox1;
        private Label label5;
        private TextBox vscuipText;
        private Button button2;
    }
}