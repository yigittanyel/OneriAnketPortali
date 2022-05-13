using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Models
{
    public class OneriDokumanlari
    {
        [Key]
        public int OneriDokumanlariId { get; set; } 
        public byte[] DosyaYolu { get; set; }

        public string DosyaUzantisi { get; set; }
        public Guid DosyaGuidId { get; set; }
        public string DosyaAdi { get; set; }

        [ForeignKey("Oneri")]
        public int OneriId { get; set; } 
        public virtual Oneri Oneri { get; set; }
    }
}