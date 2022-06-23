using SpotifySocialMedia.SpotifyAuthorizationDataDatabase.Models;
using SpotifySocialMedia.SpotifySettingsDatabase.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SpotifySocialMedia.Areas.Admin.Services
{
    public interface ISpotifyTokenService
    {
        Task SaveNewTokenToDatabase(string code, string clientId, string clientSecret);
        Task RefreshToken(string refreshToken, string clientId, string clientSecret);
        Task CheckTokenExpireDate();
    }
    public class SpotifyTokenService: ISpotifyTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IDatabaseSpotifyTokenService _databaseSpotifyTokenService;
        private readonly IConfiguration _configurationAppSettingJSON;

        public SpotifyTokenService(HttpClient httpClient,
            IDatabaseSpotifyTokenService databaseSpotifyTokenService,
             IConfiguration configurationAppSettingJSON)
        {
            _httpClient = httpClient;
           _databaseSpotifyTokenService = databaseSpotifyTokenService;
            _configurationAppSettingJSON = configurationAppSettingJSON;
        }
        /// <summary>
        /// save token from spotify that will allow me access spotify data to database
        /// </summary>
        /// <param name="code"> you wil get this code from action on SpotifyAppAuthorizationController </param>
        /// <param name="clientId"> account id that i got from registering app on https://developer.spotify.com  </param>
        /// <param name="clientSecret"> client secret that i got from registering app on https://developer.spotify.com </param>
   
        public async Task SaveNewTokenToDatabase(string code, string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                {"code",$"{code}" },
                { "redirect_uri","https://localhost:7115/Admin/Authorization/CallBack"}
            });
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            var spotifyToken = await JsonSerializer.DeserializeAsync<SpotifyToken>(responseStream);
            var seconds = spotifyToken.expires_in;
            spotifyToken.expires_at = DateTime.Now.AddSeconds(seconds);
            _databaseSpotifyTokenService.DropToken();
            _databaseSpotifyTokenService.AddToken(spotifyToken);




        }

        /// <summary>
        /// Refresh token that allow access spotify data
        /// </summary>
        /// <param name="refreshToken">expired old token </param>
        /// <param name="clientId">account id that i got from registering app on https://developer.spotify.com </param>
        /// <param name="clientSecret">secret that i got from registering app on https://developer.spotify.com </param>
     
        public async Task RefreshToken(string refreshToken, string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                {"refresh_token",$"{refreshToken}" },

            });
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            var spotifyToken = await JsonSerializer.DeserializeAsync<SpotifyToken>(responseStream);
            var seconds = spotifyToken.expires_in;
            spotifyToken.expires_at = DateTime.Now.AddSeconds(seconds);
            _databaseSpotifyTokenService.DropToken();
            _databaseSpotifyTokenService.AddToken(spotifyToken);


        }

        public async Task CheckTokenExpireDate()
        {
            var token = _databaseSpotifyTokenService.GetToken();
            if (token.refresh_token is not null)
            {
                if (token.expires_at <= DateTime.Now)
                {
                    RefreshToken(token.refresh_token, _configurationAppSettingJSON["Spotify:ClientId"], _configurationAppSettingJSON["Spotify:ClientSecret"]).Wait();
                }
            }
        }

    }
}
