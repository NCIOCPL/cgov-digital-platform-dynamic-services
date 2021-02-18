using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CancerGov.ClinicalTrialsAPI
{
   public class Intervention
    {
        /// <summary>
        /// Gets or sets the name for this term
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or the NCI Thesaurus Concept ID codes for this term
        /// </summary>
        [JsonProperty("codes")]
        public string[] Codes { get; set; }

        /// <summary>
        /// Gets or sets the ancestor IDs of this term.
        /// </summary>
        [JsonProperty("synonyms")]
        public string[] Synonyms { get; set; }

        /// <summary>
        /// Gets or sets the parent ID for this term
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the type of this term.  (e.g. subtype)
        /// </summary>
        [JsonProperty("count")]
        public string Count { get; set; }
    }
}
