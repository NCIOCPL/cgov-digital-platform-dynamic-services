using System;
using System.Collections.Generic;
using System.Text;

namespace NCI.Logging
{
    
    
    /// <summary>
    /// Specifies the level of Error Messages that should be logged.
    /// </summary>
    [Flags]
    public enum NCIErrorLevel : uint
    {
        /// <summary>
        /// No Logging neccessary.
        /// </summary>
        Clear = 0x00,
        /// <summary>
        /// Messages related to debugging.
        /// </summary>
        Debug = 0x01,
        /// <summary>
        /// Messages related to specific information.
        /// </summary>
        Info = Debug<<1,
        /// <summary>
        /// Warning Messages only.
        /// </summary>
        Warning = Info << 1,
        /// <summary>
        /// All Error Messages.
        /// </summary>
        Error = Warning << 1,
        /// <summary>
        /// Complete Error Messages, such as failed database connectivitiy.
        /// </summary>
        Critical = Error << 1,
        /// <summary>
        /// All messages for any level
        /// </summary>
        All = Debug | Info | Warning | Error | Critical

    }

    /// <summary>
    /// Class contains Behaviour and properties related to Logging purposes within NCI.
    /// </summary>
    public static class Logger
    {
        private static Common.Logging.ILog log;

        static Logger()
        {
            try
            {
                log = Common.Logging.LogManager.GetLogger(typeof(Logger));
            }
            catch (Exception ex)
            {
                throw new TypeInitializationException("NCI.Logging.Logger", ex);
            }
        }

        /// <summary>
        /// Logs Error to the provider by sending facility, message and NCIErrorLevel in the message.
        /// </summary>
        /// <param name="message">Description of the Error Message.</param>
        /// <param name="facility">Description of the facility of the Error Message.</param>
        /// <param name="level">Specifies the level of Error Messages.</param>
        public static void LogError(string facility, string message, NCIErrorLevel level)
        {
            RouteToLogger(level, null, facility, message);
        }

        /// <summary>
        /// Logs Error to the provider by sending facility, message, NCIErrorLevel and Exception in the message.
        /// </summary>
        /// <param name="message">Description of the Error Message.</param>
        /// <param name="facility">Description of the facility of the Error Message.</param>
        /// <param name="level">Specifies the level of Error Messages.</param>
        /// <param name="ex">Actual Exception object.</param>
        public static void LogError(string facility, string message, NCIErrorLevel level, Exception ex)
        {
            RouteToLogger(level, ex, facility, message);
        }

        /// <summary>
        /// Logs Error to the provider by sending facility,NCIErrorLevel and Exception in the message.
        /// </summary>
        /// <param name="facility">Description of the facility of the Error Message.</param>
        /// <param name="level">Specifies the level of Error Messages.</param>
        /// <param name="ex">Actual Exception object.</param>
        public static void LogError(string facility, NCIErrorLevel level, Exception ex)
        {
            RouteToLogger(level, ex, facility);
        }

        private static void RouteToLogger(NCIErrorLevel level, Exception ex, string facility, string message = "")
        {
            switch (level)
            {
                case NCIErrorLevel.Critical:
                    if (ex != null)
                    {
                        log.Fatal(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message), ex);
                    }
                    else
                    {
                        log.Fatal(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message));
                    }
                    break;
                case NCIErrorLevel.Error:
                    if (ex != null)
                    {
                        log.Error(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message), ex);
                    }
                    else
                    {
                        log.Error(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message));
                    }
                    break;
                case NCIErrorLevel.Warning:
                    if (ex != null)
                    {
                        log.Warn(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message), ex);
                    }
                    else
                    {
                        log.Warn(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message));
                    }
                    break;
                case NCIErrorLevel.Info:
                    if (ex != null)
                    {
                        log.Info(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message), ex);
                    }
                    else
                    {
                        log.Info(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message));
                    }
                    break;
                case NCIErrorLevel.Debug:
                    if (ex != null)
                    {
                        log.Debug(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message), ex);
                    }
                    else
                    {
                        log.Debug(facility + (String.IsNullOrWhiteSpace(message) ? "" : ": " + message));
                    }
                    break;
            }
        }
    }

}
