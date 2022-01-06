using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiDeneme14.ModelsGenerated
{
    public partial class Sehirler
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public int? Bolgeid { get; set; }

        public virtual Bolgeler Bolge { get; set; }
    }
}
