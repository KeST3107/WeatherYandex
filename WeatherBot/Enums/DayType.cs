namespace WeatherBot.Enums
{
    using System.ComponentModel;

    public enum DayType
    {
        /// <summary>
        /// Будний
        /// </summary>
        Weekday,

        /// <summary>
        /// Праздник
        /// </summary>
        [Description("ЗАВТРА ПРАЗДНИЧНЫЙ ДЕНЬ! ОТДЫХАЕМ И НАБИРАЕМСЯ СИЛ (:")]
        Holiday,

        /// <summary>
        /// Выходной
        /// </summary>
        [Description("ЗАВТРА ВЫХОДНОЙ ДЕНЬ! ОТДЫХАЕМ И НАБИРАЕМСЯ СИЛ (:")]
        Weekend,

        /// <summary>
        /// Каникулы
        /// </summary>
        [Description("КАНИКУЛЫ! ОТДЫХАЕМ И НАБИРАЕМСЯ СИЛ (:")]
        Vacation
    }
}
