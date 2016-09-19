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
    public class GrabberDvrcpodNews : IGrabber
    {
        ILogger theLogger;
        private void Log(string msg)
        {
            if (theLogger != null)
            {
                theLogger.Log(msg);
            }
        }
        
        public GrabberDvrcpodNews(ILogger theLogger = null)
        {
            this.theLogger = theLogger;
        }
        private void GrabberNews(string urlNews)
        {
            try
            {
                string urlSite = "http://dvrcpod.ru/";
                string urlAddress = urlSite + urlNews;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding(1251));
                string data = reader.ReadToEnd();

                reader.Close();

                var doc = new HtmlAgilityPack.HtmlDocument();
                HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                doc.LoadHtml(data);

                string xpathDivSelector = "//div[@class='article']";
                var tagArticle = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                if (tagArticle == null)
                {
                    throw new Exception("Не обнаружен тег div class=article");
                }

                string Title = tagArticle.FirstChild.InnerText;
                tagArticle.RemoveChild(tagArticle.FirstChild);
                string Date = tagArticle.FirstChild.InnerText;
                DateTime dt = DateTime.ParseExact(Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                tagArticle.RemoveChild(tagArticle.FirstChild);
                string Anons = tagArticle.FirstChild.InnerText;
                string Content = tagArticle.InnerHtml;

                NewsEntity.Models.Article theArticle = NewsEntity.Models.Article.GetBySource(urlAddress);
                if (theArticle == null)
                {
                    theArticle = new NewsEntity.Models.Article();
                    theArticle.Title = Title;
                    theArticle.Anons = Anons;
                    theArticle.Content = Content;

                    theArticle.Source_Published_At = dt;
                    theArticle.Source_Site = urlSite;
                    theArticle.Source_Url = urlAddress;

                    theArticle.Category = 2;

                    theArticle.Save();
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dvrcpod.ru/News.php/");
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding(1251));
                string data = reader.ReadToEnd();

                reader.Close();

                var doc = new HtmlAgilityPack.HtmlDocument();
                HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                doc.LoadHtml(data);

                string xpathDivSelector = "//ul[@type='circle']";
                var tagCircle = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                if (tagCircle == null)
                {
                    throw new Exception("Не обнаружен тег ul type=circle");
                }
                tagCircle.RemoveChild(tagCircle.FirstChild);

                var tagLI = tagCircle.FirstChild;
                if (tagLI == null)
                {
                    throw new Exception("Не обнаружен тег li");
                }
                tagLI.RemoveChild(tagLI.FirstChild);


                var tagNews = tagLI.FirstChild;
                if (tagNews == null)
                {
                    throw new Exception("Не обнаружен тег ul новостей");
                }

                var tagNewsList = tagNews.FirstChild;
                if (tagNewsList == null)
                {
                    throw new Exception("Не обнаружен тег NewsList");
                }
                                         

                foreach(var item in tagNewsList.InnerHtml.Split(new string [] {"<li>"}, StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        string hrefHtml = item.Replace("</li>", "");

                        var html = HtmlNode.CreateNode("<div>" + hrefHtml + "</div>");

                        var tagA = html.LastChild;
                        if (tagA == null)
                        {
                            throw new Exception("Не обнаружен тег A новостей");
                        }

                        this.GrabberNews(tagA.Attributes["href"].Value);

                    }
                    catch(Exception ex)
                    {
                        string err = ex.Message + "\n" + ex.StackTrace;

                        if (ex.InnerException != null)
                        {
                            err += ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
                        }
                        Log(err);
                    }
                }

                Log("GrabberDvrcpodNews: Run");
                
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
