namespace WeatherBot.Providers
{
    using System;
    using WeatherBot.Interfaces;
    using WeatherBot.Models.Yandex;

    public class WeatherStateProvider
    {
        public string GetForecastWeatherState(DateTime date, HourWeather hourWeather)
        {
            var weatherState = GetWeatherState(hourWeather);
            if (weatherState.Condition != String.Empty)
            {
                weatherState.Condition = ", " + weatherState.Condition;
            }

            return string.Format(
                $"На {date.ToShortDateString()} 6:00 ожидается:\n" +
                $"Температура: {weatherState.Temperature} " +
                $"ощущается на {weatherState.TemperatureFeelsLike}\n" +
                $"Ветер: {weatherState.WindDirection} {weatherState.WindSpeed} порывами до {weatherState.WindGust}\n" +
                $"Влажность: {weatherState.Humidity} Состояние: {weatherState.Cloudness}{weatherState.Condition} \n");
        }

        public string GetCurrentWeatherState(FactInfo factInfo)
        {
            var weatherState = GetWeatherState(factInfo);
            if (weatherState.Condition != String.Empty)
            {
                weatherState.Condition = ", " + weatherState.Condition;
            }

            return string.Format(
                $"Сейчас:\nТемпература: {weatherState.Temperature} " +
                $"ощущается на {weatherState.TemperatureFeelsLike}\n" +
                $"Ветер: {weatherState.WindDirection} {weatherState.WindSpeed} порывами до {weatherState.WindGust}\n" +
                $"Влажность: {weatherState.Humidity} Состояние: {weatherState.Cloudness}{weatherState.Condition} \n");
        }

        private WeatherState GetWeatherState(IHaveWeatherState model)
        {
            return new WeatherState
            {
                Temperature = model.Temperature + "°С",
                TemperatureFeelsLike = model.FeelsLike + "°С",
                WindSpeed = model.WindSpeed + " м/c",
                WindGust = model.WindGust + " м/c",
                Humidity = model.Humidity + "%",

                WindDirection = GetWeatherDirection(model.WindDirection),
                Cloudness = GetCloudness(model.Cloudness.ToString()),
                Condition = GetPrecipitation(model.Condition),
            };
        }

        private static string GetCloudness(string cloudness)
        {
            switch (cloudness)
            {
                case "0":
                    return "ясно";
                case "0,25":
                    return "малооблачно";
                case "0,5":
                    return "облачно с прояснениями";
                case "0,75":
                    return "облачно с прояснениями";
                case "1":
                    return "пасмурно";
                default:
                    return string.Empty;
            }
        }

        private static string GetPrecipitation(string condition)
        {
            switch (condition)
            {
                case "drizzle":
                    return "морось";
                case "light-rain":
                    return "небольшой дождь";
                case "rain":
                    return "дождь";
                case "moderate-rain":
                    return "умеренно сильный дождь";
                case "continuous-heavy-rain":
                    return "длительный сильный дождь";
                case "showers":
                    return "ливень";
                case "wet-snow":
                    return "дождь со снегом";
                case "light-snow":
                    return "небольшой снег";
                case "snow":
                    return "снег";
                case "snow-showers":
                    return "снегопад";
                case "hail":
                    return "град";
                case "thunderstorm":
                    return "гроза";
                case "thunderstorm-with-rain":
                    return "дождь с грозой";
                case "thunderstorm-with-hail":
                    return "гроза с градом";
                default:
                    return string.Empty;
            }
        }

        private string GetWeatherDirection(string windDirection)
        {
            switch (windDirection)
            {
                case "nw":
                    return "СЗ";
                case "n":
                    return "С";
                case "ne":
                    return "СВ";
                case "e":
                    return "В";
                case "se":
                    return "ЮВ";
                case "s":
                    return "Ю";
                case "sw":
                    return "ЮЗ";
                case "w":
                    return "З";
                case "c":
                    return "";
                default:
                    return string.Empty;
            }
        }

        private class WeatherState
        {
            public string Temperature { get; set; }

            public string TemperatureFeelsLike { get; set; }

            public string WindSpeed { get; set; }

            public string WindGust { get; set; }

            public string Humidity { get; set; }

            public string WindDirection { get; set; }

            public string Cloudness { get; set; }

            public string Condition { get; set; }
        }
    }
}
