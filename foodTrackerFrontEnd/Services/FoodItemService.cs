using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Interfaces;
using System.Net.Http.Json;

namespace foodTrackerFrontEnd.Services
{
    public class FoodItemService : IFoodTrackerApiService<FoodItem>
    {
        private HttpClient _apiClient;
        private ILocalStorageService _localStorage;
        private string _path;
        public FoodItemService(HttpClient apiClient,  ILocalStorageService localStorage)
        {
            _apiClient = apiClient;
            _localStorage = localStorage;
            _path = "/dev/food";
        }
        public async Task<IEnumerable<FoodItem>> List(int household)
        {
            string token = await _localStorage.GetItemAsync<string>("token");
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await _apiClient.GetFromJsonAsync<IEnumerable<FoodItem>>(_path);
        }
    }
}
