namespace WeatherBot.JSONClasses
{
    using System;
    using System.Collections.Generic;

    public class Root
    {
        public int now { get; set; }
        public DateTime now_dt { get; set; }
        public Info info { get; set; }
        public GeoObject geo_object { get; set; }
        public Yesterday yesterday { get; set; }
        public Fact fact { get; set; }
        public List<Forecast> forecasts { get; set; }
    }
}
