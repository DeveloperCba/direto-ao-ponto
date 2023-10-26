using System.Text.RegularExpressions;

namespace ConsoleDependencyInjection.Helpers;

public static class StringExtension
{
    public static string ReturnNumberOnly(this string text)
    {
        return Regex.Replace(text, "[^0-9,]", "");
    }
}