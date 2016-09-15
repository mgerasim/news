using News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            try
            {
                var theView = new Models.NewsMain();

                return View(theView);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err += ": " + ex.InnerException.Message;
                }
                var theView = new Models.NewsMain(false);
                theView.Error = err;
                return View(theView);
            }
        }
        
        public ActionResult Post(int id)
        {
            try
            {
                var model = NewsEntity.Models.Article.GetById(id);
                return View(model);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err += ": " + ex.InnerException.Message;
                }
                return RedirectToAction("Index", "Home", new { error = err, notice = "" });
            }
        }

        public JsonResult NewsAll()
        {
            List<NewsEntity.Models.Article> theList = (List<NewsEntity.Models.Article>)NewsEntity.Models.Article.GetAll();
            List<JsonNews> theResult = new List<JsonNews>();
            foreach (var item in theList)
            {
                JsonNews jsonObj = new JsonNews(item);
                theResult.Add(jsonObj);
            }
            return Json(theResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NewsPublished()
        {
            List<NewsEntity.Models.Article> theList = (List<NewsEntity.Models.Article>)NewsEntity.Models.Article.GetPublished();
            List<JsonNews> theResult = new List<JsonNews>();
            foreach (var item in theList)
            {
                JsonNews jsonObj = new JsonNews(item);
                theResult.Add(jsonObj);
            }
            return Json(theResult, JsonRequestBehavior.AllowGet);
        }
    }
}
