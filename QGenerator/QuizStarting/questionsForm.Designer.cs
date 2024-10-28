namespace QuizGeneratorPresentation.QuizStarting
{
    partial class questionsForm
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
            webBrowser1 = new WebBrowser();
            webBrowser2 = new WebBrowser();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // webBrowser1
            // 
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.Location = new Point(0, 0);
            webBrowser1.Margin = new Padding(2, 3, 2, 3);
            webBrowser1.MinimumSize = new Size(23, 23);
            webBrowser1.Name = "webBrowser1";
            webBrowser1.Size = new Size(933, 519);
            webBrowser1.TabIndex = 0;
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            // 
            // webBrowser2
            // 
            webBrowser2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webBrowser2.Location = new Point(0, 75);
            webBrowser2.Margin = new Padding(2, 3, 2, 3);
            webBrowser2.MinimumSize = new Size(23, 23);
            webBrowser2.Name = "webBrowser2";
            webBrowser2.Size = new Size(919, 444);
            webBrowser2.TabIndex = 1;
            webBrowser2.DocumentCompleted += webBrowser2_DocumentCompleted;
            // 
            // button1
            // 
            button1.Location = new Point(14, 14);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(110, 40);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += godNext_Click;
            // 
            // button2
            // 
            button2.Location = new Point(149, 14);
            button2.Margin = new Padding(2, 3, 2, 3);
            button2.Name = "button2";
            button2.Size = new Size(110, 40);
            button2.TabIndex = 3;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += badNext_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.Location = new Point(793, 14);
            button3.Margin = new Padding(2, 3, 2, 3);
            button3.Name = "button3";
            button3.Size = new Size(106, 39);
            button3.TabIndex = 4;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += backButton_Click;
            // 
            // button4
            // 
            button4.Location = new Point(323, 13);
            button4.Margin = new Padding(2, 3, 2, 3);
            button4.Name = "button4";
            button4.Size = new Size(110, 40);
            button4.TabIndex = 5;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += AnswearButton_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Location = new Point(566, 23);
            textBox1.Margin = new Padding(2, 3, 2, 3);
            textBox1.MaximumSize = new Size(83, 20);
            textBox1.MinimumSize = new Size(83, 20);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(83, 20);
            textBox1.TabIndex = 6;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // questionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(textBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(webBrowser2);
            Controls.Add(webBrowser1);
            Margin = new Padding(2, 3, 2, 3);
            Name = "questionsForm";
            Text = "Quiz Generator";
            Load += questionsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
    }
}