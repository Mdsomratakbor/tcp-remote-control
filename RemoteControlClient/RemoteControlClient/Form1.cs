//using System.Net.Sockets;
//using System.Windows.Forms;

//namespace RemoteControlClient
//{
//    public partial class Form1 : Form
//    {
//        private TcpClient _client;
//        private NetworkStream _stream;
//        private Thread _receiveThread;

//        public Form1()
//        {
//            InitializeComponent();
//        }

//        private void BtnConnect_Click(object sender, EventArgs e)
//        {
//            string serverIP = txtServerIP.Text.Trim();
//            if (string.IsNullOrEmpty(serverIP))
//            {
//                MessageBox.Show("Enter Server IP Address!");
//                return;
//            }

//            _client = new TcpClient();
//            try
//            {
//                _client.Connect(serverIP, 9000); // Connect to server
//                _stream = _client.GetStream();
//                MessageBox.Show("Connected to Server!");

//                _receiveThread = new Thread(ReceiveScreen);
//                _receiveThread.IsBackground = true;
//                _receiveThread.Start();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Connection failed: {ex.Message}");
//            }
//        }

//        private void ReceiveScreen()
//        {
//            try
//            {
//                while (true)
//                {
//                    byte[] buffer = new byte[1024 * 500]; // Adjust size as needed
//                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);

//                    if (bytesRead > 0)
//                    {
//                        using (MemoryStream ms = new MemoryStream(buffer, 0, bytesRead))
//                        {
//                            Image img = Image.FromStream(ms);
//                            pictureBox1.Invoke(new Action(() => pictureBox1.Image = img));
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error receiving screen: {ex.Message}");
//            }
//        }
//    }
//}


using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace RemoteControlClient
{
    public partial class Form1 : Form
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _receiveThread;
        private bool _isConnected = false;
        private CancellationTokenSource _cts; 
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            string serverIP = txtServerIP.Text.Trim();
            if (string.IsNullOrEmpty(serverIP))
            {
                MessageBox.Show("Please enter the server IP address.");
                return;
            }

            try
            {
                _client = new TcpClient(serverIP, 9000);
                _stream = _client.GetStream();
                _isConnected = true;
                MessageBox.Show("Connected to the server!");

                _receiveThread = new Thread(ReceiveScreen);
                _receiveThread.IsBackground = true;
                _receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}");
            }
        }

        private void ReceiveScreen()
        {
            try
            {
                while (_isConnected)
                {
                    byte[] sizeBytes = new byte[4];
                    int bytesRead = _stream.Read(sizeBytes, 0, 4);
                    if (bytesRead == 0) break;

                    int imageSize = BitConverter.ToInt32(sizeBytes, 0);
                    byte[] buffer = new byte[imageSize];
                    int totalBytesRead = 0;

                    while (totalBytesRead < imageSize)
                    {
                        int read = _stream.Read(buffer, totalBytesRead, imageSize - totalBytesRead);
                        if (read == 0) break;
                        totalBytesRead += read;
                    }

                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        Image image = Image.FromStream(ms);
                        pictureBox1.Invoke((MethodInvoker)delegate
                        {
                            pictureBox1.Image = image;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error receiving screen: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            _isConnected = false;

            _cts?.Cancel(); 
            _client?.Close();

            if (_receiveThread != null && _receiveThread.IsAlive)
            {
                _receiveThread.Join(); 
            }
        }
    }
}

