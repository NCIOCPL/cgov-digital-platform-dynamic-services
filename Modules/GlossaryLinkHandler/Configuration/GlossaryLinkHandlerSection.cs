using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryLinkHandler.Configuration
{
    /// <summary>
    /// GlossaryLinkHandler configuration information
    /// </summary>
    public class GlossaryLinkHandlerSection : ConfigurationSection
    {
        private static readonly string CONFIG_SECTION_NAME = "legacyServices/glossaryLinkHandler";
        private static readonly string ENGLISH_TERMS_URL = "EnglishTerms";
        private static readonly string SPANISH_TERMS_URL = "SpanishTerms";
        private static readonly string ENGLISH_GENETICS_URL = "EnglishGenetics";

        /// <summary>
        /// Collection of dictionary URLs
        /// </summary>
        [ConfigurationProperty("dictionaryUrls")]
        [ConfigurationCollection(typeof(DictionaryUrlElement), AddItemName = "add")]
        public DictionaryUrlElementCollection DictionaryUrls
        {
            get { return (DictionaryUrlElementCollection)base["dictionaryUrls"]; }
        }

        /// <summary>
        /// Gets the English Dictionary of Cancer Terms URL
        /// </summary>
        public static string GetEnglishTermDictionaryUrl()
        {
            string loc = "";
            GlossaryLinkHandlerSection config = (GlossaryLinkHandlerSection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

        if (config == null)
                throw new ConfigurationErrorsException("The configuration section, " + CONFIG_SECTION_NAME + ", cannot be found");

            if (config.DictionaryUrls == null)
                throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + " error: dictionaryUrls cannot be null or empty");

            if(config.DictionaryUrls[ENGLISH_TERMS_URL] == null)
                throw new ConfigurationErrorsException(ENGLISH_TERMS_URL + " error: element cannot be null or empty");

            if (string.IsNullOrWhiteSpace(config.DictionaryUrls[ENGLISH_TERMS_URL].Url))
                throw new ConfigurationErrorsException(ENGLISH_TERMS_URL + " error: element's URL cannot be null or empty");

            loc = config.DictionaryUrls[ENGLISH_TERMS_URL].Url;
            return loc;
        }

        /// <summary>
        /// Gets the Spanish Dictionary of Cancer Terms URL
        /// </summary>
        public static string GetSpanishTermDictionaryUrl()
        {
            string loc = "";
            GlossaryLinkHandlerSection config = (GlossaryLinkHandlerSection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

            if (config == null)
                throw new ConfigurationErrorsException("The configuration section, " + CONFIG_SECTION_NAME + ", cannot be found");

            if (config.DictionaryUrls == null)
                throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + " error: dictionaryUrls cannot be null or empty");

            if (config.DictionaryUrls[SPANISH_TERMS_URL] == null)
                throw new ConfigurationErrorsException(SPANISH_TERMS_URL + " error: element cannot be null or empty");

            if (string.IsNullOrWhiteSpace(config.DictionaryUrls[SPANISH_TERMS_URL].Url))
                throw new ConfigurationErrorsException(SPANISH_TERMS_URL + " error: element's URL cannot be null or empty");

            loc = config.DictionaryUrls[SPANISH_TERMS_URL].Url;
            return loc;
        }

        /// <summary>
        /// Gets the English Dictionary of Genetics Terms URL
        /// </summary>
        public static string GetEnglishGeneticsDictionaryUrl()
        {
            string loc = "";
            GlossaryLinkHandlerSection config = (GlossaryLinkHandlerSection)ConfigurationManager.GetSection(CONFIG_SECTION_NAME);

            if (config == null)
                throw new ConfigurationErrorsException("The configuration section, " + CONFIG_SECTION_NAME + ", cannot be found");

            if (config.DictionaryUrls == null)
                throw new ConfigurationErrorsException(CONFIG_SECTION_NAME + " error: dictionaryUrls cannot be null or empty");

            if (config.DictionaryUrls[ENGLISH_GENETICS_URL] == null)
                throw new ConfigurationErrorsException(ENGLISH_GENETICS_URL + " error: element cannot be null or empty");

            if (string.IsNullOrWhiteSpace(config.DictionaryUrls[ENGLISH_GENETICS_URL].Url))
                throw new ConfigurationErrorsException(ENGLISH_GENETICS_URL + " error: element's URL cannot be null or empty");

            loc = config.DictionaryUrls[ENGLISH_GENETICS_URL].Url;
            return loc;
        }
    }
}
