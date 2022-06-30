using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Models
{
    public class AnketCevap
    {

        public int Id { get; set; }
        public int AnketOlusturId { get; set; }
        public string Cevap { get; set; }
        public bool CevapDurum { get; set; }
        public virtual AnketOlustur AnketOlustur { get; set; }
    }
}