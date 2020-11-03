using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingEngine
{
    public struct LogMessage
    {
        public string Text { get; set; }
        public LogLevel Level { get; set; }
        public bool SaveToFile { get; set; }
        public LoggerConfig Config { get; set; }
    }
}
