using News.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace News.Views
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/
        [ValidateInput(false)]
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
                model.Update();
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
        public ActionResult PublishToKhabmeteo(int id)
        {
            try
            {
                var model = NewsEntity.Models.Article.GetById(id);

                var request = (HttpWebRequest)WebRequest.Create("http://khabmeteo.ru/cgi-bin/auth/addnews.cgi?news=transfer");

                var postData = "heading=" + model.Title;
                postData += "&addnews=" + model.Anons;

                string str = postData;
                Encoding srcEncodingFormat = Encoding.UTF8;
                Encoding dstEncodingFormat = Encoding.GetEncoding("windows-1251");
                byte[] originalByteString = srcEncodingFormat.GetBytes(str);
                byte[] convertedByteString = Encoding.Convert(srcEncodingFormat,
                dstEncodingFormat, originalByteString);
                string finalString = dstEncodingFormat.GetString(convertedByteString);
                                
                var data = convertedByteString;
                
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                
                // authentication
                var cache = new CredentialCache();
                Uri uri = new Uri("http://khabmeteo.ru/cgi-bin/auth");
                cache.Add(uri, "Basic", new NetworkCredential("khabmeteo", "mqnihq8j"));
                request.Credentials = cache;


                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                string notice = "Публикация " + model.Title + " успешна размещена на khabmeteo.ru";
                return RedirectToAction("Index", "Article", new { error = "", notice = notice });
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
                model.Published_At = null;
                model.Source_Url = Guid.NewGuid().ToString();
                model.Category = Convert.ToInt32(collection.Get("Category"));
                model.Save();
                string notice = "Публикация " + model.Title + " успешна создана";
                return RedirectToAction("Index", new { error = "", notice = notice });
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
                model.Category = Convert.ToInt32(collection.Get("Category"));
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
