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
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtServerIP;

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServerIP = new System.Windows.Forms.TextBox();

            // PictureBox
            this.pictureBox1.Location = new System.Drawing.Point(10, 50);
            this.pictureBox1.Size = new System.Drawing.Size(800, 600);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(this.pictureBox1);

            // TextBox for Server IP
            this.txtServerIP.Location = new System.Drawing.Point(10, 10);
            this.txtServerIP.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(this.txtServerIP);

            // Button
            this.btnConnect.Text = "Connect";
            this.btnConnect.Location = new System.Drawing.Point(220, 10);
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            this.Controls.Add(this.btnConnect);
        }

        #endregion
    }
}
