namespace WeatherBot.Providers
{
    using System;
    using System.Net.Http;
    using WeatherBot.Models.LocalJson;
    using WeatherBot.Services;

    public class YandexRequestProvider
    {
        public static HttpRequestMessage GetWeatherRequestMessage(int limitDays)
        {
            var tokens = ExtensionService.GetModelJson<Token>();
            var token = tokens.Yandex;
            var uri = new UriBuilder
            {
                Scheme = "https",
                Host = "api.weather.yandex.ru",
                Path = "/v2/forecast",
                Query = $"lat=67.4988&lon=64.0525&lang=ru_RU&limit={limitDays}&hours=True&extra=False"
            };
            var request = new HttpRequestMessage(HttpMethod.Get, uri.Uri);
            request.Headers.Add("X-Yandex-API-Key", token);
            return request;
        }
    }
}
