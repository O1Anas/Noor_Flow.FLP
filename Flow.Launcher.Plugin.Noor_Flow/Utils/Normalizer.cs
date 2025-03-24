using System.Text.RegularExpressions;

namespace Flow.Launcher.Plugin.Noor_Flow
{
    public static class Normalizer
    {
        public static string Normalize(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
        
            // Remove diacritics, normalize Arabic characters, remove specific letters, and remove spaces
            text = Regex.Replace(text, @"[\u0621\u0622\u0623\u0625\u0626\u0627\u0670-\u0675\u064B-\u0652\u06D6-\u06DC\u06DF-\u06E8\u06EA-\u06ED]|\s|[^ء-ي]", "");

            System.Diagnostics.Debug.WriteLine($"Normalized text: {text}");
            return text;
        }
        public static string NormalizeLatin(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Remove apostrophes and spaces
            text = Regex.Replace(text, @"['\s]", "");

            // Remove Arabic prefixes like "Al-", "An-", etc.
            text = Regex.Replace(text, @"^(?:\s*A[lnrshzdt](-|\b)?)", "", RegexOptions.IgnoreCase);

            // Remove all Arabic letters (Unicode range: 0600-06FF)
            text = Regex.Replace(text, @"[\u0600-\u06FF]", "");

            return text;
        }
    }
}