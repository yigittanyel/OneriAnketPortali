using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ibrasOneriAnket.Sinif;
namespace ibrasOneriAnket.Controllers
{
    public class AnketController : Controller
    {
        // GET: Anket
        Context c = new Context();
        [HttpGet]

        //string name = "John Doe";
        //if (!String.IsNullOrEmpty(HttpContext.Request.Query["name"]))
        //    name = HttpContext.Request.Query["name"];

        //return Content("Name from query string: " + name);       
        public ActionResult Index()
        {
            Class1 c1 = new Class1();

            c1.DegerAC = c.AnketCevaps.Where(a => a.CevapDurum && a.AnketOlusturId == 28).ToList();
            c1.DegerAO = c.AnketOlusturs.Where(x => x.Durum & x.Id == 28).ToList();
            //c1.DegerAC = c.AnketCevaps.OrderBy(x=>x.Id).Take(3).ToList();
            //c1.DegerAO= c.AnketOlusturs.OrderBy(x => x.Id).Take(3).ToList();
            return View(c1);
         }
        [HttpPost]
        public ActionResult Index(KullaniciAnketCevap kac)
        {
            kac.CevaplamaZamani=DateTime.Now; 
            c.KullaniciAnketCevaps.Add(kac);
            c.SaveChanges();
            return RedirectToAction("Anket2","Anket");
            /*return RedirectToAction("Index", "Oneri")*/;
        }

        public ActionResult Tesekkurler()
        {
            return View();
        }



        //[HttpGet]
        //public ActionResult Anket2()
        //{
        //    Class1 c1 = new Class1();
        //    c1.DegerAC = c.AnketCevaps.Where(a => a.CevapDurum&&a.AnketOlusturId==20).ToList();
        //    c1.DegerAO = c.AnketOlusturs.Where(x => x.Durum&&x.Id==20).ToList();
        //    return View(c1);
        //}
        //[HttpPost]
        //public ActionResult Anket2(KullaniciAnketCevap kac)
        //{
        //    kac.CevaplamaZamani = DateTime.Now;
        //    c.KullaniciAnketCevaps.Add(kac);
        //    c.SaveChanges();

        //    //EĞER İKİ SORU OLACAKSA BU KISMIN YERİNE
        //    return RedirectToAction("Anket3", "Anket");

        //    // BU KISMI EKLE
        //    //return RedirectToAction("Tesekkurler", "Anket");

        //}
        //[HttpGet]
        //public ActionResult Anket3()
        //{
        //    Class1 c1 = new Class1();
        //    c1.DegerAC = c.AnketCevaps.Where(a => a.CevapDurum && a.AnketOlusturId == 11).ToList();
        //    c1.DegerAO = c.AnketOlusturs.Where(x => x.Durum && x.Id == 11).ToList();
        //    return View(c1);
        //}
        //[HttpPost]
        //public ActionResult Anket3(KullaniciAnketCevap kac)
        //{
        //    kac.CevaplamaZamani = DateTime.Now;
        //    c.KullaniciAnketCevaps.Add(kac);
        //    c.SaveChanges();
        //    return RedirectToAction("Tesekkurler", "Anket");
        //    /*return RedirectToAction("Index", "Oneri")*/
        //    ;
        //}



    }
}