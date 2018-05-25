using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using NetCrawler.Data;
using NetCrawler.Models;
using NetCrawler.Common;
using NetCrawler.Helper;
namespace NetCrawler.Crawler
{
    public class Girl13Crawler
    {
        private static HtmlParser htmlParser = new HtmlParser();

        private static DataContext MovieDataContent { get; } = new DataContext();

        public static void CrawlHostMovieInfo()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    #region

                    var indexURL = String.Format("http://www.girl13.com/page/1/");
                    var html = HTTPHelper.GetHTMLByURL(indexURL, true);
                    if (string.IsNullOrEmpty(html))
                        return;
                    var htmlDom = htmlParser.Parse(html);
                    int totalImgNum = 0;
                    int pageCount = 0;
                    int pageIndexItem = 0;

                    htmlDom.QuerySelector(".page-navigator")
                           .QuerySelectorAll("a")
                           .ForEach(a =>
                           {

                               if (int.TryParse(a.TextContent, out pageIndexItem))
                               {
                                   if (pageCount < pageIndexItem)
                                   {
                                       pageCount = pageIndexItem;
                                   }
                               }
                           });
                    Console.WriteLine(String.Format("找到美女页面{0}个", pageCount));
                    for (var i = 1; i <= pageCount; i++)
                    {
                        if (i != 1)
                        {
                            indexURL = String.Format("http://www.girl13.com/page/{0}/", i);
                            html = HTTPHelper.GetHTMLByURL(indexURL, true);
                            if (string.IsNullOrEmpty(html))
                                break;
                            htmlDom = htmlParser.Parse(html);
                        }

                        var imgInPageCount = 0;
                        htmlDom.QuerySelector("#loop-square")
                            .QuerySelectorAll("img")
                            .ForEach(img =>
                            {

                                imgInPageCount++;
                                var onlineURL = img.GetAttribute("src");
                                if (!onlineURL.Contains("weix2.gif"))
                                {
                                    MovieDataContent.Database.EnsureCreated();
                                    if (!MovieDataContent.GirlsPics.Any(mo => mo.PicOriginUrl == onlineURL))
                                    {

                                        var girlInfo = new GirlsPics();
                                        girlInfo.Id = Guid.NewGuid().ToString();
                                        girlInfo.PicOriginUrl = onlineURL;
                                        girlInfo.CreateTime = System.DateTime.Now;

                                        var savedImgName = "";
                                    HTTPHelper.SaveResourceByURL(girlInfo.PicOriginUrl, out savedImgName);
                                        girlInfo.PicLocalUrl = savedImgName;

                                    MovieDataContent.GirlsPics.Add(girlInfo);
                                        Console.WriteLine($"{imgInPageCount}/{i}/{++totalImgNum}:{girlInfo.PicOriginUrl} | success.");
                                    }
                                }

                            });
                        MovieDataContent.SaveChanges();
                        Console.WriteLine($"finished page {i}.");
                        LogHelper.Info($"finished page {i}.");
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.Error("Girl13 CrawlImg Exception", ex);
                }
            });
        }

        private static string GetHTMLByHTTPWebRequest(string indexURL)
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(indexURL);
            AddCookies(httpWebRequest);
            var html = HTTPHelper.GetHTML(httpWebRequest);
            return html;
        }

        private static void AddCookies(HttpWebRequest httpWebRequest)
        {
            httpWebRequest.CookieContainer = new CookieContainer() { };
            httpWebRequest.CookieContainer.Add(new Uri("http://www.btdytt520.com"),
                new Cookie() { Name = "JXD705135", Value = "1", Path = "/" });
            httpWebRequest.CookieContainer.Add(new Uri("http://www.btdytt520.com"),
               new Cookie() { Name = "JXD730293", Value = "1", Path = "/" });
            httpWebRequest.CookieContainer.Add(new Uri("http://www.btdytt520.com"),
               new Cookie() { Name = "JXM705135", Value = "1", Path = "/" });
            httpWebRequest.CookieContainer.Add(new Uri("http://www.btdytt520.com"),
               new Cookie() { Name = "JXM730293", Value = "1", Path = "/" });

            //httpWebRequest.CookieContainer.Add(new Uri("http://www.btdytt520.com"),
            //   new Cookie() { Name = "JXS705135   ", Value = "1", Path = "/" });
            //httpWebRequest.CookieContainer.Add(new Uri("http://www.btdytt520.com"),
            // new Cookie() { Name = "JXS730293", Value = "1", Path = "/" });

            httpWebRequest.CookieContainer.Add(new Uri("http://www.btdytt520.com"),
            new Cookie() { Name = "CNZZDATA1254168247", Value = "1890928383-1481026152-null%7C1482640111", Path = "/" });
        }



        private static Movies GetMovieInfoURL(string onlineURL)
        {
            try
            {
                var html = GetHTMLByHTTPWebRequest(onlineURL);
                if (string.IsNullOrEmpty(html))
                    return null;
                var htmlDom = htmlParser.Parse(html);
                var nameDom = htmlDom.QuerySelector("h1.font14");
                var introDom = htmlDom.QuerySelector("div.Drama_c");
                var infoTable = htmlDom.QuerySelectorAll("tr.CommonListCell");
                var pubDate = DateTime.Now;
                if (infoTable != null && infoTable.Length > 2 && !string.IsNullOrEmpty(infoTable[1].TextContent))
                {
                    DateTime.TryParse(infoTable[1].TextContent.Replace("发布时间", "").Replace("\n", ""), out pubDate);
                }
                return new Movies()
                {
                    MovieName = nameDom?.TextContent ?? "获取名称失败...",
                    OnlineUrl = onlineURL,
                    MovieIntro = introDom?.TextContent ?? "",
                    PubDate = pubDate,
                    DataCreateTime = DateTime.Now,
                    MovieType = MovieType.Latest,
                    SoureceDomain = SoureceDomainConsts.BTdytt520,
                };
            }
            catch (Exception ex)
            {
                LogHelper.Error(" Btdytt520 GetMovieInfoURL Exception", ex);
                return null;
            }

        }

    }
}
