using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDeneme14.ModelsGenerated;

namespace WebApiDeneme14.Models
{
    public class DatabaseTablo
    {
        public List<Bolgeler> Bolge { get; set; }
        public List<Sehirler> Sehir { get; set; }
    }
}
