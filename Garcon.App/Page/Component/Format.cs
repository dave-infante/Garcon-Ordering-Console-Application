namespace Garcon.App.Page.Component
{
    public static class Format
    {
        /// <summary>
        /// Display processing time with appropriate time unit.
        /// </summary>
        public static string FixPlaceholder(decimal value, string unit)
            => value < 2 ? $"{(int)value} {unit}" : $"{(int)value} {unit}s";

        /// <summary>
        /// Gets the proper string format of time in minutes
        /// </summary>
        public static string GetTimeUnitFormat(decimal minutes)
             => minutes * 60 == 0 ? "none" :
                minutes * 60 % 60 == 0 ? $"{FixPlaceholder(minutes, "minute")}" :
                minutes * 60 < 60 ? $"{FixPlaceholder(minutes * 60 % 60, "second")}" :
                $"{FixPlaceholder(minutes * 60 / 60, "minute")} {FixPlaceholder(minutes * 60 % 60, "second")}";

        /// <summary>
        /// Get money format with currency
        /// </summary>
        public static string GetMoneyFormat(decimal val)
            => $"PHP {string.Format("{0:0.00}", val)}";
    }
}
