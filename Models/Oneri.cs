using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ibrasOneriAnket.Models
{
    public class Oneri
    {
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public virtual Kullanici Kullanici { get; set; }

        public string OneriMesajı { get; set; }
        public string MevcutDurum { get; set; }
        public string File { get; set; }
        public bool Degerlendirme { get; set; }
        public bool Durum { get;set; }

        public virtual List<OneriDokumanlari> OneriDokumanlaris { get; set; }
    }
}