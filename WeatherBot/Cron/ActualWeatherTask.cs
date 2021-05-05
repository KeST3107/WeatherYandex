namespace WeatherBot.Cron
{
    using System.Threading;
    using System.Threading.Tasks;
    using CronScheduler.Extensions.Scheduler;
    using WeatherBot.Services;
    public class ActualWeatherTask : IScheduledJob
    {
        private readonly SendService _sendService;

        public string Name { get; } = nameof(ActualWeatherTask);

        public ActualWeatherTask()
        {
            _sendService = new SendService();
        }


        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _sendService.PostActualWeather();
            await Task.CompletedTask;
        }


        public class ActualWeatherTaskOptions : SchedulerOptions
        {
            public string CustomField { get; set; } = string.Empty;
        }
    }
}
