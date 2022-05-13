using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Sinif
{
    public class OneriveDokuman
    {
        public Oneri Oneri { get; set; }
        public IEnumerable<OneriDokumanlari> OneriDokumanlaris { get; set; }
    }
}