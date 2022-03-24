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
        public ActionResult Index()
        {
            Class1 c1 = new Class1();

            c1.DegerAC = c.AnketCevaps.Where(a=>a.CevapDurum).ToList();
            c1.DegerAO = c.AnketOlusturs.Where(x=>x.Durum).ToList();
            return View(c1);
         }
        [HttpPost]
        public ActionResult Index(KullaniciAnketCevap kac)
        {
            kac.CevaplamaZamani=DateTime.Now; 
            c.KullaniciAnketCevaps.Add(kac);
            c.SaveChanges();
            return RedirectToAction("Tesekkurler","Anket");
            /*return RedirectToAction("Index", "Oneri")*/;
        }

        public ActionResult Tesekkurler()
        {
            return View();
        }
    }
}