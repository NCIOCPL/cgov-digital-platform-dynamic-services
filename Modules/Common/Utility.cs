using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCI.Util
{
    /// <summary>
    /// Class to hold common CDE utility functions
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// Determines if a URL matches a common web resource extension.  This is usually for ignoring a URL in a redirection or loader.
        /// </summary>
        /// <param name="url">The URL to check</param>
        /// <returns>True if the URL matches a known extension</returns>
        static public bool IgnoreWebResource(string url)
        {
            url = url.ToLower();
            return url.IndexOf(".axd") != -1 || 
                   url.IndexOf(".css") != -1 || 
                   url.IndexOf(".eot") != -1 || 
                   url.IndexOf(".gif") != -1 || 
                   url.IndexOf(".ico") != -1 || 
                   url.IndexOf(".jpg") != -1 || 
                   url.IndexOf(".js") != -1 || 
                   url.IndexOf(".png") != -1 || 
                   url.IndexOf(".svg") != -1 || 
                   url.IndexOf(".ttf") != -1 ||
                   url.IndexOf(".woff2") != -1 ||
                   url.IndexOf(".woff") != -1;
        }
    }
}
