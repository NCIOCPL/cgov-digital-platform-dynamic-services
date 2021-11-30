using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CancerGov.ClinicalTrialsAPI
{
    /// <summary>
    /// Represents a collection of diseases as returned by the disease endpoint
    /// </summary>
    public class DiseaseCollection
    {
        /// <summary>
        /// Gets the total number of results.  The API does not currently support TotalResults for diseases endpoint.
        /// </summary>
        //[JsonProperty("total")]
        //public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the collection of diseases
        /// <remarks>NOTE: this will not be *all* of the diseases, but a subset based on the size and code parameters passed to the API</remarks>
        /// </summary>
        [JsonProperty("terms")]
        public Disease[] Terms { get; set; }
    }
}
