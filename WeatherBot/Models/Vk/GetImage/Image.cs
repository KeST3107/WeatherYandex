namespace WeatherBot.Models.Vk.GetImage
{

        public class Image
        {
            public ActivatedType ActivatedType { get; set; }
            public DayType DayType { get; set; }
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
}
