using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TcpClientMobileApp.Models;
using TcpClientMobileApp.Views;
using TcpClientMobileApp.ViewModels;
using TcpClientLib;
using TcpClientLib.Helpers;

namespace TcpClientMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public Client Client;
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

            Client = new Client();
            Client.Connect("127.0.0.1", 8005);

            Client.MainDataReceived += OnClient_MainDataReceived;
        }

        void Connect_Clicked(object sender, EventArgs e)
        {
            var success = Client.Connect("127.0.0.1", 8005);

            if(success.HasError)
            {
                DisplayAlert("Error", success.ErrorMessage, ":-(");
            }
        }

        void SendData_Clicked(object sender, EventArgs e)
        {
            if (Client.IsConnected.Value)
            {
                GenericResult<bool> test = Client.SendData("TEST DATA");
            }
        }

        private void OnClient_MainDataReceived(object sender, DataReceivedArgs e)
        {
            // Success! It got called!!
            throw new NotImplementedException();
        }

    }
}