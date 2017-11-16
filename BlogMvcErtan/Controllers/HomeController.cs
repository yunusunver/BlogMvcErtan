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
    public class HomeController : Controller
    {
        Db_Blog db = new Db_Blog();
        // GET: Home
        public ActionResult Index(int sayfa=1)
        {
            var gelenVeri = db.Kategorilers.OrderBy(x => x.KategoriAdi).ToPagedList(sayfa, 4);
            return View(gelenVeri);
        }

        public ActionResult _PartialKategoriler()
        {
            return View(db.Kategorilers.ToList());
        }

        public ActionResult MakaleListele(int id,int sayfa=1) {
            return View(db.Makalelers.OrderByDescending(x=>x.MakaleTarih).Where(x=>x.KategoriID==id).ToPagedList(sayfa,5));
        }

        public ActionResult MakaleDetay(int id) {
            var gelendeger = db.Makalelers.Where(x => x.MakaleID == id).FirstOrDefault();
            return View(gelendeger);
        }

        public ActionResult SonMakaleler() {
            var gelenmakale = db.Makalelers.OrderByDescending(x => x.MakaleTarih).ToList();

            return View(gelenmakale);
        }

        public ActionResult OkunmaSayisiArtir(int id) {
            var gelenSayi = db.Makalelers.Where(x=>x.MakaleID==id).SingleOrDefault();
            gelenSayi.OkunmaSayisi += 1;
            db.SaveChanges();
            return View("SonMakaleler",db.Makalelers.OrderByDescending(x=>x.OkunmaSayisi));
        }

        public ActionResult Enpopulerler()
        {

            return View("SonMakaleler", db.Makalelers.OrderByDescending(x => x.OkunmaSayisi));
        }

        public JsonResult YorumYap(string yorum,int makaleID) {
            if (yorum!=null)
            {
                db.Yorumlars.Add(new Yorumlar() {
                    YorumIcerik = yorum,
                    UyeId=Convert.ToInt32(Session["uyeid"]),
                    MakaleId=makaleID,
                    YorumTarihi=DateTime.Now
                });
                db.SaveChanges();
            }
            return Json(false,JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var gelenVeri = db.Yorumlars.Where(x=>x.YorumId==id).SingleOrDefault();
            var makale = db.Makalelers.Where(x=>x.MakaleID==gelenVeri.MakaleId).SingleOrDefault();
            if (gelenVeri.UyeId==Convert.ToInt32(uyeid))
            {
                db.Yorumlars.Remove(gelenVeri);
                db.SaveChanges();
            }
            return RedirectToAction("MakaleDetay","Home",new { id=makale.MakaleID});
        }

      public ActionResult _PartialFooter()
        {
            return View();
        }
        public ActionResult _PartialSonYorumlar()
        {
            var sonYorumlar = db.Yorumlars.OrderByDescending(x => x.YorumTarihi).Take(3);
            if (sonYorumlar == null)
            {
                return HttpNotFound();
            }
            return View(sonYorumlar);
        }
       
      public ActionResult Arama(string txtSearch)
        {
            var gelenVeri = db.Makalelers.Where(x => x.MakaleBasligi.StartsWith(txtSearch)).ToList();
            return View(gelenVeri);
        }
      

      
    }
}