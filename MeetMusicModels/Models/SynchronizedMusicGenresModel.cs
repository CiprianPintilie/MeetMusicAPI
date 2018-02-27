using Newtonsoft.Json;

namespace MeetMusicModels.Models
{
    public class SynchronizedMusicGenresModel
    {
        [JsonProperty("genres")]
        public string[] Genres { get; set; }
    }
}
