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
            QuestionsHtmlVisualizer = new WebBrowser();
            nextGoodBtn = new Button();
            nextBadBtn = new Button();
            quitBtn = new Button();
            showAnswerBtn = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // QuestionsHtmlVisualizer
            // 
            QuestionsHtmlVisualizer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            QuestionsHtmlVisualizer.Location = new Point(0, 75);
            QuestionsHtmlVisualizer.Margin = new Padding(2, 3, 2, 3);
            QuestionsHtmlVisualizer.MinimumSize = new Size(23, 23);
            QuestionsHtmlVisualizer.Name = "QuestionsHtmlVisualizer";
            QuestionsHtmlVisualizer.Size = new Size(919, 444);
            QuestionsHtmlVisualizer.TabIndex = 1;
            QuestionsHtmlVisualizer.DocumentCompleted += QuestionsHtmlVisualizer_DocumentCompleted;
            // 
            // button1
            // 
            nextGoodBtn.Location = new Point(14, 14);
            nextGoodBtn.Margin = new Padding(2, 3, 2, 3);
            nextGoodBtn.Name = "button1";
            nextGoodBtn.Size = new Size(110, 40);
            nextGoodBtn.TabIndex = 2;
            nextGoodBtn.Text = "button1";
            nextGoodBtn.UseVisualStyleBackColor = true;
            nextGoodBtn.Click += godNext_Click;
            // 
            // button2
            // 
            nextBadBtn.Location = new Point(149, 14);
            nextBadBtn.Margin = new Padding(2, 3, 2, 3);
            nextBadBtn.Name = "button2";
            nextBadBtn.Size = new Size(110, 40);
            nextBadBtn.TabIndex = 3;
            nextBadBtn.Text = "button2";
            nextBadBtn.UseVisualStyleBackColor = true;
            nextBadBtn.Click += badNext_Click;
            // 
            // button3
            // 
            quitBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            quitBtn.Location = new Point(793, 14);
            quitBtn.Margin = new Padding(2, 3, 2, 3);
            quitBtn.Name = "button3";
            quitBtn.Size = new Size(106, 39);
            quitBtn.TabIndex = 4;
            quitBtn.Text = "button3";
            quitBtn.UseVisualStyleBackColor = true;
            quitBtn.Click += backButton_Click;
            // 
            // button4
            // 
            showAnswerBtn.Location = new Point(323, 13);
            showAnswerBtn.Margin = new Padding(2, 3, 2, 3);
            showAnswerBtn.Name = "button4";
            showAnswerBtn.Size = new Size(110, 40);
            showAnswerBtn.TabIndex = 5;
            showAnswerBtn.Text = "button4";
            showAnswerBtn.UseVisualStyleBackColor = true;
            showAnswerBtn.Click += AnswearButton_Click;
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
            Controls.Add(showAnswerBtn);
            Controls.Add(quitBtn);
            Controls.Add(nextBadBtn);
            Controls.Add(nextGoodBtn);
            Controls.Add(QuestionsHtmlVisualizer);
            Margin = new Padding(2, 3, 2, 3);
            Name = "questionsForm";
            Text = "Quiz Generator";
            Load += questionsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.WebBrowser QuestionsHtmlVisualizer;
        private System.Windows.Forms.Button nextGoodBtn;
        private System.Windows.Forms.Button nextBadBtn;
        private System.Windows.Forms.Button quitBtn;
        private System.Windows.Forms.Button showAnswerBtn;
        private System.Windows.Forms.TextBox textBox1;
    }
}