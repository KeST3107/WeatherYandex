namespace WeatherBot
{
    using System;
    using System.Threading.Tasks;
    using WeatherBot.Http;
    using WeatherBot.Models.Vk;
    using WeatherBot.Providers;
    using WeatherBot.Services;

    internal class Program
    {
        private static async Task Main()
        {
            var probability = WeatherService.GetActivatedProbability(nextDayForecast)
                .ToUpper();

            var vk = await HttpRequestSender.SendRequestAsync<PublishResponse>(VkRequestProvider.GetPostRequestMessage(factForecast + nextForecast + probability,
                "photo71827838_457258993"));
            Console.WriteLine(vk.Response.Id);
            /*
             * @TODO
             * Сделать класс получения id фотки по типу актировки и дня недели +
             * Заменять сообщение об актировке если день праздничный, выходной, каникулы +
             * Вынести логику выкладывания поста в VkService +
             * Переделать под вебсервис
             * Выделить интерфейсы для провайдеров и сервисов
             * Зарегистрировать интерфейсы и их реализации в DI контейнер
             * Добавить Cron и переделать постинг под его таймер
             */
        }
    }
}
