using ibrasOneriAnket.Models;
using ibrasOneriAnket.Sinif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ibrasOneriAnket.Controllers
{
    public class KullaniciAnketSonuclarController : Controller
    {
        // GET: KullaniciAnketSonuclar
        Context c = new Context();
        [HttpGet]
        public ActionResult Listele(int id)
        {
            //var x = c.Kisis.Where(a => a.ServisId == id).ToList();

            Class2 cl2 = new Class2();

            cl2.DegerAC = c.AnketCevaps.Where(x=>x.CevapDurum&&x.Id==id).ToList();
            cl2.DegerAO = c.AnketOlusturs.Where(x=>x.Durum&&x.Id==id).ToList();
            cl2.DegerCvp = c.KullaniciAnketCevaps.Where(x=>x.Id==id).ToList();
            return View(cl2);
        }
    }
}