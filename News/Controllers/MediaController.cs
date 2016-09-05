using News.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class MediaController : Controller
    {
        //
        // GET: /Media/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Media/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Media/Create

        public ActionResult Create()
        {
            MediaNew model = new MediaNew(); 
            return View(model);
        }

        //
        // POST: /Media/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    string fileName = "";

                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(file.FileName);
                        if (!Directory.Exists(Server.MapPath("~/Content/Media/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/Media/"));
                        }
                        var path = Path.Combine(Server.MapPath("~/Content/Media/"), fileName);
                        file.SaveAs(path);
                    }

                    var model = new NewsEntity.Models.Media();
                    model.Name = fileName;
                    model.Title = collection.Get("Title");
                    model.Source = collection.Get("Title");
                    model.Save();
                }

                return RedirectToAction("Create");
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
        // GET: /Media/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Media/Edit/5

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
        // GET: /Media/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Media/Delete/5

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
