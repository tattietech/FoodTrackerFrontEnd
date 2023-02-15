using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Interfaces;
using System.Net.Http.Json;

namespace foodTrackerFrontEnd.Services
{
    public class FoodStorageService : IFoodTrackerApiService<FoodStorage>, IFoodStorageService
    {
        private HttpClient _apiClient;
        private ILocalStorageService _localStorage;
        private string _path;
        public FoodStorageService(HttpClient apiClient, ILocalStorageService localStorage)
        {
            _apiClient = apiClient;
            _localStorage = localStorage;
            _path = "/dev/storage";
        }
        public async Task<IEnumerable<FoodStorage>> List(string? storageId = null)
        {
            string token = await _localStorage.GetItemAsync<string>("token");
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await _apiClient.GetFromJsonAsync<IEnumerable<FoodStorage>>(_path);
        }

        public async Task<string?> GetStorageId(string name)
        {
            var storage = await List();

            if(storage.Any(s => s.Name.ToLower() == name))
                return storage.Where(s => s.Name.ToLower() == name).First().Id;

            return null;
        }
    }
}
