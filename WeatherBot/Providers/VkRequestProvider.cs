namespace WeatherBot.Providers
{
    using System;
    using System.Net.Http;
    using WeatherBot.Models.LocalJson;
    using WeatherBot.Services;

    public class VkRequestProvider
    {
        public static HttpRequestMessage GetPostRequestMessage(string message, string photo)
        {
            var tokens = ExtensionService.GetModelJson<Token>();
            var token = tokens.Vk;
            var uri = new UriBuilder
            {
                Scheme = "https",
                Host = "api.vk.com",
                Path = "/method/wall.post",
                Query = $"owner_id=-202995818" +
                        $"&friends_only=0" +
                        $"&access_token={token}" +
                        $"&from_group=1" +
                        $"&lang=ru" +
                        $"&message={message}" +
                        $"&attachments={photo}" +
                        $"&v=5.130"
            };
            var request = new HttpRequestMessage(HttpMethod.Get, uri.Uri);
            return request;
        }

        public static HttpRequestMessage GetWallRequestMessage(int countPosts)
        {
            var uri = new UriBuilder
            {
                Scheme = "https",
                Host = "api.vk.com",
                Path = "/method/wall.get",
                Query = $"owner_id=-165339354" +
                        $"&filter=owner" +
                        $"&access_token=d88424725f96158bc7df248601fd51d70650e2cab2aeb625de9ba01622d65ce395d6ada484b78af371f1c" +
                        $"&count={countPosts}" +
                        $"&v=5.130"
            };
            var request = new HttpRequestMessage(HttpMethod.Get, uri.Uri);
            return request;
        }
    }
}
