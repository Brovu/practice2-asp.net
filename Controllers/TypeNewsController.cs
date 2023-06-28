using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practice2.Models;


namespace Practice2.Controllers
{
    public class TypeNewsController : Controller
    {
        public ActionResult TypeList()
        {
            return View(new ManageNewsEntities().TypeNews.ToList());
        }

        public ActionResult AddType()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddType(TypeNew model)
        {
            ManageNewsEntities db = new ManageNewsEntities();

            if(string.IsNullOrEmpty(model.typeName) == true)
            {
                ModelState.AddModelError("", "Chưa nhập");
                return View(model);
            }

            try
            {
                db.TypeNews.Add(model);
                db.SaveChanges();
                return RedirectToAction("TypeList");
            }catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

        }

        public ActionResult EditType(int id)
        {
            ManageNewsEntities db = new ManageNewsEntities();
            var model = db.TypeNews.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditType(TypeNew model) {
            

            if (string.IsNullOrEmpty(model.typeName) == true)
            {
                ModelState.AddModelError("", "Chưa nhập");
                return View(model);
            }

            ManageNewsEntities db = new ManageNewsEntities();
            var updateType = db.TypeNews.Find(model.ID);
            try
            {
                updateType.typeName = model.typeName;
                db.SaveChanges();
                return RedirectToAction("TypeList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        
        public ActionResult DeleteType(int id)
        {
            ManageNewsEntities db = new ManageNewsEntities();
            var model = db.TypeNews.Find(id);

            db.TypeNews.Remove(model);
            db.SaveChanges();

            return RedirectToAction("TypeList"); 
        }
    }
}