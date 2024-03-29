﻿using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ibrasOneriAnket.Controllers
{
    
    public class AdminOneriController : Controller
    {
        // GET: AdminOneri
        Context c=new Context();
        [Authorize(Roles = "M,A")]
        public ActionResult OneriGoruntule()
        {
            var deger = c.Oneris.Where(x => x.Durum == true).ToList();
            return View(deger);
        }
        //KİŞİ SİL
        [Authorize(Roles = "M,A")]
        public ActionResult OneriSil(int id)
        {
            var x = c.Oneris.FirstOrDefault(a => a.Id == id);
            x.Durum = false;
            c.SaveChanges();
            return RedirectToAction("OneriGoruntule");
        }
        //ID'YE GÖRE KİŞİ GETİRME
        [Authorize(Roles = "M,A")]
        public ActionResult OneriGetir(int id)
        {
            List<SelectListItem> dep = (from x in c.Birims.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad,
                                            Value = x.Id.ToString(),
                                            Selected = false
                                        }).ToList();
            ViewBag.dp = dep;

            var d = c.Oneris.Find(id);
            return View("OneriGetir", d);
        }
        [Authorize(Roles = "M,A")]
        [HttpPost]
        public ActionResult OneriGuncelle(Oneri p)
        {
            var user = c.Kullanicis.FirstOrDefault(q => q.KullaniciAdi == User.Identity.Name);
            var x = c.Oneris.FirstOrDefault(a => a.Id == p.Id);
            x.Id = p.Id;
            x.KullaniciId = user.Id;
            x.OneriMesajı = p.OneriMesajı;
            x.MevcutDurum = p.MevcutDurum;
            //x.Degerlendirme = p.Degerlendirme;

            //BURAYA BİR DAHA BAK!!!
            x.Degerlendirme = true;
            c.SaveChanges();
            return RedirectToAction("OneriGoruntule");
        }
        [Authorize(Roles = "M,A")] //manager
        public ActionResult PdfExcel()
        {
            var deger = c.Oneris.Where(x=>x.Durum==true).ToList();
            return View(deger);
        }
        [Authorize(Roles = "M")] //manager
        public ActionResult YoneticiDegerlendirme(int id)
        {
            var deger = c.Oneris.Where(x => x.Kullanici.BirimId== id).ToList();
            return View(deger);
        }








        //public ActionResult OneriDegerlendir(int id)
        //{
        //    var x = c.Oneris.FirstOrDefault(a => a.Id == id);
        //    x.Degerlendirme = true;
        //    c.SaveChanges();
        //    return RedirectToAction("OneriGoruntule");
        //}
    }
}