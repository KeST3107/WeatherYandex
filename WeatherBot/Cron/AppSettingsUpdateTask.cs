namespace WeatherBot.Cron
{
    using System.Threading;
    using System.Threading.Tasks;
    using CronScheduler.Extensions.Scheduler;
    using WeatherBot.Services;

    public class AppSettingsUpdateTask : IScheduledJob
    {
        public string Name { get; } = nameof(AppSettingsUpdateTask);


        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            ExtensionService.DeserializeAppSettings();
            await Task.CompletedTask;
        }


        public class AppSettingsUpdateTaskOptions : SchedulerOptions
        {
            public string CustomField { get; set; } = string.Empty;
        }
    }
}
