using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDeneme14.Models
{
    public class HavaDurumuCvp
    {
        public class HavaDurumuCevap
        {
            public string Name { get; set; }

            public IEnumerable<WeatherDescription> Weather { get; set; }

            public Main Main { get; set; }
        }

        public class WeatherDescription
        {
            public string Main { get; set; }            //Acıklama
            public string Description { get; set; }     
            public string icon { get; set; }            //icon

        }

        public class Main
        {
            public string Temp { get; set; }            //Sıcaklık
            public string Humidity { get; set; }        //nem
            public string Feels_like { get; set; }      //hissedilen
        }
    }
}
