using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsEntity.Common;
using NHibernate;
using NHibernate.Criterion;
using NewsEntity.Models;

namespace NewsEntity.Repositories
{
    public class CodeUmagfRepository : IRepository<NewsEntity.Models.CodeUmagf>
    {
        #region IRepository<Measurement> Members

        List<CodeUmagf> IRepository<CodeUmagf>.GetAll()
        {
            throw new NotImplementedException();
        }

        void IRepository<NewsEntity.Models.CodeUmagf>.Save(NewsEntity.Models.CodeUmagf entity)
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

        void IRepository<NewsEntity.Models.CodeUmagf>.Update(NewsEntity.Models.CodeUmagf entity)
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

        void IRepository<NewsEntity.Models.CodeUmagf>.Delete(NewsEntity.Models.CodeUmagf entity)
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

        NewsEntity.Models.CodeUmagf IRepository<NewsEntity.Models.CodeUmagf>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<NewsEntity.Models.CodeUmagf>().Add(Restrictions.Eq("ID", id)).UniqueResult<NewsEntity.Models.CodeUmagf>();
        }

        public NewsEntity.Models.CodeUmagf GetByCode(int code)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<NewsEntity.Models.CodeUmagf>().Add(Restrictions.Eq("Code", code)).UniqueResult<NewsEntity.Models.CodeUmagf>();
        }

        //IList<NewsEntity.Models.CodeUmagf> IRepository<NewsEntity.Models.CodeUmagf>.GetAll()
        //{
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        ICriteria criteria = session.CreateCriteria(typeof(NewsEntity.Models.CodeUmagf));
        //        criteria.AddOrder(Order.Desc("ID"));
        //        criteria.AddOrder(Order.Asc("MI"));
        //        return criteria.List<NewsEntity.Models.CodeUmagf>();
        //    }
        //}

        public IList<NewsEntity.Models.CodeUmagf> GetByPeriod(int station, int startYYYY, int startMM, int startDD, int endYYYY, int endMM, int endDD)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(NewsEntity.Models.CodeUmagf));
                criteria.AddOrder(Order.Desc("ID"));
                criteria.Add(Restrictions.Eq("Station_ID", station));

                System.DateTime startDate = new DateTime(startYYYY, startMM, startDD);
                System.DateTime endDate = new DateTime(endYYYY, endMM, endDD);

                var strYYYY = Projections.Cast(NHibernateUtil.String, Projections.Property("YYYY"));
                var strMM = Projections.Cast(NHibernateUtil.String, Projections.Property("MM"));
                var strDD = Projections.Cast(NHibernateUtil.String, Projections.Property("DD"));

                var sl = Projections.Cast(NHibernateUtil.String, Projections.Constant("/"));

                var projDate = Projections.SqlFunction("concat", NHibernateUtil.String, strDD, sl,
                    strMM, sl,
                    strYYYY);

                projDate = Projections.Cast(NHibernateUtil.DateTime, projDate);

                criteria.Add(Restrictions.Between(projDate, startDate, endDate));

                criteria.AddOrder(Order.Asc("MI"));

                return criteria.List<NewsEntity.Models.CodeUmagf>();
            }
        }

        public NewsEntity.Models.CodeUmagf GetByDateUTC(int station, int YYYY, int MM, int DD, int HH, int MI)
        {
            using (ISession session = NHibernateHelper.OpenSession())

                return session.CreateCriteria<NewsEntity.Models.CodeUmagf>()
                    .Add(Restrictions.Eq("Station_ID", station))
                    .Add(Restrictions.Eq("YYYY", YYYY))
                    .Add(Restrictions.Eq("MM", MM))
                    .Add(Restrictions.Eq("DD", DD))
                    .Add(Restrictions.Eq("HH", HH))
                    .Add(Restrictions.Eq("MI", MI)).UniqueResult<NewsEntity.Models.CodeUmagf>();

        }

        public NewsEntity.Models.CodeUmagf GetByDate(int station, int YYYY, int MM, int DD)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var list = session.CreateCriteria<NewsEntity.Models.CodeUmagf>()
                    .Add(Restrictions.Eq("Station_ID", station))
                    .Add(Restrictions.Eq("YYYY", YYYY))
                    .Add(Restrictions.Eq("MM", MM))
                    .Add(Restrictions.Eq("DD", DD))
                    .AddOrder(Order.Asc("MI"))
                    .List<NewsEntity.Models.CodeUmagf>();

                if (list.Count != 0)
                {
                    return list[0];
                }

                return null;

            }

        }

        #endregion


    }

}
