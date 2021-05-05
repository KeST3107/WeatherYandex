namespace WeatherBot.Services
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using WeatherBot.Enums;
    using WeatherBot.Http;
    using WeatherBot.Models.Vk;
    using WeatherBot.Providers;

    public class SendService
    {
        private readonly WeatherService _weatherService;
        private readonly DayTypeProvider _dayTypeProvider;
        private readonly ImageTypeProvider _imageTypeProvider;

        public SendService()
        {
            _weatherService = new WeatherService();
            _dayTypeProvider = new DayTypeProvider();
            _imageTypeProvider = new ImageTypeProvider();
        }

        public async Task PostActualWeather()
        {
            var publicationWeather = await _weatherService.GetActualWeatherAsync();
            var dayType = _dayTypeProvider.GetDayType(publicationWeather.Date);
            var textPost = GetTextMessage(publicationWeather, dayType);
            var image = _imageTypeProvider.GetImageActual(publicationWeather.ActivatedType, dayType);

            var vkPostRequest = VkRequestProvider.GetPostRequestMessage(textPost, image);
            await TelegramService.PostMessage(textPost, image);
            await HttpRequestSender.SendRequestAsync<PublishResponse>(vkPostRequest);
        }

        public async Task PostWeatherWithForecast()
        {
            var publicationWeather = await _weatherService.GetWeatherWithForecastAsync();
            var dayType = _dayTypeProvider.GetDayType(publicationWeather.Date);
            var textPost = GetTextMessage(publicationWeather, dayType);
            var image = _imageTypeProvider.GetImageForecast(publicationWeather.ActivatedType, dayType);

            var vkPostRequest = VkRequestProvider.GetPostRequestMessage(textPost, image);
            await TelegramService.PostMessage(textPost, image);
            await HttpRequestSender.SendRequestAsync<PublishResponse>(vkPostRequest);
        }

        private string GetTextMessage(PublicationWeather publicationWeather, DayType dayType)
        {
            var nextDayWeather = publicationWeather.NextDayWeather;
            if (nextDayWeather != null)
            {
                nextDayWeather += "\n";
            }

            var activatedCondition = publicationWeather.ActivatedCondition;
            if (dayType != DayType.Weekday)
            {
                activatedCondition = ExtensionService.GetEnumDescription(dayType);
            }

            var message = $"{publicationWeather.ActualWeather}\n" +
                          $"{nextDayWeather}" +
                          $"{activatedCondition}";
            return message;
        }
    }
}
