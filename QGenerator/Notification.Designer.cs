namespace QuizGeneratorPresentation
{
    partial class Notification
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
            MessageLabel = new Label();
            SuspendLayout();
            // 
            // MessageLabel
            // 
            MessageLabel.AllowDrop = true;
            MessageLabel.AutoSize = true;
            MessageLabel.Cursor = Cursors.IBeam;
            MessageLabel.Location = new Point(12, 9);
            MessageLabel.MaximumSize = new Size(300, 200);
            MessageLabel.Name = "MessageLabel";
            MessageLabel.Size = new Size(67, 20);
            MessageLabel.TabIndex = 0;
            MessageLabel.Text = "Message";
            MessageLabel.Click += MessageLabel_Click;
            // 
            // Notification
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 305);
            Controls.Add(MessageLabel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Notification";
            Text = "Form1";
            Load += Notification_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label MessageLabel;
    }
}