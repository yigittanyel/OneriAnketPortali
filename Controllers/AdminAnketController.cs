using ibrasOneriAnket.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Documents;

namespace ibrasOneriAnket.Controllers
{
    [Authorize(Roles ="A")]
    public class AdminAnketController : Controller
    {
        // GET: AdminAnket

        Context c=new Context();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AnketOlustur ao)
        {
            ao.Durum = true;
            c.AnketOlusturs.Add(ao);
            c.SaveChanges();
            return View();
        }
        public ActionResult AnketListele()
        {
            var deger = c.AnketCevaps.ToList();
            return View(deger);
        }
        //public ActionResult AnketSil(int id)
        //{
        //    var y = c.AnketCevaps.Find(id);
        //    y.CevapDurum = false;
        //    c.SaveChanges();
        //    return RedirectToAction("AnketListele");
        //}

        //ID'YE GÖRE KİŞİ GETİRME
        public ActionResult AnketGetir(int id)
        {
            var d = c.AnketCevaps.Find(id);
            return View("AnketGetir", d);
        }
        [HttpPost]
        public ActionResult AnketGuncelle(AnketCevap p)
        {
            var x = c.AnketCevaps.FirstOrDefault(a => a.Id == p.Id);
            x.Id = p.Id;
            x.Cevap = p.Cevap;
            x.AnketOlustur.AnketAdi = p.AnketOlustur.AnketAdi;
            x.CevapDurum = p.CevapDurum;
            x.AnketOlustur.Durum = p.AnketOlustur.Durum;
            c.SaveChanges();
            return RedirectToAction("AnketListele");
        }
    }
}