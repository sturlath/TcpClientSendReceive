using System;

namespace TcpClientSendReceiveTest.Helpers
{
    public class DataReceivedArgs : EventArgs
    {
        public DataReceivedArgs(string data)
        {
            Data = data;
        }

        public string Data { get; set; }

    }
}
