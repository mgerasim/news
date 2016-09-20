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
        public List<Article> GetPublished()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Article));
                criteria.AddOrder(Order.Desc("ID"));
                criteria.Add(Restrictions.IsNotNull("Published_At"));
                return criteria.List<Article>().ToList<Article>();
            }
        }

        public List<Article> GetByCategory(int Category, int offset = 0, int max = -1, int recent_days = 0)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Article));
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
                return criteria.List<Article>().ToList<Article>();
            }
        }
        public List<Article> GetBySearch(string S)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Article));
                criteria.AddOrder(Order.Desc("ID"));
                criteria.Add(Restrictions.IsNotNull("Published_At"));
                criteria.Add(Restrictions.Or(Restrictions.Like("Content", S, MatchMode.Anywhere), Restrictions.Like("Title", S, MatchMode.Anywhere)));
                return criteria.List<Article>().ToList<Article>();
            }
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
