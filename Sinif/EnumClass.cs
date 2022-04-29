using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibrasOneriAnket.Sinif
{
    public enum State
    {
        Taslak = 1,
        Yayınla = 2
    }
    public class EnumClass
    {
        public State StateId { get; set; }
    }
}