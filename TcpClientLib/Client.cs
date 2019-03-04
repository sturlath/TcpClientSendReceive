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

        public bool IsConnected { get { return _client?.Connected ?? false; } }

        public Client()
        {
        }

        public async Task<GenericResult<bool>> ConnectAsync(string hostName, int port, int timeoutSec = 1)
        {
            GenericResult<bool> readyResponse = await GetTcpClientReadyAsync(hostName, port, timeoutSec).ConfigureAwait(false);

            if (!readyResponse.HasError && _client.Connected)
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

        private async Task<GenericResult<bool>> GetTcpClientReadyAsync(string hostName, int port, int timeoutSec)
        {
            if (_client == null)
            {
                _client = new TcpClient();
            }

            if (!_client.Connected)
            {
                try
                {
                    IAsyncResult result = _client.BeginConnect(hostName, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(timeoutSec));

                    if (!success)
                    {
                        var response = new GenericResult<bool>
                        {
                            Succeeded = false,
                            ErrorMessage = $"Timed out connecting in {timeoutSec}sec."
                        };

                        return response;
                    }

                    // If this method is run 2x the Client.Connected == true! Why is that? And only in WinForms/Xamarin

                    // End a pending asynchronous connection attempt.
                    // _client.EndConnect(result);

                    return new GenericResult<bool>(success);
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
