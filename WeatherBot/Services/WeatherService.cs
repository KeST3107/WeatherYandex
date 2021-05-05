namespace WeatherBot.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using WeatherBot.Enums;
    using WeatherBot.Http;
    using WeatherBot.Models.Vk;
    using WeatherBot.Models.Yandex;
    using WeatherBot.Providers;

    public class WeatherService
    {
        private readonly WeatherStateProvider _weatherStateProvider;

        public WeatherService()
        {
            _weatherStateProvider = new WeatherStateProvider();
        }

        public async Task<PublicationWeather> GetWeatherWithForecastAsync()
        {
            var yandexWeatherRequest = YandexRequestProvider.GetWeatherRequestMessage(2);
            var weather = await HttpRequestSender.SendRequestAsync<Weather>(yandexWeatherRequest);

            var nextDayForecast = weather.Forecasts
                .Where(x => x.Date.Date == DateTime.Now.AddDays(1).Date)
                .FirstOrDefault();

            var (activatedType, activatedProbability) = GetActivatedProbability(nextDayForecast);

            return new PublicationWeather()
            {
                ActualWeather = _weatherStateProvider.GetCurrentWeatherState(weather.FactInfo),
                NextDayWeather = _weatherStateProvider.GetForecastWeatherState(nextDayForecast.Date, nextDayForecast.Hours[6]),
                ActivatedCondition = activatedProbability,
                ActivatedType = activatedType,
                Date = nextDayForecast.Date.Date
            };
        }

        public async Task<PublicationWeather> GetActualWeatherAsync()
        {
            var yandexWeatherRequest = YandexRequestProvider.GetWeatherRequestMessage(2);
            var weather = await HttpRequestSender.SendRequestAsync<Weather>(yandexWeatherRequest);

            var vkPostRequest = VkRequestProvider.GetWallRequestMessage(5);
            var wall = await HttpRequestSender.SendRequestAsync<WallResponse>(vkPostRequest);

            var (activatedType, activatedProbability) = GetActivatedProbability(wall);

            return new PublicationWeather()
            {
                ActualWeather = _weatherStateProvider.GetCurrentWeatherState(weather.FactInfo),
                NextDayWeather = null,
                ActivatedCondition = activatedProbability,
                ActivatedType = activatedType,
                Date = weather.DateNowUtc.Date
            };
        }

        private (ActivatedType, string) GetActivatedProbability(Forecast forecast)
        {
            var conditionsHour = new int[4];
            var graphDay = new int[46, 26]
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3}
            };

            var hour = 6;
            for (var i = 0; i < conditionsHour.Length; i++)
            {
                var temperature = Math.Abs(forecast.Hours[hour].Temperature);
                var speed = (int)(forecast.Hours[hour].WindSpeed + forecast.Hours[hour].WindGust) / 2;

                if (temperature > 41 || speed > 22)
                    conditionsHour[i] = 3;
                else
                    conditionsHour[i] = graphDay[temperature, speed];
                hour = hour + 3;
            }

            var whichClass = conditionsHour.Max();
            var probability = conditionsHour
                .Where(x => x == whichClass)
                .Count() * 25 - new Random().Next(1, 5);
            switch (whichClass)
            {
                case 1:
                    return (ActivatedType.OneToFive,
                        $"Актированный с 1 по 5 класс с вероятностью {probability}%");
                case 2:
                    return (ActivatedType.OneToEight,
                        $"Актированный с 1 по 8 класс с вероятностью {probability}%");
                case 3:
                    return (ActivatedType.OneToEleven,
                        $"Актированный с 1 по 11 класс, а также учащиеся 1 и 2 курсов с вероятностью {probability}%");
                default:
                    return (ActivatedType.NotActivated,
                        $"Актированного не будет с вероятностью {probability}%");
            }
        }

        private (ActivatedType, string) GetActivatedProbability(WallResponse wallResponse)
        {
            var dateNow = DateTimeOffset.Now;
            var unixBegin = dateNow.AddHours(-1).ToUnixTimeSeconds();
            var unixEnd = dateNow.AddHours(1).ToUnixTimeSeconds();
            var posts = wallResponse.Wall.Posts
                .Where(x => x.DateUnix > unixBegin && x.DateUnix < unixEnd)
                .ToList();
            foreach (var post in posts)
            {
                var textPost = post.Text;
                if (textPost.Contains("По данным"))
                {
                    if (textPost.Contains("с 1-го по 5-ый"))
                    {
                        return (ActivatedType.OneToFive,
                            $"Актированный с 1 по 5 класс (Информация ГО И ЧС)");
                    }

                    if (textPost.Contains("с 1-го по 8-ой"))
                    {
                        return (ActivatedType.OneToEight,
                            $"Актированный с 1 по 8 класс (Информация ГО И ЧС)");
                    }

                    if (textPost.Contains("с 1-го по 11-ый"))
                    {
                        return (ActivatedType.OneToEleven,
                            $"Актированный с 1 по 11 класс, а также учащиеся 1 и 2 (Информация ГО И ЧС)");
                    }
                    else
                    {
                        return (ActivatedType.NotActivated,
                            $"Актированного нету (Информация ГО И ЧС)");
                    }
                }
            }

            return (ActivatedType.NotActivated, null);
        }
    }
}
