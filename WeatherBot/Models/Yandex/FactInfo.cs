namespace WeatherBot.Models.Yandex
{
    using System.Text.Json.Serialization;
    using WeatherBot.Interfaces;

    public class FactInfo : IHaveWeatherState
    {
        [JsonPropertyName("temp")]
        public int Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public int FeelsLike { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; }

        [JsonPropertyName("cloudness")]
        public double Cloudness { get; set; }

        [JsonPropertyName("prec_type")]
        public int PrecipitationType { get; set; }

        [JsonPropertyName("prec_strength")]
        public double PrecipitationStrength { get; set; }

        [JsonPropertyName("is_thunder")]
        public bool IsThunder { get; set; }

        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("wind_dir")]
        public string WindDirection { get; set; }

        [JsonPropertyName("wind_gust")]
        public double WindGust { get; set; }

        [JsonPropertyName("pressure_mm")]
        public int PressureInMm { get; set; }

        [JsonPropertyName("pressure_pa")]
        public int PressureInPa { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("daytime")]
        public string TimesOfDay { get; set; }

        [JsonPropertyName("polar")]
        public bool IsPolar { get; set; }

        [JsonPropertyName("season")]
        public string Season { get; set; }
    }
}
