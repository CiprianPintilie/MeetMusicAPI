namespace MeetMusicModels.Models
{
    public class SpotifyArtistModel
    {
        public string Id { get; set; }
        public string[] Genres { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Uri { get; set; }
    }

    //[ {
    //    "external_urls" : {
    //        "spotify" : "https://open.spotify.com/artist/0E76QDtyHZgw7i5l7mdn5K"
    //    },
    //    "followers" : {
    //        "href" : null,
    //        "total" : 7256
    //    },
    //    "genres" : [ "compositional ambient", "post rock" ],
    //    "href" : "https://api.spotify.com/v1/artists/0E76QDtyHZgw7i5l7mdn5K",
    //    "id" : "0E76QDtyHZgw7i5l7mdn5K",
    //    "images" : [ {
    //        "height" : 640,
    //        "url" : "https://i.scdn.co/image/c334b78e2b1d90f9de2b4b30e8db58f8a23b9cac",
    //        "width" : 640
    //    }, {
    //        "height" : 320,
    //        "url" : "https://i.scdn.co/image/8fa6321452e70b5854d537eee1d596578e201a9c",
    //        "width" : 320
    //    }, {
    //        "height" : 160,
    //        "url" : "https://i.scdn.co/image/de76bf3a02e896adb4e7a0d7f8788dd9a2bd5ddc",
    //        "width" : 160
    //    } ],
    //    "name" : "Epic45",
    //    "popularity" : 45,
    //    "type" : "artist",
    //    "uri" : "spotify:artist:0E76QDtyHZgw7i5l7mdn5K"
    //} ]
}
