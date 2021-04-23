namespace WeatherBot.Http
{
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    public static class HttpRequestSender
    {
        public static async Task<TResponse> SendRequestAsync<TResponse>(HttpRequestMessage requestMessage)
            where TResponse : class
        {
            using var client = new HttpClient();
            var response = await client.SendAsync(requestMessage);
            var str = await response.Content.ReadAsStringAsync();
            var deserializeData = JsonSerializer.Deserialize<TResponse>(str);
            return deserializeData;
        }
    }
}
