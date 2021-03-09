using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryLinkHandler.Configuration
{
    /// <summary>
    /// Configuration information for a Web API
    /// </summary>
    public class WebAPISection : ConfigurationSection
    {
        private static readonly string CONFIG_SECTION_NAME = "legacyServices/webAPI";

        /// <summary>
        /// Gets the URL of the Web API server.
        /// </summary>
        [ConfigurationProperty("apiUrl", IsRequired=true)]
        public string APIUrl
        {
            get { return (string)base["apiUrl"]; }
        }

        /// <summary>
        /// Gets the URL for the Web API from the configuration
        /// </summary>
        public static string GetAPIUrl()
        {
            WebAPISection config = (WebAPISection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

            if (config == null)
                throw new ConfigurationErrorsException("The configuration section, " + CONFIG_SECTION_NAME + ", cannot be found");

            if (string.IsNullOrWhiteSpace(config.APIUrl))
                throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + "error: apiUrl cannot be null or empty");

            return config.APIUrl;
        }
    }
}
