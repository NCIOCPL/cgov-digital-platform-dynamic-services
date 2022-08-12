using System;

namespace CancerGov.ClinicalTrialsAPI
{
    /// <summary>
    /// Error thrown in the event of an API server problem.
    /// </summary>
    public class APIServerErrorException : Exception
    {
        /// <inheritdoc />
        public APIServerErrorException() : base() { }

        /// <inheritdoc />
        public APIServerErrorException(string message) : base(message) { }

        /// <inheritdoc />
        public APIServerErrorException(string message, Exception inner) : base(message, inner) { }
    }
}
