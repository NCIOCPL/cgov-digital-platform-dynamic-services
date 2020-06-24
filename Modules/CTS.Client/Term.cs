using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CancerGov.ClinicalTrialsAPI
{
    /// <summary>
    /// Represents a term as returned by the API
    /// </summary>
    public class Term
    {
        /// <summary>
        /// Gets or sets the key for this term
        /// </summary>
        [JsonProperty("term_key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the display text for this term
        /// </summary>
        [JsonProperty("term")]
        public string DisplayText { get; set; }

        /// <summary>
        /// Gets or sets the type of this term.  (e.g. _disease)
        /// </summary>
        [JsonProperty("term_type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the NCI Thesaurus Concept ID codes for this term
        /// </summary>
        [JsonProperty("codes")]
        public string[] Codes { get; set; }


        //Not exposing counts or scores for now.

    }
}
