namespace WeatherBot.Services
{
    using System;
    using System.IO;
    using System.ComponentModel;
    using System.Text.Json;
    using System.Runtime.Caching;
    using WeatherBot.Models.LocalJson;

    public static class ExtensionService
    {
        public static string GetEnumDescription(Enum enumType)
        {
            System.Reflection.FieldInfo field = enumType.GetType().GetField(enumType.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])field
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            return enumType.ToString();
        }


        public static void DeserializeAppSettings()
        {
            var cache = MemoryCache.Default;
            var jsonFile = File.ReadAllText(
                $"C:/Users/KeST3107/RiderProjects/WeatherYandex/WeatherBot/JsonFiles/AppSettings.json");
            var deserialize = JsonSerializer.Deserialize<AppSettings>(jsonFile);
            cache.Set("AppSettings", deserialize, new CacheItemPolicy());
            cache.Set("Image", deserialize.Image, new CacheItemPolicy());
            cache.Set("Token", deserialize.Token, new CacheItemPolicy());
            cache.Set("TaskConfig", deserialize.TaskConfig, new CacheItemPolicy());
        }

        public static T GetModelJson<T>()
            where T : class
        {
            var cache = MemoryCache.Default;
            var className = typeof(T).Name;
            var cacheItems = cache[className] as T;
            return cacheItems;
        }
    }
}
