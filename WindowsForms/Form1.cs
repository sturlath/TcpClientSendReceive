using Serilog;
using System;
using System.Windows.Forms;
using TcpClientLib;
using TcpClientLib.Helpers;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Client client;

        public Form1()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            InitializeComponent();
        }

        private void btnConnectToService_Click(object sender, EventArgs e)
        {
            client = new Client(txtIpAddress.Text, Convert.ToInt32(txtPort.Text));
            client.MainDataReceived += OnClient_MainDataReceived;

            if (!client.IsConnected.Value)
            {
                txbResponseFromServer.AppendText("\r\n" + client.IsConnected.ErrorMessage);
                Log.Debug("Not connected... {ErrorMessage}", client.IsConnected.ErrorMessage);
                //Do your re-connect etc..
            }
        }

        private void OnClient_MainDataReceived(object sender, DataReceivedArgs e)
        {
            txbResponseFromServer.AppendText(e.Data);
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            GenericResult<bool> test = client.SendData(txtDataToSend.Text);
        }
    }
}
