using ibrasOneriAnket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ibrasOneriAnket.Areas.Anket.Controllers
{
    public class SoruController : Controller
    {
        private readonly Context _dbContext;

        public SoruController()
        {
            _dbContext = new Context();
        }

        // GET: Anket/Soru
        public ActionResult Index(int? id)
        {

            AnketOlustur anket = new AnketOlustur();
            if (id.HasValue)
            {
                anket = _dbContext.AnketOlusturs.FirstOrDefault(q => q.Id == id);
            }
            else
            {
                anket = _dbContext.AnketOlusturs
                    .Where(q=> q.AnketBaslangic < DateTime.Now && q.AnketBitis > DateTime.Now && q.Durum==true)
                    .OrderBy(o => o.AnketSira).FirstOrDefault();
            }

            return View(anket);
        }

        public ActionResult AnketBitti()
        {
            return View();
        }

        public ActionResult CevapVer(KullaniciAnketCevap cevap)
        {
            cevap.CevaplamaZamani = DateTime.Now;
            _dbContext.KullaniciAnketCevaps.Add(cevap);

            var soru = _dbContext.AnketOlusturs.FirstOrDefault(q => q.Id == cevap.SoruId);

           var siradakiSoru = _dbContext.AnketOlusturs
                .Where(q => 
                q.AnketBaslangic < DateTime.Now &&
                q.AnketBitis > DateTime.Now &&
                q.AnketSira > soru.AnketSira )
                .OrderBy(o => o.AnketSira)
                .FirstOrDefault();

            if (siradakiSoru != null)
            {
                _dbContext.SaveChanges();
                return Content(JsonConvert.SerializeObject(siradakiSoru), "application/json");
            }
            return Content(JsonConvert.SerializeObject(new AnketOlustur()), "application/json");           
        }

        public ActionResult Tesekkurler()
        {
            return View();
        }
    }
}