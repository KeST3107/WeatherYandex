namespace WeatherBot.Providers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using WeatherBot.Models.Vk.GetImage;
    using ActivatedType = WeatherBot.Enums.ActivatedType;
    using DayType = WeatherBot.Enums.DayType;

    public class ImageTypeProvider
    {
        public static string GetImageActual(ActivatedType activatedType, DayType dayType)
        {
            var images = DeserializeImages();
            if (dayType == DayType.Weekday)
                {
                    switch (activatedType)
                    {
                        case ActivatedType.NotActivated:
                            return images.ActivatedType.FactNotActiated;
                        case ActivatedType.OneToFive:
                            return images.ActivatedType.FactOneFive;
                        case ActivatedType.OneToEight:
                            return images.ActivatedType.FactOneEight;
                        case ActivatedType.OneToEleven:
                            return images.ActivatedType.FactOneEleven;
                    }
                }
                else
                {
                    switch (dayType)
                    {
                        case DayType.Weekend:
                            return images.DayType.Weekend;
                        case DayType.Holiday:
                            return images.DayType.Holiday;
                        case DayType.Vacation:
                            return images.DayType.Vacation;
                    }
                }
            return null;
        }


        public static string GetImageForecast(ActivatedType activatedType, DayType dayType)
        {
            var images = DeserializeImages();
            if (dayType == DayType.Weekday)
            {
                switch (activatedType)
                {
                    case ActivatedType.NotActivated:
                        return images.ActivatedType.ForecastNotActiated;
                    case ActivatedType.OneToFive:
                        return images.ActivatedType.ForecastOneFive;
                    case ActivatedType.OneToEight:
                        return images.ActivatedType.ForecastOneEight;
                    case ActivatedType.OneToEleven:
                        return images.ActivatedType.ForecastOneEleven;
                }
            }
            else
            {
                switch (dayType)
                {
                    case DayType.Weekend:
                        return images.DayType.Weekend;
                    case DayType.Holiday:
                        return images.DayType.Holiday;
                    case DayType.Vacation:
                        return images.DayType.Vacation;
                }
            }
            return null;
        }

        private static Image DeserializeImages()
        {
            StreamReader stream = new StreamReader("JsonFiles/ImagesVk.json");
            string json = stream.ReadToEnd();
            var images = JsonConvert.DeserializeObject<Image>(json);
            return images;
        }
    }
}
