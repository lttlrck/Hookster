namespace s16n.Hookster
{
    public class HooksterSettings
    {
        public int IdleTimeout { set; get; }= 14400;
        public string ActiveBatPath { set; get; } = "active.bat";
        public string IdleBatPath { set; get; } = "idle.bat";
        public string ActiveWebhook { set; get; } = "";
        public string IdleWebhook { set; get; } = "";
        public string Webhook1 { set; get; } = "";
        public string Webhook2 { set; get; } = "";
        public string Webhook3 { set; get; } = "";
        public string Webhook4 { set; get; } = "";
        public string GameScriptPath { get; set; } = "";
    }
} 