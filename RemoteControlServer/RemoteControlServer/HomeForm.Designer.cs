using System;
using System.Windows.Forms;

namespace RemoteControlServer
{
    public partial class HomeForm : Form
    {


        private void InitializeComponent()
        {
            this.btnStartShare = new System.Windows.Forms.Button();
            this.btnCaptureScreenshot = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Start Screen Sharing Button
            this.btnStartShare.Location = new System.Drawing.Point(50, 50);
            this.btnStartShare.Size = new System.Drawing.Size(200, 40);
            this.btnStartShare.Text = "Start Screen Sharing";
            this.btnStartShare.Click += new System.EventHandler(this.BtnStartShare_Click);

            // Capture Screenshot Button
            this.btnCaptureScreenshot.Location = new System.Drawing.Point(50, 120);
            this.btnCaptureScreenshot.Size = new System.Drawing.Size(200, 40);
            this.btnCaptureScreenshot.Text = "Capture Screenshot";
            this.btnCaptureScreenshot.Click += new System.EventHandler(this.BtnCaptureScreenshot_Click);

            // Exit Button
            this.btnExit.Location = new System.Drawing.Point(50, 190);
            this.btnExit.Size = new System.Drawing.Size(200, 40);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);

            // HomeForm
            this.ClientSize = new System.Drawing.Size(300, 260);
            this.Controls.Add(this.btnStartShare);
            this.Controls.Add(this.btnCaptureScreenshot);
            this.Controls.Add(this.btnExit);
            this.Text = "Remote Control - Home";
            this.ResumeLayout(false);
        }

        private Button btnStartShare;
        private Button btnCaptureScreenshot;
        private Button btnExit;

        private void BtnStartShare_Click(object sender, EventArgs e)
        {
            // Open ScreenShareForm
            ScreenShareForm screenShareForm = new ScreenShareForm();
            screenShareForm.Show();
        }

        private void BtnCaptureScreenshot_Click(object sender, EventArgs e)
        {
            // Open CaptureScreenshotForm
            ScreenCaptureForm captureScreenshotForm = new ScreenCaptureForm();
            captureScreenshotForm.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
