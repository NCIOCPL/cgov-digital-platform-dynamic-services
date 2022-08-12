using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Logging;

namespace CancerGov.ClinicalTrialsAPI
{
    public class ClinicalTrialSearchAPISection : ConfigurationSection, IClinicalTrialSearchAPISection
    {
        static ILog log = LogManager.GetLogger(typeof(ClinicalTrialSearchAPISection));

        private static readonly string CONFIG_SECTION_NAME = "clinicalTrialSearchAPI";

        /// <summary>
        /// Gets the base URL of the Clinical Trials API.
        /// </summary>
        [ConfigurationProperty("baseUrl", IsRequired = true)]
        public string BaseUrl => (string)base["baseUrl"];

        /// <summary>
        /// Gets the name of the environment variable containing the API key.
        /// </summary>
        [ConfigurationProperty("ClinicalTrialsAPIKey_VAR", IsRequired = true)]
        public string APIKeyVariableName => (string)base["ClinicalTrialsAPIKey_VAR"];

        /// <summary>
        /// Retrieves the API key from the environment variable specified by the ClinicalTrialsAPIKey_VAR setting.
        /// </summary>
        public string APIKey
        {
            get
            {
                string keyVariable = APIKeyVariableName;
                if (String.IsNullOrWhiteSpace(keyVariable))
                {
                    string configMsg = $"Section: {CONFIG_SECTION_NAME}, ClinicalTrialsAPIKey_VAR property is not set.";

                    log.Error(configMsg);
                    throw new ConfigurationErrorsException();
                }

                string key = Environment.GetEnvironmentVariable(keyVariable);
                if (String.IsNullOrWhiteSpace(key))
                {
                    string configMsg = $"API key environment variable '{keyVariable}' is not set.";

                    log.Error(configMsg);
                    throw new ConfigurationErrorsException();
                }

                return key;
            }
        }

        /// <summary>
        /// Gets an instance of the configuration section.
        /// </summary>
        public static ClinicalTrialSearchAPISection Instance
            => (ClinicalTrialSearchAPISection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

    }
}
