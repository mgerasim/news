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

        public ActionResult Publish(int id)
        {
            try
            {
                var model = NewsEntity.Models.Article.GetById(id);
                model.Published_At = DateTime.Now;
                model.Save();
                return RedirectToAction("Index", "Article");
            }
            catch (Exception ex)
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
        // GET: /Article/Show/5

        public ActionResult Show(int id)
        {
            var model = NewsEntity.Models.Article.GetById(id);
            return View(model);
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
                model.Anons = collection.Get("Anons");
                model.Source_Url = "http://meteo-dv.ru";
                model.Source_Site = "http://meteo-dv.ru";
                model.Source_Published_At = DateTime.Now;
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
            var model = NewsEntity.Models.Article.GetById(id);
            return View(model);
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var model = NewsEntity.Models.Article.GetById(id);
                model.Content = collection.Get("Content");
                model.Title = collection.Get("Title");
                model.Anons = collection.Get("Anons");             
                model.Update();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
        // GET: /Article/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                var model = NewsEntity.Models.Article.GetById(id);
                if (model != null)
                {
                    model.Delete();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
