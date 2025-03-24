using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flow.Launcher.Plugin.Noor_Flow
{
    public class Surah
    {
        [JsonPropertyName("s")]
        public int Id { get; set; }

        [JsonPropertyName("n")]
        public string Name { get; set; }

        [JsonPropertyName("l")]
        public string Transliteration { get; set; }

        [JsonPropertyName("p")]
        public string Type { get; set; }

        [JsonPropertyName("v")]
        public int TotalVerses { get; set; }

        [JsonPropertyName("x")]
        public List<Verse> Verses { get; set; }
    }

    public class Verse
    {
        [JsonPropertyName("s")] // Only for ayahs.json
        public int SId { get; set; }

        [JsonPropertyName("a")]
        public int Id { get; set; }

        [JsonPropertyName("t")]
        public string Text { get; set; }

        [JsonPropertyName("n")] // directly for quran.json and ayahs.json
        public string Name { get; set; }

        [JsonPropertyName("m")]
        public string NormalizedText { get; set; }
    }
    public class SurahAyahPair
    {
        public Surah Surah { get; set; }
        public Verse Verse { get; set; }

        public SurahAyahPair(Surah surah, Verse verse)
        {
            Surah = surah;
            Verse = verse;
        }
    }

}
