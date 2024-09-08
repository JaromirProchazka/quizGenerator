namespace QGenerator.UIComponents
{
    partial class ChooseQuizBeginningFrom
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
            SuspendLayout();
            // 
            // notesChooseOptions
            // 
            notesChooseOptions.FormattingEnabled = true;
            notesChooseOptions.ItemHeight = 15;
            notesChooseOptions.Location = new Point(23, 12);
            notesChooseOptions.Name = "notesChooseOptions";
            notesChooseOptions.Size = new Size(287, 334);
            notesChooseOptions.TabIndex = 1;
            // 
            // ContinueBtn
            // 
            ContinueBtn.Location = new Point(23, 390);
            ContinueBtn.Name = "ContinueBtn";
            ContinueBtn.Size = new Size(105, 48);
            ContinueBtn.TabIndex = 2;
            ContinueBtn.Text = "button1";
            ContinueBtn.UseVisualStyleBackColor = true;
            ContinueBtn.Click += ContinueBtn_Click;
            // 
            // ChooseQuizBeginningFrom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ContinueBtn);
            Controls.Add(notesChooseOptions);
            Name = "ChooseQuizBeginningFrom";
            Text = "ChooseQuizBeginningFrom";
            Load += ChooseQuizBeginningFrom_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox notesChooseOptions;
        private Button ContinueBtn;
    }
}