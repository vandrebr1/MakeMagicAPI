using MakeMagic.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MakeMagic.HttpClients
{
    public class HouseApiClient : IHouseApiClient
    {
        private readonly IApiConfig _apiConfig;
        private readonly HttpClient _httpClient;


        public HouseApiClient(HttpClient httpClient, IApiConfig apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig;
        }
        public async Task<Houses> SelectAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<Houses>($"{_apiConfig.BaseUrl}");
        }

    }
}
