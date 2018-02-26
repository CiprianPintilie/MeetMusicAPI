using Newtonsoft.Json;

namespace MeetMusicModels.Models
{
    public class MusicFamilyUpdateModel
    {
        [JsonProperty("id_style")]
        public int StyleId { get; set; }
        [JsonProperty("genre")]
        public string GenreName { get; set; }
        [JsonProperty("family")]
        public string GenreFamily { get; set; }
    }
}
