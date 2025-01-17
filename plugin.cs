using GameReaderCommon;
using SimHub.Plugins;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Linq;

namespace s16n.Hookster
{
    [PluginDescription("Hookster")]
    [PluginAuthor("Stuart")]
    [PluginName("Hookster")]
    public class Hookster : IPlugin, IDataPlugin, IWPFSettingsV2
    {
        public HooksterSettings Settings;

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
        public string LeftMenuTitle => "Hookster";
        void RunActiveScript()
        {
            if (File.Exists("C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.ActiveBatPath))
            {
                if (Settings.ActiveBatPath.Contains(".bat"))
                {
                    System.Diagnostics.Process.Start("C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.ActiveBatPath);
                }
                else if (Settings.ActiveBatPath.Contains(".ahk"))
                {
                    System.Diagnostics.Process.Start("C:\\Program Files\\AutoHotkey\\v2\\AutoHotKey64.exe", "\"C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.ActiveBatPath);
                }
            }
        }
        void RunIdleScript()
        {
            if (File.Exists("C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.IdleBatPath))
            {
                if (Settings.IdleBatPath.Contains(".bat"))
                {
                    System.Diagnostics.Process.Start("C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.IdleBatPath);
                }
                else if (Settings.IdleBatPath.Contains(".ahk"))
                {
                    System.Diagnostics.Process.Start("C:\\Program Files\\AutoHotkey\\v2\\AutoHotKey64.exe", "\"C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.IdleBatPath);
                }
            }
        }
        void RunGameChangeScript(string game)
        {
            if (File.Exists("C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.GameScriptPath))
            {
                if (Settings.GameScriptPath.Contains(".bat"))
                {
                    System.Diagnostics.Process.Start("C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.GameScriptPath, game);
                }
                else if (Settings.GameScriptPath.Contains(".ahk"))
                {
                    System.Diagnostics.Process.Start("C:\\Program Files\\AutoHotkey\\v2\\AutoHotKey64.exe", "\"C:\\Program Files (x86)\\SimHub\\Hookster\\" + Settings.GameScriptPath + "\" " + game);
                }  
            }
        } 
        void RunActiveWebhook()
        {
            if(Settings.ActiveWebhook == "")
            {
                return;
            }

            string[] hooks = Settings.ActiveWebhook.Split('|');

            foreach (var hook in hooks)
            {
                var client = new HttpClient();

                client.GetAsync(hook);
            }

            SimHub.Logging.Current.Info(Settings.ActiveWebhook);
        }

        void RunIdleWebhook()
        {
            if (Settings.IdleWebhook == "")
            {
                return;
            }

            string[] hooks = Settings.IdleWebhook.Split('|');

            foreach (var hook in hooks)
            {
                var client = new HttpClient();

                client.GetAsync(hook);
            }

            SimHub.Logging.Current.Info(Settings.IdleWebhook);
        }

        void RunWebhook(int num)
        {
            var wh = "";
            if(num == 1) {
                wh = Settings.Webhook1;
            }
            if (num == 2)
            {
                wh = Settings.Webhook2;
            }
            if (num == 3)
            {
                wh = Settings.Webhook3;
            }
            if (num == 4)
            {
                wh = Settings.Webhook4;
            }

            if (wh== "")
            {
                return;
            }

            string[] hooks = wh.Split('|');

            foreach (var hook in hooks)
            {
                var client = new HttpClient();

                client.GetAsync(hook);
            }

            SimHub.Logging.Current.Info(wh);
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
            if (data.GameName != lastGameName)
            {
                var name = data.GameName;

                SimHub.Logging.Current.Info("game changed to " + data.GameName);

                _ = Task.Run(() => RunGameChangeScript(name));

                lastGameName = name;
            }

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

                            _ = Task.Run(() => RunActiveWebhook());

                            _ = Task.Run(() => RunActiveScript());

                            active = true;
                        } 
                    }
                }
            }

            // if the last receive time is older than the configured timeout, send idle
            if (active == true && (lastPacketTime.AddSeconds(Settings.IdleTimeout) < DateTime.Now))
            {
                active = false;
                
                SimHub.Logging.Current.Info("TelemetryIdle");

                this.TriggerEvent("TelemetryIdle");

                _ = Task.Run(() => RunIdleWebhook());

                _ = Task.Run(() => RunIdleScript());
            }
        }

        //int lastPrint;
        DateTime lastPacketTime;
        string lastGameName;
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
            Settings = this.ReadCommonSettings<HooksterSettings>("GeneralSettings", () => new HooksterSettings());
            SimHub.Logging.Current.Info("Timeout "+Settings.IdleTimeout);
            SimHub.Logging.Current.Info("ActiveBat " + Settings.ActiveBatPath);
            // Declare a property available in the property list, this gets evaluated "on demand" (when shown or used in formulas)
            //this.AttachDelegate(name: "CurrentDateTime", valueProvider: () => DateTime.Now);
            
            // Declare an event
            this.AddEvent(eventName: "TelemetryIdle");
            this.AddEvent(eventName: "TelemetryActive");

            // Declare an action which can be called
            this.AddAction(
                actionName: "Webhook 1",
                actionStart: (a, b) =>
                {
                    _ = Task.Run(() => RunWebhook(1));
                });

            this.AddAction(
                actionName: "Webhook 2",
                actionStart: (a, b) =>
                {
                    _ = Task.Run(() => RunWebhook(2));
                });
            
            this.AddAction(
                actionName: "Webhook 3",
                actionStart: (a, b) =>
                {
                    _ = Task.Run(() => RunWebhook(3));
                });

            this.AddAction(
                actionName: "Webhook 4",
                actionStart: (a, b) =>
                {
                    _ = Task.Run(() => RunWebhook(4));
                });

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