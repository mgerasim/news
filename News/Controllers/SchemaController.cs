using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class SchemaController : Controller
    {
        //
        // GET: /Schema/

        public ActionResult Update()
        {
            try
            {
                NewsEntity.Common.NHibernateHelper.UpdateSchema();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            
            return RedirectToAction("Index", "Home");
        }

    }
}
