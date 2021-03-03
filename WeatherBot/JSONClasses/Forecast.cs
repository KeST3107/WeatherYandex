namespace WeatherBot.JSONClasses
{
    using System;
    using System.Collections.Generic;

    public class Forecast
    {
        public DateTime date { get; set; }
        public int date_ts { get; set; }
        public int week { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string rise_begin { get; set; }
        public string set_end { get; set; }
        public int moon_code { get; set; }
        public string moon_text { get; set; }
        public List<Hour> hours { get; set; }
    }
}
