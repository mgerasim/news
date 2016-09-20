using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewsCore.Grabber
{
    public class GrabberKhabmeteoHydrology : IGrabber
    {
        ILogger theLogger = null;
        public GrabberKhabmeteoHydrology(ILogger  theLogger = null)
        {
            this.theLogger = theLogger;
        }

        private void Log(string msg)
        {
            if (theLogger != null)
            {
                theLogger.Log(msg);
            }
        }

        void IGrabber.Run()
        {
            try
            {
                Log("GrabberKhabmeteoHydrology: Run");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://khabmeteo.ru/cgi-bin/gidrolog.cgi");
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding(1251));
                string data = reader.ReadToEnd();

                reader.Close();

                var doc = new HtmlAgilityPack.HtmlDocument();
                HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                doc.LoadHtml(data);

                string xpathDivSelector = "//div[@id='content']";
                var tagContent = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                if (tagContent == null)
                {
                    throw new Exception("Не обнаружен тег div id=content");
                }

                var tagAnons = tagContent.FirstChild;
                if (tagAnons == null)
                {
                    throw new Exception("Не обнаружен тег Anons");
                }
                string Text = tagAnons.InnerText.Trim();

                                
                var hydroList = NewsEntity.Models.Article.GetBySearch(Text);
                if (hydroList.Count == 0)
                {
                    var hydroArticle = new NewsEntity.Models.Article();
                    hydroArticle.Anons = Text;
                    hydroArticle.Content = Text;
                    hydroArticle.Published_At = DateTime.Now;
                    hydroArticle.Source_Published_At = DateTime.Now;
                    hydroArticle.Source_Site = "khabmeteo.ru";
                    hydroArticle.Source_Url = "http://khabmeteo.ru/cgi-bin/gidrolog.cgi";
                    hydroArticle.Title = "Гидрологическая обстановка за " + DateTime.Now.ToShortDateString();
                    hydroArticle.Category = 5;
                    hydroArticle.Save();
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
