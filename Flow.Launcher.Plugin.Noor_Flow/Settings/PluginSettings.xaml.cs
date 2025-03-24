using System.Windows.Controls; // Add this for UserControl
using System.Windows;          // Required for WPF elements
using System.Windows.Media;    // Sometimes needed for UI components
using Flow.Launcher.Plugin; // Required for PluginInitContext
using Flow.Launcher.Plugin.Noor_Flow.Settings; // Ensure correct namespace

namespace Flow.Launcher.Plugin.Noor_Flow.Settings
{
    public partial class PluginSettings : UserControl
    {
        private readonly PluginSettingsData _settings;
        private readonly PluginInitContext _context;

        public PluginSettings(PluginSettingsData settings, PluginInitContext context)
        {
            InitializeComponent();
            _settings = settings;
            _context = context;

            LoadSettings();
        }

        private void LoadSettings()
        {
            // Load Tafsir setting
            TafsirComboBox.SelectedIndex = _settings.PreferredTafsir switch
            {
                PluginSettingsData.TafsirOption.AlTabari => 0,
                PluginSettingsData.TafsirOption.Muyassar => 1,
                _ => 1
            };
        }

        private void TafsirComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Prevent unnecessary save during initialization
            if (e.RemovedItems.Count > 0)
            {
                SaveSettings();
            }
        }

        private void SaveSettings()
        {
            // Save Tafsir setting
            _settings.PreferredTafsir = TafsirComboBox.SelectedIndex switch
            {
                0 => PluginSettingsData.TafsirOption.AlTabari,
                1 => PluginSettingsData.TafsirOption.Muyassar,
                _ => PluginSettingsData.TafsirOption.Muyassar
            };

            // Save settings using the correct API method
            _context.API.SaveSettingJsonStorage<PluginSettingsData>();
        }
    }
}