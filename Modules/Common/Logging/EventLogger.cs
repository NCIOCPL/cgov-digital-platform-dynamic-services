using System;
using System.Diagnostics;
using Common.Logging;
using Common.Logging.Simple;

namespace NCI.Logging
{
    /// <summary>
    /// Extension of AbstractSimpleLogger - logs all messages of a valid loglevel to the Windows Event Log.
    /// </summary>
    public class EventLogger : AbstractSimpleLogger
    {
        private const string _loggingFormat = "Facility: {0} \n\nError Level: {1} \n\nMessage:\n{2}\n\nException:\n{3}";

        /// <summary>
        /// The name of the Windows log source to which messages will be logged.
        /// </summary>
        public string LogSource { get; private set; }

        /// <summary>
        /// Constructor for an EventLogger object.
        /// </summary>
        /// <param name="logName">The name of the logger.</param>
        /// <param name="logLevel">The minimum LogLevel of messages that will be logged.</param>
        /// ...
        /// <param name="logSource">The Windows Logsource to receive</param>
        public EventLogger(string logName, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat, string logSource)
            : base(logName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
        {
            LogSource = logSource;
        }

        /// <summary>
        /// The internal write method that will be called once a message has been determined to be of the configured log level.
        /// </summary>
        /// <param name="level">The LogLevel of the message.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">Any exception that has been passed along with the message.</param>
        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            string fullMessage = String.Format(
                            _loggingFormat,
                            Name,
                            level,
                            message,
                            exception);

            WriteEvent(level, fullMessage);
        }

        private void WriteEvent(LogLevel level, string fullMessage)
        {
            try
            {
                EventLog.WriteEntry(
                    LogSource,
                    fullMessage,
                    GetEventLogETFromLogLevel(level));
            }
            catch (Exception newex)
            {
                throw new NCILoggingException("NCI.Logging.EventLogger: Could not write error to Event Log.", newex);
            }
        }

        private EventLogEntryType GetEventLogETFromLogLevel(LogLevel level)
        {
            if (level >= LogLevel.Error)
                return EventLogEntryType.Error;
            else if (level > LogLevel.Info)
                return EventLogEntryType.Warning;
            else
                return EventLogEntryType.Information;
        }
    }
}
