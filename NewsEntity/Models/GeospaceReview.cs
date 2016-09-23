using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntity.Models
{
    public class GeospaceReview
    {
        public virtual int ID { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime updated_at { get; set; }
        public virtual int DD { get; set; }
        public virtual int MM { get; set; }
        public virtual int YYYY { get; set; }

        public virtual string Text { get; set; }

        public GeospaceReview()
        {
            ID = -1;
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            DD = DateTime.Now.Day;
            MM = DateTime.Now.Month;
            YYYY = DateTime.Now.Year;
            Text = "";
        }

        public virtual string Content
        {
            get
            {
                    string Text = this.Text;
                    string Body = Text.Split(new string[] { ">" }, StringSplitOptions.RemoveEmptyEntries)[1];

                    string res = "";
                    foreach (var line in Body.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        res += line + "<br />";
                    }

                    return res;
                
            }
        }
        public GeospaceReview(int year, int month, int day, string text)
        {
            ID = -1;
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            DD = day;
            MM = month;
            YYYY = year;
            Text = text;
        }
        public virtual void Save()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            NewsEntity.Common.IRepository<GeospaceReview> repo = new Repositories.GeospaceReviewRepository();
            repo.Save(this);
        }

        public virtual void Update()
        {
            this.updated_at = DateTime.Now;
            NewsEntity.Common.IRepository<GeospaceReview> repo = new Repositories.GeospaceReviewRepository();
            repo.Update(this);
        }

        public static GeospaceReview GetById(int id)
        {
            NewsEntity.Common.IRepository<GeospaceReview> repo = new Repositories.GeospaceReviewRepository();
            return repo.GetById(id);
        }
        public static GeospaceReview GetByLast()
        {
            Repositories.GeospaceReviewRepository repo = new Repositories.GeospaceReviewRepository();
            return repo.GetByLast();
        }
        public static GeospaceReview GetByDateUTC(int YYYY, int MM, int DD)
        {
            Repositories.GeospaceReviewRepository repo = new Repositories.GeospaceReviewRepository();
            return repo.GetByDateUTC(YYYY, MM, DD);
        }

        public static IList<GeospaceReview> GetAll()
        {
            NewsEntity.Common.IRepository<GeospaceReview> repo = new Repositories.GeospaceReviewRepository();
            return repo.GetAll();
        }

        public virtual void Send_SubdayReview()
        {
            throw new NotImplementedException();
        }
    }
}
