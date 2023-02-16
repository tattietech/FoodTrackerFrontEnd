using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Interfaces;
using System.Net.Http.Json;
using foodTrackerFrontEnd.Pages;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MudBlazor;
using MudBlazor.Services;

namespace foodTrackerFrontEnd.Services
{
    public class FoodItemService : IFoodItemService
    {
        private HttpClient _apiClient;
        private ILocalStorageService _localStorage;
        private string _path;
        private ISnackbar _snackBar;

        public List<FoodItem> LocalList { get; set; } = new List<FoodItem>();

        public FoodItemService(HttpClient apiClient,  ILocalStorageService localStorage, ISnackbar snackbar)
        {
            _apiClient = apiClient;
            _localStorage = localStorage;
            _path = "/dev/food";
            _snackBar = snackbar;
            _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            _snackBar.Configuration.ShowTransitionDuration = 100;
        }

        public async Task<FoodItem> Add(FoodItem item)
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

                return await response.Content.ReadFromJsonAsync<FoodItem>();
            }
            catch(Exception ex)
            {
                this.LocalList.Remove(item);
                _snackBar.Add("Something went wrong, your item has not been saved", Severity.Error);
            }

            return null;
        }

        public async Task List(string? storageId=null)
        {
            string token = await _localStorage.GetItemAsync<string>("token");
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = new HttpResponseMessage();
            try
            {
                response = await _apiClient.GetAsync($"{_path}?storageId={storageId}");

                if(!response.IsSuccessStatusCode)
                    throw new Exception();

                LocalList = await response.Content.ReadFromJsonAsync<List<FoodItem>>();

            }
            catch(Exception ex)
            {
                _snackBar.Add("Oops! Something went wrong, please refresh the page", Severity.Error);
            }
        }
    }
}
