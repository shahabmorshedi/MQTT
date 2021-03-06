﻿using MqttCore.Core;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQTTCore.Device
{
    public class Publisher
    {
        private MqttCore.Core.Tcp _tcp;

        private MqttCore.Core.Tcp TCP
        {
            get
            {
                if (_tcp == null)
                    BuildConnection();
                return _tcp;
            }
            set
            {
                _tcp = value;
            }
        }

        public string Name
        {
            get;
            set;
        }   

        public string IP
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public Publisher(string name, string ip, int port)
        {
            Name = name;
            IP = ip;
            Port = port;
        }

        public void BuildConnection()
        {
            _tcp = new MqttCore.Core.Tcp(IP, Port);
            _tcp.BuildConnectionToRecieve();
        }

        public void Start()
        {
            TCP.StartListening();
        }

        public async Task<string> RecieveAsync(CancellationToken cancellationToken)
        {
            return await TCP.RecieveAsync(cancellationToken);
        }

        public static bool operator ==(Publisher publisher1, Publisher publisher2)
        {
            return (publisher1.IP == publisher2.IP);
        }

        public static bool operator !=(Publisher publisher1, Publisher publisher2)
        {
            return !(publisher1.IP == publisher2.IP);
        }
    }
}
