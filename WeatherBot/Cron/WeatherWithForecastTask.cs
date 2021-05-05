namespace WeatherBot.Cron
{
    using WeatherBot.Services;
    using System.Threading;
    using System.Threading.Tasks;
    using CronScheduler.Extensions.Scheduler;
    public class WeatherWithForecastTask : IScheduledJob
    {
        private readonly SendService _sendService;
        public string Name { get; }

        public WeatherWithForecastTask(string taskName)
        {
            _sendService = new SendService();
            Name = taskName;
        }


        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _sendService.PostWeatherWithForecast();
            await Task.CompletedTask;
        }
    }
}
