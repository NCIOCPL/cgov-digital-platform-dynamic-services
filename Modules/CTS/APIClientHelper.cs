using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CancerGov.ClinicalTrialsAPI;
using NCI.ClinicalTrials.Configuration;

namespace NCI.ClinicalTrials
{
    /// <summary>
    /// Helper class get getting an API client instance.
    /// </summary>
    public static class APIClientHelper
    {
        /// <summary>
        /// Gets an instance of a v1 CTAPI client
        /// </summary>
        /// <returns></returns>
        public static ClinicalTrialsAPIClient GetV1ClientInstance()
        {

            string baseApiPath = BasicClinicalTrialSearchAPISection.GetAPIUrl();
            string versionPath = ConfigurationManager.AppSettings["ClinicalTrialsAPIBasepath"];

            if (string.IsNullOrWhiteSpace(versionPath))
                throw new ConfigurationErrorsException("error: ClinicalTrialsAPIBasepath cannot be null or empty");



            HttpClient client = new HttpClient();
            //NOTE: the base URL MUST have a trailing slash
            client.BaseAddress = new Uri(String.Format("{0}/{1}/", baseApiPath, versionPath));

            return new ClinicalTrialsAPIClient(client);
        }   
    }
}
