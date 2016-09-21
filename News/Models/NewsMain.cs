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
        public NewsEntity.Models.Article theReviewHydrology;
        public NewsEntity.Models.Article theReviewOperative;
        public string Error = "";
        public string S = "";

        public NewsMain(bool isLoadCategory = true, string S = "")
        {
            if (isLoadCategory == true)
            {
                int recent_days = 3;
                int max_news = 3;
                this.theArticleOfKhabarovsk = NewsEntity.Models.Article.GetByCategory(1, 0, max_news, recent_days);
                this.theArticleOfRegion = NewsEntity.Models.Article.GetByCategory(2, 0, max_news, recent_days);
                this.theArticleOfRussia = NewsEntity.Models.Article.GetByCategory(3, 0, max_news, recent_days);
                this.theArticleOfWorld = NewsEntity.Models.Article.GetByCategory(4, 0, max_news, recent_days);
                this.theArticleOfOther = NewsEntity.Models.Article.GetByCategory(999, 0, max_news, recent_days);
            }
            if (S != "")
            {
                this.S = S;
                this.theSearchResult = NewsEntity.Models.Article.GetBySearch(S);
            }
            this.theReviewHydrology = null;
            this.theReviewOperative = null;
        }

        public void LoadReview()
        {
            var list = NewsEntity.Models.Article.GetByCategory(5, 0, 1);            
            if (list.Count > 0)
            {
                this.theReviewHydrology = list.First() ;
            }

            list = NewsEntity.Models.Article.GetByCategory(6, 0, 1);
            if (list.Count > 0)
            {
                this.theReviewOperative = list.First();
            }           
        }

        public List<NewsEntity.Models.Article> theSearchResult;
    }
}