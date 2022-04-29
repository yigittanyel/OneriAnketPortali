using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ibrasOneriAnket.Controllers
{
    [Authorize(Roles="A,E")]
    public class OneriController : Controller
    {
        // GET: Oneri
        Context c=new Context();
        [HttpGet]
        public ActionResult Index()
        {
            //int id = 1;
            //string p;
            //p = (string)Session["Ad"];
            //ViewBag.deger = p;
            ////id = (int)Session["Id"];

            var kullaniciAdi = HttpContext.User.Identity.Name;
            var kullanici = c.Kullanicis.FirstOrDefault(q => q.KullaniciAdi == kullaniciAdi);

            ViewBag.Kullanici = kullanici;


            List<SelectListItem> der = (from x in c.Birims.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad,
                                            Value = x.Id.ToString(),
                                            Selected = false
                                        }).ToList();
            ViewBag.dr = der;
            return View();
        }

        [HttpPost]
        public ActionResult Index(Oneri o)
        {
            Session["KisiAd"] = o.Ad;
            Session["KisiSoyad"] = o.Soyad;
            o.Durum = true;
            c.Oneris.Add(o);
            c.SaveChanges();
            FormsAuthentication.SignOut();
            //Anket/Soru/Tesekkurler
            return RedirectToAction("Tesekkurler","Anket/Soru");
        }

        [HttpGet]
        public PartialViewResult Upload()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Upload(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var _pathname=Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), _pathname);
                file.SaveAs(path);
            }

            return PartialView("Upload");
        }






        //public PartialViewResult GirenKisiGoruntule()
        //{
        //    ViewBag.ad = c.Oneris.Select(x => x.Ad).FirstOrDefault();
        //    ViewBag.soyad = c.Oneris.Select(x => x.Soyad).FirstOrDefault();
        //    return PartialView();
        //}



        //[HttpGet]
        //public PartialViewResult UploadFile()
        //{
        //    return PartialView();
        //}
        //[HttpPost]
        //public PartialViewResult UploadFile(HttpPostedFileBase file)
        //{
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        var path = Path.Combine(Server.MapPath("~/Upload_Yukleme"), Path.GetFileName(file.FileName));
        //        file.SaveAs(path);
        //        TempData["result"] = "Güncelleme Başarılı.";
        //        return PartialView();
        //    }

        //    return PartialView();
        //}
        //[HttpPost]
        //public PartialViewResult UploadFile(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
        //            file.SaveAs(_path);
        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";
        //        return PartialView();
        //    }
        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return PartialView();
        //    }
        //}

    }

}