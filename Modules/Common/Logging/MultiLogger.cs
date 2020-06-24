using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Factory;

namespace NCI.Logging
{
    /// <summary>
    /// Adapter hub for Common.Logging that can send logs to multiple other adapters
    /// 
    /// Borrowed from http://stackoverflow.com/a/34396738
    /// </summary>
    public class MultiLogger : AbstractLogger
    {
        private readonly List<ILog> Loggers;
        
        /// <summary>
        /// dictionary to map the various log levels to the logger's log methods
        /// </summary>
        public static readonly IDictionary<LogLevel, Action<ILog, object, Exception>> LogActions = new Dictionary<LogLevel, Action<ILog, object, Exception>>()
        {
            { LogLevel.Debug, (logger, message, exception) => logger.Debug(message, exception) },
            { LogLevel.Error, (logger, message, exception) => logger.Error(message, exception) },
            { LogLevel.Fatal, (logger, message, exception) => logger.Fatal(message, exception) },
            { LogLevel.Info, (logger, message, exception) => logger.Info(message, exception) },
            { LogLevel.Trace, (logger, message, exception) => logger.Trace(message, exception) },
            { LogLevel.Warn, (logger, message, exception) => logger.Warn(message, exception) },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLogger"/> class.
        /// </summary>
        /// <param name="loggers">The loggers.</param>
        public MultiLogger(List<ILog> loggers)
        {
            Loggers = loggers;
        }

        public override bool IsDebugEnabled { get { return Loggers.Any(l => l.IsDebugEnabled); } }
        public override bool IsErrorEnabled { get { return Loggers.Any(l => l.IsErrorEnabled); } }
        public override bool IsFatalEnabled { get { return Loggers.Any(l => l.IsFatalEnabled); } }
        public override bool IsInfoEnabled { get { return Loggers.Any(l => l.IsInfoEnabled); } }
        public override bool IsTraceEnabled { get { return Loggers.Any(l => l.IsTraceEnabled); } }
        public override bool IsWarnEnabled { get { return Loggers.Any(l => l.IsWarnEnabled); } }

        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            List<Exception> exceptions = null;
            foreach (var logger in Loggers)
            {
                try
                {
                    LogActions[level](logger, message, exception);
                }
                catch (Exception e)
                {
                    if (exceptions == null)
                        exceptions = new List<Exception>();
                    exceptions.Add(e);
                }
            }

            if (exceptions != null)
                throw new AggregateException("One or more exceptions occured while forwarding log message to multiple loggers", exceptions);
        }
    }
}
