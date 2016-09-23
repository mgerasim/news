using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NewsEntity.Models;
using NewsEntity.Common;

namespace NewsEntity.Repositories
{
    public class GeospaceReviewRepository : IRepository<GeospaceReview>
    {
        #region IRepository<GeospaceReview> Members

        void IRepository<Models.GeospaceReview>.Save(GeospaceReview entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        void IRepository<Models.GeospaceReview>.Update(GeospaceReview entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(entity);
                    transaction.Commit();
                }
            }
        }

        void IRepository<Models.GeospaceReview>.Delete(GeospaceReview entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
            }
        }

        GeospaceReview IRepository<Models.GeospaceReview>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<GeospaceReview>().Add(Restrictions.Eq("ID", id)).UniqueResult<GeospaceReview>();
        }

        public GeospaceReview GetBySource(string Source)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<GeospaceReview>().Add(Restrictions.Eq("Source_Url", Source)).UniqueResult<GeospaceReview>();
        }
        public List<GeospaceReview> GetPublished()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(GeospaceReview));
                criteria.AddOrder(Order.Desc("ID"));
                criteria.Add(Restrictions.IsNotNull("Published_At"));
                return criteria.List<GeospaceReview>().ToList<GeospaceReview>();
            }
        }

        public List<GeospaceReview> GetByCategory(int Category, int offset = 0, int max = -1, int recent_days = 0)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(GeospaceReview));
                criteria.AddOrder(Order.Desc("ID"));
                criteria.Add(Restrictions.IsNotNull("Published_At"));
                criteria.Add(Restrictions.Eq("Category", Category));
                if (max > 0)
                {
                    criteria.SetMaxResults(max);                    
                }

                if (recent_days > 0)
                {
                    criteria.Add(Restrictions.Ge("Published_At", DateTime.Now.AddDays(-1 * recent_days)));
                }
                return criteria.List<GeospaceReview>().ToList<GeospaceReview>();
            }
        }
        public List<GeospaceReview> GetBySearch(string S)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(GeospaceReview));
                criteria.AddOrder(Order.Desc("ID"));
                criteria.Add(Restrictions.IsNotNull("Published_At"));
                criteria.Add(Restrictions.Or(Restrictions.Like("Content", S, MatchMode.Anywhere), Restrictions.Like("Title", S, MatchMode.Anywhere)));
                return criteria.List<GeospaceReview>().ToList<GeospaceReview>();
            }
        }

        List<GeospaceReview> IRepository<Models.GeospaceReview>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(GeospaceReview));
                criteria.AddOrder(Order.Desc("ID"));
                return criteria.List<GeospaceReview>().ToList<GeospaceReview>();
            }
        }
        #endregion

         public NewsEntity.Models.GeospaceReview GetByDateUTC(int YYYY, int MM, int DD)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria<NewsEntity.Models.GeospaceReview>()
                    .Add(Restrictions.Eq("YYYY", YYYY))
                    .Add(Restrictions.Eq("MM", MM))
                    .Add(Restrictions.Eq("DD", DD))
                    .UniqueResult<NewsEntity.Models.GeospaceReview>();
                
            }
        }

        public Models.GeospaceReview GetByLast()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(NewsEntity.Models.GeospaceReview));
                criteria.AddOrder(Order.Asc("ID"));
                return criteria.List<NewsEntity.Models.GeospaceReview>().Last();
            }
        }
    }
    
}
