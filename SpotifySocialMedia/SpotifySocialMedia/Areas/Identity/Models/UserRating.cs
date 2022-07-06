namespace SpotifySocialMedia.Areas.Identity.Models
{
    public class UserRating
    {
        public int Rate { get; set; }
        public string SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }

    }
}
