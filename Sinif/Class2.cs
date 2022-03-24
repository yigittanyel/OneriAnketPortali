using ibrasOneriAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Sinif
{
    public class Class2
    {
        public IEnumerable<AnketCevap> DegerAC { get; set; }
        public IEnumerable<AnketOlustur> DegerAO { get; set; }
        public IEnumerable<KullaniciAnketCevap> DegerCvp { get; set; }
    }
}