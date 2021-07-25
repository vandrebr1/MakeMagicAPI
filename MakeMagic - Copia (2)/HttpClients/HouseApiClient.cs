using MakeMagic.Models;
using Newtonsoft.Json;
using System.Net.Http;

using System.Threading.Tasks;

namespace MakeMagic.HttpClients
{
    public class HouseApiClient
    {
        private readonly HttpClient _httpClient;

        public HouseApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Houses> SelectAllAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var houses = JsonConvert.DeserializeObject<Houses>(json);

            return houses;
        }
    }
}
