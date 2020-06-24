using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CancerGov.ClinicalTrialsAPI
{
    /// <summary>
    /// Represents a collection of terms as returned by the terms endpoint
    /// </summary>
    public class TermCollection
    {
        /// <summary>
        /// Gets the total number of results
        /// </summary>
        [JsonProperty("total")]
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the collection of terms
        /// <remarks>NOTE: this will not be *all* of the terms, but a subset based on the size parameter passed to the API</remarks>
        /// </summary>
        [JsonProperty("terms")]
        public Term[] Terms { get; set; }

    }
}
