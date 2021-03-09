using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using Common.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using GlossaryLinkHandler.Configuration;

namespace GlossaryLinkHandler
{
    public class GlossaryAPIClient
    {
        static ILog log = LogManager.GetLogger(typeof(GlossaryAPIClient));

        private HttpClient _client = null;

        /// <summary>
        /// Creates a new instance of a Glossary API client.
        /// </summary>
        public GlossaryAPIClient()
        {
            string apiUrl = WebAPISection.GetAPIUrl();

            HttpClient client = new HttpClient();

            //NOTE: the base URL MUST have a trailing slash
            client.BaseAddress = new Uri(apiUrl);

            this._client = client;
        }

        /// <summary>
        /// Calls the GetById endpoint of the Glossary API
        /// </summary>
        /// <param name="dictionary">Dictionary name (required)</param>
        /// <param name="audience">Audience type(required)</param>
        /// <param name="language">Language to use (required)</param>
        /// <param name="id">The ID of the term</param>
        /// <param name="useFallback">Whether or not to use the fallback logic</param>
        /// <returns>A Glossary Term object</returns>
        public GlossaryTerm GetById(
            string dictionary,
            string audience,
            string language,
            string id,
            bool useFallback = false 
            )
        {// Check fields
            if (String.IsNullOrWhiteSpace(dictionary))
            {
                throw new ArgumentNullException("The dictionary is null or an empty string");
            }
            if (String.IsNullOrWhiteSpace(audience))
            {
                throw new ArgumentNullException("The audience is null or an empty string");
            }
            if (String.IsNullOrWhiteSpace(language))
            {
                throw new ArgumentNullException("The language is null or an empty string");
            }
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("The ID is null or an empty string");
            }

            // Set up search param string: {dictionary}/{audience}/{language}/{id:long}?useFallback={useFallback}
            string searchParam = $"{dictionary}/{audience}/{language}/{id}?useFallback={useFallback.ToString()}";

            //Get the HTTP response content from GET request
            HttpContent httpContent = ReturnGetRespContent("Terms", searchParam);
            if (httpContent != null)
            {
                Task<GlossaryTerm> term = ReadAsJsonAsync<GlossaryTerm>(httpContent);
                var resultTerm = term.Result;
                return resultTerm;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Gets the response content of a GET request.
        /// </summary>
        /// <param name="path">Path for client address</param>
        /// <param name="param">Param in URL</param>
        /// <returns>HTTP response content if successful, or null if not found</returns>
        public HttpContent ReturnGetRespContent(String path, String param)
        {
            HttpResponseMessage response = null;
            HttpContent content = null;
            String notFound = "NotFound";

            
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // We want this to be synchronous, so call Result right away.
            //NOTE: When using HttpClient.BaseAddress as we are, the path must not have a preceeding slash
            response = _client.GetAsync(path + "/" + param).Result;
            if (response.IsSuccessStatusCode)
            {
                content = response.Content;
            }
            else
            {
                string errorMessage = "Response: " + response.Content.ReadAsStringAsync().Result + "\nAPI path: " + this._client.BaseAddress.ToString() + path + "/" + param;
                if (response.StatusCode.ToString() == notFound)
                {
                    // If trial is not found, log 404 message and return content as null
                    log.Debug(errorMessage);
                }
                else
                {
                    // If response is other error message, log and throw exception
                    log.Error(errorMessage);
                    throw new Exception(errorMessage);
                }
            }

            return content;
        }

        /// <summary>
        /// Deserialize API-returned JSON into a GlossaryTerm object
        /// </summary>
        /// <param name="content">The HttpContent returned by the API (a JSON string)</param>
        /// <returns>A deserialized glossary term object</returns>
        public static async Task<GlossaryTerm> ReadAsJsonAsync<GlossaryTerm>(HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            GlossaryTerm result = JsonConvert.DeserializeObject<GlossaryTerm>(json);
            return result;
        }
    }
}
