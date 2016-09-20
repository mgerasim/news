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
        public NewsEntity.Models.Article theArticleHydrology;
        public string Error = "";
        public string S = "";

        public NewsMain(bool isLoadCategory = true, string S = "")
        {
            if (isLoadCategory == true)
            {
                int recent_days = 3;
                this.theArticleOfKhabarovsk = NewsEntity.Models.Article.GetByCategory(1, 0, -1, recent_days);
                this.theArticleOfRegion = NewsEntity.Models.Article.GetByCategory(2, 0, -1, recent_days);
                this.theArticleOfRussia = NewsEntity.Models.Article.GetByCategory(3, 0, -1, recent_days);
                this.theArticleOfWorld = NewsEntity.Models.Article.GetByCategory(4, 0, -1, recent_days);
                this.theArticleOfOther = NewsEntity.Models.Article.GetByCategory(999, 0, -1, recent_days);
            }
            if (S != "")
            {
                this.S = S;
                this.theSearchResult = NewsEntity.Models.Article.GetBySearch(S);
            }
            this.theArticleHydrology = null;
        }

        public void LoadHydrology()
        {
            var list = NewsEntity.Models.Article.GetByCategory(5, 0, 1);            
            if (list.Count > 0)
            {
                this.theArticleHydrology = NewsEntity.Models.Article.GetByCategory(5, 0, 1).First() ;
            }            
        }

        public List<NewsEntity.Models.Article> theSearchResult;
    }
}