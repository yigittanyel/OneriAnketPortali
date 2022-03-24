using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ibrasOneriAnket.Controllers
{
    [Authorize(Roles ="A")]
    public class AdminOneriController : Controller
    {
        // GET: AdminOneri
        Context c=new Context();
        public ActionResult OneriGoruntule()
        {
            var deger = c.Oneris.Where(x => x.Durum == true).ToList();
            return View(deger);
        }
        //KİŞİ SİL
        public ActionResult OneriSil(int id)
        {
            var x = c.Oneris.FirstOrDefault(a => a.Id == id);
            x.Durum = false;
            c.SaveChanges();
            return RedirectToAction("OneriGoruntule");
        }
        //ID'YE GÖRE KİŞİ GETİRME
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
        [HttpPost]
        public ActionResult OneriGuncelle(Oneri p)
        {
            var x = c.Oneris.FirstOrDefault(a => a.Id == p.Id);
            x.Id = p.Id;
            x.Ad = p.Ad;
            x.Soyad = p.Soyad;
            x.BirimId = p.BirimId;
            x.OneriMesajı = p.OneriMesajı;
            x.MevcutDurum = p.MevcutDurum;
            //x.Degerlendirme = p.Degerlendirme;

            //BURAYA BİR DAHA BAK!!!
            x.Degerlendirme = true;

            c.SaveChanges();
            return RedirectToAction("OneriGoruntule");
        }
        public ActionResult PdfExcel()
        {
            var deger = c.Oneris.Where(x=>x.Durum==true).ToList();
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