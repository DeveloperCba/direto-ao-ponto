using System.Text.RegularExpressions;

namespace ConsoleEntityFrameworkCore.Extensions
{
    public static class StringExtension
    {
        public static string ToSnakeCase(this string name)
            => Regex.Replace(
                name,
                @"([a-z0-9])([A-Z])",
                "$1_$2").ToLower();
    }
}
