using News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Views
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public ActionResult Index(string notice = "", string error = "")
        {
            ViewBag.Notice = notice;
            ViewBag.Error = error;
            return View(NewsEntity.Models.Article.GetAll());
        }

        //
        // GET: /Article/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Article/Create

        public ActionResult Create()
        {
            ArticleNew model = new ArticleNew();
            return View(model);
        }

        //
        // POST: /Article/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var model = new NewsEntity.Models.Article();
                model.Content = collection.Get("Content");
                model.Title = collection.Get("Title");
                model.Source = collection.Get("Source");
                model.Save();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err += ": " + ex.InnerException.Message;
                }
                return RedirectToAction("Index", "Article", new { error = err, notice = "" });

            }
        }

        //
        // GET: /Article/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Article/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Article/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
