namespace WeatherBot.Services
{
    using System.Threading.Tasks;
    using Telegram.Bot;
    using WeatherBot.Models.LocalJson;

    public class TelegramService
    {
        public static async Task PostMessage(string textPost, string image)
        {
            var tokens = ExtensionService.GetModelJson<Token>();
            var token = tokens.Telegram;
            var botClient = new TelegramBotClient(token);
            await botClient.SendPhotoAsync(
                chatId: -1001415542126,
                photo: $"https://vk.com/albums-202995818?z={image}",
                caption: $"{textPost}"
            );
        }
    }
}
