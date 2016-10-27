using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class Meteodata
    {
        public string Error = "";
        public float Temperature = 0;
        public float Wind = 0;
        public float WindSpeed = 0;
        public DateTime Date;
        public Meteodata()
        {            
            try
            {
                MeteoService.HydroServiceClient theMeteo = new MeteoService.HydroServiceClient();

                var SiteId = theMeteo.GetSite("31721", 1);
                if (SiteId != null)
                {
                    var values = theMeteo.GetDataValuesLocal(SiteId.SiteId, DateTime.UtcNow.AddHours(-1), DateTime.Now.AddHours(12), 5, null, null, 1);
                    if (values == null || values.Count == 0)
                    {
                        throw new Exception("Данные по температуре не отобраны");
                    }
                    this.Temperature = values.Last().Value;
                    this.Date = values.Last().DateUTC.AddHours(10);

                    values = theMeteo.GetDataValuesLocal(SiteId.SiteId, DateTime.UtcNow.AddHours(-1), DateTime.Now.AddHours(12), 1, null, null, 1);
                    if (values == null || values.Count == 0)
                    {
                        throw new Exception("Данные по направлению ветра не отобраны");
                    }
                    this.Wind = values.Last().Value;

                    values = theMeteo.GetDataValuesLocal(SiteId.SiteId, DateTime.UtcNow.AddHours(-1), DateTime.Now.AddHours(12), 7, null, null, 1);
                    if (values == null || values.Count == 0)
                    {
                        throw new Exception("Данные по скорости ветра не отобраны");
                    }
                    this.WindSpeed = values.Last().Value;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err += ": " + ex.InnerException.Message;
                }
                var theView = new Models.NewsMain(false);
                Error = err;
            }
        }

        public string WindDirection
        {
            get
            {
                if (this.Wind < 45)
                {
                    return "северный";
                }

                if (this.Wind < 90)
                {
                    return "северо-восточный";
                }

                if (this.Wind < 135)
                {
                    return "восточный";
                }

                if (this.Wind < 180)
                {
                    return "юго-восточный";
                }

                if (this.Wind < 225)
                {
                    return "южный";
                }
                
                if (this.Wind < 270)
                {
                    return "юго-западный";
                }

                if (this.Wind < 315)
                {
                    return "западный";
                }

                if (this.Wind <= 360)
                {
                    return "северо-западный";
                }
                return "";
            }
        }
    }
}