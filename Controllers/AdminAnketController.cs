using ibrasOneriAnket.Models;
using ibrasOneriAnket.Sinif;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            var deger = c.AnketOlusturs.Where(x => x.AnketBaslangic < DateTime.Now
                   && x.AnketBitis > DateTime.Now && x.Durum).Max(m => m.AnketSira);
            if (!deger.HasValue)
            {
                deger = 0;
            }
            ViewBag.siraNo = deger + 1;
            return View();
        }
        [HttpPost]
        public ActionResult Index(AnketOlustur ao)
        {
            var deger =  c.AnketOlusturs.Where(x => x.AnketBaslangic < DateTime.Now
                         && x.AnketBitis > DateTime.Now && x.Durum)
                         .Max(m => m.AnketSira);

            var ab = c.AnketCevaps.FirstOrDefault(x => x.AnketOlusturId == ao.Id);
            ao.Durum = true;
            ao.AnketSira= deger + 1; 
            ao.State =(ibrasOneriAnket.Sinif.State)1;       
            ao.AnketCevaps.Select(a => a.CevapDurum == true);
            c.AnketOlusturs.Add(ao);
            c.SaveChanges();
            return RedirectToAction("AnketListele","AdminAnket");
        }
        public ActionResult YeniCevapEkle(int id)
        {
            var soru = c.AnketOlusturs.FirstOrDefault(q => q.Id == id);
            soru.Durum = true;
            ViewBag.Soru = soru.AnketAdi;
            var d = c.AnketOlusturs.FirstOrDefault(x => x.Id == id);
            return View("YeniCevapEkle",d);
        }

        [HttpPost]
        public ActionResult YeniCevapEkle(AnketOlustur ac)
        {
            var x=c.AnketCevaps.Where(a => a.Id == ac.Id).FirstOrDefault();
            ac.Durum = true;
            var sadeceDoluOlanlar = ac.AnketCevaps.Where(q => !string.IsNullOrEmpty(q.Cevap)).ToList();
            var anket = c.AnketOlusturs.FirstOrDefault(q => q.Id == ac.Id);
            anket.AnketCevaps.AddRange(sadeceDoluOlanlar);
            c.SaveChanges();
            return RedirectToAction("AnketCevapGoruntule", new { id = ac.Id });
        }

        public ActionResult AnketListele()
        {
            var deger = c.AnketOlusturs.Where(x=>x.Durum==true).ToList();
            return View(deger);
        }

        [HttpPost]
        public ActionResult AnketGuncelle(AnketOlustur p)
        {
            var x = c.AnketOlusturs.FirstOrDefault(a => a.Id == p.Id);
            x.Id = p.Id;
            x.AnketAdi = p.AnketAdi;
            //güncelleyince false geliyordu.
            x.Durum = true;
            //x.Durum = p.Durum;
            x.AnketBaslangic = p.AnketBaslangic;
            x.AnketBitis = p.AnketBitis;
            x.AnketSira = p.AnketSira;
            x.AnketYayinDurumu = p.AnketYayinDurumu;
            x.State = p.State;
            c.SaveChanges();
            return RedirectToAction("AnketListele");
        }

        public ActionResult AnketGetirr(int id)
        {
            var d = c.AnketOlusturs.Find(id);
            d.Durum = true;
            return View("AnketGetirr", d);
        }

        [HttpPost]
        public ActionResult AnketCevapGuncelle(AnketCevap ac)
        {
            var x=c.AnketCevaps.FirstOrDefault(a=>a.Id == ac.Id);
            x.Id = ac.Id;
            x.Cevap = ac.Cevap;
            x.CevapDurum = true;
            c.SaveChanges();
            return RedirectToAction("AnketCevapGoruntule/ " + x.AnketOlusturId);
        }

        public ActionResult AnketCevapGetir(int id)
        {
            var d = c.AnketCevaps.FirstOrDefault(a => a.Id == id);
            return View("AnketCevapGetir", d);
        }

        [HttpGet]
        public ActionResult AnketDurum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AnketDurum(EnumClass ec)
        {
            //if (ec.StateId == 1 )
            //{
            //    return "taslak";
            //}

            ViewBag.a = ec.StateId;
            return View();
        }

        public PartialViewResult AnketDurumu(EnumClass ec)
        {
            ViewBag.a = ec.StateId;
            return PartialView();
        }

        public ActionResult AnketCevapGoruntule(int id)  // CEVAPLAR LİSTELENİYOR
        {
            Class1 c1 = new Class1();
            c1.DegerAC = c.AnketCevaps.Where(a => a.AnketOlusturId == id).ToList();
            c1.DegerAO = c.AnketOlusturs.Where(a=>a.Id==id).ToList();


            ViewBag.state =

            ViewBag.State = c.AnketOlusturs.Where(x=>(Convert.ToInt32(x.State)==1) || Convert.ToInt32(x.State)==2).Select(a => a.State);
            ViewBag.State = c.AnketOlusturs.Select(a => a.State).FirstOrDefault();
            
            //BURADA HATA. silerken soru null geliyor.
            var soru = c.AnketOlusturs.FirstOrDefault(q => q.Id == id);
            ViewBag.state = soru.State;
            ViewBag.Soru = soru.AnketAdi;

            return View(c1);
        }

        public ActionResult AnketCevapSil(int id)
        {
            var x = c.AnketCevaps.FirstOrDefault(a => a.Id == id);
            c.AnketCevaps.Remove(x);
            c.SaveChanges();
            return RedirectToAction("AnketCevapGoruntule/ "+ x.AnketOlusturId);
        }

        public ActionResult GenelSonucc()
        {
            ArrayList xvalue=new ArrayList();
            ArrayList yvalue=new ArrayList();
            var veri = c.KullaniciAnketCevaps.ToList();
            veri.ToList().ForEach(a =>xvalue.Add(a.SoruId));   
            //veri.ToList().ForEach(a =>yvalue.Add("SELECT COUNT(CevapId)FROM KullaniciAnketCevaps WHERE CevapId = a.CevapId"));
            veri.ToList().ForEach(a =>yvalue.Add((from row in c.KullaniciAnketCevaps
                                                  where row.CevapId == a.CevapId
                                                  select row).Count()));
            var dgr= c.KullaniciAnketCevaps.Where(p => p.Id>0).Count().ToString();

            var grafik = new Chart(width: 750, height: 750).AddTitle("Sonuçlar").AddSeries
                (chartType: "Column", name: "SoruId", xValue:xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult AnketeAitSonuclar(int id)
        {
            var d = c.AnketOlusturs.FirstOrDefault(a => a.Id == id);
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var veri = c.KullaniciAnketCevaps.Where(x=>x.Id==id).ToList();
            veri.ToList().ForEach(a => xvalue.Add(a.SoruId));
            //veri.ToList().ForEach(a =>yvalue.Add("SELECT COUNT(CevapId)FROM KullaniciAnketCevaps WHERE CevapId = a.CevapId"));
            veri.ToList().ForEach(a => yvalue.Add((from row in c.KullaniciAnketCevaps
                                                   where row.CevapId == a.CevapId
                                                   select row).Count()));
            var dgr = c.KullaniciAnketCevaps.Where(p => p.Id > 0).Count().ToString();
            var grafik = new Chart(width: 350, height: 500).AddTitle("Sonuçlar").AddSeries
                (chartType: "Column", name: "SoruId", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult GenelSonucGrafik()
        {
            return View();
        }

        public ActionResult ResList()
        {
            List<KullaniciAnketCevap> kac = new List<KullaniciAnketCevap>();
            var students = (from p in c.KullaniciAnketCevaps
                            select new
                            {
                                Id = p.Id,
                                soruid = p.SoruId,
                                cevapid = p.CevapId,
                            }).ToList()
         .Select(a => new KullaniciAnketCevap
         {
             Id = a.Id,
             SoruId = (int)a.soruid,
             CevapId = (int)a.cevapid
         });
            return View(students.ToList());
        }

        public ActionResult SonucPie()
        {
            return View();
        }

        //public ActionResult VisualizeResult()
        //{
        //    return Json(ResultList(),JsonRequestBehavior.AllowGet);
        //}

        public ActionResult VisualizeResult()
        {
            Context context = new Context();
            return Json(context.KullaniciAnketCevaps.ToList(), JsonRequestBehavior.AllowGet);
        }
        //public List<ChartClass> ResultList()
        //{
        //    List<ChartClass> chartClasses = new List<ChartClass>();
        //    using(var c=new Context())
        //    {
        //        chartClasses = c.KullaniciAnketCevaps.Select(x => new ChartClass
        //        {
        //            cevapid = (int) x.CevapId,
        //            soruid=(int) x.SoruId
        //        }).ToList();
        //    }
        //    return chartClasses; 
        //}


    }
}
