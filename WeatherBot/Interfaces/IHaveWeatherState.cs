namespace WeatherBot.Interfaces
{
    public interface IHaveWeatherState
    {
        public int Temperature { get; set; }

        public int FeelsLike { get; set; }

        public double WindSpeed { get; set; }

        public double WindGust { get; set; }

        public int Humidity { get; set; }

        public string WindDirection { get; set; }

        public double Cloudness { get; set; }

        public string Condition { get; set; }
    }
}
