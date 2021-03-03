using System;

namespace WeatherBot
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading.Tasks;
    using WeatherBot.JSONClasses;

    class Program
    {
        static async Task Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.weather.yandex.ru/v2/forecast");
            client.DefaultRequestHeaders.Add("X-Yandex-API-Key", "6dee4b55-34bd-47cb-8fe6-9dce4982bbf5");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = GetRequestUrl();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var str = await response.Content.ReadAsStringAsync();
            var deserializeData = JsonSerializer.Deserialize<Root>(str);

            var dateNow = DateTime.Now;
            var nextDate = dateNow.AddDays(1);
            int[] conditionsHour = new int[4];
            int hour = 6;
            var forecast = deserializeData.forecasts.Where(x => x.date.Date == nextDate.Date).FirstOrDefault();
            for (int i = 0; i < conditionsHour.Length; i++)
            {
                var forecastHour = forecast.hours[hour];
                var temperature = Math.Abs(forecastHour.temp);
                var speed = (int)(forecastHour.wind_speed + forecastHour.wind_gust) / 2;
                hour = hour + 3;
                conditionsHour[i] = Calculate.DetermineCondition(temperature, speed);
            }

            Console.WriteLine(Calculate.WhichClassOut(conditionsHour));
            Console.ReadLine();
        }

        public static string GetRequestUrl()
        {
            var amp = "&";
            var lat = 67.4988;
            var lon = 64.0525;
            var lang = "ru_RU";
            var limit = 2;
            var hours = true;
            var extra = false;
            return "?lat=" + lat + amp
                   + "lon=" + lon + amp
                   + "lang=" + lang + amp
                   + "limit=" + limit + amp
                   + "hours=" + hours + amp
                   + "extra=" + extra;
        }
    }
}
