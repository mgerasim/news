using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntity.Models
{
    public class Article
    {
        public struct CategoryItem
        {
            public readonly int ID;
            public readonly string Name;
            public CategoryItem(int ID, string Name)
            {
                this.ID = ID;
                this.Name = Name;
            }
        }
        
        public static readonly IList<CategoryItem> CategoryList = new ReadOnlyCollection<CategoryItem>
        (new[] {
            new CategoryItem(1, "Новости Хабаровска"),
            new CategoryItem(2, "Новости Дальневосточного региона РФ"),
            new CategoryItem(3, "Новости России"),
            new CategoryItem(4, "Новости в Мире"),
            new CategoryItem(999, "Статьи")
        }

        );
        

        public virtual int ID { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime updated_at { get; set; }
        public virtual string Content { get; set; }
        public virtual string Anons { get; set; }
        public virtual string Source_Url { get; set; }
        public virtual string Source_Site { get; set; }
        public virtual DateTime? Source_Published_At { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime? Published_At { get; set; }

        public virtual int Category { get; set; }

        public Article()
        {
            this.Published_At = null;
            this.Source_Published_At = null;
            this.Category = NewsEntity.Models.Article.CategoryList[0].ID;
        }

        public virtual void Save()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            Common.IRepository<Article> repo = new Repositories.ArticleRepository();

            repo.Save(this);

        }

        public virtual void Delete()
        {
            Common.IRepository<Article> repo = new Repositories.ArticleRepository();

            repo.Delete(this);
        }

        public virtual void Update()
        {
            this.updated_at = DateTime.Now;
            Common.IRepository<Article> repo = new Repositories.ArticleRepository();
            repo.Update(this);
        }

        public static List<Article> GetAll()
        {
            Common.IRepository<Article> repo = new Repositories.ArticleRepository();
            return repo.GetAll();
        }

        public static Article GetById(int ID)
        {
            Common.IRepository<Article> repo = new Repositories.ArticleRepository();
            return repo.GetById(ID);
        }
        public static Article GetBySource(string Source)
        {
            NewsEntity.Repositories.ArticleRepository repo = new Repositories.ArticleRepository();
            return repo.GetBySource(Source);
        }
        public static List<Article> GetPublished()
        {
            NewsEntity.Repositories.ArticleRepository repo = new Repositories.ArticleRepository();
            return repo.GetPublished();
        }
        public static List<Article> GetByCategory(int Category)
        {
            NewsEntity.Repositories.ArticleRepository repo = new Repositories.ArticleRepository();
            return repo.GetByCategory(Category);
        }
        public static List<Article> GetBySearch(string S)
        {
            NewsEntity.Repositories.ArticleRepository repo = new Repositories.ArticleRepository();
            return repo.GetBySearch(S);
        }
    }
}
