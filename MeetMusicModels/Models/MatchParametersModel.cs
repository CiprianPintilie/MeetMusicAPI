using Newtonsoft.Json;

namespace MeetMusicModels.Models
{
    public class MatchParametersModel
    {
        [JsonProperty("gender")]
        public int Gender { get; set; }
        [JsonProperty("radius")]
        public double Radius { get; set; }
    }
}
