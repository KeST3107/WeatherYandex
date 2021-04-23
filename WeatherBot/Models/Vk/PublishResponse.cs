namespace WeatherBot.Models.Vk
{
    using System.Text.Json.Serialization;

    public class PublishResponse
    {
        [JsonPropertyName("response")]
        public Post Response { get; set; }


        public class Post
        {
            [JsonPropertyName("post_id")]
            public int Id { get; set; }
        }
    }


}
