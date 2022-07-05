using Database.Entities;
using SpotifySocialMedia.Areas.Admin.Services;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.Services.Repositories.Interfaces;
using SpotifySocialMedia.SpotifySettingsDatabase.Services;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SpotifySocialMedia.Services.Repositories
{
    public class ArtistRepository: IArtistRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ISpotifyTokenService _spotifyTokenService;
        private readonly IDatabaseSpotifyTokenService _databaseSpotifyTokenService;
        private readonly ApplicationDbContext _applicationDbContext;


        public ArtistRepository(HttpClient httpClient
         , ISpotifyTokenService spotifyTokenService
          , IDatabaseSpotifyTokenService databaseSpotifyTokenService
          ,ApplicationDbContext  applicationDbContext )
        {

            _httpClient = httpClient;
            _spotifyTokenService = spotifyTokenService;
            _databaseSpotifyTokenService = databaseSpotifyTokenService;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<string> AddArtist(string songId)
        {
             await _spotifyTokenService.CheckTokenExpireDate();
            string accessToken = _databaseSpotifyTokenService.GetToken().access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"tracks/{songId}");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<SpotifySong>(responseStream);
            string artistId;
            artistId = responseObject.artists.FirstOrDefault().id;
            if (artistId is not null)
            {
                 response = await _httpClient.GetAsync($"artists/{artistId}");
                response.EnsureSuccessStatusCode();
                using var responseStream2 = await response.Content.ReadAsStreamAsync();
                var responseObjectArtist = await JsonSerializer.DeserializeAsync<SpotifyArtist>(responseStream2);
                string genres = "";
                genres = String.Join(",", responseObjectArtist.genres);

               var artist =  new Artist() { Id = responseObjectArtist.id, genres = genres };
                await _applicationDbContext.Artists.AddAsync(artist);
                return artistId;
            }
            return "";

        }
    }
}
