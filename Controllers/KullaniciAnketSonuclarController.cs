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
            //kullanıcıanketsonuclar
            return View();
        }
    }
}