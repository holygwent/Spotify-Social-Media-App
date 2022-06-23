namespace SpotifySocialMedia.Models
{
    public class SongItemsTrack
    {
        public List<SongItem> songItems { get; set; }
        public string nextItemsPage { get; set; }
        public string previosItemsPage { get; set; }
    }
}
