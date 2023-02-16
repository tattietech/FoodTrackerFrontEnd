using foodTrackerFrontEnd.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace foodTrackerFrontEnd
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        //private const string LOGIN_URL = "https://foodtracker.auth.eu-west-2.amazoncognito.com/oauth2/authorize?client_id=7kernf23q06t1oujubvbo0ljs9&response_type=code&scope=openid&redirect_uri=https%3A%2F%2Ffoodtracker.andrewbruce.me";
        private const string LOGIN_URL = "https://foodtracker.auth.eu-west-2.amazoncognito.com/oauth2/authorize?client_id=7kernf23q06t1oujubvbo0ljs9&response_type=code&scope=openid&redirect_uri=https%3A%2F%2Flocalhost%3A7048";
        private NavigationManager _navManager;
        private ILocalStorageService _localStorage;
        private HttpClient _apiClient;
        public AuthStateProvider(NavigationManager navManager, ILocalStorageService localStorage, HttpClient apiClient)
        {
            _navManager = navManager;
            _localStorage = localStorage;
            _apiClient = apiClient;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity = new();
            string token;
            string refresh;

            bool foundCode = QueryHelpers.ParseQuery(new Uri(_navManager.Uri).Query).TryGetValue("code", out var code);

            if (foundCode)
            {
                var authResponse = await _apiClient.GetFromJsonAsync<AuthResponse>("/dev/auth?code="+code);
                token = authResponse.AccessToken;
                refresh = authResponse.RefreshToken;

                await _localStorage.SetItemAsync("token", token);
                await _localStorage.SetItemAsync("refresh", refresh);
            }
            else
            {
                token = await _localStorage.GetItemAsync<string>("token");
                refresh = await _localStorage.GetItemAsync<string>("refresh");
            }

            if (IsValid(token))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            }
            else
            {
                if (!string.IsNullOrEmpty(refresh))
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiClient.BaseAddress}/dev/auth/refresh");
                    request.Content = new StringContent(refresh);

                    var response = await _apiClient.SendAsync(request);
                    var newToken = await response.Content.ReadAsStringAsync();

                    if (IsValid(newToken))
                    {
                        await _localStorage.SetItemAsync("token", newToken);
                        identity = new ClaimsIdentity(ParseClaimsFromJwt(newToken), "jwt");
                    }
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));

            if (foundCode)
                _navManager.NavigateTo(_navManager.BaseUri);

            return state;
        }

        public static string LoginUrl()
        {
            return LOGIN_URL;
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            try
            {
                var payload = jwt.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
                return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
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
