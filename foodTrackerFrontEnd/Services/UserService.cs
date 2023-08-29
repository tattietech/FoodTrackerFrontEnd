namespace foodTrackerFrontEnd.Services
{
    using foodTrackerFrontEnd.Interfaces;
    using foodTrackerFrontEnd.Models;
    using foodTrackerFrontEnd.Pages;
    using MudBlazor;
    using System.Net.Http.Json;

    public class UserService : IUserService
    {
        private HttpClient _apiClient;
        private string _path;
        private ISnackbar _snackBar;
        private IApiAuthService _apiAuthService;
        private AppState _appState;

        public UserService(HttpClient apiClient, ISnackbar snackbar, IApiAuthService apiAuthService, AppState appState)
        {
            _apiClient = apiClient;
            _path = "/dev/user";
            _snackBar = snackbar;
            _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            _snackBar.Configuration.ShowTransitionDuration = 100;
            _apiAuthService = apiAuthService;
            _appState = appState;
        }

        public async Task SetCurrentUser()
        {
            string token = await _apiAuthService.GetToken();
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = new HttpResponseMessage();
            try
            {
                response = await _apiClient.GetAsync(_path);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var user = await response.Content.ReadFromJsonAsync<User>();
                _appState.SetCurrentUser(user);
            }
            catch (Exception ex)
            {
                _snackBar.Add("Oops! Something went wrong, please refresh the page", Severity.Error);
            }
        }
    }
}
