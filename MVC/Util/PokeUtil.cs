using MVC.Models;
using System.Text.Json;

namespace MVC.Util
{
    public class PokeUtil
    {
        public HttpClient Client { get; set; }
        public PokeUtil(HttpClient client)
        {
            this.Client = client;
        }

        public async Task<Pokemon> GetPokemon(string id)
        {
            var res = await this.Client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
            var content = await res.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Pokemon>(content);
        }

    }
}
