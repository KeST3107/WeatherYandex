namespace WeatherBot.JSONClasses
{
    public class Fact
    {
        public int obs_time { get; set; }
        public int uptime { get; set; }
        public int temp { get; set; }
        public int feels_like { get; set; }
        public string icon { get; set; }
        public string condition { get; set; }
        public int cloudness { get; set; }
        public int prec_type { get; set; }
        public int prec_prob { get; set; }
        public double prec_strength { get; set; }
        public bool is_thunder { get; set; }
        public double wind_speed { get; set; }
        public string wind_dir { get; set; }
        public int pressure_mm { get; set; }
        public int pressure_pa { get; set; }
        public int humidity { get; set; }
        public string daytime { get; set; }
        public bool polar { get; set; }
        public string season { get; set; }
        public string source { get; set; }
        public double soil_moisture { get; set; }
        public int soil_temp { get; set; }
        public int uv_index { get; set; }
        public double wind_gust { get; set; }
    }
}
