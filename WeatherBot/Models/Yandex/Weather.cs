namespace WeatherBot.Models.Yandex
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Weather
    {
        [JsonPropertyName("now")]
        public int DateNowUnix { get; set; }

        [JsonPropertyName("now_dt")]
        public DateTime DateNowUtc { get; set; }

        [JsonPropertyName("fact")]
        public FactInfo FactInfo { get; set; }

        [JsonPropertyName("forecasts")]
        public List<Forecast> Forecasts { get; set; }
    }
}
