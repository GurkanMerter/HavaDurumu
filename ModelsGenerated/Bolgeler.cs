using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiDeneme14.ModelsGenerated
{
    public partial class Bolgeler
    {
        public Bolgeler()
        {
            Sehirlers = new HashSet<Sehirler>();
        }
        public int Id { get; set; }
        public string Isim { get; set; }

        public virtual ICollection<Sehirler> Sehirlers { get; set; }
    }
}
