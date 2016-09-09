using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Models
{
    [JsonObject]
    public class JsonNews
    {
        public JsonNews(NewsEntity.Models.Article theArticle)
        {
            this.ID = theArticle.ID;
            this.Title = theArticle.Title;
            this.Anons = theArticle.Anons;
        }

        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Anons")]
        public string Anons { get; set; }
    }
}