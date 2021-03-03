namespace WeatherBot.JSONClasses
{
    public class Hour
    {
        public string hour { get; set; }
        public int hour_ts { get; set; }
        public int temp { get; set; }
        public int feels_like { get; set; }
        public string icon { get; set; }
        public string condition { get; set; }
        public double cloudness { get; set; }
        public int prec_type { get; set; }
        public double prec_strength { get; set; }
        public bool is_thunder { get; set; }
        public string wind_dir { get; set; }
        public double wind_speed { get; set; }
        public double wind_gust { get; set; }
        public int pressure_mm { get; set; }
        public int pressure_pa { get; set; }
        public int humidity { get; set; }
        public int uv_index { get; set; }
        public int soil_temp { get; set; }
        public double soil_moisture { get; set; }
        public double prec_mm { get; set; }
        public int prec_period { get; set; }
        public int prec_prob { get; set; }
    }
}
