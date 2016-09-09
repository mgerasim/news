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
    public class ArticleRepository : IRepository<Article>
    {
        #region IRepository<Article> Members

        void IRepository<Models.Article>.Save(Article entity)
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

        void IRepository<Models.Article>.Update(Article entity)
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

        void IRepository<Models.Article>.Delete(Article entity)
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

        Article IRepository<Models.Article>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Article>().Add(Restrictions.Eq("ID", id)).UniqueResult<Article>();
        }

        public Article GetBySource(string Source)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Article>().Add(Restrictions.Eq("Source_Url", Source)).UniqueResult<Article>();
        }

        List<Article> IRepository<Models.Article>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Article));
                criteria.AddOrder(Order.Desc("ID"));
                return criteria.List<Article>().ToList<Article>();
            }
        }
        #endregion
    }
    
}
