using GameReaderCommon;
using SimHub.Plugins;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;

namespace s16n.TelemetryDetector
{
    [PluginDescription("Telemetry detector")]
    [PluginAuthor("Stuart")]
    [PluginName("Telemetry Detector")]
    public class TelemetryDetector : IPlugin, IDataPlugin, IWPFSettingsV2
    {
        public TelemetryDetectorSettings Settings;

        /// <summary>
        /// Instance of the current plugin manager
        /// </summary>
        public PluginManager PluginManager { get; set; }

        /// <summary>
        /// Gets the left menu icon. Icon must be 24x24 and compatible with black and white display.
        /// </summary>
        public ImageSource PictureIcon => this.ToIcon(Properties.Resources.sdkmenuicon);

        /// <summary>
        /// Gets a short plugin title to show in left menu. Return null if you want to use the title as defined in PluginName attribute.
        /// </summary>
        public string LeftMenuTitle => "Telemetry Detector";
        void runScript(string game)
        {
            if( File.Exists("C:\\Program Files (x86)\\SimHub\\ShellMacros\\telemetryDetector.bat"))
            {
                System.Diagnostics.Process.Start("C:\\Program Files (x86)\\SimHub\\ShellMacros\\telemetryDetector.bat", game);
            }
        }

        /// <summary>
        /// Called one time per game data update, contains all normalized game data,
        /// raw data are intentionally "hidden" under a generic object type (A plugin SHOULD NOT USE IT)
        ///
        /// This method is on the critical path, it must execute as fast as possible and avoid throwing any error
        /// 
        /// </summary>
        /// <param name="pluginManager"></param>
        /// <param name="data">Current game data, including current and previous data frame.</param>
        public void DataUpdate(PluginManager pluginManager, ref GameData data)
        {
            //SimHub.Logging.Current.Info(Settings.IdleTimeout);

            // Define the value of our property (declared in init)
            if (data.GameRunning)
            { 
                if (data.OldData != null && data.NewData != null)
                {
                    if (data.OldData.PacketTime.Second != lastPacketTime.Second )
                    {
                        if (data.OldData.PacketTime.Year == 1)
                        {
                            SimHub.Logging.Current.Info("first timestamp after pause is empty, ignore");
                            return;
                        }

                        // if we are receiving telemtry, update last receive time  
                        lastPacketTime = data.OldData.PacketTime;
                        //SimHub.Logging.Current.Info(data.OldData.PacketTime);
                        // Trigger an event

                        if (!active) 
                        {
                            this.TriggerEvent("TelemetryActive");
                            SimHub.Logging.Current.Info("TelemetryActive " + data.GameName);

                            var name = data.GameName;

                            _ = Task.Run(() => runScript(name));

                            active = true;
                        } 
                    }
                }
            }

            //if (DateTime.Now.Second != lastPrint)
            //{
            //    lastPrint = DateTime.Now.Second;
            //    SimHub.Logging.Current.Info(Settings.IdleTimeout);
            //    SimHub.Logging.Current.Info(DateTime.Now.AddSeconds(10));
            //    SimHub.Logging.Current.Info(lastPacketTime);
            //}

            // if the last receive time is older than the configured timeout, send idle
            if (active == true && (lastPacketTime.AddSeconds(Settings.IdleTimeout) < DateTime.Now))
            {
                active = false;
                
                SimHub.Logging.Current.Info("TelemetryIdle");

                this.TriggerEvent("TelemetryIdle");
            }
        }

        //int lastPrint;
        DateTime lastPacketTime;
        bool active;

        /// <summary>
        /// Called at plugin manager stop, close/dispose anything needed here !
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void End(PluginManager pluginManager)
        {
            // Save settings
            this.SaveCommonSettings("GeneralSettings", Settings);
        }

        /// <summary>
        /// Returns the settings control, return null if no settings control is required
        /// </summary>
        /// <param name="pluginManager"></param>
        /// <returns></returns>
        public System.Windows.Controls.Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return new SettingsControlDemo(this);
        }

        /// <summary>
        /// Called once after plugins startup
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Starting plugin");

            // Load settings
            Settings = this.ReadCommonSettings<TelemetryDetectorSettings>("GeneralSettings", () => new TelemetryDetectorSettings());
            SimHub.Logging.Current.Info(Settings.IdleTimeout);
            // Declare a property available in the property list, this gets evaluated "on demand" (when shown or used in formulas)
            //this.AttachDelegate(name: "CurrentDateTime", valueProvider: () => DateTime.Now);

            // Declare an event
            this.AddEvent(eventName: "TelemetryIdle");
            this.AddEvent(eventName: "TelemetryActive");

            // Declare an action which can be called
            //this.AddAction(
            //    actionName: "IncrementSpeedWarning",
            //    actionStart: (a, b) =>
            //    {
            //        Settings.SpeedWarningLevel++;
            //        SimHub.Logging.Current.Info("Speed warning changed");
            //    });

            // Declare an action which can be called, actions are meant to be "triggered" and does not reflect an input status (pressed/released ...)
            //this.AddAction(
            //    actionName: "DecrementSpeedWarning",
            //    actionStart: (a, b) =>
            //    {
            //        Settings.SpeedWarningLevel--;
            //    });

            // Declare an input which can be mapped, inputs are meant to be keeping state of the source inputs,
            // they won't trigger on inputs not capable of "holding" their state.
            // Internally they work similarly to AddAction, but are restricted to a "during" behavior
            //this.AddInputMapping(
            //    inputName: "InputPressed",
            //    inputPressed: (a, b) => {/* One of the mapped input has been pressed   */},
            //    inputReleased: (a, b) => {/* One of the mapped input has been released */}
            //);
        }
    }
}