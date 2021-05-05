namespace WeatherBot
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WeatherBot.Cron;
    using WeatherBot.Models.LocalJson;
    using WeatherBot.Services;

    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(services =>
                {

                    services.AddScheduler(ctx =>
                    {
                        ExtensionService.DeserializeAppSettings();
                        var configs = ExtensionService.GetModelJson<TaskConfig>();
                        ctx.AddJob<AppSettingsUpdateTask, AppSettingsUpdateTask.AppSettingsUpdateTaskOptions>(c =>
                        {
                            c.CronSchedule = configs.AppSettingsTask;
                            c.RunImmediately = false;
                        });
                        ctx.AddJob<ActualWeatherTask, ActualWeatherTask.ActualWeatherTaskOptions>(c =>
                        {
                            c.CronSchedule = configs.ActualWeatherTask;
                            c.RunImmediately = false;
                        });

                        var jobName1 = "WeatherWithForecastTask1";
                        ctx.AddJob(
                            sp => { return new WeatherWithForecastTask(jobName1); },
                            options =>
                            {
                                options.CronSchedule = configs.WeatherWithForecastTask1;
                                options.RunImmediately = false;
                            },
                            jobName: jobName1);

                        var jobName2 = "WeatherWithForecastTask2";
                        ctx.AddJob(
                            sp => { return new WeatherWithForecastTask(jobName2); },
                            options =>
                            {
                                options.CronSchedule = configs.WeatherWithForecastTask2;
                                options.RunImmediately = false;
                            },
                            jobName: jobName2);

                        var jobName3 = "WeatherWithForecastTask3";
                        ctx.AddJob(
                            sp => { return new WeatherWithForecastTask(jobName3); },
                            options =>
                            {
                                options.CronSchedule = configs.WeatherWithForecastTask3;
                                options.RunImmediately = true;
                            },
                            jobName: jobName3);
                        ctx.UnobservedTaskExceptionHandler = UnobservedHandler;
                    });
                });
        }

        private static void UnobservedHandler(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception?.GetBaseException());
            e.SetObserved();
        }
    }
}
