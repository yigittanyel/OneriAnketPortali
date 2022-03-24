using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Models
{
    public class Birim
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public IEnumerable<Oneri>Oneris { get; set; }   
    }
}