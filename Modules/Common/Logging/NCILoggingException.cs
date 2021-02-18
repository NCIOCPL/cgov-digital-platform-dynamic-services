using System;

namespace NCI.Logging
{
    /// <summary>
    /// This Class has behaviour that raises Exceptions raised on performing logging operations.
    /// </summary>
    public class NCILoggingException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NCILoggingException() : base() { }
        /// <summary>
        /// Constructor that takes Log Message as Argument
        /// </summary>
        /// <param name="msg">The message passed into the Custom Exception.</param>
        public NCILoggingException(string msg) : base(msg) { }
        /// <summary>
        /// Constructor that takes Log Message and Exception objects as Arguments
        /// </summary>
        /// <param name="msg">The message passed into the Custom Exception.</param>
        /// <param name="innerException">The inner Exception for the Custom Exception.</param>
        public NCILoggingException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
