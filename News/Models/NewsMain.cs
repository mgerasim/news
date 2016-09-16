using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class NewsMain
    {
        public List<NewsEntity.Models.Article> theArticleOfKhabarovsk;
        public List<NewsEntity.Models.Article> theArticleOfRegion;
        public List<NewsEntity.Models.Article> theArticleOfRussia;
        public List<NewsEntity.Models.Article> theArticleOfWorld;
        public List<NewsEntity.Models.Article> theArticleOfOther;
        public string Error = "";
        public string S = "";

        public NewsMain(bool isLoadCategory = true, string S = "")
        {
            if (isLoadCategory == true)
            {
                this.theArticleOfKhabarovsk = NewsEntity.Models.Article.GetByCategory(1);
                this.theArticleOfRegion = NewsEntity.Models.Article.GetByCategory(2);
                this.theArticleOfRussia = NewsEntity.Models.Article.GetByCategory(3);
                this.theArticleOfWorld = NewsEntity.Models.Article.GetByCategory(4);
                this.theArticleOfOther = NewsEntity.Models.Article.GetByCategory(999);
            }
            if (S != "")
            {
                this.S = S;
                this.theSearchResult = NewsEntity.Models.Article.GetBySearch(S);
            }
        }

        public List<NewsEntity.Models.Article> theSearchResult;
    }
}