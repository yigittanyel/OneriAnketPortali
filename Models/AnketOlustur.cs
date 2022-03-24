using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Models
{
    public class AnketOlustur
    {
        public AnketOlustur()
        {
            AnketCevaps = new List<AnketCevap>();
        }
        public int Id { get; set; }
        public string AnketAdi { get; set; }
        //public string Soru{ get; set; }
        public bool Durum { get; set; }

        public virtual List<AnketCevap> AnketCevaps { get; set;}
    }
}                                                               