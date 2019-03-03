using System.Net.Sockets;
using System.Text;

namespace TcpClientSendReceiveTest.Helpers
{
    /// <summary>
    ///  State object for receiving data from remote device. 
    /// </summary>
    public class StateObject
    {
        // Client socket.  
        public Socket WorkSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
}
