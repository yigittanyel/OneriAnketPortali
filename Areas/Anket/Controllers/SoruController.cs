using DevExpress.BarCodes;
using ibrasOneriAnket.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
            _dbContext.SaveChanges();

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
                return Content(JsonConvert.SerializeObject(siradakiSoru), "application/json");
            }
            return Content(JsonConvert.SerializeObject(new AnketOlustur()), "application/json");           
        }

        public string GenerateToken(int id)
        {
            const string secret = "J1XTPOuXDVvVBOYg5Mbe0GQDstcKsx0NHjuFiwrk";

            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            double secondsSinceEpoch = UnixEpoch.GetSecondsSince(now.AddHours(1));

            var payload = new Dictionary<string, object>
            {
                { "oneriid", id },
                { "exp", secondsSinceEpoch }
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            return token;
        }

        public void QrCode(int id)
        {
            var linkTemplate = "http://192.168.2.75:7575/oneri/detay?token={0}";
            var link = string.Format(linkTemplate, GenerateToken(id));

            BarCode barCode = new BarCode();
            barCode.Symbology = Symbology.QRCode;
            barCode.CodeText = link;
            barCode.BackColor = Color.White;
            barCode.ForeColor = Color.Black;
            barCode.RotationAngle = 0;
            barCode.CodeBinaryData = Encoding.Default.GetBytes(barCode.CodeText);
            barCode.Options.QRCode.CompactionMode = QRCodeCompactionMode.Byte;
            barCode.Options.QRCode.ErrorLevel = QRCodeErrorLevel.Q;
            barCode.Options.QRCode.ShowCodeText = false;
            barCode.DpiX = 72;
            barCode.DpiY = 72;
            barCode.Module = 2f;

            barCode.Save(Server.MapPath("~/Uploads") + "\\" + string.Format("BarCodeImage_{0}.png",id), System.Drawing.Imaging.ImageFormat.Png);
        }

        public ActionResult Tesekkurler(int id)
        {
            QrCode(id);
            return View(id);
        }
    }
}