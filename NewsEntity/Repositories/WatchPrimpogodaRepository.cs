using NewsEntity.Models;
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
    public class WatchPrimpogodaRepository : IRepository<NewsEntity.Models.WatchPrimpogoda>
    {
        #region IRepository<Watch> Members

        void IRepository<Models.WatchPrimpogoda>.Save(WatchPrimpogoda entity)
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

        void IRepository<Models.WatchPrimpogoda>.Update(WatchPrimpogoda entity)
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

        void IRepository<Models.WatchPrimpogoda>.Delete(WatchPrimpogoda entity)
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

        WatchPrimpogoda IRepository<Models.WatchPrimpogoda>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<WatchPrimpogoda>().Add(Restrictions.Eq("ID", id)).UniqueResult<WatchPrimpogoda>();
        }

        public WatchPrimpogoda GetByDate(DateTime dateWatch)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<WatchPrimpogoda>().Add(Restrictions.Eq("Watched_At", dateWatch)).UniqueResult<WatchPrimpogoda>();
        }


        List<WatchPrimpogoda> IRepository<Models.WatchPrimpogoda>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(WatchPrimpogoda));
                criteria.AddOrder(Order.Desc("ID"));
                return criteria.List<WatchPrimpogoda>().ToList<WatchPrimpogoda>();
            }
        }
        #endregion
    }
    
}
