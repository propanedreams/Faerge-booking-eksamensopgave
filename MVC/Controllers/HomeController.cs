        using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessLogic.BLL;
using BusinessLogic.FaergeBLL;
using DataAccess.Model;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var task = client.GetStringAsync("https://localhost:7177/api/Faerge/getallfaerges");
            var msg = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<DTO.Model.Faerge> faerges = JsonSerializer.Deserialize<List<DTO.Model.Faerge>>(msg, option);

            List<MVC.Models.Faerge> faerge = new();
            foreach (var x in faerges)
            {
                faerge.Add(new MVC.Models.Faerge(x.id, x.navn, x.maxAntalBiler, x.maxAntalGaester, x.minAntalGaester, x.minAntalBiler, x.laengde, x.prisPrBil, x.prisPrGaest));
            }

            return View(faerge);
        }



        public IActionResult OpdaterFaerge()
        {
            HttpClient client = new HttpClient();
            var task = client.GetStringAsync("https://localhost:7177/api/Faerge/getallfaerges");
            var msg = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<DTO.Model.Faerge> faerges = JsonSerializer.Deserialize<List<DTO.Model.Faerge>>(msg, option);

            List<MVC.Models.Faerge> faerge = new();
            foreach (var x in faerges)
            {
                faerge.Add(new MVC.Models.Faerge(x.id, x.navn, x.maxAntalBiler, x.maxAntalGaester, x.minAntalGaester, x.minAntalBiler, x.laengde, x.prisPrBil, x.prisPrGaest));
            }
            return View(faerge);
        }



        [HttpPost]
        public async Task<IActionResult> SubmitFaerge(Faerge faerge)
        {
            DTO.Model.Faerge fa = new DTO.Model.Faerge
            {
                navn = faerge.navn,
                minAntalGaester = faerge.minAntalGaester,
                maxAntalGaester = faerge.maxAntalGaester,
                prisPrGaest = faerge.prisPrGaest,
                maxAntalBiler = faerge.maxAntalBiler,
                minAntalBiler = faerge.minAntalBiler,
                prisPrBil = faerge.prisPrBil,

            };

            using (HttpClient client = new HttpClient())
            {
                string jsonData = JsonSerializer.Serialize(fa);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://localhost:7177/api/Faerge/opretfaerge", content);

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> OpdaterFaerge(IFormCollection collection)
        {

            DTO.Model.Faerge faergePut = new DTO.Model.Faerge
            {
                id = int.Parse(collection["id"]), //usynligt felt
                navn = collection["navn"],
                minAntalGaester = int.Parse(collection["minAntalGaester"]),
                maxAntalGaester = int.Parse(collection["maxAntalGaester"]),
                prisPrGaest = int.Parse(collection["prisPrGaest"]),
                maxAntalBiler = int.Parse(collection["maxAntalBiler"]),
                minAntalBiler = int.Parse(collection["minAntalBiler"]),
                prisPrBil = int.Parse(collection["prisPrBil"]),
            };
            using (HttpClient client = new HttpClient())
            {
                string jsonData = JsonSerializer.Serialize(faergePut);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                string url = $"https://localhost:7177/api/Faerge/updatefaerge/{int.Parse(collection["id"])}";
                HttpResponseMessage response = await client.PutAsync(url, content);
                
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SletFaerge(IFormCollection iform)
        {
            using (HttpClient client = new HttpClient())
            {
                int n = int.Parse(iform["id"]);
                string url = $"https://localhost:7177/api/Faerge/deletefaerge/{int.Parse(iform["id"])}";
                HttpResponseMessage response = await client.DeleteAsync(url);
               
                return RedirectToAction("Index");
            }
        }

        public IActionResult SletFaerge()
        {
            HttpClient client = new HttpClient();
            var task = client.GetStringAsync("https://localhost:7177/api/Faerge/getallfaerges");
            var msg = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<DTO.Model.Faerge> faerges = JsonSerializer.Deserialize<List<DTO.Model.Faerge>>(msg, option);

            List<MVC.Models.Faerge> faerge = new();
            foreach (var x in faerges)
            {
                faerge.Add(new MVC.Models.Faerge(x.id, x.navn, x.maxAntalBiler, x.maxAntalGaester, x.minAntalGaester, x.minAntalBiler, x.laengde, x.prisPrBil, x.prisPrGaest));
            }
            return View(faerge);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new MVC.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}