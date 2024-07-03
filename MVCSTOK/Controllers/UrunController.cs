using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.Tbl_Urunler.ToList().ToPagedList(sayfa, 100);

            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriyAd,
                                                 Value = i.KategoriyID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Tbl_Urunler P1)
        {
            var ktg = db.Tbl_Kategoriler.Where(m => m.KategoriyID == P1.Tbl_Kategoriler.KategoriyID).FirstOrDefault();
            P1.Tbl_Kategoriler = ktg;
            db.Tbl_Urunler.Add(P1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urn = db.Tbl_Urunler.Find(id);

            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriyAd,
                                                 Value = i.KategoriyID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urn);
        }
        public ActionResult Guncelle(Tbl_Urunler P1)
        {
            var urn = db.Tbl_Urunler.Find(P1.UrunID);
            urn.UrunAd = P1.UrunAd;
            urn.Marka = P1.Marka;
            urn.Stok = P1.Stok;
            urn.Fiyat = P1.Fiyat;
            //urn.UrunKatgori = P1.UrunKatgori;
            var ktg = db.Tbl_Kategoriler.Where(m => m.KategoriyID == P1.Tbl_Kategoriler.KategoriyID).FirstOrDefault();
            urn.UrunKatgori = ktg.KategoriyID;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}