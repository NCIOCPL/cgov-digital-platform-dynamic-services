using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GlossaryLinkHandler
{
    /// <summary>
    /// Represents a Glossary Term as returned by http://webapis.cancer.gov/glossary/v1/
    /// </summary>
    public class GlossaryTerm
    {

        #region Classes for child objects

        /// <summary>
        /// Class representing the details required for Pronunciation
        /// </summary>
        public class TermPronunciation
        {
            /// <summary>
            /// Gets or sets the Key for Pronunciation
            /// </summary>
            [JsonProperty("key")]
            public string Key { get; set; }

            /// <summary>
            /// Gets or sets the value for Audio for Pronunciation
            /// </summary>
            [JsonProperty("audio")]
            public string Audio { get; set; }
        }

        /// <summary>
        /// Class representing the details required for Definition
        /// </summary>    
        public class TermDefinition
        {
            /// <summary>
            /// Gets or sets the html value for the definition
            /// </summary>
            [JsonProperty("html")]
            public string Html { get; set; }

            /// <summary>
            /// Gets or sets the text for the definition
            /// </summary>
            [JsonProperty("text")]
            public string Text { get; set; }
            /// TODO Convert string to URL class
        }

        /// <summary>
        /// Represets a glossary term in another language
        /// </summary>
        public class TermOtherLanguage
        {
            /// <summary>
            /// Gets or sets the Language for the Glosary Term
            /// </summary>
            [JsonProperty("language")]
            public string Language { get; set; }

            /// <summary>
            /// Gets or sets the TermName for the translation
            /// </summary>
            [JsonProperty("termName")]
            public string TermName { get; set; }

            /// <summary>
            /// If available, the translation's human readable name, rendered in a URL-friendly format.
            /// </summary>
            /// <value>Empty string if no human-readable name is available.</value>
            [JsonProperty("prettyUrlName")]
            public string PrettyUrlName { get; set; }
        }

        /// <summary>
        /// Describes a related GlossaryTerm.
        /// </summary>
        public class GlossaryResource : IRelatedResource
        {
            /// <summary>
            /// Notes the related resource type.
            /// </summary>
            /// <value>Always RelatedResourceType.GlossaryTerm</value>
            [JsonProperty("Type")]
            [JsonConverter(typeof(StringEnumConverter))]
            public RelatedResourceType Type { get; set; }

            /// <summary>
            /// Short text description or name of the resource.
            /// </summary>
            [JsonProperty("Text")]
            public string Text { get; set; }

            /// <summary>
            /// The glossary term's CDR ID.
            /// </summary>
            /// <value></value>
            [JsonProperty("termId")]
            public long TermId { get; set; }

            /// <summary>
            /// The GlossaryTerm's intended audience.
            /// </summary>
            /// <value>
            /// healthprofessional - Doctors and other health professionals.
            /// patient - Patients, friends, and family members.
            /// </value>
            [JsonProperty("audience")]
            [JsonConverter(typeof(StringEnumConverter))]
            public AudienceType Audience { get; set; }

            /// <summary>
            /// If available, the term's human readable name, rendered in a URL-friendly format.
            /// </summary>
            /// <value>Empty string if no human-readable name is available.</value>
            [JsonProperty("prettyUrlName")]
            public string PrettyUrlName { get; set; }
        }

        /// <summary>
        /// Describes the link to a related resource.
        /// May be Drug Information Summary, Cancer Information Summary,
        /// or an external link.
        /// </summary>
        public class LinkResource : IRelatedResource
        {
            /// <summary>
            /// Notes the related resource type.
            /// </summary>
            /// <value>One of RelatedResourceType.DrugSummary,
            /// RelatedResourceType.CancerSummary or
            /// RelatedResourceType.External
            /// </value>
            [JsonProperty("Type")]
            [JsonConverter(typeof(StringEnumConverter))]
            public RelatedResourceType Type { get; set; }

            /// <summary>
            /// URL of the resource item.
            /// </summary>
            [JsonProperty("Url")]
            public Uri Url;

            /// <summary>
            /// Short text description or name of the resource.
            /// </summary>
            [JsonProperty("Text")]
            public string Text { get; set; }
        }

        /// <summary>
        /// Metalink
        /// </summary>
        public class Metalink
        {
            /// <summary>
            /// url
            /// </summary>
            [JsonProperty("self")]
            public Uri Self;
        }

        /// <summary>
        /// Describes an Image file source.
        /// </summary>
        public class ImageSource
        {
            /// <summary>
            /// The logical size.
            /// </summary>
            [JsonProperty("Size")]
            public string Size { get; set; }

            /// <summary>
            /// The image's source's URI.
            /// </summary>
            [JsonProperty("Src")]
            public Uri Src { get; set; }
        }

        /// <summary>
        /// Describes an Image content item.
        /// </summary>
        public class Image : IMedia
        {
            /// <summary>
            /// Type of media this class will represent.
            /// </summary>
            /// <value>Always MediaType.Image</value>
            [JsonProperty("Type")]
            [JsonConverter(typeof(StringEnumConverter))]
            public MediaType Type { get; set; }

            /// <summary>
            /// A collection of image source files.
            /// </summary>
            /// <value></value>
            [JsonProperty("ImageSources")]
            public ImageSource[] ImageSources { get; set; }

            /// <summary>
            /// The CDR ID of the referenced image.
            /// </summary>
            [JsonProperty("Ref")]
            public string Ref { get; set; }

            /// <summary>
            /// The image's alternate text version, suitable for displaying in an HTML alt= attribute.
            /// </summary>
            [JsonProperty("Alt")]
            public string Alt { get; set; }

            /// <summary>
            /// String containing the image's caption.
            /// </summary>
            [JsonProperty("Caption")]
            public string Caption { get; set; }
        }

        /// <summary>
        /// Reporesents a video media item.
        /// </summary>
        public class Video : IMedia
        {
            /// <summary>
            /// Notes the media type.
            /// </summary>
            /// <value>Always MediaType.Video</value>
            [JsonProperty("Type")]
            [JsonConverter(typeof(StringEnumConverter))]
            public MediaType Type { get; set; }

            /// <summary>
            /// Where is the video hosted?
            /// </summary>
            /// <value>Always HostingTypes.youtube</value>
            [JsonProperty("Hosting")]
            [JsonConverter(typeof(StringEnumConverter))]
            public HostingTypes Hosting { get; set; }

            /// <summary>
            /// The CDR ID of the referenced video.
            /// </summary>
            [JsonProperty("Ref")]
            public string Ref { get; set; }

            /// <summary>
            /// The video's unique identifier.
            /// </summary>
            [JsonProperty("UniqueId")]
            public string UniqueId { get; set; }

            /// <summary>
            /// String naming the template to use when rendering the video.
            /// </summary>
            [JsonProperty("Template")]
            public string Template { get; set; }

            /// <summary>
            /// The video's title.
            /// </summary>
            [JsonProperty("Title")]
            public string Title { get; set; }

            /// <summary>
            /// The video's caption.
            /// </summary>
            [JsonProperty("Caption")]
            public string Caption { get; set; }
        }

        #endregion

        /// <summary>
        /// Gets or sets the Id for the Glossary Term
        /// </summary>
        [JsonProperty("termId")]
        public string TermId { get; set; }

        /// <summary>
        /// Gets or sets the Language for the Glossary Term
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the Dictionary for the Glossary Term
        /// </summary>
        [JsonProperty("dictionary")]
        public string Dictionary { get; set; }

        /// <summary>
        /// Gets or sets the AudienceType for the Glossary Term
        /// </summary>
        [JsonProperty("audience")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AudienceType Audience { get; set; }

        /// <summary>
        /// Gets or sets the TermName for the Glossary Term
        /// </summary>
        [JsonProperty("termName")]
        public string TermName { get; set; }

        /// <summary>
        /// Gets or sets the FirstLetter for the Glossary Term
        /// </summary>
        [JsonProperty("firstLetter")]
        public string FirstLetter { get; set; }

        /// <summary>
        /// Gets or sets the prettyUrlName for the Glossary Term
        /// </summary>
        [JsonProperty("prettyUrlName")]
        public string PrettyUrlName { get; set; }

        /// <summary>
        /// Gets or sets the Pronunciation for the Glossary Term
        /// </summary>
        [JsonProperty("pronunciation")]
        public TermPronunciation Pronunciation { get; set; }

        /// <summary>
        /// Gets or sets the Definition for the Glossary Term
        /// </summary>
        [JsonProperty("definition")]
        public TermDefinition Definition { get; set; }

        /// <summary>
        /// Gets or sets the translations of the Glossary Term
        /// </summary>
        [JsonProperty("otherLanguages")]
        public TermOtherLanguage[] OtherLanguages { get; set; }

        /// <summary>
        /// Gets or sets the related resources for the Glossary Term
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(RelatedResourceJsonConverter))]
        public IRelatedResource[] RelatedResources { get; set; }

        /// <summary>
        /// Gets or sets the Definition for the Glosary Term
        /// </summary>
        
        [JsonProperty(ItemConverterType = typeof(MediaJsonConverter))]
        public IMedia[] Media { get; set; }

        /// <summary>
        /// Constructs a new instance of a ClinicalTrial
        /// </summary>
        public GlossaryTerm()
        {
            //Ensure we have one of these in case the trial does not
            OtherLanguages = new TermOtherLanguage[] { };
            RelatedResources = new IRelatedResource[] { };
            Media = new IMedia[] { };
        }
    }
}
