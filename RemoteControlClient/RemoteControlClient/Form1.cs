using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace RemoteControlClient;
public partial class Form1 : Form
{
    private TcpClient _client;
    private NetworkStream _stream;
    private Thread _receiveThread;
    private Thread _chatThread;
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

           // _chatThread = new Thread(ReceiveMessages);
           // _chatThread.IsBackground = true;
           // _chatThread.Start();
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

                if (imageSize < 0 || imageSize > Int32.MaxValue)
                {
                    throw new InvalidOperationException("Received invalid image size.");
                }

                byte[] buffer = new byte[imageSize];
                int totalBytesRead = 0;

                while (totalBytesRead < imageSize)
                {
                    int read = _stream.Read(buffer, totalBytesRead, imageSize - totalBytesRead);
                    if (read == 0) break;
                    totalBytesRead += read;
                }

                if (totalBytesRead != imageSize)
                {
                    throw new InvalidOperationException("Failed to receive the complete image data.");
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


    private void ReceiveMessages()
    {
        try
        {
            while (_isConnected)
            {
                byte[] sizeBytes = new byte[4];
                int bytesRead = _stream.Read(sizeBytes, 0, 4);
                if (bytesRead == 0) break;

                int messageSize = BitConverter.ToInt32(sizeBytes, 0);
                byte[] buffer = new byte[messageSize];
                int totalBytesRead = 0;

                while (totalBytesRead < messageSize)
                {
                    int read = _stream.Read(buffer, totalBytesRead, messageSize - totalBytesRead);
                    if (read == 0) break;
                    totalBytesRead += read;
                }

                string message = System.Text.Encoding.UTF8.GetString(buffer);

                Invoke((MethodInvoker)delegate
                {
                    txtChat.AppendText("Server: " + message);
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error receiving message: {ex.Message}");
        }
    }


    private void BtnSend_Click(object sender, EventArgs e)
    {
        if (_stream != null)
        {
            string message = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                StreamWriter writer = new StreamWriter(_stream, Encoding.UTF8) { AutoFlush = true };
                writer.WriteLine(message);

                txtChat.AppendText("Me: " + message + Environment.NewLine);
                txtMessage.Clear();
            }
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
        if (_chatThread != null && _chatThread.IsAlive)
        {
            _chatThread.Join();
        }
    }
}
