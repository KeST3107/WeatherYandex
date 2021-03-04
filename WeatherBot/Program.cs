using System;

namespace WeatherBot
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using WeatherBot.JSONClasses;

    class Program
    {
        static async Task Main()
        {
            using var yandexApi = new HttpClient();
            yandexApi.BaseAddress = new Uri("https://api.weather.yandex.ru/v2/forecast");
            yandexApi.DefaultRequestHeaders.Add("X-Yandex-API-Key", "6dee4b55-34bd-47cb-8fe6-9dce4982bbf5");
            yandexApi.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = GetRequestUrlYandex();
            var responseYandex = await yandexApi.GetAsync(url);
            responseYandex.EnsureSuccessStatusCode();
            var str = await responseYandex.Content.ReadAsStringAsync();
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

            using var vkApi = new HttpClient();
            vkApi.BaseAddress = new Uri("https://api.vk.com/method/");
            var message = string.Format("На {0} {1}:00 ожидается:\nТемпература: {2}\nВетер: {3} м/с ", forecast.date.Date.ToLongDateString(), forecast.hours[6].hour, forecast.hours[6].temp, forecast.hours[6].wind_speed, forecast.hours[6].wind_dir);
            message = message + string.Format("\n{0} ",Calculate.WhichClassOut(conditionsHour));
            url = WallPostVk(message);
            var responseVk = await vkApi.GetAsync(url);
            var responseBody = await responseVk.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }

        public static string GetRequestUrlYandex()
        {
            var amp = "&";
            var lat = "67.4988";
            var lon = "64.0525";
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

        public static string WallPostVk(string textMessage)
        {
            var amp = "&";
            var message = textMessage;
            var lang = "ru";
            //var lat = 67.4988;
            //var lon = 64.0525;
            string v = "5.130";
            string image = "photo71827838_457259137";

            return
                "wall.post?owner_id=-202995818&friends_only=0&access_token=d88424725f96158bc7df248601fd51d70650e2cab2aeb625de9ba01622d65ce395d6ada484b78af371f1c&from_group=1" +
                amp
                //+ "lat=" + lat + amp
                //+ "long=" + lon + amp
                + "lang=" + lang + amp
                + "message=" + message + amp
                + "attachments=" + image + amp
                + "v=" + v;
        }
    }
}
