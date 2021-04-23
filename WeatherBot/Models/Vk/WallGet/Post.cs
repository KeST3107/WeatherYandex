namespace WeatherBot.Models.Vk
{
    using System.Text.Json.Serialization;

    public class Post
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("from_id")]
        public int EditorId { get; set; }
        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }
        [JsonPropertyName("date")]
        public int DateUnix { get; set; }
        [JsonPropertyName("post_type")]
        public string Type { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
