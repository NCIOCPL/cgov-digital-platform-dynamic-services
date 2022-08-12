using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Common.Logging;
using Newtonsoft.Json.Linq;

namespace CancerGov.ClinicalTrialsAPI
{
    public class ClinicalTrialsAPIClient : IClinicalTrialsAPIClient
    {
        public const string ARGUMENT_NOT_NULL_MSG = "Argument must not be null.";
        public const string BASE_ADDRESS_REQUIRED_MSG = "A base address is required";
        public const string ARGUMENT_API_KEY_MSG = "A valid API key is required.";
        public const string INVALID_BASE_URL_MSG = "Must be a valid, absolute URL.";

        public const string DISEASE_LIST_EMPTY_ARGUMENT_LIST = "Must be a list of one or more concept IDs.";
        public const string DISEASE_LIST_INVALID_CCODES = "Disease concept IDs must be of the form C#####.";

        public const string INTERVENTION_LIST_EMPTY_ARGUMENT_LIST = "Must be a list of one or more concept IDs.";
        public const string INTERVENTION_LIST_INVALID_CCODES = "Intervention concept IDs must be of the form C#####.";

        public const string JSON_CONTENT = "application/json";

        static ILog log = LogManager.GetLogger(typeof(ClinicalTrialsAPIClient));

        static readonly Regex CCodeMatcher = new Regex(@"^\s*C\d+\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private HttpClient _client = null;

        private IClinicalTrialSearchAPISection _config;

        /// <summary>
        /// Creates a new instance of a Clinicaltrials API client.
        /// </summary>
        /// <param name="client">An instance of HttpClient for making the actual HTTP calls.
        /// </param>
        /// <param name="config">An instance of IClinicalTrialSearchAPISection</param>
        /// <remarks>The trials API always gzips the response, regardless of the accepted encodings. As a result, the HttpClient instance must be constructed with an
        /// HttpClientHandler which has the AutomaticDecompression property set to allow gzip. Anticipating future changes, deflate is also required.
        /// 
        /// The classic .Net framework does not support brotli compression.
        /// </remarks>
        public ClinicalTrialsAPIClient(HttpClient client, IClinicalTrialSearchAPISection config)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), ARGUMENT_NOT_NULL_MSG);
            if (config == null)
                throw new ArgumentNullException(nameof(config), ARGUMENT_NOT_NULL_MSG);

            if (client.BaseAddress == null || String.IsNullOrWhiteSpace(client.BaseAddress.ToString()))
                throw new ArgumentException(BASE_ADDRESS_REQUIRED_MSG, nameof(client));

