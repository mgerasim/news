using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntity.Models
{
    public class Article
    {
        public virtual int ID { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime updated_at { get; set; }
        public virtual string Content { get; set; }
        public virtual string Source { get; set; }
        public virtual string Title { get; set; }

        public Article()
        {

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
    }
}
