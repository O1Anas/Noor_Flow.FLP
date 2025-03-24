namespace Flow.Launcher.Plugin.Noor_Flow.Settings
{
    public class PluginSettingsData
    {
        public TafsirOption PreferredTafsir { get; set; } = TafsirOption.Muyassar;

        public enum TafsirOption
        {
            AlTabari,
            Muyassar
        }
    }
}