namespace WeatherBot.Services
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using WeatherBot.Enums;
    using WeatherBot.Http;
    using WeatherBot.Models.Vk;
    using WeatherBot.Providers;

    public class VkService
    {
        private readonly WeatherService _weatherService;
        private readonly DayTypeProvider _dayTypeProvider;

        public VkService()
        {
            _weatherService = new WeatherService();
        }

        public async Task PostActualWeather()
        {
            var post = await _weatherService.GetActualWeather();
            var textPost = GetTextMessage(post);
            var dayType = DayTypeProvider.GetDayType(post.Date);
            var image = ImageTypeProvider.GetImageActual(post.ActivatedType, dayType);

            var vkPostRequest = VkRequestProvider.GetPostRequestMessage(textPost, image);
            var vkResponse = await HttpRequestSender.SendRequestAsync<PublishResponse>(vkPostRequest);
        }

        public async Task PostWeatherWithForecast()
        {
            var post = await _weatherService.GetWeatherWithForecastAsync();
            var textPost = GetTextMessage(post);
            var dayType = DayTypeProvider.GetDayType(post.Date);
            var image = ImageTypeProvider.GetImageForecast(post.ActivatedType, dayType);

            var vkPostRequest = VkRequestProvider.GetPostRequestMessage(textPost, image);
            var vkResponse = await HttpRequestSender.SendRequestAsync<PublishResponse>(vkPostRequest);
        }

        private string GetTextMessage(PublicationWeather publicationWeather)
        {
            var nextDayWeather = publicationWeather.NextDayWeather;
            if (nextDayWeather != null)
            {
                nextDayWeather += "\n";
            }

            var dayType = DayTypeProvider.GetDayType(publicationWeather.Date);
            var activatedCondition = publicationWeather.ActivatedCondition;
            if (dayType != DayType.Weekday)
            {
                activatedCondition = GetEnumDescription(dayType);
            }

            var message = $"{publicationWeather.ActualWeather}\n" +
                          $"{nextDayWeather}" +
                          $"{activatedCondition}";
            return message;
        }

        public static string GetEnumDescription(DayType dayType)
        {
            System.Reflection.FieldInfo field = dayType.GetType().GetField(dayType.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])field
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            return dayType.ToString();
        }
    }
}
