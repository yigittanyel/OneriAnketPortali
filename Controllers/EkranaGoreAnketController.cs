using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ibrasOneriAnket.Controllers
{
    public class EkranaGoreAnketController : Controller
    {
        Context c = new Context();
        // GET: EkranaGoreAnket
        public ActionResult Yemekhane()
        {
            return View();
        }
    }
}