namespace SpotifySocialMedia.Models
{
    public class NotificationDbo
    {
        public Guid Id { get; set; }
        public string SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
