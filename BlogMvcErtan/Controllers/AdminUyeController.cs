using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMvcErtan.Models;
using PagedList;
using PagedList.Mvc;

namespace BlogMvcErtan.Controllers
{
    public class AdminUyeController : Controller
    {
        Db_Blog db = new Db_Blog();
        // GET: AdminUye
        public ActionResult UyeListele(int sayfa=1)
        {
            return View(db.Uyelers.OrderByDescending(x=>x.Tarih).ToPagedList(sayfa,10));
        }
        public ActionResult UyeDetay(int id) {
            var gelenVeri = db.Uyelers.Where(x => x.UyeId == id).FirstOrDefault();
            if (gelenVeri==null)
            {
                return HttpNotFound();
            }
            return View(gelenVeri);
        }

        public ActionResult UyeSil(int id)
        {
            var gelenVeri = db.Uyelers.Where(x => x.UyeId == id).FirstOrDefault();
            if (gelenVeri==null)
            {
                return HttpNotFound();
            }
            return View(gelenVeri);
        }
        [HttpPost,ActionName("UyeSil")]
        public ActionResult SilOk(int id) {
            var gelenVeri = db.Uyelers.Where(x => x.UyeId == id).FirstOrDefault();
            
           

            if (gelenVeri == null)
            {
                return HttpNotFound();
            }
            foreach (var item in db.Yorumlars.Where(x => x.UyeId == id).ToList())
            {
                db.Yorumlars.Remove(item);
            }
            

            
            db.SaveChanges();

            if (System.IO.File.Exists(Server.MapPath(gelenVeri.UyeFoto)))
            {
                System.IO.File.Delete(Server.MapPath(gelenVeri.UyeFoto));
            }
            db.Uyelers.Remove(gelenVeri);
            db.SaveChanges();
            return RedirectToAction("UyeListele");
        }
    }
}