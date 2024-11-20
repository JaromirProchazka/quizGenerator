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
            SuspendLayout();
            // 
            // notesChooseOptions
            // 
            notesChooseOptions.FormattingEnabled = true;
            notesChooseOptions.ItemHeight = 15;
            notesChooseOptions.Location = new Point(28, 17);
            notesChooseOptions.Name = "notesChooseOptions";
            notesChooseOptions.Size = new Size(287, 334);
            notesChooseOptions.TabIndex = 0;
            notesChooseOptions.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            // 
            // ContinueBtn
            // 
            ContinueBtn.Location = new Point(28, 390);
            ContinueBtn.Name = "ContinueBtn";
            ContinueBtn.Size = new Size(105, 48);
            ContinueBtn.TabIndex = 1;
            ContinueBtn.Text = "button1";
            ContinueBtn.UseVisualStyleBackColor = true;
            ContinueBtn.Click += ContinueBtn_Click;
            // 
            // NotionLink
            // 
            NotionLink.Location = new Point(321, 61);
            NotionLink.Name = "NotionLink";
            NotionLink.Size = new Size(226, 23);
            NotionLink.TabIndex = 2;
            // 
            // ChooseLocalFile
            // 
            ChooseLocalFile.FileName = "openFileDialog1";
            // 
            // label1
            // 
            NotionLinkLabel.AutoSize = true;
            NotionLinkLabel.Location = new Point(321, 43);
            NotionLinkLabel.Name = "label1";
            NotionLinkLabel.Size = new Size(288, 15);
            NotionLinkLabel.TabIndex = 3;
            // 
            // ChooseSourceStep
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(NotionLinkLabel);
            Controls.Add(NotionLink);
            Controls.Add(ContinueBtn);
            Controls.Add(notesChooseOptions);
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
    }
}