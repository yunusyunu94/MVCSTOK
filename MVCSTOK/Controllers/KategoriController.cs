using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVCSTOK.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.Tbl_Kategoriler.ToList();
            var degerler = db.Tbl_Kategoriler.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(Tbl_Kategoriler P1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.Tbl_Kategoriler.Add(P1);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil (int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            db.Tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir (int id)
        {
            var ktgr = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult Guncelle(Tbl_Kategoriler P1)
        {
            var ktg = db.Tbl_Kategoriler.Find(P1.KategoriyID);
            ktg.KategoriyAd = P1.KategoriyAd;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}