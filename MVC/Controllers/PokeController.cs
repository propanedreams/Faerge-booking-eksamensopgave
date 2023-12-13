using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MVC.Controllers
{
    public class PokeController : Controller
    {

        private readonly ILogger<PokeController> _logger;

        public PokeController(ILogger<PokeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Pokemon()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            HttpClient client = new HttpClient();
            for (int i = 1; i < 10; i++)
            {
                var task = client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{i.ToString()}");
                var msg = task.Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                pokemons.Add(JsonSerializer.Deserialize<Pokemon>(msg, option));
            }



            return View(pokemons);
        }


        [HttpPost]
        public async Task<IActionResult> GetPokemon(IFormCollection iffy)
        {
            string pokemonIdString = iffy["pokemonId"];

            if (string.IsNullOrEmpty(pokemonIdString))
            {
                // Handle empty Pokemon ID
                return BadRequest("Pokemon ID is empty");
            }

            if (!int.TryParse(pokemonIdString, out int id))
            {
                // Handle invalid Pokemon ID format
                return BadRequest($"Invalid Pokemon ID format: {pokemonIdString}");
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var pokemon = JsonSerializer.Deserialize<Pokemon>(response, options);

                    return View(pokemon);
                }
            }
            catch (HttpRequestException)
            {
                // Handle API request failure
                return View("Error");
            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new MVC.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
