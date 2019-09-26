using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox
{
    public static class TNetWork
    {
        private static string IP { get; set; }

        /// <summary>
        /// 获取本机ip
        /// </summary>
        /// <param name="ClearCache">清除缓存</param>
        /// <returns></returns>
        public static string GetIpAddress(bool ClearCache = false)
        {
            if (!ClearCache && !string.IsNullOrEmpty(IP))
            {
                return IP;
            }

            string loolback = "Microsoft Loopback Adapter";
            string hostIP = String.Empty;
            NetworkInterface networkInterfaces =
                NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(c => c.Description == loolback);
            if (networkInterfaces != null)
            {
                hostIP = networkInterfaces.GetIPProperties()
                        .UnicastAddresses.FirstOrDefault(c => c.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        .Address.ToString();
                IEnumerable<NetworkInterface> arrNetworkInterfaces =
                    NetworkInterface.GetAllNetworkInterfaces()
                        .Where(c =>
                                c.OperationalStatus == OperationalStatus.Up &&
                                c.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                                c.Description.ToString() != loolback &&
                                c.NetworkInterfaceType != NetworkInterfaceType.Tunnel);
                foreach (NetworkInterface objNetworkInterface in arrNetworkInterfaces)
                {
                    var unicastIPAddressInformationCollection =
                        objNetworkInterface.GetIPProperties().UnicastAddresses;
                    foreach (UnicastIPAddressInformation ip in unicastIPAddressInformationCollection)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                            hostIP.Substring(0, hostIP.LastIndexOf('.')) ==
                            ip.Address.ToString().Substring(0, ip.Address.ToString().LastIndexOf('.')))
                        {
                            hostIP = ip.Address.ToString();
                            break;
                        }
                    }
                }
            }
            else
            {
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                foreach (IPAddress address in addressList)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        hostIP = address.ToString();
                        break;
                    }
                }
            }
            IP = hostIP;
            return hostIP;
        }
    }
}
