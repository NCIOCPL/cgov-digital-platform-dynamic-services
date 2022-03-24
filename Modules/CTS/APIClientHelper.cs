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
    /// Factory class to setup and instaniate API client instances.
    /// </summary>
    public static class APIClientHelper
    {

        // A single instance of HttpClient is intended to be shared by all requests within
        // an application.
        // See: https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netframework-4.6.1
        static readonly HttpClient _httpClient;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static APIClientHelper()
        {
            Uri baseUrl = new Uri(ClinicalTrialSearchAPISection.Instance.BaseUrl);

            // The ClinicalTrials API always returns using gzip encoding, regardless of whether the client sends
            // an Accept header. Even if this weren't the case, we'd likely want to save some time and bandwidth.
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = baseUrl;

            // Formally add the accept headers. NOTE: Brotli compression is not supported in the 4.x framework. That requires .Net Core 3.0 and later.
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
        }


        /// <summary>
        /// Factory method for creating an instance of ClinicalTrialsAPIClient.
        /// </summary>
        /// <returns></returns>
        public static ClinicalTrialsAPIClient GetClientInstance()
        {
            ClinicalTrialSearchAPISection config = ClinicalTrialSearchAPISection.Instance;
            return new ClinicalTrialsAPIClient(_httpClient, config);
        }   
    }
}
