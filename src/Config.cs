using System.IO;

namespace SMSHandler
{
    public class Config
    {
        private static ConfigData Instance { get; set; }
        
        public static string SendgridApiKey => Instance.SendgridApiKey;
        public static string EmailFrom => Instance.EmailFrom;
        public static string EmailTo => Instance.EmailTo;
        public static string EmailToName => Instance.EmailToName;
        
        public static void Init(string path)
        {
            if (Instance != null) return;
            Instance = Jil.JSON.Deserialize<ConfigData>(File.ReadAllText(path));
        }

        private class ConfigData
        {
            public string SendgridApiKey { get; set; }
            public string EmailFrom { get; set; }
            public string EmailTo { get; set; }
            public string EmailToName { get; set; }
        }
    }
}