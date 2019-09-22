using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ToolBox
{
    public class TTcpServer
    {
        string IP { get; } //ip
        int Port { get; } //端口 
        IPAddress localAddress;      //IP地址 
        TcpListener tcpListener;  //监听套接字 
        IPAddress[] listenIp;
        List<TcpClient> tcpClients = new List<TcpClient>();
        Thread serverThread; //用来接收客户端的线程
        public delegate void MsgCallBack(string msg);
        MsgCallBack _msgCallBack;
        private object lock1 = new object();

        public TTcpServer(string ip, int port, MsgCallBack msgCallback)
        {
            IP = ip;
            Port = port;
            _msgCallBack = msgCallback;
        }


        /// <summary>
        /// 打开服务器
        /// </summary>
        public void Open()
        {
            string msg = $"服务器搭建成功 ip:{IP} port:{Port}";

            if (!IsRuning())
            {
                try
                {
                    serverThread = new Thread(SocketServer); //用一个线程单独处理这个连接  
                    serverThread.Start();
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            else
            {
                msg = "服务器正在运行";
            }
            _msgCallBack(msg);
        }

        private void SocketServer()
        {
            listenIp = Dns.GetHostAddresses(IP);
            localAddress = listenIp[0];
            tcpListener = new TcpListener(localAddress, Port);
            tcpListener.Start(); //开始监听  
            while (true)
            {
                try
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();//每接受一个客户端则生成一个TcpClient  
                    tcpClients.Add(tcpClient);
                    string msg = "新客户端连接 tcpClients.Count: " + tcpClients.Count;
                    _msgCallBack(msg);
                }
                catch (Exception ex)
                {
                    string msg = "服务器运行错误： " + ex.Message;
                    _msgCallBack(msg);
                }
            }
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        public void Close()
        {
            if (tcpListener != null) tcpListener.Stop();
            serverThread.Abort();
            foreach (TcpClient tcpClient in tcpClients)
            {
                tcpClient.Close();
            }
            _msgCallBack("服务器已关闭");
        }

        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void Send(string msg)
        {
            lock (lock1) //防止弹幕姬多线程调用错误
            {
                for (int i = tcpClients.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        NetworkStream networkStream = tcpClients[i].GetStream();
                        BinaryWriter bw = new BinaryWriter(networkStream);
                        bw.Write(msg);
                    }
                    catch (Exception err)
                    {
                        tcpClients[i].Close();
                        tcpClients.RemoveAt(i);
                        string _msg = $"发送失败 客户端销毁 tcpClients.Count:{tcpClients.Count} errMsg:{err.Message}";
                        _msgCallBack(_msg);
                        continue;
                    }
                }
            }
        }

        public bool IsRuning()
        {
            if (serverThread == null) return false;
            return serverThread.IsAlive;
        }
    }
}
