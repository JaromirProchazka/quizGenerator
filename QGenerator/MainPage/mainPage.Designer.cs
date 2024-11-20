namespace QuizGeneratorPresentation.MainPage
{
    partial class mainPage
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
            createBtn = new Button();
            TopicsList = new ListBox();
            openFileDialog1 = new OpenFileDialog();
            openBtn = new Button();
            editBtn = new Button();
            SuspendLayout();
            // 
            // createBtn
            // 
            createBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            createBtn.Location = new Point(14, 460);
            createBtn.Margin = new Padding(4, 3, 4, 3);
            createBtn.Name = "createBtn";
            createBtn.Size = new Size(198, 45);
            createBtn.TabIndex = 0;
            createBtn.Text = "Create New Topic";
            createBtn.UseVisualStyleBackColor = true;
            createBtn.Click += newTopicBtn_Click;
            // 
            // TopicsList
            // 
            TopicsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TopicsList.BorderStyle = BorderStyle.None;
            TopicsList.Cursor = Cursors.Hand;
            TopicsList.FormattingEnabled = true;
            TopicsList.ItemHeight = 15;
            TopicsList.Location = new Point(14, 14);
            TopicsList.Margin = new Padding(4, 3, 4, 3);
            TopicsList.Name = "TopicsList";
            TopicsList.Size = new Size(905, 420);
            TopicsList.TabIndex = 1;
            TopicsList.SelectedIndexChanged += Topics_SelectedIndexChanged;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // openBtn
            // 
            openBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            openBtn.Location = new Point(516, 460);
            openBtn.Margin = new Padding(4, 3, 4, 3);
            openBtn.Name = "openBtn";
            openBtn.Size = new Size(198, 45);
            openBtn.TabIndex = 2;
            openBtn.Text = "Open";
            openBtn.UseVisualStyleBackColor = true;
            openBtn.Click += openBtn_Click;
            // 
            // editBtn
            // 
            editBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            editBtn.Location = new Point(721, 460);
            editBtn.Margin = new Padding(4, 3, 4, 3);
            editBtn.Name = "editBtn";
            editBtn.Size = new Size(198, 45);
            editBtn.TabIndex = 3;
            editBtn.Text = "Edit";
            editBtn.UseVisualStyleBackColor = true;
            editBtn.Click += editBtn_Click;
            // 
            // mainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(editBtn);
            Controls.Add(openBtn);
            Controls.Add(TopicsList);
            Controls.Add(createBtn);
            Margin = new Padding(4, 3, 4, 3);
            Name = "mainPage";
            Text = "Quiz";
            Load += mainPage_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.ListBox TopicsList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Button editBtn;
    }
}