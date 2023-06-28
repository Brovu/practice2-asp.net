using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practice2.Models;

namespace Practice2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string filter, int? id)
        {
            ManageNewsEntities db = new ManageNewsEntities();
            List<News> listNews = db.News.Where(m => m.Title.ToLower().Contains(filter.ToLower()) == true || m.Date.ToString().Contains(filter) & (m.idType == id | id == null)).ToList();

            /*List<News> listNews = (from m in db.News
                                   join typeNews in db.TypeNews on m.idType equals typeNews.ID
                                   where (m.Title.ToLower().Contains(filter.ToLower()) == true || m.Date.ToString().Contains(filter) & (m.idType == id | id == null))
                                   select m
                                 ).ToList();*/
            if (!string.IsNullOrEmpty(filter) && listNews.Count == 0)
            {
                ViewBag.Message = "Không tìm thấy kết quả!";
            }

            if (listNews.Count == 0)
            {
                return View(db.News.ToList());
            }

            return View(listNews);
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult AddNews()
        {
            return View(new News());
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult AddNews(News model)
        {
            ManageNewsEntities db = new ManageNewsEntities();

            db.News.Add(model);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditNews(string id)
        {
            ManageNewsEntities db = new ManageNewsEntities();
            var model = db.News.Find(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult EditNews(News model)
        {
            ManageNewsEntities db = new ManageNewsEntities();

            var updateNews = db.News.Find(model.ID);

            updateNews.isDisplay = model.isDisplay;
            updateNews.Title = model.Title;
            updateNews.ID = model.ID;
            updateNews.ImagePath = model.ImagePath;
            updateNews.Date = model.Date;
            updateNews.idType = model.idType;
            updateNews.Description = model.Description;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteNews(string id)
        {
            ManageNewsEntities db = new ManageNewsEntities();
            var model = db.News.Find(id);
            db.News.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}