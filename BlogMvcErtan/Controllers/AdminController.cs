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
    public class AdminController : Controller
    {
        Db_Blog db = new Db_Blog();
        // GET: Admin
        public ActionResult AdminCategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminCategoryAdd(Kategoriler kategori,HttpPostedFileBase gelenresim)
        {
            var kategoriIsmi = db.Kategorilers.Where(x => x.KategoriAdi == kategori.KategoriAdi).FirstOrDefault();
            if (kategoriIsmi!=null)
            {
                ModelState.AddModelError("","Kategori adı mevcut.");
            }
            else if (gelenresim!=null && kategoriIsmi==null)
            {
    
            WebImage image = new WebImage(gelenresim.InputStream);
            FileInfo info = new FileInfo(gelenresim.FileName);

            string newphoto = Guid.NewGuid().ToString() + info.Extension;

            image.Resize(150,150);
            image.Save("~/img/makaleresim/"+newphoto);
            kategori.Resim = "/img/makaleresim/" + newphoto;
                db.Kategorilers.Add(kategori);
                db.SaveChanges();
            }
            else if (gelenresim==null) {
                ModelState.AddModelError("", "Lütfen resim seçiniz.");
            }
           
           
            return View(kategori);
        }

        public ActionResult Listele(int sayfa=1)
        {
            return View(db.Kategorilers.OrderByDescending(x=>x.KategoriAdi).ToPagedList(sayfa,5));
        }

        public ActionResult Duzenle(int id)
        {
            var gelenVeri = db.Kategorilers.Where(x => x.KategoriId == id).FirstOrDefault();
            if (gelenVeri==null)
            {
                return HttpNotFound();
            }
           
            return View(gelenVeri);
        }
        [HttpPost]
        public ActionResult Duzenle(int id,HttpPostedFileBase resmimiz,Kategoriler kategoriler)
        {
            var gelenVeri = db.Kategorilers.Where(x => x.KategoriId == id).FirstOrDefault();
            if (gelenVeri==null)
            {
                return HttpNotFound();
            }
            if (resmimiz!=null)
            {
                if (System.IO.File.Exists(gelenVeri.Resim))
                {
                    System.IO.File.Delete(gelenVeri.Resim);
                }
                WebImage image = new WebImage(resmimiz.InputStream);
                FileInfo info = new FileInfo(resmimiz.FileName);

                string newphoto = Guid.NewGuid().ToString() + info.Extension;

                image.Resize(150, 150);
                image.Save("~/img/makaleresim/" + newphoto);
                gelenVeri.Resim = "/img/makaleresim/" + newphoto;
             
                
            }
            gelenVeri.KategoriAdi = kategoriler.KategoriAdi;
            gelenVeri.KategoriTanim= kategoriler.KategoriTanim;
            db.SaveChanges();
            return RedirectToAction("Listele","Admin");
        }

        public ActionResult Detay(int id) {
            var gelenVeri = db.Kategorilers.Where(x => x.KategoriId == id).FirstOrDefault();
            if (gelenVeri==null)
            {
                return HttpNotFound();
            }
            return View(gelenVeri);
        }

        public ActionResult Sil(int id)
        {
            var gelenVeri = db.Kategorilers.Where(x => x.KategoriId == id).FirstOrDefault();
            if (gelenVeri == null)
            {
                return HttpNotFound();
            }
            return View(gelenVeri);
        }

        [HttpPost,ActionName("Sil")]
        public ActionResult SilOk(int id)
        {
            var gelenVeri = db.Kategorilers.Where(x => x.KategoriId == id).FirstOrDefault();
            if (gelenVeri==null)
            {
                return HttpNotFound();
            }

            if (System.IO.File.Exists(gelenVeri.Resim))
            {
                System.IO.File.Delete(gelenVeri.Resim);
            }
            db.Kategorilers.Remove(gelenVeri);
            db.SaveChanges();
            return RedirectToAction("Listele","Admin");
        }
        public ActionResult _PartialAdminRows()
        {
            ViewBag.MakaleSayisi = db.Makalelers.Count();
            ViewBag.YorumSayisi = db.Yorumlars.Count();
            ViewBag.KategoriSayisi = db.Kategorilers.Count();
            ViewBag.UyeSayisi = db.Uyelers.Count();
            return View();
        }
       

    }
}