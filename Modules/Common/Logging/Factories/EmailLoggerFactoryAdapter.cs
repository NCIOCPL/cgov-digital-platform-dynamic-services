using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;

namespace NCI.Logging.Factories
{
    class EmailLoggerFactoryAdapter : AbstractSimpleLoggerFactoryAdapter
    {
        /// <summary>
        /// The sender email address
        /// </summary>
        public string EmailAddressFrom { get; private set; }

        /// <summary>
        /// The Destination email addresses
        /// </summary>
        public string EmailAddressesTo { get; private set; }

        public EmailLoggerFactoryAdapter(NameValueCollection properties)
            : base(properties)
        {
            string addressFrom, addressesTo;
            if (properties.TryGetValue("emailAddressFrom", out addressFrom))
            {
                EmailAddressFrom = addressFrom;
            }

            if (properties.TryGetValue("emailAddressesTo", out addressesTo))
            {
                EmailAddressesTo = addressesTo;
            }

        }

        protected override ILog CreateLogger(string name, LogLevel level, 
            bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
        {
            return new EmailLogger(name, level, showLevel, showDateTime, showLogName, dateTimeFormat,
                EmailAddressFrom, EmailAddressesTo);
        }
    }
}
