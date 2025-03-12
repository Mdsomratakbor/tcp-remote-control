namespace RemoteControlClient
{
    partial class Form1
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtServerIP;

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            btnConnect = new Button();
            txtServerIP = new TextBox();
            txtChat = new TextBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(586, 26);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 600);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(220, 10);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.Click += BtnConnect_Click;
            // 
            // txtServerIP
            // 
            txtServerIP.Location = new Point(10, 10);
            txtServerIP.Name = "txtServerIP";
            txtServerIP.Size = new Size(200, 23);
            txtServerIP.TabIndex = 1;
            // 
            // txtChat
            // 
            txtChat.Location = new Point(10, 89);
            txtChat.Multiline = true;
            txtChat.Name = "txtChat";
            txtChat.ReadOnly = true;
            txtChat.Size = new Size(400, 334);
            txtChat.TabIndex = 3;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(10, 460);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(300, 23);
            txtMessage.TabIndex = 4;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(320, 460);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 5;
            btnSend.Text = "Send";
            btnSend.Click += BtnSend_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1460, 713);
            Controls.Add(pictureBox1);
            Controls.Add(txtServerIP);
            Controls.Add(btnConnect);
            Controls.Add(txtChat);
            Controls.Add(txtMessage);
            Controls.Add(btnSend);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
