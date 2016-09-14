using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntity.Models
{
    public class WatchPrimpogoda
    {
        public virtual int ID { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime updated_at { get; set; }
        public virtual int Humidity { get; set; }
        public virtual DateTime Watched_At { get; set; }

        public virtual City City { get; set; }

        public WatchPrimpogoda()
        {

        }

        public virtual void Save()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            Common.IRepository<WatchPrimpogoda> repo = new Repositories.WatchPrimpogodaRepository();

            repo.Save(this);

        }

        public virtual void Delete()
        {
            Common.IRepository<WatchPrimpogoda> repo = new Repositories.WatchPrimpogodaRepository();

            repo.Delete(this);
        }

        public virtual void Update()
        {
            this.updated_at = DateTime.Now;
            Common.IRepository<WatchPrimpogoda> repo = new Repositories.WatchPrimpogodaRepository();
            repo.Update(this);
        }

        public static List<WatchPrimpogoda> GetAll()
        {
            Common.IRepository<WatchPrimpogoda> repo = new Repositories.WatchPrimpogodaRepository();
            return repo.GetAll();
        }

        public static WatchPrimpogoda GetById(int ID)
        {
            Common.IRepository<WatchPrimpogoda> repo = new Repositories.WatchPrimpogodaRepository();
            return repo.GetById(ID);
        }
        public static WatchPrimpogoda GetByDate(DateTime watchDate)
        {
            Repositories.WatchPrimpogodaRepository repo = new Repositories.WatchPrimpogodaRepository();
            return repo.GetByDate(watchDate);
        }
    }
}
