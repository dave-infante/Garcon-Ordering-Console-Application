using System;

namespace Garcon.Data.Helper
{
    public static class Parse
    {
        public static TEnum ToEnum<TEnum>(dynamic value)
        {
            if (value is null)
            {
                throw new Exception("Enum value cannot be null.");
            }

            if (!Enum.TryParse(typeof(TEnum), value.ToString(), out object res))
            {
                throw new Exception("Enum value cannot be found");
            }

            return (TEnum)res;
        }

        public static decimal ToDecimal(dynamic value, decimal defaultValue = 0)
            => decimal.TryParse(value.ToString(), out decimal res) ? res : defaultValue;

        public static int ToInt(dynamic value, int defaultValue = 0)
            => int.TryParse(value.ToString(), out int res) ? res : defaultValue;

        public static bool ToBool(dynamic value, bool defaultValue = false)
            => bool.TryParse(value.ToString(), out bool res) ? res : defaultValue;

        public static string ToString(dynamic value, string defaultValue = "")
            => value is null ? defaultValue : value.ToString();


    }


    


}
