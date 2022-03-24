using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ibrasOneriAnket.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Context c=new Context();
        [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(Kullanici d)
        {
            var bilgi = c.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == d.KullaniciAdi && x.Sifre == d.Sifre);
            if (bilgi != null&& bilgi.Rol=="A")
            {
                FormsAuthentication.SetAuthCookie(bilgi.KullaniciAdi, true);
                Session["KullaniciAdi"] = bilgi.KullaniciAdi.ToString();
                return RedirectToAction("OneriGoruntule", "AdminOneri");
            }
            else if (bilgi != null && bilgi.Rol == "E")
            {
                FormsAuthentication.SetAuthCookie(bilgi.KullaniciAdi, true);
                Session["KullaniciAdi"] = bilgi.KullaniciAdi.ToString();
                return RedirectToAction("Index", "Oneri");
            }
            else
            {
                return View();
            }
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
    }
}