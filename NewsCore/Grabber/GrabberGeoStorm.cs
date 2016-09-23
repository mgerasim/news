using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewsCore.Grabber
{
    public class GrabberGeoStorm : IGrabber
    {
        ILogger theLogger;
        private void Log(string msg)
        {
            if (theLogger != null)
            {
                theLogger.Log(msg);
            }
        }
        
        public GrabberGeoStorm(ILogger theLogger = null)
        {
            this.theLogger = theLogger;
        }
        private void GrabberNews(string urlNews)
        {
            try
            {
                string urlSite = "http://geo-storm.ru";
                string urlAddress = urlSite + urlNews;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    string data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();

                    var doc = new HtmlAgilityPack.HtmlDocument();
                    HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                    doc.LoadHtml(data);

                    string xpathDivSelector = "//div[@class='content']";
                    var tagNewsWrapper = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                    if (tagNewsWrapper == null)
                    {
                        throw new Exception("Не обнаружен тег div с классом content");
                    }
                    tagNewsWrapper.RemoveChild(tagNewsWrapper.FirstChild);
                    tagNewsWrapper.RemoveChild(tagNewsWrapper.FirstChild);
                    tagNewsWrapper.RemoveChild(tagNewsWrapper.FirstChild);
                    
                    string Title = tagNewsWrapper.FirstChild.InnerText;

                    var tagContent = doc.DocumentNode.SelectSingleNode("//div[@class='banons b']");
                    if (tagContent == null)
                    {
                        throw new Exception("Не обнаружен тег div с классом banons b");
                    }

                    var watchDateTimeString = tagContent.FirstChild.InnerText;
                    
                    DateTime watchDateTime = DateTime.ParseExact(watchDateTimeString, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    tagContent.RemoveChild(tagContent.FirstChild);
                    tagContent.RemoveChild(tagContent.FirstChild);

                    tagContent.FirstChild.Attributes["href"].Value = urlSite + tagContent.FirstChild.Attributes["href"].Value;

                    var tagImg = tagContent.FirstChild.FirstChild;
                    tagImg.Attributes["src"].Value = urlSite + tagImg.Attributes["src"].Value;
                    tagContent.RemoveChild(tagContent.FirstChild);
                    tagContent.InsertBefore(tagImg, tagContent.FirstChild);

                    NewsEntity.Models.Article theNews = NewsEntity.Models.Article.GetBySource(urlAddress);
                    if (theNews == null)
                    {
                        theNews = new NewsEntity.Models.Article();
                        theNews.Title = Title;
                        tagContent.RemoveChild(tagContent.LastChild);
                        tagContent.RemoveChild(tagContent.LastChild);
                        tagContent.RemoveChild(tagContent.LastChild);
                        tagContent.RemoveChild(tagContent.LastChild);
                        tagContent.RemoveChild(tagContent.LastChild);
                        tagContent.RemoveChild(tagContent.LastChild);
                        tagContent.RemoveChild(tagContent.LastChild);
                        theNews.Content = tagContent.InnerHtml;

                        tagContent.RemoveChild(tagContent.FirstChild);
                        theNews.Anons = tagContent.FirstChild.InnerText;
                        theNews.Source_Published_At = watchDateTime;
                        theNews.Source_Site = urlSite;
                        theNews.Source_Url = urlAddress;
                        theNews.Category = 999;
                        theNews.Published_At = DateTime.Now;
                        theNews.Save();
                    }
                }               

            }
            catch (Exception ex)
            {
                Log("GrabberNews: Error: " + urlNews);
                string err = ex.Message + "\n" + ex.StackTrace;

                if (ex.InnerException != null)
                {
                    err += ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
                }
                Log(err);
            }
        }
        void IGrabber.Run()
        {
            try
            {
                Log("GrabberGeoStorm::Run");
                string urlSite = "http://geo-storm.ru/priroda-i-klimat/pogoda/";
                string urlAddress = urlSite;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    string data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();

                    var doc = new HtmlAgilityPack.HtmlDocument();
                    HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                    doc.LoadHtml(data);

                    string xpathDivSelector = "//div[@class='banons']";
                    var tagNewsList = doc.DocumentNode.SelectNodes(xpathDivSelector);
                    if (tagNewsList == null)
                    {
                        throw new Exception("Не обнаружен тег div с классом banons");
                    }
                    foreach (var news in tagNewsList)
                    {
                        var tagNews = news.FirstChild;
                        if (tagNews == null) {
                            Log("Error: Не обнаружена ссылка на статью");
                            continue;
                        }
                        if (tagNews.Name == "a")
                        {
                            this.GrabberNews(tagNews.Attributes["href"].Value);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string err = ex.Message + "\n" + ex.StackTrace;

                if (ex.InnerException != null)
                {
                    err += ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
                }
                Log(err);
            }
        }
    }
}
