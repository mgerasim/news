using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class CityController : Controller
    {
        //
        // GET: /City/

        public ActionResult Index(string notice = "", string error = "")
        {
            ViewBag.Notice = notice;
            ViewBag.Error = error;
            return View(NewsEntity.Models.City.GetAll());
        }

        //
        // GET: /City/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /City/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /City/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var model = new NewsEntity.Models.City();
                model.Name = collection.Get("Name");
                model.Url_Primpogoda_Weather_Now = collection.Get("Url_Primpogoda_Weather_Now");
                model.Url_Primpogoda_Weather_Today = collection.Get("Url_Primpogoda_Weather_Today");
                model.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err += ": " + ex.InnerException.Message;
                }
                return RedirectToAction("Index", "City", new { error = err, notice = "" });

            }
        }

        //
        // GET: /City/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /City/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var model = new NewsEntity.Models.City();
                model.Name = collection.Get("Name");
                model.Url_Primpogoda_Weather_Now = collection.Get("Url_Primpogoda_Weather_Now");
                model.Url_Primpogoda_Weather_Today = collection.Get("Url_Primpogoda_Weather_Today");
                model.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err += ": " + ex.InnerException.Message;
                }
                return RedirectToAction("Index", "City", new { error = err, notice = "" });

            }
        }

        //
        // GET: /City/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /City/Delete/5

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
