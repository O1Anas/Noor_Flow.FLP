<div align="center">
  <img src="Flow.Launcher.Plugin.Noor_Flow/Images/app.png" width="100" alt="NoorFlow">
  <h1>Noor Flow - النّورُ الجاري</h1>
  <h3>The Flowing Light that Eases the Seeker of Heights - النّورُ الجاري لِلتَّيسير عَلى طالِبِ المَعالي</h3>
</div>

Noor Flow is a powerful Flow Launcher plugin designed to ease the search and extraction of Islamic revelation texts, currently focusing on The Noble Quran with plans to expand to Sunnah texts (such as Sahih al-Bukhari / the 9 books of hadith).

## Current features
- **Surah Search**: Quickly find any surah by number or name in Arabic or Latin
  - **Ayah Search**: Search ayahs using text 
    - and/or specific notations and formats (like `surah:ayah` e.g., `3:5`)
- **Rich Context Menu**: Access additional options for each result (Surah or Ayah: Copy text, open in [quran.com](https://quran.com/) website, read translation [there](https://quran.com/), ..etc)

Check the [Usage heading](#usage) for a detailed feature showcase

## Installation
1. Install latest version of [Flow Launcher here](https://github.com/Flow-Launcher/Flow.Launcher).
2. Then proceed with one of the following installation methods:

**Recommended**:
- Search for the plugin on the Flow Launcher app's plugin store and install it

**Manual**:
1. Download the latest release from the [Releases](https://github.com/O1Anas/Noor_Flow.FLP/releases) page
2. Extract the ZIP file to your Flow Launcher plugins directory:
   ```
   %APPDATA%\FlowLauncher\Plugins\
   ```
3. Restart Flow Launcher so the plugin appears on the plugins list.

## Usage
Activate the plugin using the default keyword `noor` followed by your search:
- `noor 1` - Find Surah Al-Fatiha (first surah).
- `noor 1:1` or `noor 1 1` - Find first ayah from first surah (Surah Al-Fatiha).
- `noor الرحمن` - Search for ayahs or surahs containing "الرحمن" in their text or name.
- `noor rahman` - Search for Surah Ar-Rahman using transliteration.
- `noor 2:255` - Show Ayat al-Kursi (Surah Al-Baqarah, ayah 255).
- `noor اللَّهُ لا إلَهَ إلّا هُوَ الحَيُّ` - Show Ayat al-Kursi (Surah Al-Baqarah, ayah 255).
- `noor قل هو الله أحد` - Show Ayat al-Kursi (Surah Al-Baqarah, ayah 255).

**Note**: Fuzzy search is an implemented feature. For example: even if you search for `بسماللهالرحمنالرحيم`, your search will match the result: `بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ`

**Tip**: If you set the default keyword for the plugin's activation to `*` (nothing), it will activate as soon as you open up the Flow Launcher search panel, then you won't have to type `noor` each time you need to search

### Context Menu
**Right-click** (or press **shift+enter** or **right arrow button**) on any result to access additional options:
- For Surahs:
  - Copy surah name
  - Open surah in [Quran.com](https://quran.com/)
  - View surah information on [Quran.com](https://quran.com/)
  - **The remaining results** will display the ayahs of that surah, allowing you to search for specific ayahs within it.

- For Ayahs:
  - Copy ayah text
  - Open ayah in [Quran.com](https://quran.com/)
  - View ayah's tafseer (interpretation) on [Quran.com](https://quran.com/)

## Settings
Configure plugin preferences through the Flow Launcher settings panel:
- **Preferred tafseer**: Choose between multiple tafseer options from [Quran.com](https://quran.com/)

## Roadmap
Future enhancements planned for Noor Flow:

- **Hadith Search**: Integration with Sahih al-Bukhari and other authentic Sunnah collections (mostly the 9 books)
  - **Hadith Context Options**: Copy hadith text, view complete narrations, access chains of narrators and open the hadith on [Sunnah.com](https://sunnah.com/) or similar websites
- **Option for default action upon choosing an ayah**: Currently, it is to copy the ayah's text, but we should allow users to set it to whatever available action they want
- **Custom copy formats**: Where users can combine elements to create their desired copying format
  - **Multiple formats to copy**: Where in the context menu of an ayah, users can choose different pre-defined and customized copy formats, instead of limiting to only 1 custom copy format
- **Multiple Language Support**: Add translations in various languages

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development
This plugin is written in `C#`
#### Prerequisites
- Install `dotnet` from Microsoft [here](https://dotnet.microsoft.com/en-us/download). 
- Install latest version of [Flow Launcher](https://github.com/Flow-Launcher/Flow.Launcher). (default installation path is recommended for faster testing)

#### Building the project
After editing the source code of the plugin:
1. Exit [Flow Launcher](https://github.com/Flow-Launcher/Flow.Launcher) if it is already open.
2. Run `dotnet build` in the `%APPDATA%\FlowLauncher\Plugins\Flow.Launcher.Plugin.Noor_Flow` folder.
  - **Note**: the source code should be compiled in the mentioned folder to get recognized as a plugin (Flow launcher's plugins folder)
3. Restart [Flow Launcher](https://github.com/Flow-Launcher/Flow.Launcher) to get the updated and built version of the plugin.

## Copyright
See [LICENSE-en.md](LICENSE-en.md).

## Acknowledgments
- Quran data from [quran-json](https://github.com/risan/quran-json).
- [Icon](/Flow.Launcher.Plugin.Noor_Flow/Images/app.png) from [Freepik.com](https://www.freepik.com/icon/koran_384372).
- Thanks to [@AmmarCodes](https://github.com/AmmarCodes)' [Obsidian Quran Plugin](https://github.com/AmmarCodes/obsidian-quran-helper-plugin) for huge inspiration.

<div align="center"><a href="https://github.com/Safouene1/support-palestine-banner/blob/master/Markdown-pages/Support.md"><img src="https://raw.githubusercontent.com/Safouene1/support-palestine-banner/master/banner-support.svg" alt="Support Palestine" style="width: 100%;"></a></div>