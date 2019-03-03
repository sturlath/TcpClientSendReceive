using System;

namespace TcpClientLib.Helpers
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
