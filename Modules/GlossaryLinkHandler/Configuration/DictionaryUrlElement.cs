using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryLinkHandler.Configuration
{
    /// <summary>
    /// This class represents a single dictionary url
    /// </summary>
    public class DictionaryUrlElement : ConfigurationElement
    {
        /// <summary>
        /// Name of the dictionary
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
        }

        /// <summary>
        /// URL of the dictionary
        /// </summary>
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)base["url"]; }
        }
    }
}
