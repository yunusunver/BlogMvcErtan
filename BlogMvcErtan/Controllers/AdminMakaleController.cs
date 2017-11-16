using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMvcErtan.Models;
using System.Web.Helpers;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace BlogMvcErtan.Controllers
{
    public class AdminMakaleController : Controller
    {
        Db_Blog db = new Db_Blog();
        // GET: AdminMakale
        public ActionResult MakaleListele(int sayfa=1)
        {
            return View(db.Makalelers.OrderByDescending(x=>x.MakaleTarih).ToPagedList(sayfa,5));
        }

        public ActionResult MakaleEkle() {
            ViewBag.Kategoriler = new SelectList(db.Kategorilers.ToList(),"KategoriId","KategoriAdi");
            return View();
        }
        [HttpPost]
        public ActionResult MakaleEkle(Makaleler makale,HttpPostedFileBase makalefotosu)
        {
            if (ModelState.IsValid)
            {
                if (makalefotosu!=null) {
                    WebImage image = new WebImage(makalefotosu.InputStream);
                    FileInfo info = new FileInfo(makalefotosu.FileName);

                    string newphoto = Guid.NewGuid().ToString() + info.Extension;

                    image.Resize(150,150);
                    image.Save("~/img/makaleresim/AdminMakaleFoto/"+newphoto);
                    makale.MakaleFoto = "/img/makaleresim/AdminMakaleFoto/" + newphoto;
                   
                }
                makale.OkunmaSayisi = 0;
                makale.MakaleTarih = DateTime.Now;
                db.Makalelers.Add(makale);
                db.SaveChanges();
                return RedirectToAction("MakaleListele", "AdminMakale");
            }
            ViewBag.Kategoriler = new SelectList(db.Kategorilers.ToList(), "KategoriId", "KategoriAdi");
            return View(makale);
        }

        public ActionResult MakaleDuzenle(int id)
        {
            ViewBag.Kategoriler = new SelectList(db.Kategorilers.ToList(), "KategoriId", "KategoriAdi");
            var gelenMakale = db.Makalelers.Where(x=>x.MakaleID==id).FirstOrDefault();
            if (gelenMakale==null) {
                return HttpNotFound();
            }
            return View(gelenMakale);
        }
        [HttpPost]
        public ActionResult MakaleDuzenle(int id,Makaleler makale,HttpPostedFileBase makalefotosu)
        {
            
            var gelenMakale = db.Makalelers.Where(x => x.MakaleID == id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (gelenMakale == null)
                {
                    return HttpNotFound();
                }
                if (makalefotosu!=null)
                {
                    if (System.IO.File.Exists(makalefotosu.FileName)) {
                        System.IO.File.Delete(makalefotosu.FileName);
                    }
                    WebImage image = new WebImage(makalefotosu.InputStream);
                    FileInfo info = new FileInfo(makalefotosu.FileName);

                    string newphoto = Guid.NewGuid().ToString() + info.Extension;
                    image.Resize(150,150);
                    image.Save("~/img/makaleresim/AdminMakaleFoto/"+newphoto);
                    gelenMakale.MakaleFoto = "/img/makaleresim/AdminMakaleFoto/" + newphoto;

                }
                    gelenMakale.MakaleTarih = DateTime.Now.Date;
                    gelenMakale.KategoriID = makale.KategoriID;
                    gelenMakale.MakaleBasligi = makale.MakaleBasligi;
                    gelenMakale.Makaleİcerik = makale.Makaleİcerik;
                db.SaveChanges();
                return RedirectToAction("MakaleListele","AdminMakale");
            }
            ViewBag.Kategoriler = new SelectList(db.Kategorilers.ToList(), "KategoriId", "KategoriAdi");
            return View(gelenMakale);
        }

        public ActionResult MakaleDetay(int id) {

            var gelenMakale = db.Makalelers.Where(x=>x.MakaleID==id).FirstOrDefault();
            if (gelenMakale==null)
            {
                return HttpNotFound();
            }
            return View(gelenMakale);
        }

        public ActionResult MakaleSil(int id) {

            var gelenMakale = db.Makalelers.Where(x => x.MakaleID == id).FirstOrDefault();
            if (gelenMakale==null)
            {
                return HttpNotFound();
            }
            return View(gelenMakale);
        }
        [HttpPost,ActionName("MakaleSil")]
        public ActionResult MakaleSilOk(int id)
        {

            var gelenMakale = db.Makalelers.Where(x => x.MakaleID == id).FirstOrDefault();
            if (gelenMakale == null)
            {
                return HttpNotFound();
            }

            if (System.IO.File.Exists(gelenMakale.MakaleFoto))
            {
                System.IO.File.Delete(gelenMakale.MakaleFoto);
            }
            foreach (var item in gelenMakale.Yorumlars.ToList())
            {
                db.Yorumlars.Remove(item);
            }
            db.Makalelers.Remove(gelenMakale);
            db.SaveChanges();
            return RedirectToAction("MakaleListele","AdminMakale");
        }

    }
}