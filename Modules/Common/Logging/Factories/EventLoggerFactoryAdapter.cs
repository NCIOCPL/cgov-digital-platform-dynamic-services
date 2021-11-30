using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;

namespace NCI.Logging.Factories
{
    class EventLoggerFactoryAdapter : AbstractSimpleLoggerFactoryAdapter
    {
        public string LogSource { get; private set; }

        public EventLoggerFactoryAdapter(NameValueCollection properties)
            : base(properties)
        {
            string logsource;
            if (properties.TryGetValue("logSource", out logsource))
            {
                LogSource = logsource;
            }
        }

        protected override ILog CreateLogger(string name, LogLevel level, 
            bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
        {
            return new EventLogger(name, level, showLevel, showDateTime, showLogName, dateTimeFormat, LogSource);
        }
    }
}
