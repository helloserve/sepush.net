using System;
using System.Globalization;

namespace helloserve.SePush
{
    internal static class Extensions
    {
        public static Tuple<string, string> AsQueryParam(this string value, string name)
        {
            return new Tuple<string, string>(name, value);
        }

        public static Tuple<string, string> AsQueryParam(this double value, string name)
        {
            return new Tuple<string, string>(name, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }
    }
}
