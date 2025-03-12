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
    public partial class ScreenCaptureForm : Form
    {
        private TcpListener _server;
        private Thread _serverThread;
        private NetworkStream _stream;
        private TcpClient _client;
        private CancellationTokenSource _cts;

        public ScreenCaptureForm()
        {
            InitializeComponent();
            StartServer();
        }

        private void StartServer()
        {
            _cts = new CancellationTokenSource();
            _serverThread = new Thread(() => ServerLoop(_cts.Token))
            {
                IsBackground = true
            };
            _serverThread.Start();
        }

        private void ServerLoop(CancellationToken token)
        {
            try
            {
                _server = new TcpListener(IPAddress.Any, 9000);
                _server.Start();
                Invoke(new Action(() => MessageBox.Show("Server started, waiting for connections...")));

                _client = _server.AcceptTcpClient();
                Invoke(new Action(() => MessageBox.Show("Client connected!")));
                _stream = _client.GetStream();

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        Bitmap screenshot = CaptureScreen();
                        byte[] imageData = ImageToByteArray(screenshot);

                        _stream.Write(imageData, 0, imageData.Length);
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        if (!token.IsCancellationRequested)
                        {
                            Invoke(new Action(() => MessageBox.Show($"Error sending screen: {ex.Message}")));
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                {
                    Invoke(new Action(() => MessageBox.Show($"Server error: {ex.Message}")));
                }
            }
            finally
            {
                // Ensure proper cleanup when server stops
                Cleanup();
            }
        }

        private Bitmap CaptureScreen()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            }
            return bitmap;
        }

        private byte[] ImageToByteArray(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        // Cleanup method for stopping the server properly
        private void Cleanup()
        {
            try
            {
                _cts?.Cancel();
                _stream?.Close();
                _stream?.Dispose();
                _client?.Close();
                _server?.Stop();
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => MessageBox.Show($"Cleanup error: {ex.Message}")));
            }
        }

        // Properly close server when form closes
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Cleanup();

            // Ensure server thread exits before closing form
            if (_serverThread != null && _serverThread.IsAlive)
            {
                _serverThread.Join();
            }
        }
    }
}
