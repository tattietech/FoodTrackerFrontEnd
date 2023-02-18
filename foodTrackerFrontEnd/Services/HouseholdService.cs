using foodTrackerFrontEnd.Interfaces;
using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Pages;
using MudBlazor;
using System.Net.Http.Json;

namespace foodTrackerFrontEnd.Services
{
    public class HouseholdService : IHouseholdService
    {
        private HttpClient _apiClient;
        private string _path;
        private ISnackbar _snackBar;
        private IApiAuthService _apiAuthService;

        public HouseholdService(HttpClient apiClient, ISnackbar snackbar, IApiAuthService apiAuthService)
        {
            _apiClient = apiClient;
            _path = "/dev/household";
            _snackBar = snackbar;
            _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            _snackBar.Configuration.ShowTransitionDuration = 100;
            _apiAuthService = apiAuthService;
        }

        public async Task<Household> Get()
        {
            string token = await _apiAuthService.GetToken();
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = new HttpResponseMessage();
            try
            {
                response = await _apiClient.GetAsync($"{_path}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var houseList = await response.Content.ReadFromJsonAsync<List<Household>>();

                return houseList.First();
            }
            catch (Exception ex)
            {
                _snackBar.Add("Oops! Something went wrong, please refresh the page", Severity.Error);
            }

            return null;
        }
    }
}
