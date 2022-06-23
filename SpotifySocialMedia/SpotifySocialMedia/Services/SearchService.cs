using SpotifySocialMedia.Areas.Admin.Services;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.SpotifySettingsDatabase.Services;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SpotifySocialMedia.Services
{
    public class SearchService:ISearchService
    {
        private readonly HttpClient _httpClient;
        private readonly ISpotifyTokenService _spotifyTokenService;
        private readonly IDatabaseSpotifyTokenService _databaseSpotifyTokenService;

        public SearchService(HttpClient httpClient
         ,ISpotifyTokenService spotifyTokenService
          ,IDatabaseSpotifyTokenService databaseSpotifyTokenService)
        {
           
            _httpClient = httpClient;
           _spotifyTokenService = spotifyTokenService;
            _databaseSpotifyTokenService = databaseSpotifyTokenService;
        }
        //public async Task<IEnumerable<SongItem>> SearchSong(string phrase)
        public async Task<SongItemsTrack> SearchSong(string phrase)
        {
            await _spotifyTokenService.CheckTokenExpireDate();
            string accessToken = _databaseSpotifyTokenService.GetToken().access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"?q={phrase}&type=track&market=ES&limit=5&offset=0");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<RootTracks>(responseStream);
            SongItemsTrack track = new SongItemsTrack();
            track.nextItemsPage = responseObject.tracks.next;
            List<Item> items = responseObject?.tracks?.items;
            List<SongItem> songItems = new List<SongItem>();
            foreach (var item in items)
            {
                songItems.Add(new SongItem { Id = item.id });
            }
           track.songItems = songItems;
            return track ;
        }
    }
}
