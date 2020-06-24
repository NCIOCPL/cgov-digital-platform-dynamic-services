using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CancerGov.ClinicalTrialsAPI
{
    /// <summary>
    /// Represents a collection of clinical trials as returned by the listing endpoint
    /// </summary>
    public class ClinicalTrialsCollection
    {
        /// <summary>
        /// Gets the total number of results
        /// </summary>
        [JsonProperty("total")]
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the collection of trials
        /// <remarks>NOTE: this will not be *all* of the trials, but a subset based on the size parameter passed to the API</remarks>
        /// </summary>
        [JsonProperty("trials")]
        public ClinicalTrial[] Trials { get; set; }

    }
}
