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
            o.Durum = true;
            c.Oneris.Add(o);
            c.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Anket");
        }

        [HttpGet]
        public PartialViewResult Upload()
        {
            return PartialView();
        }
            [HttpPost]
        public PartialViewResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/Uploads"), file.FileName);
                file.SaveAs(path);
                TempData["result"] = "Güncelleme Başarılı.";
            }

            return PartialView();
        }







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