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
    public class GrabberPrimpogoda : IGrabber 
    {
        ILogger theLogger;
        private void Log(string msg)
        {
            if (theLogger != null)
            {
                theLogger.Log(msg);
            }
        }

        public GrabberPrimpogoda(ILogger theLogger = null)
        {
            this.theLogger = theLogger;
        }

        private void GrabberNews(string urlNews, string urlImage, string newsAnons) 
        {
            try
            {
                string urlSite = "http://primpogoda.ru";
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

                    string xpathDivSelector = "//div[@class='news-detail']";
                    var tagNewsDetail = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                    if (tagNewsDetail == null)
                    {
                        throw new Exception("Не обнаружен тег с классом news-detail");
                    }
                    
                    tagNewsDetail.RemoveChild(tagNewsDetail.FirstChild);

                    Log(tagNewsDetail.InnerHtml);

                    var tagImgNewsPhoto = tagNewsDetail.SelectSingleNode("//img[@class='news_photo']");
                    if (tagImgNewsPhoto != null)
                    {
                        tagImgNewsPhoto.Attributes["src"].Value = urlImage;    
                    }
                    

                    var tagH6Date = tagNewsDetail.SelectSingleNode("//h6[@class='date']");
                    if (tagH6Date == null)
                    {
                        throw new Exception("Не обнаружен тег h6 с классом date");
                    }
                    string strDate = tagH6Date.InnerText;
                    DateTime dt = DateTime.ParseExact(strDate, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                    if (NewsEntity.Models.Article.GetBySource(urlAddress) == null)
                    {
                        NewsEntity.Models.Article theArticle = new NewsEntity.Models.Article();
                        theArticle.Source_Url = urlAddress;
                        theArticle.Source_Site = urlSite;
                        theArticle.Source_Published_At = dt;
                        theArticle.Content = tagNewsDetail.InnerHtml;
                        theArticle.Title = tagNewsDetail.FirstChild.InnerText;
                        theArticle.Anons = newsAnons;
                        theArticle.Save();
                    }                   

                }

            }
            catch(Exception ex)
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
                Log("GrabberPrimpogoda: Run");
                string urlAddress = "http://primpogoda.ru/news/";
                Log("GrabberPrimpogoda: urlAddress: " + urlAddress);

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

                    string xpathDivSelector = "//ul[@class='no-bullet main-news']";
                    var tagMainNews = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                    if (tagMainNews == null)
                    {
                        throw new Exception("Не обнаружен тег с классом main-news");
                    }
                    
                    foreach (var item in tagMainNews.ChildNodes)
                    {
                        if (item.Name == "#text")
                        {
                            continue;
                        }
                        
                        item.RemoveChild(item.FirstChild);
                        var tagDivThumb = item.FirstChild;
                        if (tagDivThumb == null)
                        {
                            throw new Exception("Не обнаружен тег div class=thumb");
                        }                       
                        
                        var tagA = tagDivThumb.FirstChild;
                        if (tagA == null)
                        {
                            throw new Exception("Не обнаружен тег a class=th");
                        }

                        tagA.RemoveChild(tagA.FirstChild);
                        var tagImg = tagA.FirstChild;
                        if (tagImg == null)
                        {
                            throw new Exception("Не обнаружен тег img");
                        }
                        string urlImage = "http://primpogoda.ru" + tagImg.Attributes["src"].Value;
                        
                        string urlNews = tagA.Attributes["href"].Value;

                        var tagDivAnons = item.SelectSingleNode("//div[@class='anons']");
                        if (tagDivAnons == null)
                        {
                            throw new Exception("Не обнаружен тег div с классом anons");
                        }
                        string newsAnons = tagDivAnons.InnerText;

                        Log(urlNews);
                        this.GrabberNews(urlNews, urlImage, newsAnons);
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
 