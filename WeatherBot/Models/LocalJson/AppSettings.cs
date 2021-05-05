namespace WeatherBot.Models.LocalJson
{
    public class AppSettings
    {
        public TaskConfig TaskConfig { get; set; }
        public Token Token { get; set; }
        public Image Image { get; set; }
    }

    public class TaskConfig
    {
        public string ActualWeatherTask { get; set; }
        public string WeatherWithForecastTask1 { get; set; }
        public string WeatherWithForecastTask2 { get; set; }
        public string WeatherWithForecastTask3 { get; set; }

        public string AppSettingsTask { get; set; }
    }

    public class Token
    {
        public string Vk { get; set; }
        public string Yandex { get; set; }
        public string Telegram { get; set; }
    }

    public class ActivatedType
    {
        public string ForecastNotActiated { get; set; }
        public string ForecastOneFive { get; set; }
        public string ForecastOneEight { get; set; }
        public string ForecastOneEleven { get; set; }
        public string FactNotActiated { get; set; }
        public string FactOneFive { get; set; }
        public string FactOneEight { get; set; }
        public string FactOneEleven { get; set; }
    }

    public class DayType
    {
        public string Weekend { get; set; }
        public string Holiday { get; set; }
        public string Vacation { get; set; }
    }

    public class Image
    {
        public ActivatedType ActivatedType { get; set; }
        public DayType DayType { get; set; }
    }
}
