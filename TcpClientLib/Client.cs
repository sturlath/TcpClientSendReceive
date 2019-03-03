using Serilog;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpClientLib.Helpers;

namespace TcpClientLib
{
    public sealed partial class Client : IDisposable
    {
        // Called by producers to send data over the socket.
        public async Task<GenericResult<bool>> SendData(string data)
        {
            try
            {
                return await _sender.SendData(Encoding.ASCII.GetBytes(data)).ConfigureAwait(false);
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

        public bool IsConnected { get { return _client.Connected; } }

        public Client()
        {
        }

        public async Task<GenericResult<bool>> ConnectAsync(string hostName, int port)
        {
            var readyResponse = await GetTcpClientReadyAsync(hostName, port).ConfigureAwait(false);

            if (_client.Connected)
            {
                _stream = _client.GetStream();
                _receiver = new Receiver(_stream);
                _sender = new Sender(_stream);

                _receiver.DataReceived += OnDataReceived;
            }

            return readyResponse;
        }

        private void OnDataReceived(object sender, DataReceivedArgs e)
        {
            EventHandler<DataReceivedArgs> handler = MainDataReceived;
            if (handler != null) MainDataReceived(this, e);  // re-raise event
        }

        private async Task<GenericResult<bool>> GetTcpClientReadyAsync(string hostName, int port)
        {
            if (_client == null)
            {
                _client = new TcpClient();
            }

            if (!_client.Connected)
            {
                try
                {
                    //TODO: Shorten timeout! Takes to long to get an error...
                    await _client.ConnectAsync(hostName, port).ConfigureAwait(false);

                    return new GenericResult<bool>(_client.Connected);
                }
                catch (Exception ex)
                {
                    return new GenericResult<bool>(ex);
                }
            }

            return new GenericResult<bool>(_client.Connected);
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
