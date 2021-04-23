namespace WeatherBot.Models.Vk
{
    using System;
    using WeatherBot.Enums;

    public class PublicationWeather
    {
        public string ActualWeather { get; set; }

        public string NextDayWeather { get; set; }

        public string ActivatedCondition { get; set; }

        public ActivatedType ActivatedType { get; set; }

        public DateTime Date { get; set; }
    }
}
