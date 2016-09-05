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
    public class MediaRepository : IRepository<Media>
    {
        #region IRepository<Media> Members

        void IRepository<Models.Media>.Save(Media entity)
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

        void IRepository<Models.Media>.Update(Media entity)
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

        void IRepository<Models.Media>.Delete(Media entity)
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

        Media IRepository<Models.Media>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Media>().Add(Restrictions.Eq("ID", id)).UniqueResult<Media>();
        }

        List<Media> IRepository<Models.Media>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Media));
                criteria.AddOrder(Order.Desc("ID"));
                return criteria.List<Media>().ToList<Media>();
            }
        }
        #endregion
    }
    
}
