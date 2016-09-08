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

        private void GrabberNews(string urlNews, string urlImage) 
        {
            try
            {
                string urlAddress = "http://primpogoda.ru" + urlNews;

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
                    Log("GrabberNews: readStream:\n " + data);


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
                    if (tagImgNewsPhoto == null)
                    {
                        throw new Exception("Не обнаружен тег img с классом news-photo");
                    }
                    tagImgNewsPhoto.Attributes["src"].Value = urlImage;

                    if (NewsEntity.Models.Article.GetBySource(urlAddress) == null)
                    {
                        NewsEntity.Models.Article theArticle = new NewsEntity.Models.Article();
                        theArticle.Source = urlAddress;
                        theArticle.Content = tagNewsDetail.InnerHtml;
                        theArticle.Title = tagNewsDetail.FirstChild.InnerText;
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
                    Log("GrabberPrimpogoda: readStream:\n " + data);


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
                        Log(urlNews);
                        this.GrabberNews(urlNews, urlImage);
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
 