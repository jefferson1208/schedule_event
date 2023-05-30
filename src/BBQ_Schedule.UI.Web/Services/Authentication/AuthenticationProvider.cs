using BBQ_Schedule.UI.Web.Dtos;
using BBQ_Schedule.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BBQ_Schedule.UI.Web.Services.Authentication
{
    public class AuthenticationProvider : AuthenticationStateProvider, IAuthorizationService
	{
        private readonly IJSRuntime _jsRuntime;
        private static HttpClient _httpClient;
        private static readonly string  TokenKeyLocalStorage = "accessToken";
        public AuthenticationProvider(IHttpClientFactory httpClientFactory, IJSRuntime jsRuntime)
        {
            _httpClient = httpClientFactory.CreateClient("Schedule_Event");
            _jsRuntime = jsRuntime;
        }

        private AuthenticationState Unauthorized =>
			new AuthenticationState(
						new ClaimsPrincipal(new ClaimsIdentity()));
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _jsRuntime.GetFromLocalStorage(TokenKeyLocalStorage);

            if (string.IsNullOrEmpty(accessToken)) return Unauthorized;

			return CreateAuthenticationState(accessToken);
        }

        public AuthenticationState CreateAuthenticationState(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            return new AuthenticationState(
                        new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(accessToken), "JWT")));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            foreach (var item in keyValuePairs)
            {
                if(item.Key == "email")
                {
                    claims.Add(new Claim(ClaimTypes.Name, item.Value.ToString()));
                }

                claims.Add(new Claim(item.Key, item.Value.ToString()));
            }

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

		public async Task Login(string accessToken)
		{
			await _jsRuntime.SetInLocalStorage(TokenKeyLocalStorage, accessToken);

			var authState = CreateAuthenticationState(accessToken);

			NotifyAuthenticationStateChanged(Task.FromResult(authState));
		}
		public async Task<HttpResponseMessage> Login(LoginDto login)
		{
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await _httpClient.PostAsync("/api/v1/login",
                new StringContent(Util.ConvertToJson(login), Encoding.UTF8, "application/json"));
        }

		public async Task Logout()
		{
			await _jsRuntime.RemoveItemFromLocalStorage(TokenKeyLocalStorage);
            _httpClient.DefaultRequestHeaders.Authorization = null;
			NotifyAuthenticationStateChanged(Task.FromResult(Unauthorized));
		}


	}
}
