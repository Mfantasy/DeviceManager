﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager
{
    class DataSubscribe
    {
        static void ProcessBytes(object o)
        {
            byte[] bts = (byte[])o;
            int length = bts.Length;
            string jstr0 = Encoding.UTF8.GetString(bts, 0, length);
                              
            if (bts.Length > 4 && bts[0] == 0 && bts[1] == 0)
            {
                int jbtlength = bts[2] * 256 + bts[3];
                if (jbtlength != bts.Length-4)
                {
                    return;
                }
                string jstr = Encoding.UTF8.GetString(bts, 4, jbtlength);             
                try
                {
                    JObject.Parse(jstr);
                }
                catch (Exception ex)
                {
                    Utils.WriteEX(ex);
                    return;               
                }
                JObject jobj = JObject.Parse(jstr);
                DataParser.ParseJObj(jobj);
                {
                    if (jbtlength + 4 < length)
                    {
                        byte[] btSub = bts.Skip(jbtlength + 4).ToArray();
                        ProcessBytes(btSub);
                    }
                }
            }
        }

        public static void StartSubscribe(out bool success)
        {
            TcpClient tcp = new TcpClient();
            NetworkStream streamToServer = null;
            int byteLength = 8 * 1024;
            string ip = ConfigurationManager.AppSettings["ip"];
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);                        
            tcp.Connect(ip, port);
            success = true;
            streamToServer = tcp.GetStream();                                
            while (tcp.Connected)
            {
                byte[] bufferR = new byte[byteLength];
                int bfLength = streamToServer.Read(bufferR, 0, bufferR.Length);
                if (bfLength > 0)
                {
                    byte[] bts = bufferR.Take(bfLength).ToArray();
                    ThreadPool.QueueUserWorkItem(ProcessBytes, bts);
                }
            }                     
        }
    }
}
