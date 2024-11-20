namespace QuizGeneratorPresentation.TopicCreation
{
    partial class ChooseSourceStep
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
            notesChooseOptions = new ListBox();
            ContinueBtn = new Button();
            NotionLink = new TextBox();
            ChooseLocalFile = new OpenFileDialog();
            NotionLinkLabel = new Label();
            DescriptionLabel = new Label();
            SuspendLayout();
            // 
            // notesChooseOptions
            // 
            notesChooseOptions.FormattingEnabled = true;
            notesChooseOptions.Location = new Point(32, 43);
            notesChooseOptions.Margin = new Padding(3, 4, 3, 4);
            notesChooseOptions.Name = "notesChooseOptions";
            notesChooseOptions.Size = new Size(327, 424);
            notesChooseOptions.TabIndex = 0;
            notesChooseOptions.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            // 
            // ContinueBtn
            // 
            ContinueBtn.Location = new Point(32, 520);
            ContinueBtn.Margin = new Padding(3, 4, 3, 4);
            ContinueBtn.Name = "ContinueBtn";
            ContinueBtn.Size = new Size(120, 64);
            ContinueBtn.TabIndex = 1;
            ContinueBtn.Text = "button1";
            ContinueBtn.UseVisualStyleBackColor = true;
            ContinueBtn.Click += ContinueBtn_Click;
            // 
            // NotionLink
            // 
            NotionLink.Location = new Point(367, 81);
            NotionLink.Margin = new Padding(3, 4, 3, 4);
            NotionLink.Name = "NotionLink";
            NotionLink.Size = new Size(258, 27);
            NotionLink.TabIndex = 2;
            // 
            // ChooseLocalFile
            // 
            ChooseLocalFile.FileName = "openFileDialog1";
            // 
            // NotionLinkLabel
            // 
            NotionLinkLabel.AutoSize = true;
            NotionLinkLabel.Location = new Point(367, 57);
            NotionLinkLabel.Name = "NotionLinkLabel";
            NotionLinkLabel.Size = new Size(0, 20);
            NotionLinkLabel.TabIndex = 3;
            // 
            // label1
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.Location = new Point(32, 9);
            DescriptionLabel.Name = "label1";
            DescriptionLabel.Size = new Size(50, 20);
            DescriptionLabel.TabIndex = 4;
            DescriptionLabel.Text = "label1";
            // 
            // ChooseSourceStep
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(DescriptionLabel);
            Controls.Add(NotionLinkLabel);
            Controls.Add(NotionLink);
            Controls.Add(ContinueBtn);
            Controls.Add(notesChooseOptions);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ChooseSourceStep";
            Text = "Form1";
            Load += ChooseSourceStep_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox notesChooseOptions;
        private Button ContinueBtn;
        private TextBox NotionLink;
        private OpenFileDialog ChooseLocalFile;
        private Label NotionLinkLabel;
        private Label DescriptionLabel;
    }
}