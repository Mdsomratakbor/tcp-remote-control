namespace RemoteControlServer
{
    partial class ScreenShareForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private System.Windows.Forms.Label lblStatus;
        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(10, 10);
            this.lblStatus.Size = new System.Drawing.Size(300, 20);
            this.lblStatus.Text = "Server is not running...";
            this.Controls.Add(this.lblStatus);

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Text = "Remote Control Server";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
