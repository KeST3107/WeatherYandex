namespace WeatherBot.JSONClasses
{
    public class GeoObject
    {
        public object district { get; set; }
        public Locality locality { get; set; }
        public Province province { get; set; }
        public Country country { get; set; }
    }
}
