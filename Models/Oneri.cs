using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Models
{
    public class Oneri
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int? BirimId { get; set; }
        public virtual Birim Birim { get; set; }
        public string OneriMesajı { get; set; }
        public string MevcutDurum { get; set; }
        public string File { get; set; }
        public bool Degerlendirme { get; set; }
        public bool Durum { get;set; }

        public virtual List<OneriDokumanlari> OneriDokumanlaris { get; set; }
    }
}