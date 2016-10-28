using NewsEntity.Models;
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
        public NewsEntity.Models.GeospaceReview theReviewGeospace;
        public string Error = "";
        public string S = "";
        
        public NewsMain(bool isLoadCategory = true, string S = "")
        {
            if (isLoadCategory == true)
            {
                this.theArticleOfKhabarovsk = NewsEntity.Models.Article.GetDisplayed(1);
                this.theArticleOfRegion = NewsEntity.Models.Article.GetDisplayed(2);
                this.theArticleOfRussia = NewsEntity.Models.Article.GetDisplayed(3);
                this.theArticleOfWorld = NewsEntity.Models.Article.GetDisplayed(4);
                this.theArticleOfOther = NewsEntity.Models.Article.GetDisplayed(999);
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
            this.theReviewGeospace = NewsEntity.Models.GeospaceReview.GetByLast();
                        
            
        }

        public List<NewsEntity.Models.Article> theSearchResult;
    }
}