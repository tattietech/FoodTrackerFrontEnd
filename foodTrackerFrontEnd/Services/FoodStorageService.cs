using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Interfaces;
using System.Net.Http.Json;

namespace foodTrackerFrontEnd.Services
{
    public class FoodStorageService : IFoodTrackerApiService<FoodStorage>
    {
        private HttpClient _apiClient;
        private string _path;
        public FoodStorageService(HttpClient apiClient)
        {
            _apiClient = apiClient;
            _path = "/storage";
        }
        public async Task<IEnumerable<FoodStorage>> List(int household)
        {
            return await _apiClient.GetFromJsonAsync<IEnumerable<FoodStorage>>(_path);
        }
    }
}
