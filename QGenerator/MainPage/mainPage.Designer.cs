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
            this.createBtn = new System.Windows.Forms.Button();
            this.TopicsList = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.createBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createBtn.Location = new System.Drawing.Point(12, 399);
            this.createBtn.Name = "button1";
            this.createBtn.Size = new System.Drawing.Size(170, 39);
            this.createBtn.TabIndex = 0;
            this.createBtn.Text = "Create New Topic";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.newTopicBtn_Click);
            // 
            // TopicsList
            // 
            this.TopicsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TopicsList.FormattingEnabled = true;
            this.TopicsList.Location = new System.Drawing.Point(12, 12);
            this.TopicsList.Name = "TopicsList";
            this.TopicsList.Size = new System.Drawing.Size(776, 368);
            this.TopicsList.TabIndex = 1;
            this.TopicsList.SelectedIndexChanged += new System.EventHandler(this.Topics_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.openBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openBtn.Location = new System.Drawing.Point(442, 399);
            this.openBtn.Name = "button2";
            this.openBtn.Size = new System.Drawing.Size(170, 39);
            this.openBtn.TabIndex = 2;
            this.openBtn.Text = "Open";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // button3
            // 
            this.editBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editBtn.Location = new System.Drawing.Point(618, 399);
            this.editBtn.Name = "button3";
            this.editBtn.Size = new System.Drawing.Size(170, 39);
            this.editBtn.TabIndex = 3;
            this.editBtn.Text = "Edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // mainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.TopicsList);
            this.Controls.Add(this.createBtn);
            this.Name = "mainPage";
            this.Text = "Quiz";
            this.Load += new System.EventHandler(this.mainPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.ListBox TopicsList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Button editBtn;
    }
}