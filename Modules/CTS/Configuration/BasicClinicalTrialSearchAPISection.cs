using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCI.ClinicalTrials.Configuration
{
    /// <summary>
    /// ClinicalTrialsAPI configuration information for the Basic Clinical Trial Search
    /// </summary>
    public class BasicClinicalTrialSearchAPISection : ConfigurationSection
    {
        private static readonly string CONFIG_SECTION_NAME = "nci/search/basicClinicalTrialSearchAPI";
        //private static readonly string EVS_MAPPING_NAME = "EvsMapping";
        //private static readonly string MAPPING_OVERRIDE_NAME = "OverrideMapping";
        //private static readonly string TOKEN_OVERRIDE_NAME = "TokensMapping";

        /// <summary>
        /// Gets the host name of the ClinicalTrialsAPI server.
        /// </summary>
        [ConfigurationProperty("apiHost", IsRequired=true)]
        public string APIHost
        {
            get { return (string)base["apiHost"]; }
        }

        /// <summary>
        /// Gets the port number for the API
        /// </summary>
        [ConfigurationProperty("apiPort", IsRequired=false)]
        public string APIPort
        {
            get { return (string)base["apiPort"]; }
        }

        /// <summary>
        /// Gets the protocol for the API
        /// </summary>
        [ConfigurationProperty("apiProtocol", IsRequired = true)]
        public string APIProtocol
        {
            get { return (string)base["apiProtocol"]; }
        }

        /// <summary>
        /// Collection of term mapping files
        /// </summary>
        //[ConfigurationProperty("termMappingFiles")]
        //[ConfigurationCollection(typeof(TermMappingFileElement), AddItemName = "add")]
        //public TermMappingFileElementCollection TermMappingFiles
        //{
        //    get { return (TermMappingFileElementCollection)base["termMappingFiles"]; }
        //}
    

        /// <summary>
        /// Gets the URL for the ClinicalTrials API from the configuration
        /// </summary>
        public static string GetAPIUrl()
        {
            string url = "";
            BasicClinicalTrialSearchAPISection config = (BasicClinicalTrialSearchAPISection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

            if (config == null)
                throw new ConfigurationErrorsException("The configuration section, " + CONFIG_SECTION_NAME + ", cannot be found");

            if (string.IsNullOrWhiteSpace(config.APIProtocol))
                throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + "error: apiProtocol cannot be null or empty");

            if (string.IsNullOrWhiteSpace(config.APIHost))
                throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + "error: apiHost cannot be null or empty");

            url = string.Format("{0}://{1}", config.APIProtocol, config.APIHost);

            if (!string.IsNullOrWhiteSpace(config.APIPort))
            {
                url += ":" + config.APIPort;
            }

            return url;
        }

        /// <summary>
        /// Gets the EVS Mapping file path for clinical trial dynamic listing pages from the configuration
        /// </summary>
        //public static string GetEvsMappingFilePath()
        //{
        //    string loc = "";
        //    BasicClinicalTrialSearchAPISection config = (BasicClinicalTrialSearchAPISection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

        //    if (config == null)
        //        throw new ConfigurationErrorsException("The configuration section, " + CONFIG_SECTION_NAME + ", cannot be found");

        //    if (config.TermMappingFiles == null)
        //        throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + "error: termMappingFiles cannot be null or empty");

        //    if(config.TermMappingFiles[EVS_MAPPING_NAME] == null)
        //        throw new ConfigurationErrorsException(EVS_MAPPING_NAME + "error: element cannot be null or empty");

        //    if (string.IsNullOrWhiteSpace(config.TermMappingFiles[EVS_MAPPING_NAME].FilePath))
        //        throw new ConfigurationErrorsException(EVS_MAPPING_NAME + "error: element's filePath cannot be null or empty");

        //    loc = config.TermMappingFiles[EVS_MAPPING_NAME].FilePath;
        //    return loc;
        //}

        /// <summary>
        /// Gets the Override Mapping file path for clinical trial dynamic listing pages from the configuration
        /// </summary>
        //public static string GetOverrideMappingFilePath()
        //{
        //    string loc = "";
        //    BasicClinicalTrialSearchAPISection config = (BasicClinicalTrialSearchAPISection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

        //    if (config == null)
        //        throw new ConfigurationErrorsException("The configuration section, " + CONFIG_SECTION_NAME + ", cannot be found");

        //    if (config.TermMappingFiles == null)
        //        throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + "error: termMappingFiles cannot be null or empty");

        //    if (config.TermMappingFiles[MAPPING_OVERRIDE_NAME] == null)
        //        throw new ConfigurationErrorsException(MAPPING_OVERRIDE_NAME + "error: element cannot be null or empty");

        //    if (string.IsNullOrWhiteSpace(config.TermMappingFiles[MAPPING_OVERRIDE_NAME].FilePath))
        //        throw new ConfigurationErrorsException(MAPPING_OVERRIDE_NAME + "error: element's filePath cannot be null or empty");

        //    loc = config.TermMappingFiles[MAPPING_OVERRIDE_NAME].FilePath;
        //    return loc;
        //}

        /// <summary>
        /// Gets the Token Mapping file path for clinical trial dynamic listing pages from the configuration
        /// </summary>
        //public static string GetTokenMappingFilePath()
        //{
        //    string loc = "";
        //    BasicClinicalTrialSearchAPISection config = (BasicClinicalTrialSearchAPISection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

        //    if (config == null)
        //        throw new ConfigurationErrorsException("The configuration section, " + CONFIG_SECTION_NAME + ", cannot be found");

        //    if (config.TermMappingFiles == null)
        //        throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + "error: termMappingFiles cannot be null or empty");

        //    if (config.TermMappingFiles[TOKEN_OVERRIDE_NAME] == null)
        //        throw new ConfigurationErrorsException(TOKEN_OVERRIDE_NAME + "error: element cannot be null or empty");

        //    if (string.IsNullOrWhiteSpace(config.TermMappingFiles[TOKEN_OVERRIDE_NAME].FilePath))
        //        throw new ConfigurationErrorsException(TOKEN_OVERRIDE_NAME + "error: element's filePath cannot be null or empty");

        //    loc = config.TermMappingFiles[TOKEN_OVERRIDE_NAME].FilePath;
        //    return loc;
        //}
    }
}
