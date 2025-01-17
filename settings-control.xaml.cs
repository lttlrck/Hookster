using SimHub.Plugins.Styles;
using System;
using System.Windows.Controls;
using System.Windows.Markup;

namespace s16n.Hookster
{
    public partial class SettingsControlDemo : UserControl
    {
        public Hookster Plugin { get; }
        public SettingsControlDemo()
        {
            InitializeComponent();
        }
        public SettingsControlDemo(Hookster plugin) : this()
        {
            this.Plugin = plugin;
            this.idleTime.Text = plugin.Settings.IdleTimeout.ToString();
            this.activeBatPath.Text = plugin.Settings.ActiveBatPath.ToString();
            this.idleBatPath.Text = plugin.Settings.IdleBatPath.ToString();
            this.gameChangeScript.Text = plugin.Settings.GameScriptPath.ToString();
            this.activeWebhook.Text = plugin.Settings.ActiveWebhook.ToString(); 
            this.idleWebhook.Text = plugin.Settings.IdleWebhook.ToString();
            this.webhook1.Text = plugin.Settings.Webhook1.ToString(); 
            this.webhook2.Text = plugin.Settings.Webhook2.ToString();
            this.webhook3.Text = plugin.Settings.Webhook3.ToString();
            this.webhook4.Text = plugin.Settings.Webhook4.ToString();
        }
        private void IdleTimeoutChanged(object sender, TextChangedEventArgs e)
        {
            // Add your event handler code here
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            { 
                // Example: Log the new text value
                string newValue = textBox.Text;

                //SimHub.Logging.Current.Info("TelemetryActive1 " + newValue);
                // Perform your logic with newValue

                if (Int32.TryParse(newValue, out int val))
                {
                    if (this.Plugin != null && this.Plugin.Settings != null)
                    {
                        this.Plugin.Settings.IdleTimeout = val;
                        //SimHub.Logging.Current.Info("TelemetryActive2 " + val);
                    }
                    else
                    {
                        //SimHub.Logging.Current.Error("Plugin or Plugin.Settings is null");
                    }
                }
            }
        }

        private void ActiveBatPath(object sender, TextChangedEventArgs e)
        {
            // Add your event handling code here
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newPath = textBox.Text;
                // Handle the new path value

                //SimHub.Logging.Current.Info("TelemetryActive1 " + newPath);
                // Perform your logic with newValue
 
                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.ActiveBatPath = newPath;
                    //SimHub.Logging.Current.Info("TelemetryActive2 " + newPath);
                }
                else
                {
                    //SimHub.Logging.Current.Error("Plugin or Plugin.Settings is null");
                }
            }
        }

        private void IdleBatPath(object sender, TextChangedEventArgs e)
        {
            // Add your event handling code here
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newPath = textBox.Text;
                // Handle the new path value

                //SimHub.Logging.Current.Info("TelemetryActive1 " + newPath);
                // Perform your logic with newValue

                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.IdleBatPath = newPath;
                    //SimHub.Logging.Current.Info("TelemetryActive2 " + newPath);
                }
                else
                {
                    //SimHub.Logging.Current.Error("Plugin or Plugin.Settings is null");
                }
            }
        }

        private void GameChangeScriptChanged(object sender, TextChangedEventArgs e)
        {
            // Add your event handling code here
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newPath = textBox.Text;
                // Handle the new path value

                //SimHub.Logging.Current.Info("TelemetryActive1 " + newPath);
                // Perform your logic with newValue

                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.GameScriptPath = newPath;
                    //SimHub.Logging.Current.Info("TelemetryActive2 " + newPath);
                }
                else
                {
                    //SimHub.Logging.Current.Error("Plugin or Plugin.Settings is null");
                }
            }
        }

        private void ActiveWebhookChanged(object sender, TextChangedEventArgs e)
        {
            // Add your event handling code here
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newPath = textBox.Text;
                // Handle the new path value

                //SimHub.Logging.Current.Info("TelemetryActive1 " + newPath);
                // Perform your logic with newValue

                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.ActiveWebhook = newPath;
                    //SimHub.Logging.Current.Info("TelemetryActive2 " + newPath);
                }
                else
                {
                    //SimHub.Logging.Current.Error("Plugin or Plugin.Settings is null");
                }
            }
        }

        private void IdleWebhookChanged(object sender, TextChangedEventArgs e)
        {
            // Add your event handling code here
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newPath = textBox.Text;
                // Handle the new path value

                //SimHub.Logging.Current.Info("TelemetryActive1 " + newPath);
                // Perform your logic with newValue

                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.IdleWebhook = newPath;
                    //SimHub.Logging.Current.Info("TelemetryActive2 " + newPath);
                }
                else
                {
                    //SimHub.Logging.Current.Error("Plugin or Plugin.Settings is null");
                }
            }
        }

        private void Webhook1Changed(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox; 
            if (textBox != null)
            {
                string newPath = textBox.Text;
                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.Webhook1 = newPath;
                }
            }
        }

        private void Webhook2Changed(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newPath = textBox.Text;
                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.Webhook2 = newPath;
                }
            }
        }
        private void Webhook3Changed(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newPath = textBox.Text;
                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.Webhook3 = newPath;
                }
            }
        }

        private void Webhook4Changed(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newPath = textBox.Text;
                if (this.Plugin != null && this.Plugin.Settings != null)
                {
                    this.Plugin.Settings.Webhook4 = newPath;
                }
            }
        }
    }
}