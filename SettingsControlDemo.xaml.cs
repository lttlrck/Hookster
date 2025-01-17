using SimHub.Plugins.Styles;
using System.Windows.Controls;

namespace s16n.TelemetryDetector
{
    /// <summary>
    /// Logique d'interaction pour SettingsControlDemo.xaml
    /// </summary>
    public partial class SettingsControlDemo : UserControl
    {
        public TelemetryDetector Plugin { get; }

        public SettingsControlDemo()
        {
            InitializeComponent();
        }

        public SettingsControlDemo(TelemetryDetector plugin) : this()
        {
            this.Plugin = plugin;
            
        }

    }
}