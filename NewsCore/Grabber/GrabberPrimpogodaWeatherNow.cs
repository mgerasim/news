using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewsCore.Grabber
{
    public class GrabberPrimpogodaWeatherNow : IGrabber
    {
        ILogger theLogger = null;
        public GrabberPrimpogodaWeatherNow(ILogger  theLogger = null)
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
                Log("GrabberPrimpogodaWeatherNow: Run");
                var cityList = NewsEntity.Models.City.GetAll();
                foreach (var city in cityList)
                {
                    int Humidity = -1;
                    try
                    {
                        Log("GrabberPrimpogodaWeatherNow: Run: " + city.Name);        
                        string urlAddress = city.Url_Primpogoda_Weather_Now;
                        Log("GrabberPrimpogodaWeatherNow: urlAddress: " + urlAddress);

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

                            string xpathDivSelector = "//ul[@class='large-block-grid-3 medium-block-grid-2 small-block-grid-1']";
                            var tagUL = doc.DocumentNode.SelectSingleNode(xpathDivSelector);
                            if (tagUL == null)
                            {
                                throw new Exception("Не обнаружен тег ul с описанием метеопараметров");
                            }

                            foreach (var item in tagUL.ChildNodes)
                            {
                                if (item.Name == "#text")
                                {
                                    continue;
                                }
                                
                                item.RemoveChild(item.FirstChild);
                                var tagDivSmall = item.FirstChild;
                                if (tagDivSmall == null)
                                {
                                    throw new Exception("Не обнаружен тег div class=small");
                                }

                                if (tagDivSmall.FirstChild.Name == "#text")
                                {
                                    tagDivSmall.RemoveChild(tagDivSmall.FirstChild);
                                }

                                var tagSmall = tagDivSmall.FirstChild;
                                if (tagDivSmall == null)
                                {
                                    throw new Exception("Не обнаружен тег small");
                                }

                                if (tagSmall.InnerText == "Относительная влажность воздуха")
                                {
                                    var tagP = item.ChildNodes[2];
                                    if (tagP == null)
                                    {
                                        throw new Exception("Не обнаружен тег p");
                                    }
                                    Humidity = Convert.ToInt32(tagP.InnerText.Replace('%',' '));
                                    break;
                                }
                            }
                            if (Humidity == -1)
                            {
                                throw new Exception("Не найден параметр отностельная влажность воздуха");
                            }

                            string xpathYear = "//meta[@name='copyright']";
                            var tagYear = doc.DocumentNode.SelectSingleNode(xpathYear);
                            if (tagYear == null)
                            {
                                throw new Exception("Не обнаружен тег meta name=\"copyright\"");
                            }

                            char[] separator = { '-' };
                            string YearString = tagYear.Attributes["content"].Value.Split(separator)[1];
                            
                            string xpathWatchDateTime = "//small[@class='options']";
                            var tagWatchDateTime = doc.DocumentNode.SelectSingleNode(xpathWatchDateTime);
                            if (tagWatchDateTime == null)
                            {
                                throw new Exception("Не обнаружен тег small class=options с временем наблюдения");
                            }

                            
                            string watchDateTimeString = tagWatchDateTime.InnerText;
                            string[] months = {"января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря"};

                            int i = 0;
                            foreach (var month in months)
                            {
                                i++;
                                watchDateTimeString = watchDateTimeString.Replace(month, i.ToString("00") + " " + YearString);
                            }
                            watchDateTimeString = watchDateTimeString.Replace("Время наблюдения: ", "");
                            DateTime watchDateTime = DateTime.ParseExact(watchDateTimeString, "dd MM yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                            NewsEntity.Models.WatchPrimpogoda theWatch = NewsEntity.Models.WatchPrimpogoda.GetByDate(watchDateTime);
                            if (theWatch == null)
                            {
                                theWatch = new NewsEntity.Models.WatchPrimpogoda();
                                theWatch.Humidity = Humidity;
                                theWatch.Watched_At = watchDateTime;
                                theWatch.Save();
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
