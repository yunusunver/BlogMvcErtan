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
    public class AdminYorumController : Controller
    {
        Db_Blog db = new Db_Blog();
        // GET: AdminYorum
        public ActionResult YorumListele(int sayfa=1)
        {
            return View(db.Yorumlars.OrderByDescending(x=>x.YorumTarihi).ToPagedList(sayfa,10));
        }

        public ActionResult YorumSil(int id)
        {
            var gelenVeri = db.Yorumlars.Where(x => x.YorumId == id).FirstOrDefault();
            if (gelenVeri == null)
            {
                return HttpNotFound();
            } 
            return View();
        }
        [HttpPost,ActionName("YorumSil")]
        public ActionResult YorumSilOk(int id) {
            var gelenVeri = db.Yorumlars.Where(x => x.YorumId == id).FirstOrDefault();
            if (gelenVeri==null)
            {
                return HttpNotFound();
            }
            db.Yorumlars.Remove(gelenVeri);
            db.SaveChanges();
            return RedirectToAction("YorumListele");
        }
    }
}