using Serilog;
using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace TcpClientProgram
{
    internal sealed partial class Client : IDisposable
    {
        // Called by producers to send data over the socket.
        public void SendData(byte[] data)
        {
           _sender.SendData(data);
        }

        // Consumers register to receive data.
        public event EventHandler<DataReceivedEventArgs> MainDataReceived;
        public event EventHandler<EventArgs> MainDataReceivedThatWorks;

        public Client(string hostName, int port)
        {
            IsTcpClientReady(hostName, port);

            _stream = _client.GetStream();

            _receiver = new Receiver(_stream);
            _sender = new Sender(_stream);

            _receiver.DataReceived += OnDataReceived;
            _receiver.DataReceivedThatWorks += OnDataReceivedThatWorks;
        }

        private void OnDataReceivedThatWorks(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler = MainDataReceivedThatWorks;
            if (handler != null) MainDataReceivedThatWorks(this, e);  // re-raise event
        }

        //This off course doesn't fire because Client.Receiver never triggers the event 
        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            EventHandler<DataReceivedEventArgs> handler = MainDataReceived;
            if (handler != null) MainDataReceived(this, e);  // re-raise event
        }

        private bool IsTcpClientReady(string hostName, int port)
        {
            //if (!Network.IsConnected())
            //    return false;

            if (_client == null)
            {
                _client = new TcpClient();
            }
            if (!_client.Client.Connected)
            {
                try
                {
                    _client.Connect(hostName, port);
                }
                catch (Exception ex)
                {
                    Log.Debug("Cannot connect to the socket!");
                    return false;
                }
            }
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private TcpClient _client;
        private NetworkStream _stream;
        private Receiver _receiver;
        private Sender _sender;
    }
}
