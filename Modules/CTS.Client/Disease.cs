using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CancerGov.ClinicalTrialsAPI
{
    /// <summary>
    /// Represents a disease as returned by the API
    /// </summary>
    public class Disease
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
        [JsonProperty("ancestor_ids")]
        public string[] AncestorIDs { get; set; }

        /// <summary>
        /// Gets or sets the parent ID for this term
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentID { get; set; }

        /// <summary>
        /// Gets or sets the type of this term.  (e.g. subtype)
        /// </summary>
        [JsonProperty("type")]
        public string[] Type { get; set; }
    }
}
