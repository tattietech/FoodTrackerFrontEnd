using foodTrackerFrontEnd.Exceptions;
using foodTrackerFrontEnd.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace foodTrackerFrontEnd.Services
{
    public class ApiAuthService : IApiAuthService
    {
        private ILocalStorageService _localStorage;
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ISnackbar _snackBar;

        public ApiAuthService(ILocalStorageService localStorageService, HttpClient httpClient, NavigationManager navigationManager, ISnackbar snackbar)
        {
            _localStorage = localStorageService;
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _snackBar = snackbar;
            _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            _snackBar.Configuration.ShowTransitionDuration = 100;
        }

        public async Task<string> GetToken()
        {
            var token = await _localStorage.GetItemAsync<string>("token");

            if (IsValid(token))
                return token;


            var refresh = await _localStorage.GetItemAsync<string>("refresh");

            if (!string.IsNullOrEmpty(refresh))
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_httpClient.BaseAddress}/dev/auth/refresh");
                request.Content = new StringContent(refresh);

                var response = await _httpClient.SendAsync(request);
                var newToken = await response.Content.ReadAsStringAsync();

                if (IsValid(newToken))
                {
                    await _localStorage.SetItemAsync("token", newToken);
                    return newToken;
                }
            }

            _navigationManager.NavigateTo($"{_navigationManager.BaseUri}/logout");
            _snackBar.Add("Unauthorised request, logging out..", Severity.Error);

            return null;
        }

        private bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
    }
}
