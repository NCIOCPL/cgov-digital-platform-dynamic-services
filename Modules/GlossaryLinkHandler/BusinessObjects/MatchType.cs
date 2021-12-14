using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GlossaryLinkHandler
{
    /// <summary>
    /// Friendly names for the ways to perform a match.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MatchType
    {
        /// <summary>
        /// Search for items that begin with the search text.
        /// </summary>
        Begins,

        /// <summary>
        /// Search for items that contain the search text.
        /// </summary>
        Contains
    }
}