using ibrasOneriAnket.Sinif;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public DateTime? AnketBaslangic { get; set; }
        public DateTime? AnketBitis { get; set; }
        public int? AnketSira { get; set; }

        public char AnketYayinDurumu { get; set; }

        public State State { get; set; }

        [JsonIgnore]
        public virtual List<AnketCevap> AnketCevaps { get; set;}
    }
}                                                               