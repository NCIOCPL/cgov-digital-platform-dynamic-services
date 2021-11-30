using System;
using System.Net.Mail;
using Common.Logging;
using Common.Logging.Simple;

namespace NCI.Logging
{
    /// <summary>
    /// Extension of AbstractSimpleLogger - logs all messages of a valid loglevel to the Windows Event Log.
    /// </summary>
    public class EmailLogger : AbstractSimpleLogger
    {
        /// <summary>
        /// The sender email address
        /// </summary>
        public string EmailAddressFrom { get; private set; }

        /// <summary>
        /// The Destination email addresses
        /// </summary>
        public string EmailAddressesTo { get; private set; }

        private const string _loggingFormat = "Facility: {0} \n\nError Level: {1} \n\nMessage:\n{2}\n\nException:\n{3}";

        /// <summary>
        /// Constructor for an EmailLogger object.
        /// </summary>
        /// <param name="logName">The name of the logger.</param>
        /// <param name="logLevel">The minimum LogLevel of messages that will be logged.</param>
        /// ...
        /// <param name="emailAddressFrom">The email address that will be used in the From field.</param>
        /// <param name="emailAddressesTo">The email addresses that will be used in the To field.</param>
        public EmailLogger(string logName, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName,
            string dateTimeFormat, string emailAddressFrom, string emailAddressesTo)
            : base(logName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
        {
            EmailAddressFrom = emailAddressFrom;
            EmailAddressesTo = emailAddressesTo;
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

            SendEmail(Name, fullMessage);
        }

        /// <summary>
        /// Sends out the Email for the Error Message that the client needs to send.
        /// </summary>
        /// <param name="Subject">Subject of the Email.</param>
        /// <param name="Body">Body of the Email.</param>
        private void SendEmail(string subject, string body)
        {
            try
            {
                using (MailMessage message = new MailMessage(EmailAddressFrom, EmailAddressesTo, subject, body))
                {
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw new NCILoggingException("NCI.Logging.EmailLogger: Could not send email.", ex);
            }
        }
    }
}
