using Serilog;
using System;
using System.Net.Sockets;
using System.Text;
using TcpClientLib.Helpers;

namespace TcpClientLib
{
    public sealed partial class Client : IDisposable
    {
        // Called by producers to send data over the socket.
        public GenericResult<bool> SendData(string data)
        {
            try
            {
                return _sender.SendData(Encoding.ASCII.GetBytes(data));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error sending data to server in Client.{SendData}()", nameof(SendData));
                return new GenericResult<bool>(ex);
            }
        }

        /// <summary>
        ///  Consumers register to receive data.
        /// </summary>
        public event EventHandler<DataReceivedArgs> MainDataReceived;

        public GenericResult<bool> IsConnected { get; set; }

        public Client()
        {
        }

        public void Connect(string hostName, int port)
        {
            IsConnected = GetTcpClientReady(hostName, port);

            if (IsConnected.Succeeded && IsConnected.Value == true)
            {
                _stream = _client.GetStream();
                _receiver = new Receiver(_stream);
                _sender = new Sender(_stream);

                _receiver.DataReceived += OnDataReceived;
            }
        }

        private void OnDataReceived(object sender, DataReceivedArgs e)
        {
            EventHandler<DataReceivedArgs> handler = MainDataReceived;
            if (handler != null) MainDataReceived(this, e);  // re-raise event
        }

        private GenericResult<bool> GetTcpClientReady(string hostName, int port)
        {
            if (_client == null)
            {
                _client = new TcpClient();
            }

            if (!_client.Client.Connected)
            {
                try
                {
                    _client.Connect(hostName, port);

                    return new GenericResult<bool>(true);
                }
                catch (Exception ex)
                {
                    return new GenericResult<bool>(ex);
                }
            }

            return new GenericResult<bool>(true);
        }

        public void Dispose()
        {
            //TODO: Dispose
            throw new NotImplementedException();
        }

        private TcpClient _client;
        private NetworkStream _stream;
        private Receiver _receiver;
        private Sender _sender;
    }
}
