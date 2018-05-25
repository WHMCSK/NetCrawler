using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.IO;

namespace NetCrawler.Common
{
    public class HTTPHelper
    {

        /// <summary>
        /// 实现WebProxy接口
        /// </summary>
        public class CrawlerProxyInfo : IWebProxy
        {
            public CrawlerProxyInfo(string proxyUri)
                : this(new Uri(proxyUri))
            {
            }

            public CrawlerProxyInfo(Uri proxyUri)
            {
                ProxyUri = proxyUri;
            }

            public Uri ProxyUri { get; set; }

            public ICredentials Credentials { get; set; }

            public Uri GetProxy(Uri destination) => ProxyUri;

            public bool IsBypassed(Uri host) => false;/* Proxy all requests */
        }

        /// <summary>
        /// 代理IP池子
        /// </summary>
        private static AvailableProxy availableProxy = AvailableProxyHepler.GetAvailableProxy();

        public static HttpClient Client { get; } = new HttpClient();


        public static bool SaveResourceByURL(string url, out String resName)
        {
            resName = "";
            try
            {
                System.Net.WebRequest wRequest = System.Net.WebRequest.Create(url);
                var task = wRequest.GetResponseAsync();
                System.Net.WebResponse wResp = task.Result;
                System.IO.Stream respStream = wResp.GetResponseStream();
                Stream fs = null;
                try
                {
                    var dirPath = "../../../../GirlsAlbum";
                    var fileName = url.Substring(url.LastIndexOf("/") + 1);
                    resName = fileName;
                    if (!Directory.Exists(dirPath))
                    {
                        DirectoryInfo directory = Directory.CreateDirectory(dirPath);
                    }

                    var localFileName = $"{dirPath}/{fileName}";
                    fs = new FileStream(localFileName, FileMode.Create);

                    byte[] bArr = new byte[1024];
                    int size = respStream.Read(bArr, 0, bArr.Length);
                    while (size > 0)
                    {
                        fs.Write(bArr, 0, size);
                        size = respStream.Read(bArr, 0, bArr.Length);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }

                    respStream.Close();
                    respStream.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }



        }
        /// <summary>
        /// 通过HTTP获取HTML（默认不使用代理）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isUseProxy"></param>
        /// <returns></returns>
        public static string GetHTMLByURL(string url, bool isUseProxy = false)
        {
            

            System.Net.WebRequest wRequest = System.Net.WebRequest.Create(url);
            var task = wRequest.GetResponseAsync();
            System.Net.WebResponse wResp = task.Result;
            try
            {
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
           
            //ProxyInfo proxyInfo = null;
            //try
            //{
            //    System.Net.WebRequest wRequest = System.Net.WebRequest.Create(url);
            //    CrawlerProxyInfo crawlerProxyInfo = null;
            //    //测试中发现使用代理会跳转到中转页，解决方案暂时不明确，先屏蔽代理
            //    if (url.Contains(SoureceDomainConsts.BTdytt520) && isUseProxy)
            //    {
            //        var index = new Random(DateTime.Now.Millisecond).Next(0, 20);
            //        proxyInfo = availableProxy.Btdytt520[index];
            //        crawlerProxyInfo = new CrawlerProxyInfo($"http://{proxyInfo.Ip}:{proxyInfo.Port}");

            //    }
            //    else if (url.Contains(SoureceDomainConsts.Dy2018Domain) && isUseProxy)
            //    {
            //        var index = new Random(DateTime.Now.Millisecond).Next(0, 20);
            //        proxyInfo = availableProxy.Dy2018[index];
            //        crawlerProxyInfo = new CrawlerProxyInfo($"http://{proxyInfo.Ip}:{proxyInfo.Port}");
            //    }

            //    wRequest.Proxy = crawlerProxyInfo;
            //    wRequest.ContentType = "text/html; charset=gb2312";

            //    wRequest.Method = "get";
            //    wRequest.UseDefaultCredentials = true;
            //    // Get the response instance.
            //    var task = wRequest.GetResponseAsync();
            //    System.Net.WebResponse wResp = task.Result;
            //    System.IO.Stream respStream = wResp.GetResponseStream();
            //    using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("GB2312")))
            //    {
            //        return reader.ReadToEnd();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string proxyIP = isUseProxy ? proxyInfo.Ip : "No Use Proxy";
            //    LogHelper.Error("GetHTMLByURL Exception", ex, $"URL:{url},Proxy:{proxyIP}");
            //    return string.Empty;
            //}
        }


        /// <summary>
        /// 通过HTTP获取HTML（默认不使用代理）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isUseProxy"></param>
        /// <returns></returns>
        public static string GetHTML(HttpWebRequest wRequest, bool isUseProxy = false)
        {
            ProxyInfo proxyInfo = null;
            try
            {

                CrawlerProxyInfo crawlerProxyInfo = null;
                //测试中发现使用代理会跳转到中转页，解决方案暂时不明确，先屏蔽代理
                if (wRequest.RequestUri.Host.Contains(SoureceDomainConsts.BTdytt520) && isUseProxy)
                {
                    var index = new Random(DateTime.Now.Millisecond).Next(0, 20);
                    proxyInfo = availableProxy.Btdytt520[index];
                    crawlerProxyInfo = new CrawlerProxyInfo($"http://{proxyInfo.Ip}:{proxyInfo.Port}");

                }
                else if (wRequest.RequestUri.Host.Contains(SoureceDomainConsts.Dy2018Domain) && isUseProxy)
                {
                    var index = new Random(DateTime.Now.Millisecond).Next(0, 20);
                    proxyInfo = availableProxy.Dy2018[index];
                    crawlerProxyInfo = new CrawlerProxyInfo($"http://{proxyInfo.Ip}:{proxyInfo.Port}");
                }

                wRequest.Proxy = crawlerProxyInfo;
                wRequest.ContentType = "text/html; charset=gb2312";

                wRequest.Method = "get";
                wRequest.UseDefaultCredentials = true;
                // Get the response instance.
                var task = wRequest.GetResponseAsync();
                System.Net.WebResponse wResp = task.Result;
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("GB2312")))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                string proxyIP = isUseProxy ? proxyInfo.Ip : "No Use Proxy";
                LogHelper.Error("GetHTMLByURL Exception", ex, $"URL:{wRequest.RequestUri.Host},Proxy:{proxyIP}");
                return string.Empty;
            }
        }


    }
}
