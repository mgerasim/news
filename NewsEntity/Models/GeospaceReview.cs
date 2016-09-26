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
        public virtual List<CodeUmagf> theListCodeUmagf { get; set; }

        public GeospaceReview()
        {
            ID = -1;
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            DD = DateTime.Now.Day;
            MM = DateTime.Now.Month;
            YYYY = DateTime.Now.Year;
            Text = "";
            this.theListCodeUmagf = new List<CodeUmagf>();
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
            this.theListCodeUmagf = new List<CodeUmagf>();
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

        public virtual string Date
        {
            get
            {
                return this.DD.ToString("00") + "." + this.MM.ToString("00") + "." + this.YYYY.ToString("0000");
            }
        }
        public virtual void LoadAp()
        {
            int KhabarovskStation = 4;
            DateTime currDate = DateTime.Now;
            DateTime startDate = new DateTime(currDate.Year, currDate.Month, 1);
            DateTime endDate = new DateTime(currDate.Year, currDate.Month, DateTime.DaysInMonth(currDate.Year, currDate.Month));
            List<CodeUmagf> theUmagfList = (List<CodeUmagf>)CodeUmagf.GetByPeriod(KhabarovskStation, startDate, endDate);

            
            for (int i = 1; i <= endDate.Day; i++)
            {
                CodeUmagf theCode = null;
                if (theUmagfList.Count(x => x.DD == i) > 0)
                {
                    theCode = theUmagfList.First(x => x.DD == i);
                }
                else
                {
                    theCode = new CodeUmagf();
                    theCode.ak = 1000;
                    theCode.DD = i;
                    theCode.MM = currDate.Month;
                    theCode.YYYY = currDate.Year;
                }
                this.theListCodeUmagf.Add(theCode);
            }
        }
    }
}
