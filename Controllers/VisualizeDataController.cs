using ibrasOneriAnket.Models;
using ibrasOneriAnket.Sinif;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ibrasOneriAnket.Controllers
{
    public class VisualizeDataController : Controller
    {
        Context c = new Context();     
        public ActionResult AnketeAitSonuclar(int id)
        { 
                            // BAR GRAFİK

            var soru = c.AnketOlusturs.FirstOrDefault(q => q.Id == id);
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var veri = from anketCevap in c.KullaniciAnketCevaps
                       join ac in c.AnketCevaps on anketCevap.CevapId equals ac.Id
                       where anketCevap.SoruId == soru.Id
                       group ac by ac.Cevap into grouplanmis
                       select new
                       {
                           CevapAdi = grouplanmis.Key + " : %" + 
                           ((from t2 in grouplanmis select t2.Cevap).Count() * 100 
                           / (from t3 in c.KullaniciAnketCevaps where t3.SoruId == soru.Id select t3.CevapId)
                           .Count()),
                           Adet = (from t2 in grouplanmis select t2.Cevap).Count()
                       };
            veri.ToList().ForEach(a => xvalue.Add(a.CevapAdi));
            veri.ToList().ForEach(a => yvalue.Add(a.Adet));

            var grafik = new Chart(width: 1000, height: 750).AddTitle(soru.AnketAdi.ToUpper()).AddSeries
                (chartType: "Bar", name: soru.AnketAdi, xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult AnketeAitSonuclar2(int id)
        {
                            //PIE GRAFIK

            var soru = c.AnketOlusturs.FirstOrDefault(q => q.Id == id);
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var veri = from anketCevap in c.KullaniciAnketCevaps
                       join ac in c.AnketCevaps on anketCevap.CevapId equals ac.Id
                       where anketCevap.SoruId == soru.Id
                       group ac by ac.Cevap into grouplanmis
                       select new
                       {
                           CevapAdi =   grouplanmis.Key +" : %" + ((from t2 in grouplanmis select t2.Cevap).Count() * 100 
                           / (from t3 in c.KullaniciAnketCevaps where t3.SoruId == soru.Id select t3.CevapId)
                           .Count() ),
                           Adet = (from t2 in grouplanmis select t2.Cevap).Count()
                       };
            veri.ToList().ForEach(a => xvalue.Add(a.CevapAdi));
            veri.ToList().ForEach(a => yvalue.Add(a.Adet));
            var grafik = new Chart(width: 1000, height: 750).AddTitle("Sonuçlar").AddSeries
                (chartType: "Pie", name: "Cevap", xValue: xvalue, yValues: yvalue);

            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }
        //public ActionResult ColumnChart(int id)
        //{
        //    var a=c.AnketOlusturs.Where(x=>x.Id==id).ToList();
        //    return View(a);
        //}
        //public ActionResult VisualizeResults()
        //{
        //    return Json(Result(), JsonRequestBehavior.AllowGet);
        //}
        //public List<ChartClass> Result()
        //{
        //    List<ChartClass> chartClass = new List<ChartClass>();
        //    chartClass.Add(new ChartClass()
        //    {
        //        CevapAdi = "Atir",
        //        Adet = 88
        //    });
        //    chartClass.Add(new ChartClass()
        //    {
        //        CevapAdi = "Qasim",
        //        Adet = 60
        //    });
        //    chartClass.Add(new ChartClass()
        //    {
        //        CevapAdi = "Hassaan Tahir",
        //        Adet = 77
        //    });
        //    return chartClass;
        //}
        //using (var c = new Context())
        //{
        //    var deger = (from anketCevap in c.KullaniciAnketCevaps
        //                  join ac in c.AnketCevaps on anketCevap.CevapId equals ac.Id
        //         where anketCevap.SoruId == 11
        //         group ac by ac.Cevap into grouplanmis
        //         select new ChartClass
        //         {
        //             CevapAdi = grouplanmis.Key,
        //             Adet = (from t2 in grouplanmis select t2.Cevap).Count(),
        //         }.ToList());
        //}
    }
}
