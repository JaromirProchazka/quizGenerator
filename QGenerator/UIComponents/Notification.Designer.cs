namespace QGenerator.UIComponents
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
            // label1
            // 
            MessageLabel.AutoSize = true;
            MessageLabel.Cursor = Cursors.IBeam;
            MessageLabel.Location = new Point(158, 104);
            MessageLabel.Name = "label1";
            MessageLabel.Size = new Size(53, 15);
            MessageLabel.TabIndex = 0;
            MessageLabel.Text = "Message";
            MessageLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Notification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(368, 229);
            Controls.Add(MessageLabel);
            Name = "Notification";
            Text = "Form1";
            Load += this.Notification_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label MessageLabel;
    }
}