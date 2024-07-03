using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri

        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.Tbl_Musteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p));
            }
            return View(degerler.ToList());

            //var degerler = db.Tbl_Musteriler.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(Tbl_Musteriler P1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Tbl_Musteriler.Add(P1);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil (int id)
        {
            var musteri = db.Tbl_Musteriler.Find(id);
            db.Tbl_Musteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.Tbl_Musteriler.Find(id);
            return View("MusteriGetir", musteri);
        }
        public ActionResult Guncelle (Tbl_Musteriler P1)
        {
            var musteri = db.Tbl_Musteriler.Find(P1.MusteriID);
            musteri.MusteriAd = P1.MusteriAd;
            musteri.MusteriSoyad = P1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
    }
}