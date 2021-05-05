namespace WeatherBot.Providers
{
    using System.IO;
    using System.Runtime.Caching;
    using System.Text.Json;
    using WeatherBot.Models.LocalJson;
    using WeatherBot.Services;
    using ActivatedType = WeatherBot.Enums.ActivatedType;
    using DayType = WeatherBot.Enums.DayType;

    public class ImageTypeProvider
    {
        public string GetImageActual(ActivatedType activatedType, DayType dayType)
        {
            var imageNames = ExtensionService.GetModelJson<Image>();
            if (dayType == DayType.Weekday)
            {
                switch (activatedType)
                {
                    case ActivatedType.NotActivated:
                        return imageNames.ActivatedType.FactNotActiated;
                    case ActivatedType.OneToFive:
                        return imageNames.ActivatedType.FactOneFive;
                    case ActivatedType.OneToEight:
                        return imageNames.ActivatedType.FactOneEight;
                    case ActivatedType.OneToEleven:
                        return imageNames.ActivatedType.FactOneEleven;
                }
            }
            else
            {
                switch (dayType)
                {
                    case DayType.Weekend:
                        return imageNames.DayType.Weekend;
                    case DayType.Holiday:
                        return imageNames.DayType.Holiday;
                    case DayType.Vacation:
                        return imageNames.DayType.Vacation;
                }
            }

            return null;
        }


        public string GetImageForecast(ActivatedType activatedType, DayType dayType)
        {
            var imageNames = ExtensionService.GetModelJson<Image>();
            if (dayType == DayType.Weekday)
            {
                switch (activatedType)
                {
                    case ActivatedType.NotActivated:
                        return imageNames.ActivatedType.ForecastNotActiated;
                    case ActivatedType.OneToFive:
                        return imageNames.ActivatedType.ForecastOneFive;
                    case ActivatedType.OneToEight:
                        return imageNames.ActivatedType.ForecastOneEight;
                    case ActivatedType.OneToEleven:
                        return imageNames.ActivatedType.ForecastOneEleven;
                }
            }
            else
            {
                switch (dayType)
                {
                    case DayType.Weekend:
                        return imageNames.DayType.Weekend;
                    case DayType.Holiday:
                        return imageNames.DayType.Holiday;
                    case DayType.Vacation:
                        return imageNames.DayType.Vacation;
                }
            }

            return null;
        }
    }
}
