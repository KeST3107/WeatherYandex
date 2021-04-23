namespace WeatherBot.Models.Vk
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class WallResponse
    {
        [JsonPropertyName("response")]
        public WallItems Wall { get; set; }
    }

    public class WallItems
    {
        [JsonPropertyName("items")]
        public List<Post> Posts { get; set; }
    }
}
