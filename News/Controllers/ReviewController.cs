using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class ReviewController : Controller
    {
        //
        // GET: /Review/

        public ActionResult Index()
        {
            try {
                var model = new News.Models.NewsMain(false);
                model.LoadReview();
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


        //
        // GET: /Review/Geospace/5

        public ActionResult Geospace()
        {
            var model = NewsEntity.Models.GeospaceReview.GetByLast();
            return View(model);
        }

        //
        // GET: /Review/Operative/5

        public ActionResult Operative(int id)
        {
            var model = NewsEntity.Models.Article.GetById(id);
            return View(model);
        }

        //
        // GET: /Review/Hydrology/5

        public ActionResult Hydrology(int id)
        {
            var model = NewsEntity.Models.Article.GetById(id);
            return View(model);
        }
        //
        // GET: /Review/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Review/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Review/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Review/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Review/Edit/5

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
        // GET: /Review/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Review/Delete/5

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
