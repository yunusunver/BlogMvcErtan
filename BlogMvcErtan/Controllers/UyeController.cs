using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMvcErtan.Models;
using System.Web.Helpers;
using System.IO;

namespace BlogMvcErtan.Controllers
{
    public class UyeController : Controller
    {
        Db_Blog db = new Db_Blog();
        // GET: Uye
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(Uyeler model,HttpPostedFileBase uyefoto,string Password)
        {
            var md5pass = Password;
            var kullaniciAdiKontrol = db.Uyelers.Where(x => x.KullanıcıAdı == model.KullanıcıAdı).FirstOrDefault();

            if (kullaniciAdiKontrol != null)
            {
                ModelState.AddModelError("", "Kullanıcı adı mevcut.");
            }
            if (ModelState.IsValid)
            {
                if (uyefoto!=null)
                {
                    WebImage image = new WebImage(uyefoto.InputStream);
                    FileInfo info = new FileInfo(uyefoto.FileName);

                    string newphoto = Guid.NewGuid().ToString() + info.Extension;
                    image.Resize(250,250);
                    image.Save("~/img/uyefoto/"+newphoto);
                    model.UyeFoto = "/img/uyefoto/" + newphoto;
                }
                model.Password = Crypto.Hash(md5pass,"MD5");
                model.YetkiId= 1002;
                model.Tarih = DateTime.Now;
                db.Uyelers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Uyeler model,string Password)
        {
            var md5pass = Crypto.Hash(Password,"MD5");
            var gelenUye = db.Uyelers.Where(x => x.KullanıcıAdı==model.KullanıcıAdı).FirstOrDefault();
           
           if (gelenUye == null)
            {
                ModelState.AddModelError("", "Giriş Başarısız!");
            }
            else if (gelenUye.KullanıcıAdı==model.KullanıcıAdı && gelenUye.Password == md5pass)
            {
                Session["kullanıcıAdı"] = gelenUye.KullanıcıAdı;
                Session["yetkiid"] = gelenUye.YetkiId;
                Session["uyeid"] = gelenUye.UyeId;
                Session["uyeresim"] = gelenUye.UyeFoto;
                return RedirectToAction("Index","Home");
            }
            
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session["yetkiid"] = null;

            return RedirectToAction("Index","Home");
        }

        public ActionResult UyeListele()
        {
            return View(db.Uyelers.ToList());
        }
        public ActionResult UyeDetay(int id)
        {
            var gelenVeri = db.Uyelers.Where(x => x.UyeId == id).FirstOrDefault();
            return View(gelenVeri);
        }
        [HttpPost]
        public ActionResult UyeDetay(int id,HttpPostedFileBase resmimiz,Uyeler uyeler,string Password)
        {
            var md5pass = Password;
            var gelenVeri = db.Uyelers.Where(x => x.UyeId == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (gelenVeri == null)
                {
                    return HttpNotFound();
                }
                if (resmimiz != null)
                {
                    if (System.IO.File.Exists(gelenVeri.UyeFoto))
                    {
                        System.IO.File.Delete(resmimiz.FileName);
                    }
                    WebImage image = new WebImage(resmimiz.InputStream);
                    FileInfo info = new FileInfo(resmimiz.FileName);
                    string newphoto = Guid.NewGuid().ToString() + info.Extension;
                    image.Resize(150, 150);
                    image.Save("~/img/uyefoto/" + newphoto);
                    gelenVeri.UyeFoto = "/img/uyefoto/" + newphoto;

                }
                gelenVeri.UyeAdSoyad = uyeler.UyeAdSoyad;
                gelenVeri.Password = Crypto.Hash(md5pass,"MD5");
                db.SaveChanges();
                return RedirectToAction("UyeDetay","Uye");
            }
            
            
            return View(gelenVeri);
        }

       
    }
}