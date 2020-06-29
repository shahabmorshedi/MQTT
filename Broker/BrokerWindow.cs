﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQTTCore.Broker;

namespace Broker
{
    public partial class BrokerWindow : Form
    {
        const string brokerName = "broker";
        MQTTCore.Broker.Broker broker;
        CancellationToken cancellationToken = new CancellationToken();

        public BrokerWindow()
        {
            InitializeComponent();

            broker = new MQTTCore.Broker.Broker(
                                                brokerName,
                                                ConfigurationManager.AppSettings["ip"],
                                                int.Parse(ConfigurationManager.AppSettings["port"]));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Task task = Task.Run((Action)GatherData, cancellationToken);
        }

        private void BrokerWindow_Load(object sender, EventArgs e)
        {
        }

        private async void GatherData()
        {
            while (true)
            {
                var messages = await broker.ListeningAsync(cancellationToken);
                txtMessage.Text = messages.First().Value;
            }
        }
    }
}