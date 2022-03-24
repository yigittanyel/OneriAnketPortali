using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Models
{
    public class KullaniciAnketCevap
    {
        public int Id { get; set; }
        public int SoruId { get; set; }
        public int CevapId { get; set; }
        public DateTime CevaplamaZamani { get; set; }
    }
}