namespace QuizGeneratorPresentation.MainPage
{
    partial class topicEditBox
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
            components = new System.ComponentModel.Container();
            renameTextInput = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            renameBtn = new Button();
            deleteBtn = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            renameTextInput.Location = new Point(16, 54);
            renameTextInput.Margin = new Padding(4, 5, 4, 5);
            renameTextInput.Name = "textBox1";
            renameTextInput.Size = new Size(257, 27);
            renameTextInput.TabIndex = 0;
            renameTextInput.Text = "NewQuiz";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // button1
            // 
            renameBtn.Location = new Point(283, 51);
            renameBtn.Margin = new Padding(4, 5, 4, 5);
            renameBtn.Name = "button1";
            renameBtn.Size = new Size(100, 35);
            renameBtn.TabIndex = 2;
            renameBtn.Text = "Rename\r\n";
            renameBtn.UseVisualStyleBackColor = true;
            renameBtn.Click += renameBtn_Click;
            // 
            // button2
            // 
            deleteBtn.Location = new Point(83, 126);
            deleteBtn.Margin = new Padding(4, 5, 4, 5);
            deleteBtn.Name = "button2";
            deleteBtn.Size = new Size(233, 111);
            deleteBtn.TabIndex = 3;
            deleteBtn.Text = "Delete";
            deleteBtn.UseVisualStyleBackColor = true;
            deleteBtn.Click += deleteBtn_Click;
            // 
            // topicEditBox
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(399, 275);
            Controls.Add(deleteBtn);
            Controls.Add(renameBtn);
            Controls.Add(renameTextInput);
            Margin = new Padding(4, 5, 4, 5);
            Name = "topicEditBox";
            Text = "Topic Editor";
            Load += topicEditBox_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox renameTextInput;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button renameBtn;
        private System.Windows.Forms.Button deleteBtn;
    }
}