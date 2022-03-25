using System;
using System.Web;
using System.Net.Http;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

using GlossaryLinkHandler.Configuration;

namespace GlossaryLinkHandler.HttpHandlers
{

    public class GlossaryLinkHrefHandler : IHttpHandler
    {
        private string _id;
        private string _dictionary;
        private string _audience;
        private string _language;
        private bool _useFallback = true;
        

        // implement IsReusable method
        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// Handles requests to view glossary term popup links when JS is turned off in the browser - mainly 
        /// occurs when the site is being indexed, and we want to reduce the number of 404s that occur from this.
        /// If the term exists in a dictionary we have on Cancer.gov, we redirect to that term definition page.
        /// If it does not, we display the term and definition.
        /// <summary>
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;

            // Parse search params for API call
            ParseSearchParams(request);

            GlossaryAPIClient client = new GlossaryAPIClient();

            // Perform API request for glossary term
            GlossaryTerm term = client.GetById(_dictionary, _audience, _language, _id, _useFallback);

            // If glossary term for given CDRID does not exist, return a 404.
            if (term == null)
            {
                throw new HttpException(404, String.Format("Term with CDRID {0} does not exist", _id));
            }
            // If glossary term does exist, determine whether to redirect or draw the term and definition.
            else
            {
                string termForRedirect = String.IsNullOrEmpty(term.PrettyUrlName) ? term.TermId : term.PrettyUrlName;
                string dictionaryPath = GetDictionaryPath(term.Dictionary, term.Audience.ToString(), term.Language);

                // If the term's dictionary exists on Cancer.gov AND the requested dictionary, audience, and language match the returned term's
                // dictionary, audience, and language, redirect to the term definition page with that dictionary path.
                if (
                    !string.IsNullOrWhiteSpace(dictionaryPath) &&
                    _dictionary.ToLower() == term.Dictionary.ToLower() &&
                    _audience.ToLower() == term.Audience.ToString().ToLower() &&
                    _language.ToLower() == term.Language.ToLower()
                    )
                {
                    context.Response.RedirectPermanent($"{dictionaryPath}/def/{termForRedirect}");
                }
                // If the term's dictionary does not exist on Cancer.gov or the requesed dictionary, audience, and language do not match the
                // returned term's dictionary, audience, and language, draw the term and definition.
                else
                {
                    context.Response.Write(GetTermDefinitionHTML(term));
                }
            }
        }

        // Parse and format search parameters for API call
        private void ParseSearchParams(HttpRequest request)
        {
            // Format ID parameter for API call
            _id = request.QueryString["id"];
            _id = Regex.Replace(_id, "^CDR0+", "", RegexOptions.Compiled);

            // Format dictionary parameter for API call
            _dictionary = request.QueryString["dictionary"];
            if (!string.IsNullOrWhiteSpace(_dictionary) && _dictionary.ToLower() == "genetic")
            {
                _dictionary = "Genetics";
            }

            // Format audience parameter for API call
            _audience = request.QueryString["version"];
            if (!string.IsNullOrWhiteSpace(_audience) && _audience.ToLower() == "patient")
            {
                _audience = "Patient";
            }
            if (!string.IsNullOrWhiteSpace(_audience) && _audience.ToLower() == "healthprofessional")
            {
                _audience = "HealthProfessional";
            }

            // Fallback logic to set dictionary and audience parameters if values are missing from the request string.
            // Our default glossary is the Dictionary of Cancer Terms, which only has Patient definitions, and also is
            // the only glossary with Patient definitions.
            // Any other glossary is for HP and the logic below handles that.
            if (string.IsNullOrWhiteSpace(_dictionary) && string.IsNullOrWhiteSpace(_audience))
            {
                _dictionary = "Cancer.gov";
                _audience = "Patient";
            }

            // If audience is set but dictionary is not, set the value for dictionary depending on audience.
            if (!string.IsNullOrWhiteSpace(_audience) && string.IsNullOrWhiteSpace(_dictionary))
            {
                switch (_audience.ToLower())
                {
                    case "patient":
                        _dictionary = "Cancer.gov";
                        _audience = "Patient";
                        break;

                    case "healthprofessional":
                        _dictionary = "NotSet";
                        _audience = "HealthProfessional";
                        break;

                    default:
                        _dictionary = "NotSet";
                        _audience = "Patient";
                        break;
                }
            }

            // If dictionary is set but audience is not, set the value for audience depending on dictionary.
            if (!string.IsNullOrWhiteSpace(_dictionary) && string.IsNullOrWhiteSpace(_audience))
            {
                _audience = _dictionary.ToLower() == "cancer.gov" ? "Patient" : "HealthProfessional";
            }

            // Format language parameter for API call
            _language = request.QueryString["language"];
            if (string.IsNullOrWhiteSpace(_language) || _language == "English")
            {
                _language = "en";
            }
            else if (_language == "Spanish")
            {
                _language = "es";
            }
        }

