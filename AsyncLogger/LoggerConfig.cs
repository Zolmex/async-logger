using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace LoggingEngine
{
    public class LoggerConfig
    {
        public static readonly LoggerConfig[] DefaultConfig =
        {
            new LoggerConfig()
            {
                LoggerName = DefaultName,
                LogDir = $"/logs/{Process.GetCurrentProcess().ProcessName}/",
                LevelConfigs = new LogLevelConfig[]
                {
                    new LogLevelConfig()
                    {
                        BackgroundColor = ConsoleColor.Black,
                        ForegroundColor = ConsoleColor.Gray
                    },
                    new LogLevelConfig()
                    {
                        BackgroundColor = ConsoleColor.Black,
                        ForegroundColor = ConsoleColor.DarkGray
                    },
                    new LogLevelConfig()
                    {
                        BackgroundColor = ConsoleColor.Black,
                        ForegroundColor = ConsoleColor.Yellow
                    },
                    new LogLevelConfig()
                    {
                        BackgroundColor = ConsoleColor.Red,
                        ForegroundColor = ConsoleColor.White
                    },
                    new LogLevelConfig()
                    {
                        BackgroundColor = ConsoleColor.Black,
                        ForegroundColor = ConsoleColor.White
                    }
                }
            }
        };

        public static string DefaultName => "DefaultLogger";
        public static LoggerConfig[] Configs { get; private set; }
        public static string ConfigFile
        {
            set
            {
                string configPath = $"{Logger.ExecPath}{value}";
                string configFile = configPath + "logConfig.json";

                if (!Directory.Exists(configPath))
                {
                    Directory.CreateDirectory(configPath);
                    File.WriteAllText(configFile, JsonConvert.SerializeObject(DefaultConfig));
                }

                foreach (var level in Enum.GetValues(typeof(LogLevel)))
                {
                    string path = $"{Logger.ExecPath}{value}{level.ToString().ToLower()}";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }

                Configs = JsonConvert.DeserializeObject<LoggerConfig[]>(File.ReadAllText(configFile));
            }
        }

        public string LoggerName { get; set; }
        public string LogDir { get; set; }
        public LogLevelConfig[] LevelConfigs { get; set; }
    }

    public class LogLevelConfig
    {
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
    }
}
