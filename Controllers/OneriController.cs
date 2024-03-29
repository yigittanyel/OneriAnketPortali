﻿using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DevExpress.BarCodes;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using JWT.Builder;
using JWT.Algorithms;
using JWT;
using JWT.Serializers;
using JWT.Exceptions;
using ibrasOneriAnket.Sinif;

namespace ibrasOneriAnket.Controllers
{
    [Authorize(Roles = "A,E")]
    public class OneriController : BaseController
    {
        // GET: Oneri
        Context c = new Context();
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
            var user = c.Kullanicis.FirstOrDefault(q => q.KullaniciAdi == User.Identity.Name);
            o.KullaniciId = user.Id;
            o.Durum = true;
            o.Degerlendirme = false;
            c.Oneris.Add(o);
            c.SaveChanges();

            FormsAuthentication.SignOut();
            //Anket/Soru/Tesekkurler
            return RedirectToAction("Tesekkurler", "Anket/Soru", new { id = o.Id });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Detay(string token)
        {
            const string secret = "J1XTPOuXDVvVBOYg5Mbe0GQDstcKsx0NHjuFiwrk";
            try
            {
                var payload = JwtBuilder.Create()
                                    .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                                    .WithSecret(secret)
                                    .MustVerifySignature()
                                    .Decode<IDictionary<string, object>>(token);

                payload.TryGetValue("oneriid", out object id);

                var oneriId = Convert.ToInt32(id);
                var od = GetOneriDokumanById(oneriId);
                foreach (var item in od.OneriDokumanlaris)
                {
                    var tempFilePath = Server.MapPath("~/Uploads/temp/") + item.DosyaGuidId + item.DosyaUzantisi;
                    if (!System.IO.File.Exists(tempFilePath))
                    {
                        FileStream fs = new FileStream(tempFilePath, FileMode.Create);
                        fs.Write(item.DosyaYolu, 0, item.DosyaYolu.Length);
                        fs.Close();
                    }
                }

                od.Oneri = c.Oneris.FirstOrDefault(x => x.Id == oneriId);

                return View(od);
            }
            catch (TokenExpiredException ex)
            {
                return new HttpNotFoundResult();
                //ViewBag.TokenExpired = "Token Geçerlilik Süresi Dolmuştur";
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Detay(Oneri o)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                    {
                        fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);


                        string extension = System.IO.Path.GetExtension(file.FileName);
                        var fileName = Path.GetFileName(file.FileName);

                        var oneriDokumani = new OneriDokumanlari();

                        oneriDokumani.OneriId = o.Id;
                        oneriDokumani.DosyaGuidId = Guid.NewGuid();
                        oneriDokumani.DosyaYolu = fileData;
                        oneriDokumani.DosyaUzantisi = extension;
                        oneriDokumani.DosyaAdi = fileName;

                        c.OneriDokumanlaris.Add(oneriDokumani);
                        c.SaveChanges();
                    }
                }
            }

            var od = GetOneriDokumanById(o.Id);

            foreach (var item in od.OneriDokumanlaris)
            {
                var tempFilePath = Server.MapPath("~/Uploads/temp/") + item.DosyaGuidId + item.DosyaUzantisi;
                if (!System.IO.File.Exists(tempFilePath))
                {
                    FileStream fs = new FileStream(tempFilePath, FileMode.Create);
                    fs.Write(item.DosyaYolu, 0, item.DosyaYolu.Length);
                    fs.Close();
                }
            }

            return View("Detay", od);
        }

        public ActionResult OneriSil(int id)
        {
            var x = c.Oneris.FirstOrDefault(a => a.KullaniciId == id);
            c.Oneris.Remove(x);
            c.SaveChanges();
            return RedirectToAction("OncekiOnerileriniz", "Oneri" );
        }

        [AllowAnonymous]
        public ActionResult OneriResimsil(int id)
        {
            var oneriDokuman = c.OneriDokumanlaris.FirstOrDefault(a => a.OneriDokumanlariId == id);
            c.OneriDokumanlaris.Remove(oneriDokuman);
            c.SaveChanges();

            var od = GetOneriDokumanById(oneriDokuman.OneriId);
            return View("Detay", od);
        }

        public OneriveDokuman GetOneriDokumanById(int oneriId)
        {
            OneriveDokuman od = new OneriveDokuman();
            od.OneriDokumanlaris = c.OneriDokumanlaris.Where(x => x.OneriId == oneriId).ToList();
            od.Oneri = c.Oneris.FirstOrDefault(x => x.Id == oneriId);
            return od;
        }

        [AllowAnonymous]
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
            var user = c.Kullanicis.FirstOrDefault(q => q.KullaniciAdi == User.Identity.Name);
            var x = c.Oneris.FirstOrDefault(a => a.Id == p.Id);
            x.Id = p.Id;
            x.KullaniciId = user.Id;
            x.OneriMesajı = p.OneriMesajı;
            x.MevcutDurum = p.MevcutDurum;
            x.Degerlendirme = p.Degerlendirme;
            x.File=p.File;
            x.Durum = true;
            c.SaveChanges();
            return RedirectToAction("OncekiOnerileriniz","Oneri");
            //return RedirectToAction("OncekiOneriler", "Oneri", new {id=p.Id});
        }

        public ActionResult OncekiOneriler(int id)
        {
            int userId = GetKullaniciId();
            var oncekiOneriler = c.Oneris.Where(q => q.Id == id && q.KullaniciId == userId && q.Durum == true).ToList();

            if (oncekiOneriler != null)
            {

                return View("OncekiOneriler", oncekiOneriler);
            }

            return HttpNotFound();
        }

        public ActionResult OncekiOnerileriniz()
        {
            var user = c.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name);
            var deger = c.Oneris.Where(x => x.Durum && x.Kullanici.Id == user.Id).ToList();

            if (deger != null)
                return View(deger);
            else
                return HttpNotFound();
        }

    }

}