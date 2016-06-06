using System;

namespace PyTaskBot.Infrastructure
{
    public static class StringExtension
    {
        public static bool ContainsIgnoreCase(this string source, string sub)
        {
            return source.IndexOf(sub, StringComparison.OrdinalIgnoreCase) != -1;
        }
    }
}