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
    public class SearchRepository : IRepository<Search>
    {
        #region IRepository<Search> Members

        void IRepository<Models.Search>.Save(Search entity)
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

        void IRepository<Models.Search>.Update(Search entity)
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

        void IRepository<Models.Search>.Delete(Search entity)
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

        Search IRepository<Models.Search>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Search>().Add(Restrictions.Eq("ID", id)).UniqueResult<Search>();
        }

        List<Search> IRepository<Models.Search>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Search));
                criteria.AddOrder(Order.Desc("ID"));
                return criteria.List<Search>().ToList<Search>();
            }
        }
        #endregion
    }
    
}
