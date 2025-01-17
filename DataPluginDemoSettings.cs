namespace s16n.TelemetryDetector
{
    /// <summary>
    /// Settings class, make sure it can be correctly serialized using JSON.net
    /// </summary>
    public class DataPluginDemoSettings
    {
        public int IdleTimeout { set; get; }= 1;
    }
}