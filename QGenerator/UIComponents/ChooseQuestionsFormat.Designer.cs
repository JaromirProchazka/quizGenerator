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
            radioButton1.Location = new Point(6, 26);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(62, 24);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "AND";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += AndOption_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(99, 26);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(50, 24);
            radioButton2.TabIndex = 1;
            radioButton2.Text = "OR";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += OrOption_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(579, 60);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Select, If the looked for properties should be required all (AND) or at least one (OR)";
            // 
            // button1
            // 
            button1.Location = new Point(20, 371);
            button1.Name = "button1";
            button1.Size = new Size(138, 59);
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
            comboBox1.Location = new Point(12, 78);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(180, 28);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedIndexChanged += nameInput_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(198, 81);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 5;
            label1.Text = "Node Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(198, 115);
            label2.Name = "label2";
            label2.Size = new Size(213, 20);
            label2.TabIndex = 7;
            label2.Text = "Node Text Color (word or Hex)";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 146);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(353, 27);
            textBox1.TabIndex = 8;
            textBox1.TextChanged += classesInput_TextChanged_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(379, 149);
            label3.Name = "label3";
            label3.Size = new Size(409, 20);
            label3.TabIndex = 9;
            label3.Text = "Classes separated with spaces ' ' (node with any class passes)";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(379, 182);
            label4.Name = "label4";
            label4.Size = new Size(158, 20);
            label4.TabIndex = 11;
            label4.Text = "Node Text Font-Family";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 179);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(353, 27);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += fontInput_TextChanged;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 112);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(180, 27);
            textBox3.TabIndex = 12;
            textBox3.TextChanged += colorInput_TextChanged;
            // 
            // ChooseQuestionsFormat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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