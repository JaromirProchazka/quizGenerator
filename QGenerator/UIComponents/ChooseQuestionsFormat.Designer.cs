namespace QGenerator.UIComponents
{
    partial class ChooseQuestionsFormat
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
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            groupBox1 = new GroupBox();
            button1 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(5, 20);
            radioButton1.Margin = new Padding(3, 2, 3, 2);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(50, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "AND";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += AndOption_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(87, 20);
            radioButton2.Margin = new Padding(3, 2, 3, 2);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(41, 19);
            radioButton2.TabIndex = 1;
            radioButton2.Text = "OR";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += OrOption_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Location = new Point(10, 9);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(507, 45);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Select, If the looked for properties should be required all (AND) or at least one (OR)";
            // 
            // button1
            // 
            button1.Location = new Point(18, 278);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(121, 44);
            button1.TabIndex = 3;
            button1.Text = "Continue";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ContinueBtn_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "italics (em)", "bold (strong)", "heading1 (h1)", "heading2 (h2)", "heading3 (h3)", "hyperlink (a)", "unordered list (ul)", "order lists (li)", "description lists (dl)" });
            comboBox1.Location = new Point(10, 58);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(158, 23);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedIndexChanged += nameInput_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(173, 61);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 5;
            label1.Text = "Node Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(173, 86);
            label2.Name = "label2";
            label2.Size = new Size(168, 15);
            label2.TabIndex = 7;
            label2.Text = "Node Text Color (word or Hex)";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(10, 110);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(309, 23);
            textBox1.TabIndex = 8;
            textBox1.TextChanged += classesInput_TextChanged_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(332, 112);
            label3.Name = "label3";
            label3.Size = new Size(326, 15);
            label3.TabIndex = 9;
            label3.Text = "Classes separated with spaces ' ' (node with any class passes)";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(332, 136);
            label4.Name = "label4";
            label4.Size = new Size(127, 15);
            label4.TabIndex = 11;
            label4.Text = "Node Text Font-Family";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(10, 134);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(309, 23);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += fontInput_TextChanged;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(10, 84);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(158, 23);
            textBox3.TabIndex = 12;
            textBox3.TextChanged += colorInput_TextChanged;
            // 
            // ChooseQuestionsFormat
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ChooseQuestionsFormat";
            Text = "ChooseQuestionsFormat";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private GroupBox groupBox1;
        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private Label label4;
        private TextBox textBox2;
        private TextBox textBox3;
    }
}