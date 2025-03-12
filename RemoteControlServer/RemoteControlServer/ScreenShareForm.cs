using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace RemoteControlServer
{
    public partial class ScreenShareForm : Form
    {
        private TcpListener _server;
        private Thread _serverThread;
        private bool _isRunning = false;
        private CancellationTokenSource _cts;

        public ScreenShareForm()
        {
            InitializeComponent();
            StartServer();
        }

        private void StartServer()
        {
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            _serverThread = new Thread(() =>
            {
                try
                {
                    _server = new TcpListener(IPAddress.Any, 9000);
                    _server.Start();
                    _isRunning = true;
                    UpdateStatus("Server started... Waiting for connections...");

                    while (!_cts.Token.IsCancellationRequested)
                    {
                        if (_server.Pending())
                        {
                            TcpClient client = _server.AcceptTcpClient();
                            UpdateStatus("Client connected!");
                            Thread clientThread = new Thread(() => HandleClient(client, token));
                            clientThread.Start();
                        }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Server error: {ex.Message}");
                }
            });

            _serverThread.IsBackground = true;
            _serverThread.Start();
        }

        private void HandleClient(TcpClient client, CancellationToken token)
        {
            NetworkStream stream = client.GetStream();
            UpdateStatus("Client is receiving the screen...");
            lock (connectedClients)
            {
                connectedClients.Add(client);
            }
            try
            {
                while (!token.IsCancellationRequested)
                {
                    using (Bitmap screenshot = CaptureScreen())
                    {
                        MemoryStream ms = new MemoryStream();
                        screenshot.Save(ms, ImageFormat.Jpeg);
                        byte[] buffer = ms.ToArray();

                        stream.Write(BitConverter.GetBytes(buffer.Length), 0, 4);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Client disconnected: {ex.Message}");
            }
            finally
            {
                client.Close();
                UpdateStatus("Waiting for new connections...");
            }
        }

        private Bitmap CaptureScreen()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
            }
            return screenshot;
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => lblStatus.Text = message));
            }
            else
            {
                lblStatus.Text = message;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }
        private void BtnSendMessage_Click(object sender, EventArgs e)
        {
            string message = txtMessageBox.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
              //  SendMessageToClients(message);
                txtMessageBox.Clear();
            }
        }
        private void SendMessageToClients(string message)
        {
            try
            {
                byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);

                foreach (TcpClient client in connectedClients) // Assuming a list of clients
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(BitConverter.GetBytes(messageBytes.Length), 0, 4);
                    stream.Write(messageBytes, 0, messageBytes.Length);
                }

                UpdateStatus("Message sent: " + message);
            }
            catch (Exception ex)
            {
                UpdateStatus("Error sending message: " + ex.Message);
            }
        }
        private List<TcpClient> connectedClients = new List<TcpClient>();


        private void StopServer()
        {
            if (_isRunning)
            {
                _cts.Cancel();
                _server?.Stop();
                _isRunning = false;
                UpdateStatus("Server stopped.");
            }
        }
    }
}
