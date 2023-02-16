using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Interfaces;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using MudBlazor;

namespace foodTrackerFrontEnd.Services
{
    public class FoodStorageService : IFoodStorageService
    {
        private HttpClient _apiClient;
        private ILocalStorageService _localStorage;
        private string _path;
        private ISnackbar _snackBar;
        public List<FoodStorage> LocalList { get; set; } = new List<FoodStorage>();

        public FoodStorageService(HttpClient apiClient, ILocalStorageService localStorage, ISnackbar snackbar)
        {
            _apiClient = apiClient;
            _localStorage = localStorage;
            _path = "/dev/storage";
            _snackBar = snackbar;
            _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            _snackBar.Configuration.ShowTransitionDuration = 100;
        }
        public async Task List(string? storageId = null)
        {
            string token = await _localStorage.GetItemAsync<string>("token");
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = new HttpResponseMessage();
            try
            {
                response = await _apiClient.GetAsync($"{_path}?storageId={storageId}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
                LocalList = await response.Content.ReadFromJsonAsync<List<FoodStorage>>();

            }
            catch (Exception ex)
            {
                _snackBar.Add("Oops! Something went wrong, please refresh the", Severity.Error);
            }
        }

        public async Task<string?> GetStorageIdByName(string name)
        {
            if (!LocalList.Any(s => s.Name.ToLower() == name))
                await List();

            if (LocalList.Any(s => s.Name.ToLower() == name))
                return LocalList.Where(s => s.Name.ToLower() == name).First().Id;

            return null;
        }

        public async Task<FoodStorage> Add(FoodStorage item)
        {
            string token = await _localStorage.GetItemAsync<string>("token");
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = new HttpResponseMessage();
            try
            {
                response = await _apiClient.PutAsJsonAsync(_path, item);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                return await response.Content.ReadFromJsonAsync<FoodStorage>();
            }
            catch (Exception ex)
            {
                this.LocalList.Remove(item);
                _snackBar.Add("Something went wrong, your item has not been saved", Severity.Error);
            }

            return null;
        }
    }
}
