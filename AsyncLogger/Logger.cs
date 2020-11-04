using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;

namespace LoggingEngine
{
    public class Logger
    {
        public static readonly string ExecPath = Directory.GetCurrentDirectory();

        private static readonly Logger DefaultLogger = new Logger(LoggerConfig.DefaultName);
        private static readonly BlockingCollection<LogMessage> Logs;

        private readonly string _loggerName;
        private readonly LoggerConfig _config;

        public Logger(Type type)
            : this(type.Name) { }
        public Logger(string name)
        {
            if (LoggerConfig.Configs == null)
                LoggerConfig.ConfigFile = LoggerConfig.DefaultConfig[0].LogDir;

            _loggerName = name;
            var config = LoggerConfig.Configs.FirstOrDefault(cfg => cfg.LoggerName == _loggerName);
            if (config == null)
                _config = LoggerConfig.DefaultConfig[0];
            else _config = config;
        }

        static Logger()
        {
            Logs = new BlockingCollection<LogMessage>();
            new Thread(TickLoop).Start();
        }

        private static void TickLoop()
        {
            foreach (var msg in Logs.GetConsumingEnumerable())
            {
                var level = msg.Config.LevelConfigs[(int)msg.Level];
                Console.BackgroundColor = level.BackgroundColor;
                Console.ForegroundColor = level.ForegroundColor;
                Console.WriteLine(msg.Text);

                if (msg.SaveToFile)
                    SaveToFile(msg.Text, msg.Level, msg.Config.LogDir);
            }
        }

        public static void LogInfo(object val, bool saveToFile = true)
            => DefaultLogger.Log(val.ToString(), LogLevel.INFO, saveToFile);

        public void Info(object val, bool saveToFile = true)
            => Log(val.ToString(), LogLevel.INFO, saveToFile);

        public static void LogDebug(object val, bool saveToFile = false)
            => DefaultLogger.Log(val.ToString(), LogLevel.DEBUG, saveToFile);

        public void Debug(object val, bool saveToFile = false)
            => Log(val.ToString(), LogLevel.DEBUG, saveToFile);

        public static void LogWarn(object val, bool saveToFile = true)
            => DefaultLogger.Log(val.ToString(), LogLevel.WARN, saveToFile);

        public void Warn(object val, bool saveToFile = true)
            => Log(val.ToString(), LogLevel.WARN, saveToFile);

        public static void LogError(object val, bool saveToFile = true)
            => DefaultLogger.Log(val.ToString(), LogLevel.ERROR, saveToFile);

        public void Error(object val, bool saveToFile = true)
            => Log(val.ToString(), LogLevel.ERROR, saveToFile);

        public static void LogFatal(object val, bool saveToFile = true)
            => DefaultLogger.Log(val.ToString(), LogLevel.FATAL, saveToFile);

        public void Fatal(object val, bool saveToFile = true)
            => Log(val.ToString(), LogLevel.FATAL, saveToFile);

        private void Log(string message, LogLevel level, bool saveToFile)
        {
            Logs.Add(new LogMessage()
            {
                Text = $"{DateTime.Now.TimeOfDay}  {GetFixedLog(message, level)}",
                Level = level,
                SaveToFile = saveToFile,
                Config = _config
            });
        }

        private string GetFixedLog(string message, LogLevel level)
        {
            string lvl = level.ToString();
            int lvlPad = lvl.Length + (7 - lvl.Length);
            int senderPad = _loggerName.Length + (15 - _loggerName.Length);
            return lvl.PadRight(lvlPad) + _loggerName.PadRight(senderPad) + message;
        }

        private static void SaveToFile(string log, LogLevel level, string logDir)
        {
            string path = $"{ExecPath}{logDir}{level.ToString().ToLower()}/log.txt";
            File.AppendAllLines(path, new string[] { log });
        }
    }

    public enum LogLevel
    {
        INFO,
        DEBUG,
        WARN,
        ERROR,
        FATAL
    }
}
