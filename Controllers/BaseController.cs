using ibrasOneriAnket.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ibrasOneriAnket.Controllers
{
    public class BaseController : Controller
    {

        public Kullanici GetKullanici()
        {
            Context c = new Context();
            var user = c.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name);
            return user;
        }

        public int GetKullaniciId()
        {
            return GetKullanici().Id;
        }
    }
}