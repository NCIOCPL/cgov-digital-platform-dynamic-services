using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CancerGov.ClinicalTrialsAPI
{
    public class InterventionCollection
    {
        /// <summary>
        /// Gets the total number of results. The API does not currently support TotalResults for intervention endpoint.
        /// </summary>
        //[JsonProperty("total")]
        //public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the collection of interventions
        /// <remarks>NOTE: this will not be *all* of the interventions, but a subset based on the size and code parameters passed to the API</remarks>
        /// </summary>
        [JsonProperty("terms")]
        public Intervention[] Terms { get; set; }
    }
}
