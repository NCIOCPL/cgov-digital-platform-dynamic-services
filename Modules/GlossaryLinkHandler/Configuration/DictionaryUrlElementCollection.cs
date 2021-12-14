using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryLinkHandler.Configuration
{
    /// <summary>
    /// This class is a collection of all the dictionary URLs
    /// </summary>
    [ConfigurationCollection(
        typeof(DictionaryUrlElement),
        AddItemName = "add",
        CollectionType = ConfigurationElementCollectionType.BasicMap
        )]
    public class DictionaryUrlElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates a new ConfigurationElement
        /// </summary>
        protected override ConfigurationElement CreateNewElement()
        {
            return new DictionaryUrlElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element 
        /// </summary>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DictionaryUrlElement)element).Name;
        }

        /// <summary>
        /// Looks up a DictionaryUrlElement object from its unique key. 
        /// </summary>
        /// <value></value>
        public new DictionaryUrlElement this[String key]
        {
            get { return (DictionaryUrlElement)BaseGet(key); }
        }
    }
}
