using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ToolBox
{
    /// <summary>
    /// Http请求工具
    /// </summary>
    public static class THttp
    {
        /// <summary>
        /// 简易HttpPost请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>返回值</returns>
        public static byte[] SimplePost(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST"; //设置为post请求
            request.ContentType = "application/x-www-form-urlencoded";

            StringBuilder buffer = new StringBuilder();
            bool flag = false;
            foreach (string key in parameters.Keys)
            {
                if (flag)
                {
                    buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    flag = true;
                }
            }
            byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
            request.ContentLength = data.Length;
            using (Stream requeststream = request.GetRequestStream())
            {
                requeststream.Write(data, 0, data.Length);
                requeststream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            int count = (int)response.ContentLength;
            int offset = 0;
            byte[] buf = new byte[count];
            while (count > 0)
            { //循环度可以避免数据不完整
                int n = stream.Read(buf, offset, count);
                if (n == 0) break;
                count -= n;
                offset += n;
            }
            return buf;
        }

        /// <summary>
        /// 简易HttpPost请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>返回值</returns>
        public static string SimplePostString(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST"; //设置为post请求
            request.ContentType = "application/json";

            StringBuilder buffer = new StringBuilder();
            //bool flag = false;
            //foreach (string key in parameters.Keys)
            //{
            //    if (flag)
            //    {
            //        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
            //    }
            //    else
            //    {
            //        buffer.AppendFormat("{0}={1}", key, parameters[key]);
            //        flag = true;
            //    }
            //}
            buffer.Append("{\"queryParams\": { \"personIds\": \"[122]\"}  ");
            byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
            request.ContentLength = data.Length;
            using (Stream requeststream = request.GetRequestStream())
            {
                requeststream.Write(data, 0, data.Length);
                requeststream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 简易HttpPost方法
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postObject">请求参数对象</param>
        /// <returns>返回值</returns>
        public static byte[] SimpleJsonPost(string url, object postObject)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST"; //设置为post请求
            //request.ReadWriteTimeout = 5000;
            //request.KeepAlive = false;
            request.ContentType = "application/json";
            string postJson = Newtonsoft.Json.JsonConvert.SerializeObject(postObject);
            byte[] postData = Encoding.UTF8.GetBytes(postJson);
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            int count = (int)response.ContentLength;
            int offset = 0;
            byte[] buf = new byte[count];
            while (count > 0)
            { //循环度可以避免数据不完整
                int n = stream.Read(buf, offset, count);
                if (n == 0) break;
                count -= n;
                offset += n;
            }
            return buf;
        }

        /// <summary>
        /// 简易HttpGet请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] SimpleGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //request.Timeout = 5000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            int count = (int)response.ContentLength;
            int offset = 0;
            byte[] buf = new byte[count];
            while (count > 0)
            { //循环度可以避免数据不完整
                int n = stream.Read(buf, offset, count);
                if (n == 0) break;
                count -= n;
                offset += n;
            }
            return buf;
        }

        public static string SimpleGetString(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //request.Timeout = 5000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        public static HttpWebResponse CreateGetHttpResponse(string url, int timeout, string userAgent, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //对服务端证书进行有效性校验（非第三方权威机构颁发的证书，如自己生成的，不进行验证，这里返回true）
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;    //http版本，默认是1.1,这里设置为1.0
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "GET";

            //设置代理UserAgent和超时
            request.UserAgent = userAgent;
            request.Timeout = timeout;

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int timeout, string userAgent, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //设置代理UserAgent和超时
            request.UserAgent = userAgent;
            request.Timeout = timeout; 

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //发送POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        i++;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            string[] values = request.Headers.GetValues("Content-Type");
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();

            }
        }

        /// <summary>
        /// 验证证书
        /// </summary>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }
    }
}
