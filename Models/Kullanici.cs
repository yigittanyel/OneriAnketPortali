using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Models
{
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Rol { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public int BirimId { get; set; }
        public virtual Birim Birim { get; set; }
    }
}