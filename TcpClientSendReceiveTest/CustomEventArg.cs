using System;
using System.Diagnostics;

namespace TcpClientSendReceiveTest
{
    public class CustomEventArg : EventArgs
    {
        private readonly DataReceivedEventArgs _inner;

        public CustomEventArg(DataReceivedEventArgs inner, object extraProperty)
        {
            _inner = inner;
            ExtraProperty = extraProperty;
        }

        public object ExtraProperty { get; private set; }
        public DataReceivedEventArgs DataArgs
        {
            get
            {
                return _inner;
            }
        }
    }
}
