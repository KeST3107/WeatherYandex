namespace WeatherBot.Models.Yandex
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Forecast
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("date_ts")]
        public int DateUnix { get; set; }

        [JsonPropertyName("week")]
        public int Week { get; set; }

        [JsonPropertyName("sunrise")]
        public string Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public string Sunset { get; set; }

        [JsonPropertyName("moon_code")]
        public int MoonConditionCode { get; set; }

        [JsonPropertyName("moon_text")]
        public string MoonConditionText { get; set; }

        [JsonPropertyName("hours")]
        public List<HourWeather> Hours { get; set; }
    }
}
