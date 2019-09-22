using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToolBox
{
    public class TTcpClient
    {
        string ServerIp { get; }
        int ServerPort { get; }
        TcpClient tcpClient;
        BinaryReader br;
        public delegate void ReceiveMsgCallBack(string msg);
        ReceiveMsgCallBack _receiveMsgCallBack;
        Thread thread;

        /// <summary>
        /// TcpClient
        /// </summary>
        /// <param name="serverIp">服务器ip'</param>
        /// <param name="serverPort">服务器端口</param>
        /// <param name="receiveMsgCallBack">接受消息回调函数</param>
        public TTcpClient(string serverIp, int serverPort, ReceiveMsgCallBack receiveMsgCallBack)
        {
            ServerIp = serverIp;
            ServerPort = serverPort;
            _receiveMsgCallBack = receiveMsgCallBack;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public void Connect()
        {
            if (IsRuning()) return;
            tcpClient = new TcpClient();  //创建一个TcpClient对象，自动分配主机IP地址和端口号  
            tcpClient.Connect(ServerIp, ServerPort);   //连接服务器，其IP和端口号为127.0.0.1和51888  
            NetworkStream networkStream = tcpClient.GetStream();
            br = new BinaryReader(networkStream);

            thread = new Thread(ReceiveMsg); //用一个线程单独处理这个连接  
            thread.Start();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (tcpClient != null) tcpClient.Close();
            if (thread != null) thread.Abort();
        }

        /// <summary>
        /// 是否在运行
        /// </summary>
        /// <returns></returns>
        public bool IsRuning()
        {
            if (tcpClient == null || thread == null) return false;
            if (tcpClient.Connected && thread.IsAlive) return true;
            else return false;
        }

        private void ReceiveMsg()
        {
            while (true)
            {
                try
                {
                    string brString = br.ReadString();     //接收服务器发送的数据  
                    if (brString != null)
                    {
                        _receiveMsgCallBack(brString);
                    }
                }
                catch (ThreadAbortException ex)
                {
                    //不进行操作
                    break;
                }
                catch (Exception ex)
                {
                    //Logger.Error(ex.ToString());
                    break;       //接收过程中如果出现异常
                }
            }
        }
    }
}
