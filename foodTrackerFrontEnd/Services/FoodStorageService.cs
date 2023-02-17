using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Interfaces;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using MudBlazor;

namespace foodTrackerFrontEnd.Services
{
    public class FoodStorageService : IFoodTrackerApiService<FoodStorage>
    {
        private HttpClient _apiClient;
        private string _path;
        private ISnackbar _snackBar;
        private IApiAuthService _apiAuthService;


        public FoodStorageService(HttpClient apiClient, ISnackbar snackbar, IApiAuthService apiAuthService)
        {
            _apiClient = apiClient;
            _path = "/dev/storage";
            _snackBar = snackbar;
            _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            _snackBar.Configuration.ShowTransitionDuration = 100;
            _apiAuthService = apiAuthService;
        }
        public async Task<IEnumerable<FoodStorage>> List(string storageId = null)
        {
            string token = await _apiAuthService.GetToken();
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = new HttpResponseMessage();
            try
            {
                response = await _apiClient.GetAsync($"{_path}?storageId={storageId}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
               
                return await response.Content.ReadFromJsonAsync<List<FoodStorage>>();

            }
            catch (Exception ex)
            {
                _snackBar.Add("Oops! Something went wrong, please refresh the", Severity.Error);
            }

            return null;
        }

        public async Task<FoodStorage> Add(FoodStorage item)
        {
            string token = await _apiAuthService.GetToken();
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
                _snackBar.Add("Something went wrong, your item has not been saved", Severity.Error);
            }

            return null;
        }

        public async Task Delete(string id)
        {
            string token = await _apiAuthService.GetToken();
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = new HttpResponseMessage();
            try
            {
                response = await _apiClient.DeleteAsync($"{_path}?id={id}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

            }
            catch (Exception ex)
            {
                _snackBar.Add("Oops! Something went wrong, please refresh the page", Severity.Error);
            }
        }
    }
}
