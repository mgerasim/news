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
    public class GrabberKhabkrai : IGrabber
    {
        ILogger theLogger;
        private void Log(string msg)
        {
            if (theLogger != null)
            {
                theLogger.Log(msg);
            }
        }
        
        public GrabberKhabkrai(ILogger theLogger = null)
        {
            this.theLogger = theLogger;
        }
        private void GrabberNews(string urlNews)
        {
            try
            {
                string urlSite = "https://khabkrai.ru";
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

                    string xpathDivSelector = "//span[@class='wrapper']";
                    var tagNewsWrapper = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                    if (tagNewsWrapper == null)
                    {
                        throw new Exception("Не обнаружен тег span с классом wraper");
                    }
                    string Title = tagNewsWrapper.InnerText;

                    var tagContent = doc.DocumentNode.SelectSingleNode("//div[@class='content-text']");
                    if (tagContent == null)
                    {
                        throw new Exception("Не обнаружен тег div с классом content-text");
                    }

                    var tagWatchDateTime = doc.DocumentNode.SelectSingleNode("//div[@class='material-date data-item']");
                    string watchDateTimeString = tagWatchDateTime.InnerText.Trim().Replace("&nbsp;", " ");
                    string[] months = { "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря" };

                    int i = 0;
                    foreach (var month in months)
                    {
                        i++;
                        watchDateTimeString = watchDateTimeString.Replace(month, i.ToString("00"));
                    }
                    
                    DateTime watchDateTime = DateTime.ParseExact(watchDateTimeString, "dd MM yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    NewsEntity.Models.Article theNews = NewsEntity.Models.Article.GetBySource(urlAddress);
                    if (theNews == null)
                    {
                        theNews = new NewsEntity.Models.Article();
                        theNews.Title = Title;
                        tagContent.RemoveChild(tagContent.LastChild);
                        theNews.Content = tagContent.InnerHtml + "<p>Пресс-служба Губернатора и Правительства Хабаровского края www.khabkrai.ru</p>";
                        theNews.Anons = tagContent.FirstChild.InnerText;
                        theNews.Source_Published_At = watchDateTime;
                        theNews.Source_Site = urlSite;
                        theNews.Source_Url = urlAddress;
                        theNews.Category = 6;
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
                string urlSite = "https://khabkrai.ru/events/news";
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

                    string xpathDivSelector = "//div[@class='news-title news-textfield']";
                    var tagNewsList = doc.DocumentNode.SelectNodes(xpathDivSelector);
                    if (tagNewsList == null)
                    {
                        throw new Exception("Не обнаружен тег div с классом news-title news-textfield");
                    }
                    foreach (var news in tagNewsList)
                    {
                        string template = "Оперативная информация";
                        if (news.InnerText.Trim().Substring(0, 22) == template)
                        {
                            Log(news.InnerText.Trim());
                            news.RemoveChild(news.FirstChild);
                            string urlNews = news.FirstChild.Attributes["href"].Value;
                            Log(urlNews);
                            this.GrabberNews(urlNews);
                            break;
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
