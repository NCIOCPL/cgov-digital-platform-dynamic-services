namespace CancerGov.ClinicalTrialsAPI
{
    public interface IClinicalTrialSearchAPISection
    {
        /// <summary>
        /// Retrieves the API key.
        /// </summary>
        string APIKey { get; }

        /// <summary>
        /// Gets the base URL of the Clinical Trials API.
        /// </summary>
        string BaseUrl { get; }
    }
}
