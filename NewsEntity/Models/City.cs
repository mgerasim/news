using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntity.Models
{
    public class City
    {
        public virtual int ID { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime updated_at { get; set; }
        public virtual string Name { get; set; }
        public virtual string Url_Primpogoda_Weather_Now { get; set; }
        public virtual string Url_Primpogoda_Weather_Today { get; set; }

        public City()
        {

        }

        public virtual void Save()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            Common.IRepository<City> repo = new Repositories.CityRepository();

            repo.Save(this);

        }

        public virtual void Delete()
        {
            Common.IRepository<City> repo = new Repositories.CityRepository();

            repo.Delete(this);
        }

        public virtual void Update()
        {
            this.updated_at = DateTime.Now;
            Common.IRepository<City> repo = new Repositories.CityRepository();
            repo.Update(this);
        }

        public static List<City> GetAll()
        {
            Common.IRepository<City> repo = new Repositories.CityRepository();
            return repo.GetAll();
        }

        public static City GetById(int ID)
        {
            Common.IRepository<City> repo = new Repositories.CityRepository();
            return repo.GetById(ID);
        }
    }
}
