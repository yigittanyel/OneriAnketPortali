using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Models
{
    public class Context:DbContext
    {
        public DbSet<Kullanici> Kullanicis { get; set; }    
        public DbSet<Oneri> Oneris { get; set; }    
        public DbSet<Birim> Birims { get; set; }    
        public DbSet<AnketOlustur> AnketOlusturs { get; set; }    
        public DbSet<OneriKullanici> OneriKullanicis { get; set; }    
        public DbSet<AnketCevap> AnketCevaps { get; set; }    
        public DbSet<KullaniciAnketCevap> KullaniciAnketCevaps { get; set; }    
        public DbSet<OneriDokumanlari> OneriDokumanlaris { get; set; }    

    }
}