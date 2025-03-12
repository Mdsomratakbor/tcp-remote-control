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
        private System.Windows.Forms.TextBox txtMessageBox;
        private System.Windows.Forms.Button btnSendMessage;

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtMessageBox = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
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
            // txtMessageBox
            // 
            this.txtMessageBox.Location = new System.Drawing.Point(10, 50);
            this.txtMessageBox.Size = new System.Drawing.Size(300, 20);
            this.Controls.Add(this.txtMessageBox);

            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(320, 50);
            this.btnSendMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.Click += new System.EventHandler(this.BtnSendMessage_Click);
            this.Controls.Add(this.btnSendMessage);

            // 
            // ScreenShareForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Text = "Remote Control Server";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}