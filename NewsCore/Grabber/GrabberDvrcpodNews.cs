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
        private void GrabberNews(string urlNews, string urlImage)
        {
            try
            {
                string urlSite = "http://dvrcpod.ru/News.php";
                string urlAddress = urlSite + urlNews;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    readStream = new StreamReader(receiveStream);
                    

                    

                    //var bytes = await response.Content.ReadAsByteArrayAsync();
                    //Encoding encoding = Portable.Text.Encoding.GetEncoding(1251);
                    //var text = encoding.GetString(bytes, 0, bytes.Length);

                    string data = readStream.ReadToEnd();

                    Encoding encoding = Encoding.Default;
                    encoding = readStream.CurrentEncoding; 

                    byte[] encBytes = encoding.GetBytes(data);
                    byte[] utf8Bytes = Encoding.Convert(encoding, Encoding.UTF8, encBytes);
                    data = Encoding.UTF8.GetString(utf8Bytes);
                    
                    response.Close();
                    readStream.Close();
                
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                    doc.LoadHtml(data);

                    string xpathDivSelector = "//ul[@type='circle']";
                    var tagCircle = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                    if (tagCircle == null)
                    {
                        throw new Exception("Не обнаружен тег ul type=circle");
                    }

                    //tagCircle.RemoveChild(tagCircle.FirstChild);
                    

                    Log(data);



                    //var tagImgNewsPhoto = tagCircle.SelectSingleNode("//img[@class='news_photo']");
                    //if (tagImgNewsPhoto != null)
                    //{
                    //    tagImgNewsPhoto.Attributes["src"].Value = urlImage;
                    //}

                    //var tagList = tagNewsDetail.SelectNodes("//p");
                    //int MaxAnons = 2;
                    //int indexAnons = 0;
                    //string newsAnons = "";
                    //foreach (var p in tagList)
                    //{
                    //    indexAnons++;
                    //    if (indexAnons <= MaxAnons)
                    //    {
                    //        newsAnons += "<p>" + p.InnerHtml + "</p>";
                    //    }
                    //}


                    //var tagH6Date = tagNewsDetail.SelectSingleNode("//h6[@class='date']");
                    //if (tagH6Date == null)
                    //{
                    //    throw new Exception("Не обнаружен тег h6 с классом date");
                    //}
                    //string strDate = tagH6Date.InnerText;
                    //DateTime dt = DateTime.ParseExact(strDate, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    
                    //if (NewsEntity.Models.Article.GetBySource(urlAddress) == null)
                    //{
                    //    NewsEntity.Models.Article theArticle = new NewsEntity.Models.Article();
                    //    theArticle.Source_Url = urlAddress;
                    //    theArticle.Source_Site = urlSite;
                    //    theArticle.Source_Published_At = dt;                        
                    //    theArticle.Title = tagNewsDetail.FirstChild.InnerText;
                    //    tagNewsDetail.RemoveChild(tagNewsDetail.FirstChild); // Удалить заголовок
                    //    theArticle.Content = tagNewsDetail.InnerHtml;
                    //    theArticle.Anons = newsAnons;
                    //    theArticle.Category = 2;
                    //    theArticle.Save();
                    //}

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

                foreach(var item in tagNews.ChildNodes)
                {
                    if (item.Name != "li")
                    {
                        continue;
                    }
                    Log(item.InnerHtml);
                }

                Log("GrabberDvrcpodNews: Run");
                //string urlAddress = "http://primpogoda.ru/news/";
                //Log("GrabberDvrcpodNews: urlAddress: " + urlAddress);

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    Stream receiveStream = response.GetResponseStream();
                //    StreamReader readStream = null;

                //    if (response.CharacterSet == null)
                //    {
                //        readStream = new StreamReader(receiveStream);
                //    }
                //    else
                //    {
                //        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                //    }

                //    string data = readStream.ReadToEnd();

                //    response.Close();
                //    readStream.Close();

                //    var doc = new HtmlAgilityPack.HtmlDocument();
                //    HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                //    doc.LoadHtml(data);

                //    string xpathDivSelector = "//ul[@class='no-bullet news-list']";
                //    var tagNewsList = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                //    if (tagNewsList == null)
                //    {
                //        throw new Exception("Не обнаружен тег ul с классом news-list");
                //    }

                //    foreach (var item in tagNewsList.ChildNodes)
                //    {
                //        if (item.Name != "li")
                //        {
                //            continue;
                //        }

                //        if (item.Attributes["style"] != null && item.Attributes["style"].Value == "display: none")
                //        {
                //            continue;
                //        }

                //        item.RemoveChild(item.FirstChild);
                //        var tagDivRow = item.FirstChild;
                //        if (tagDivRow == null)
                //        {
                //            throw new Exception("Не обнаружен тег div class=row ...");
                //        }


                //        tagDivRow.RemoveChild(tagDivRow.FirstChild);
                //        var tagDivSmall = tagDivRow.FirstChild;
                //        if (tagDivSmall == null)
                //        {
                //            throw new Exception("Не обнаружен тег div class=small-3 ...");
                //        }

                //        var tagA = tagDivSmall.FirstChild;
                //        if (tagA == null)
                //        {
                //            throw new Exception("Не обнаружен тег a class=th");
                //        }

                //        tagA.RemoveChild(tagA.FirstChild);
                //        var tagImg = tagA.FirstChild;
                //        if (tagImg == null)
                //        {
                //            throw new Exception("Не обнаружен тег img");
                //        }
                //        string urlImage = "http://primpogoda.ru" + tagImg.Attributes["src"].Value;

                //        string urlNews = tagA.Attributes["href"].Value;
                                              

                //        Log(urlNews);
                //        this.GrabberNews(urlNews, urlImage);
                 //   }
               // }
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
