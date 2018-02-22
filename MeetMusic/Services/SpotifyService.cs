using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeetMusic.Services
{
    public class SpotifyService
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task Authenticate()
        {
            var client_id = "db770f92f6ef4f3f87df6c6ec279c934"; // Your client id
            var client_secret = "3246f992f0ec4c3fa71410ef32cf0bda"; // Your secret

            var encodedSecret = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{client_id}:{client_secret}")
            );

            var payload = new SpotifyAuthModel
            {
                Code = "",
                RedirectUri = "http://localhost:5000/",
                GrantType = "authorization_code"

            };

            var content = new StringContent(
                JsonConvert.SerializeObject(payload), 
                Encoding.UTF8, "application/json"
            );
            content.Headers.Add("Authorization", $"Basic {encodedSecret}");
            var response = await _client.PostAsync("https://accounts.spotify.com/api/token", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }

        private class SpotifyAuthModel
        {
            [JsonProperty("code")]
            public string Code { get; set; }
            [JsonProperty("redirect_uri")]
            public string RedirectUri { get; set; }
            [JsonProperty("grant_type")]
            public string GrantType { get; set; }
        }
    }
}
