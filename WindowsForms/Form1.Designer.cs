namespace WindowsForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnectToService = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataToSend = new System.Windows.Forms.TextBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txbResponseFromServer = new System.Windows.Forms.TextBox();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCloseConnection = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectToService
            // 
            this.btnConnectToService.Location = new System.Drawing.Point(40, 72);
            this.btnConnectToService.Name = "btnConnectToService";
            this.btnConnectToService.Size = new System.Drawing.Size(132, 23);
            this.btnConnectToService.TabIndex = 0;
            this.btnConnectToService.Text = "Connect to service";
            this.btnConnectToService.Click += new System.EventHandler(this.btnConnectToService_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data to send to service";
            // 
            // txtDataToSend
            // 
            this.txtDataToSend.Location = new System.Drawing.Point(6, 30);
            this.txtDataToSend.Name = "txtDataToSend";
            this.txtDataToSend.Size = new System.Drawing.Size(132, 20);
            this.txtDataToSend.TabIndex = 2;
            this.txtDataToSend.Text = "test data";
            // 
            // btnSendData
            // 
            this.btnSendData.Location = new System.Drawing.Point(3, 56);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(132, 23);
            this.btnSendData.TabIndex = 3;
            this.btnSendData.Text = "Send data";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSendData);
            this.panel1.Controls.Add(this.txtDataToSend);
            this.panel1.Location = new System.Drawing.Point(12, 156);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 93);
            this.panel1.TabIndex = 4;
            // 
            // txbResponseFromServer
            // 
            this.txbResponseFromServer.Location = new System.Drawing.Point(230, 36);
            this.txbResponseFromServer.Multiline = true;
            this.txbResponseFromServer.Name = "txbResponseFromServer";
            this.txbResponseFromServer.Size = new System.Drawing.Size(565, 509);
            this.txbResponseFromServer.TabIndex = 6;
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Location = new System.Drawing.Point(3, 17);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(58, 13);
            this.lblIpAddress.TabIndex = 8;
            this.lblIpAddress.Text = "IP Address";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(72, 14);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(100, 20);
            this.txtIpAddress.TabIndex = 9;
            this.txtIpAddress.Text = "localhost";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(72, 46);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 11;
            this.txtPort.Text = "14000";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCloseConnection);
            this.panel2.Controls.Add(this.txtIpAddress);
            this.panel2.Controls.Add(this.txtPort);
            this.panel2.Controls.Add(this.lblIpAddress);
            this.panel2.Controls.Add(this.btnConnectToService);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(12, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(192, 131);
            this.panel2.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Resonse from the server";
            // 
            // btnCloseConnection
            // 
            this.btnCloseConnection.Location = new System.Drawing.Point(40, 102);
            this.btnCloseConnection.Name = "btnCloseConnection";
            this.btnCloseConnection.Size = new System.Drawing.Size(132, 23);
            this.btnCloseConnection.TabIndex = 12;
            this.btnCloseConnection.Text = "Close Connection";
            this.btnCloseConnection.UseVisualStyleBackColor = true;
            this.btnCloseConnection.Click += new System.EventHandler(this.btnCloseConnection_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 557);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbResponseFromServer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "TcpClient Tester";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnectToService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDataToSend;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txbResponseFromServer;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCloseConnection;
    }
}

