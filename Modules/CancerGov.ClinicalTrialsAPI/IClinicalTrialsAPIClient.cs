using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CancerGov.ClinicalTrialsAPI
{
    public interface IClinicalTrialsAPIClient
    {
        /// <summary>
        /// Retrieve a single clinical trial from the API via its ID.
        /// </summary>
        /// <param name="id">Either the NCI ID or the NCT ID</param>
        /// <returns>JSON object representation of the clinical trial</returns>
        Task<JObject> GetOneTrial(string id);

        /// <summary>
        /// Retrieve the details of a list of trials.
        /// </summary>
        /// <param name="trialIDs">The set of trials to retrieve</param>
        /// <param name="from">Offset into the set of possible returns for the first result to retrieve.</param>
        /// <param name="size">Number of results to retrieve.</param>
        /// <returns>An object containing an array of trial details.</returns>
        Task<JObject> GetMultipleTrials(IEnumerable<string> trialIDs, int size = 10, int from = 0);

        /// <summary>
        /// Gets a collection of disease names from the API.
        /// </summary>
        /// <param name="diseaseCodes">List of disease concept codes to retrieve.</param>
        /// <returns>A JSON array containing a collection of disease names and codes</returns>
        Task<JObject> LookupDiseaseNames(IEnumerable<string> diseaseCodes);

        /// <summary>
        /// Gets a collection of intervention names from the API.
        /// </summary>
        /// <param name="interventionCodes">List of intervention concept codes to retrieve.</param>
        /// <returns>Collection of intervention details</returns>
        Task<JObject> LookupInterventionNames(IEnumerable<string> interventionCodes);
    }
}
