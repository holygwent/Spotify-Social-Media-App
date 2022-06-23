namespace SpotifySocialMedia.Areas.Admin.Services
{
    public interface IAuthorizeService
    {
        string GetLinkToAuthorize();
    }
    public class AuthorizeService: IAuthorizeService
    {
        private readonly IConfiguration _configurationAppSettingJSON;
        public AuthorizeService(IConfiguration configurationAppSettingJSON)
        {
            _configurationAppSettingJSON = configurationAppSettingJSON;
        }
        public string GetLinkToAuthorize()
        {
            _configurationAppSettingJSON["Spotify:state"] = Guid.NewGuid().ToString();
            string url = $"https://accounts.spotify.com/authorize?" +
               $"client_id={_configurationAppSettingJSON["Spotify:ClientId"]}" +
               $"&response_type=code" +
               $"&redirect_uri=https://localhost:7115/Admin/Authorization/CallBack" +
               $"&state={_configurationAppSettingJSON["Spotify:state"]}" + 
               $"&scope=user-read-private user-read-email";//if error change on space %20 | add new needed scopes
            return url;
        }
    }
}