        private string GetDictionaryPath(string termDictionary, string audience, string termLanguage)
        {
            // Return the dictionary path based on given parameters. Currently, only the Cancer.gov and Genetics
            // dictionary values could potentially return a path other than null, as they are the only dictionaries
            // that exist on the site.
            switch (termDictionary.ToLower())
            {
                case "cancer.gov":
                    // The Dictionary of Cancer Terms in both English and Spanish only include terms with the
                    // Patient audience. If the audience is not Patient, return no dictionary path.
                    if (audience.ToLower() == "patient")
                    {
                        // The only language options for the dictionaries are English or Spanish. Only return
                        // a dictionary path if one of those language options is set.
                        if (termLanguage.ToLower() == "en")
                        {
                            return GlossaryLinkHandlerSection.GetEnglishTermDictionaryUrl();
                        }

                        if (termLanguage.ToLower() == "es")
                        {
                            return GlossaryLinkHandlerSection.GetSpanishTermDictionaryUrl();
                        }
                    }

                    return null;

                case "genetics":
                    // The Genetics Dictionary only includes English terms with the HealthProfessional audience.
                    // If the audience is not Patient and the language is not English, return no dictioary path.
                    if (audience.ToLower() == "healthprofessional" && termLanguage.ToLower() == "en")
                    {
                        return GlossaryLinkHandlerSection.GetEnglishGeneticsDictionaryUrl();
                    }

                    return null;

                case "notset":
                default:
                    break;
            }

            return null;
        }

        private string GetTermDefinitionHTML (GlossaryTerm term)
        {
            string html = @"
<!DOCTYPE html>
    <html lang=""" + term.Language + @""">
    <head>
        <title>Definition of " + term.TermName + @"</title>
        <meta name=""robots"" content=""noindex, nofollow"" />
        <style>
        @import url(""https://fonts.googleapis.com/css2?family=Noto+Sans:wght@400;700&display=swap"");
        .definition {
            font-family: ""Noto Sans"";
            margin: 30px 15px;
        }
        .definition__header {
            margin-bottom: 15px;
        }
        .definition h1 {
            font-size: 16px;
            font-weight: 700;
            display: inline-block;
        }
        .definition dd {
            margin: 0;
        }
        .definition dd +* {
            margin - top: 15px;
        }
        </style>
    </head>";

            html += @"
    <body>
        <div class=""definition"">
            <dl>
            <div class=""definition__header"">
                <a href=""/"" id=""logoAnchor"">
                <img src=""https://www.cancer.gov/publishedcontent/images/images/design-elements/logos/nci-logo-full.svg""  id=""logoImage"" alt=""National Cancer Institute"" width=""300"" />
                </a>
            </div>
            <dt>
                <h1>" + term.TermName + @"</h1>";

            if (term.Pronunciation != null && term.Pronunciation.Key != null)
            {
                html += @"
                <span class=""pronunciation"">" + term.Pronunciation.Key + @"</span>";
            }

             html += @"
            </dt>";

            if (term.Definition != null && term.Definition.Text != null)
            {
                html += @"
            <dd>" + term.Definition.Text + @"</dd>";
            }

            html += @"
        </dl>
        </div>
    </body>";

            html += @"
</html>";

            return html;
        }
    }
}
