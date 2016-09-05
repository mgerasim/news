using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntity.Models
{
    public class Media
    {
        public virtual int ID { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime updated_at { get; set; }
        public virtual string Name { get; set; }
        public virtual string Source { get; set; }
        public virtual string Title { get; set; }

        public Media()
        {

        }

        public virtual void Save()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            Common.IRepository<Media> repo = new Repositories.MediaRepository();

            repo.Save(this);

        }

        public virtual void Delete()
        {
            Common.IRepository<Media> repo = new Repositories.MediaRepository();

            repo.Delete(this);
        }

        public virtual void Update()
        {
            this.updated_at = DateTime.Now;
            Common.IRepository<Media> repo = new Repositories.MediaRepository();
            repo.Update(this);
        }

        public static List<Media> GetAll()
        {
            Common.IRepository<Media> repo = new Repositories.MediaRepository();
            return repo.GetAll();
        }

        public static Media GetById(int ID)
        {
            Common.IRepository<Media> repo = new Repositories.MediaRepository();
            return repo.GetById(ID);
        }
    }
}
