using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntity.Models
{
    public class Search
    {
        public virtual int ID { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime updated_at { get; set; }
        public virtual string S { get; set; }
        public Search()
        {

        }

        public virtual void Save()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            Common.IRepository<Search> repo = new Repositories.SearchRepository();

            repo.Save(this);

        }

        public virtual void Delete()
        {
            Common.IRepository<Search> repo = new Repositories.SearchRepository();

            repo.Delete(this);
        }

        public virtual void Update()
        {
            this.updated_at = DateTime.Now;
            Common.IRepository<Search> repo = new Repositories.SearchRepository();
            repo.Update(this);
        }

        public static List<Search> GetAll()
        {
            Common.IRepository<Search> repo = new Repositories.SearchRepository();
            return repo.GetAll();
        }

        public static Search GetById(int ID)
        {
            Common.IRepository<Search> repo = new Repositories.SearchRepository();
            return repo.GetById(ID);
        }
    }
}
