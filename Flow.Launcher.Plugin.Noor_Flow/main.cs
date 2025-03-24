using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Flow.Launcher.Plugin;
using Flow.Launcher.Plugin.Noor_Flow.Settings;

namespace Flow.Launcher.Plugin.Noor_Flow
{
    public class Main : IPlugin, IContextMenu, ISettingProvider
    {
        private PluginInitContext _context;
        private List<Surah> _allSurahs;
        private readonly string _icoPath;
        private string _quranJsonFile;
        private PluginSettingsData _settings;

        public Main()
        {
            _icoPath = "Images\\app.png";
        }

        public void Init(PluginInitContext context)
        {
            _context = context;
            _quranJsonFile = Path.Combine(_context.CurrentPluginMetadata.PluginDirectory, "Data", "normalized_quran.json");
            LoadQuranData();
            _settings = _context.API.LoadSettingJsonStorage<PluginSettingsData>() ?? new PluginSettingsData();
        }

        public Control CreateSettingPanel()
        {
            return new PluginSettings(_settings, _context);
        }

        public List<Result> LoadContextMenus(Result selectedResult)
        {
            var results = new List<Result>();

            string tafsirOption = _settings.PreferredTafsir == PluginSettingsData.TafsirOption.AlTabari
                ? "ar-tafsir-al-tabari"
                : "ar-tafsir-muyassar";
        
            // Ensure the selected result is a Surah
            if (selectedResult.ContextData is Surah surah)
            {
                // Add a general info context item
                results.Add(new Result
                {
                    Title = "Copy Surah Name",
                    SubTitle = surah.Name,
                    IcoPath = _icoPath,
                    Action = _ =>
                    {
                        Clipboard.SetText(surah.Name);
                        return true;
                    }
                });

                // Open Surah on Quran.com Option
                results.Add(new Result
                {
                    Title = "Open in Quran.com",
                    SubTitle = $"View Surah {surah.Transliteration} on Quran.com",
                    IcoPath = _icoPath,
                    Action = _ =>
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = $"https://quran.com/{surah.Id}",
                            UseShellExecute = true
                        });
                        return true;
                    }
                });
                // Check Surah's info on Quran.com Option
                results.Add(new Result
                {
                    Title = "Check Surah's info on Quran.com",
                    SubTitle = $"View Surah {surah.Transliteration}'s info on Quran.com",
                    IcoPath = _icoPath,
                    Action = _ =>
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = $"https://quran.com/surah/{surah.Id}/info",
                            UseShellExecute = true
                        });
                        return true;
                    }
                });
                // Add ayahs of the surah as context menu items
                foreach (var ayah in surah.Verses)
                {
                    results.Add(new Result
                    {
                        Title = ayah.Text,
                        SubTitle = $"Surah {surah.Transliteration} ({surah.Id}:{ayah.Id})",
                        IcoPath = _icoPath,
                        Action = _ =>
                        {
                            Clipboard.SetText(ayah.Text);
                            return true;
                        },
                        // ContextData = new SurahAyahPair(surah, ayah) // context inside context doesn't work
                    });
                }
            }
            // If the selected item is an Ayah (retrieving its Surah)
            else if (selectedResult.ContextData is SurahAyahPair { Surah: var selectedSurah, Verse: var selectedAyah })
            {
                // Copy Ayah Text
                results.Add(new Result
                {
                    Title = "Copy Ayah Text",
                    SubTitle = selectedAyah.Text,
                    IcoPath = _icoPath,
                    Action = _ =>
                    {
                        Clipboard.SetText(selectedAyah.Text);
                        return true;
                    }
                });

                // Open Ayah on Quran.com
                results.Add(new Result
                {
                    Title = "Open in Quran.com",
                    SubTitle = $"View Ayah ({selectedSurah.Id}:{selectedAyah.Id}) on Quran.com",
                    IcoPath = _icoPath,
                    Action = _ =>
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = $"https://quran.com/{selectedSurah.Id}/{selectedAyah.Id}",
                            UseShellExecute = true
                        });
                        return true;
                    }
                });
                // Open Tafsir for Ayah on Quran.com
                results.Add(new Result
                {
                    Title = "Check Ayah's Tafseer on Quran.com",
                    SubTitle = $"View Ayah ({selectedSurah.Id}:{selectedAyah.Id})'s Tafseer on Quran.com",
                    IcoPath = _icoPath,
                    Action = _ =>
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = $"https://quran.com/{selectedSurah.Id}:{selectedAyah.Id}/tafsirs/{tafsirOption}",
                            UseShellExecute = true
                        });
                        return true;
                    }
                });
            }
            return results;
        }


        private void LoadQuranData()
        {
            try
            {
                if (!File.Exists(_quranJsonFile))
                {
                    _allSurahs = new List<Surah>();
                    _context.API.ShowMsg("Error", "Quran data file not found. Please reinstall the plugin.");
                    return;
                }

                var jsonContent = File.ReadAllText(_quranJsonFile);
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                _allSurahs = JsonSerializer.Deserialize<List<Surah>>(jsonContent, options) ?? new List<Surah>();
                System.Diagnostics.Debug.WriteLine($"Loaded {_allSurahs.Count} surahs.");
            }
            catch (Exception ex)
            {
                _allSurahs = new List<Surah>();
                System.Diagnostics.Debug.WriteLine($"Error loading Quran data: {ex.Message}");
            }
        }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();

            if (_allSurahs == null || _allSurahs.Count == 0)
            {
                results.Add(new Result
                {
                    Title = "Error: Quran data not loaded",
                    SubTitle = "Check if normalized_quran.json exists in the Data folder",
                    IcoPath = _icoPath
                });
                return results;
            }
            // Check if the search query is empty
            if (string.IsNullOrWhiteSpace(query.Search))
            {
                results.Add(new Result
                {
                    Title = "Search with numbers or text snippet from a verse",
                    SubTitle = "Enter a surah number or text to search ayahs",
                    IcoPath = _icoPath
                });
                return results;
            }
            // Check if the search query is a number (surah ID)
            if (int.TryParse(query.Search, out int surahId))
            {
                var surah = _allSurahs.FirstOrDefault(s => s?.Id == surahId);
                if (surah != null)
                {
                    results.Add(new Result
                    {
                        Title = surah.Name,
                        SubTitle = $"Surah {surah.Transliteration} ({surah.Id}) - {surah.TotalVerses} verses",
                        IcoPath = _icoPath,
                        Action = _ =>
                        {
                            _context.API.ChangeQuery($"{_context.CurrentPluginMetadata.ActionKeyword} {surah.Id}:");
                            return true;
                        },
                        ContextData = surah
                    });
                }
            }
            else if (Regex.IsMatch(query.Search, @"^\d+[: ]\d+$"))
            {
                var parts = query.Search.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && int.TryParse(parts[0], out int sId) && int.TryParse(parts[1], out int ayahId))
                {
                    var surah = _allSurahs.FirstOrDefault(s => s?.Id == sId);
                    if (surah != null)
                    {
                        var ayah = surah.Verses.FirstOrDefault(v => v?.Id == ayahId);
                        if (ayah != null)
                        {
                            results.Add(new Result
                            {
                                Title = ayah.Text,
                                SubTitle = $"Surah {surah.Transliteration} ({surah.Id}:{ayah.Id})",
                                IcoPath = _icoPath,
                                Action = _ =>
                                {
                                    Clipboard.SetText(ayah.Text);
                                    return true;
                                },
                                ContextData = new SurahAyahPair(surah, ayah)
                            });
                        }
                    }
                }
            }
            else
            {
                var normalizedQuery = Normalizer.Normalize(query.Search) ?? string.Empty;
                if (!string.IsNullOrEmpty(query.Search))
                {
                    var matchingSurahs = _allSurahs
                        .Where(s => Normalizer.Normalize(s.Name).Contains(normalizedQuery)
                                && Normalizer.NormalizeLatin(s.Transliteration).Contains(Normalizer.NormalizeLatin(query.Search), StringComparison.OrdinalIgnoreCase))
                        .Select(surah => new Result
                        {
                            Title = surah.Name,
                            SubTitle = $"Surah {surah.Transliteration} ({surah.Id}) - {surah.TotalVerses} verses",
                            IcoPath = _icoPath,
                            Action = _ =>
                            {
                                _context.API.ChangeQuery($"{_context.CurrentPluginMetadata.ActionKeyword} {surah.Id}:");
                                return false;
                            },
                            ContextData = surah
                        });

                    results.AddRange(matchingSurahs);
                }
                if (!string.IsNullOrEmpty(normalizedQuery))
                {
                    var matchingAyahs = _allSurahs
                        .Where(surah => surah?.Verses != null)
                        .SelectMany(surah => (surah.Verses ?? Enumerable.Empty<Verse>())
                        .Select(verse => new { Surah = surah, Verse = verse }))
                        .Where(x => (x.Verse?.NormalizedText ?? string.Empty).Contains(normalizedQuery))
                        .Take(20)
                        .Select(x => new Result
                        {
                            Title = x.Verse.Text,
                            SubTitle = $"Surah {x.Surah.Transliteration} ({x.Surah.Id}:{x.Verse.Id})",
                            IcoPath = _icoPath,
                            Action = _ =>
                            {
                                Clipboard.SetText(x.Verse.Text);
                                return true;
                            },
                            ContextData = new SurahAyahPair(x.Surah, x.Verse)
                        });
                    results.AddRange(matchingAyahs);
                    // if (matchingAyahs.Count() == 0)
                    // {
                    //     results.Add(new Result
                    //     {
                    //         Title = "No matching ayahs found",
                    //         SubTitle = "Try a different search query",
                    //         IcoPath = _icoPath
                    //     });
                    // }
                }
            }

            System.Diagnostics.Debug.WriteLine($"Found {results.Count} results for query: {query.Search}");
            return results;
        }
    }
}