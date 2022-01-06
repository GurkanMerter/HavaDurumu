using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApiDeneme14.Models;
using Microsoft.EntityFrameworkCore;
using WebApiDeneme14.ModelsGenerated;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static WebApiDeneme14.Models.HavaDurumuCvp;

namespace WebApiDeneme14.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiPostgresContext _context;
        DatabaseTablo tablo = new DatabaseTablo();
        List<Bolgeler> bolgelers = new List<Bolgeler>();
        List<Sehirler> sehirlers = new List<Sehirler>();

        
        public HomeController(ILogger<HomeController> logger, ApiPostgresContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult HavaDurumuMenu()
        {
            var bolgeler = _context.Bolgelers.ToList();
            bolgelers.AddRange(bolgeler);

            var sehirler = _context.Sehirlers.ToList();
            sehirlers.AddRange(sehirler);

            tablo.Bolge = bolgelers;
            tablo.Sehir = sehirlers;
           
            return View(tablo.Bolge);
        }

        public JsonResult SehirGetir(int id)
        {

            List<SelectListItem> Sehir = _context.Sehirlers.Where(c => c.Bolgeid == id).OrderBy(n => n.Isim).Select(n =>
                  new SelectListItem
                  {
                      Value = n.Id.ToString(),
                      Text = n.Isim.ToString()
                  }).ToList();
            
            return Json(Sehir);
        }

        [HttpGet]
        public async Task<IActionResult> SehirHavaDurumuGetir(int id)
        {
            string sehirAd = _context.Sehirlers.SingleOrDefault(d=>d.Id==id)?.Isim;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={sehirAd}&appid=bd68c3d82579aa82c6d745cc4b8209d2&units=metric&lang=tr");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<HavaDurumuCevap>(stringResult);
                    return Ok(new
                    {
                        Temp = rawWeather.Main.Temp,                                                        //derece
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Description)),          //aciklama
                        City = rawWeather.Name,                                                             //sehir
                        Icon= string.Join(",", rawWeather.Weather.Select(x => x.icon)),                     //icon
                        FeelsLike=rawWeather.Main.Feels_like,                                               //hissedilen
                        Humidity=rawWeather.Main.Humidity,                                                  //nem
                    }); 
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
                
            }
        }
    }
}