            this._client = client;
            this._config = config;
        }

        /// <summary>
        /// Retrieve a single clinical trial from the API via its ID.
        /// </summary>
        /// <param name="id">Either the NCI ID or the NCT ID</param>
        /// <returns>JSON object representing the clinical trial</returns>
        async public Task<JObject> GetOneTrial(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("The trial identifier is null or an empty string");
            }

            JObject rtnTrial = null;

            // Get the HTTP response content from GET request
            HttpContent httpContent = await ReturnGetRespContent("trials", id, null, true);
            if (httpContent != null)
            {
                string responseBody = await httpContent.ReadAsStringAsync();
                rtnTrial = JObject.Parse(responseBody);
            }

            return rtnTrial;
        }

        /// <summary>
        /// Retrieve the details of a list of trials.
        /// </summary>
        /// <param name="trialIDs">The set of trials to retrieve</param>
        /// <param name="from">Offset into the set of possible returns.</param>
        /// <param name="size">Number of results to retrieve.</param>
        /// <returns>An object containing an array of trial details.</returns>
        async public Task<JObject> GetMultipleTrials(
            IEnumerable<string> trialIDs,
            int size = 10,
            int from = 0
        )
        {
            size = size > 0 ? size : 10;
            from = from > 0 ? from : 0;

            JObject requestBody = new JObject();
            requestBody.Add(new JProperty("size", size));
            requestBody.Add(new JProperty("from", from));
            requestBody.Add(new JProperty("nci_id", trialIDs));

            HttpContent httpContent = await ReturnPostRespContent("trials", requestBody);
            string responseBody = await httpContent.ReadAsStringAsync();
            JObject result = JObject.Parse(responseBody);

            return result;
        }

        /// <inheritdoc/>
        async public Task<JObject> LookupDiseaseNames(
            IEnumerable<string> diseaseCodes
        )
        {
            if (diseaseCodes == null || diseaseCodes.Count() == 0)
                throw new ArgumentNullException(nameof(diseaseCodes), DISEASE_LIST_EMPTY_ARGUMENT_LIST);

            // The parameter lists on these two exceptions are reversed. That's OK. It's just annoying.
            if (diseaseCodes.Any(code => !CCodeMatcher.IsMatch(code)))
                throw new ArgumentException(DISEASE_LIST_INVALID_CCODES, nameof(diseaseCodes));

            // Create a list of query string parameter tuples.
            var codes = from code in diseaseCodes select ("codes", code);
            List<(string Name, string value)> queryParams = new List<(string Name, string value)>(codes);

            // Limit the returned fields
            queryParams.Add(("include", "name"));
            queryParams.Add(("include", "codes"));

            JObject rtnDiseaseNames = null;

            // Get the HTTP response content from GET request
            HttpContent httpContent = await ReturnGetRespContent("diseases", null, queryParams, false);
            if (httpContent != null)
            {
                string responseBody = await httpContent.ReadAsStringAsync();
                rtnDiseaseNames = JObject.Parse(responseBody);
            }

            return rtnDiseaseNames;
        }

        /// <inheritdoc/>
        async public Task<JObject> LookupInterventionNames(
            IEnumerable<string> interventionCodes
        )
        {
            if (interventionCodes == null || interventionCodes.Count() == 0)
                throw new ArgumentNullException(nameof(interventionCodes), INTERVENTION_LIST_EMPTY_ARGUMENT_LIST);

            // The parameter lists on these two exceptions are reversed. That's OK. It's just annoying.
            if (interventionCodes.Any(code => !CCodeMatcher.IsMatch(code)))
                throw new ArgumentException(INTERVENTION_LIST_INVALID_CCODES, nameof(interventionCodes));

            // Create a list of query string parameter tuples.
            var codes = from code in interventionCodes select ("codes", code);
            List<(string Name, string value)> queryParams = new List<(string Name, string value)>(codes);

            // Limit the returned fields
            queryParams.Add(("include", "name"));
            queryParams.Add(("include", "codes"));

            JObject rtnInterventionNames = null;

            // Get the HTTP response content from GET request
            HttpContent httpContent = await ReturnGetRespContent("interventions", null, queryParams, false);
            if (httpContent != null)
            {
                string responseBody = await httpContent.ReadAsStringAsync();
                rtnInterventionNames = JObject.Parse(responseBody);
            }

            return rtnInterventionNames;
        }


        /// <summary>
        /// Gets the response content of a GET request.
        /// </summary>
        /// <param name="path">Path for client address</param>
        /// <param name="inlineParam">Param in the path. Specify null if there is none.</param>
        /// <param name="queryParams">A list of tuples containing name value pairs for the query parameters. Specify null or an empty list if there are none.</param>
        /// <param name="endpointAllows404Response">Set true if the endpoint is one which returns a 404 as a meaningful return, set false if a 404 return is a genuine error.</param>
        /// <returns>HTTP response content</returns>
        async protected Task<HttpContent> ReturnGetRespContent(String path, String inlineParam, IEnumerable<(string Name, string Value)> queryParams, bool endpointAllows404Response)
        {
            string apiKey = _config.APIKey;

            // build up relative URL.
            if (!String.IsNullOrWhiteSpace(inlineParam))
                path += $"/{inlineParam}";
            if (queryParams != null)
            {
                var list = from item in queryParams select $"{item.Name.Trim()}={item.Value.Trim()}";
                path += "?" + String.Join("&", list);
            }

            //NOTE: When using HttpClient.BaseAddress as we are, the path must not have a preceeding slash
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_CONTENT));
            request.Headers.Add("x-api-key", apiKey);

            HttpContent content = null;
            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                content = response.Content;
            }
            else
            {
                string errorResponse = null;
                if (response.Content != null)
                    errorResponse = await response.Content.ReadAsStringAsync();
                string errorMessage = $"Response: '{errorResponse}' \nAPI path: {this._client.BaseAddress.ToString() + path + "/" + inlineParam}";

                // Some endpoints (e.g. get for a single trial) return a status 404 if nothing is found.
                // Other endpoints (e.g. diseases and interventions) return an empty JSON structure, in which case a 404 is likely a configuration error.
                if (response.StatusCode == HttpStatusCode.NotFound && endpointAllows404Response)
                {
                    // Log 404 message and return content as null
                    log.Debug(errorMessage);
                }
                else
                {
                    // If response is other error message, log and throw exception
                    log.Error(errorMessage);
                    throw new APIServerErrorException(errorMessage);
                }
            }

            return content;
        }


        /// <summary>
        /// Gets the response content of a POST request.
        /// </summary>
        /// <param name="path">Path for client address</param>
        /// <param name="request">Params passed in with request body</param>
        /// <returns>HTTP response content</returns>
        async protected Task<HttpContent> ReturnPostRespContent(String path, JObject requestBody)
        {
            string apiKey = _config.APIKey;

            //NOTE: When using HttpClient.BaseAddress as we are, the path must not have a preceeding slash
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_CONTENT));
            request.Headers.Add("x-api-key", apiKey);

            request.Content = new StringContent(requestBody.ToString(), Encoding.UTF8);

            HttpContent responseContent = null;
            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                responseContent = response.Content;
            }
            else
            {
                string errorResponse = null;
                if (response.Content != null)
                    errorResponse = await response.Content.ReadAsStringAsync();
                string errorMessage = $"Response: '{errorResponse}' \nAPI path: " + this._client.BaseAddress.ToString() + path;
                throw new APIServerErrorException(errorMessage);
            }

            return responseContent;
        }


    }
}
