using System;
using Newtonsoft.Json;

namespace TvLeanback
{
    public class MoviesResponse {
        public MovieJson[] Results { get; set; }
    }

    public class MovieJson
    {
        public int Id { get; set; }

        public String Title { get; set; }

        [JsonProperty("background_image")]
        public String BackgroundImage { get; set; }

        [JsonProperty("card_image")]
        public String CardImage { get; set; }

        [JsonProperty("video_url")]
        public String VideoUrl { get; set; }
    }
}
