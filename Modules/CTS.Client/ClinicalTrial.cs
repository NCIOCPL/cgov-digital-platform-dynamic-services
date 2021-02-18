using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using Newtonsoft.Json;

namespace CancerGov.ClinicalTrialsAPI
{
    /// <summary>
    /// Represents a Clinical Trial as returned by http://clinicaltrialsapi.cancer.gov
    /// </summary>
    public class ClinicalTrial
    {

        #region Subclasses of this object

        /// <summary>
        /// Represents a unspecified protocol ID
        /// </summary>
        public class OtherId
        {
            /// <summary>
            /// The type of identifier
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// The ID
            /// </summary>
            [JsonProperty("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// Represents the primary purpose (Type of Trial) of this trial
        /// </summary>
        public class PrimaryPurposeInformation
        {
            /// <summary>
            /// Gets or sets the purpose code for this trial. (e.g. Treatment, Prevention)
            /// </summary>
            [JsonProperty("primary_purpose_code")]
            public string Code { get; set; }

            /// <summary>
            /// Gets or sets additional text of this purpose.  
            /// <remarks>This appears on trials with a Code equal to OTHER</remarks>
            /// </summary>
            [JsonProperty("primary_purpose_other_text")]
            public string OtherText { get; set; }

            /// <summary>
            /// Gets or Sets an additional qualifier code for this trial.  
            /// <remarks>This is not currently in use</remarks>
            /// </summary>
            [JsonProperty("primary_purpose_additional_qualifier_code")]
            public string AdditionalQualifierCode { get; set; }
        }

        public class Phase
        {
            /// <summary>
            /// Gets or sets the purpose code for this trial. (e.g. "I", "I_II", "NA")
            /// </summary>
            [JsonProperty("phase")]
            public string PhaseNumber { get; set; }

            /// <summary>
            /// Gets or sets additional text for this trial's phase.
            /// </summary>
            [JsonProperty("phase_other_text")]
            public string PhaseOtherText { get; set; }

            /// <summary>
            /// Gets or Sets an additional phase qualifier code for this trial.  
            /// </summary>
            [JsonProperty("phase_additional_qualifier_code")]
            public string PhaseAdditionalQualifierCode { get; set; }
        }

        public class Masking
        {
            [JsonProperty("")]
            public object masking { get; set; }
            [JsonProperty("")]
            public object masking_allocation_code { get; set; }
            [JsonProperty("")]
            public object masking_role_investigator { get; set; }
            [JsonProperty("")]
            public object masking_role_outcome_assessor { get; set; }
            [JsonProperty("")]
            public object masking_role_subject { get; set; }
            [JsonProperty("")]
            public object masking_role_caregiver { get; set; }
        }

        /// <summary>
        /// Class representing an overall contact for the trial
        /// </summary>
        public class CentralContactInformation
        {
            /// <summary>
            /// Gets or sets the Email of this contact
            /// </summary>
            [JsonProperty("central_contact_email")]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the name of this contact
            /// </summary>
            [JsonProperty("central_contact_name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the phone of this contact
            /// </summary>
            [JsonProperty("central_contact_phone")]
            public object Phone { get; set; }

            /// <summary>
            /// Gets or sets the type of this contact
            /// </summary>
            [JsonProperty("central_contact_type")]
            public object Type { get; set; }
        }

        /// <summary>
        /// Represents a collaborator of this trial
        /// </summary>
        public class Collaborator
        {
            /// <summary>
            /// Gets or sets the name of this collaborator
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the functional role of this collaborator
            /// </summary>
            [JsonProperty("functional_role")]
            public string FunctionalRole { get; set; }

            /// <summary>
            /// Gets or sets the status of this collaborator
            /// </summary>
            [JsonProperty("status")]
            public string status { get; set; }
        }

        public class Disease2
        {
            [JsonProperty("")]
            public string id { get; set; }
            [JsonProperty("")]
            public string display_name { get; set; }
            [JsonProperty("")]
            public object lead_disease_indicator { get; set; }
            [JsonProperty("")]
            public string date_last_created { get; set; }
            [JsonProperty("")]
            public string date_last_updated { get; set; }
        }

        public class Disease
        {
            [JsonProperty("")]
            public Disease2 disease { get; set; }
        }

        /// <summary>
        /// Represents the Eligibility Information of a trial
        /// </summary>
        public class EligibilityInformation
        {

            /// <summary>
            /// Represents the structured eligibility criteria of a trial
            /// </summary>
            public class StructuredCriteriaInformation
            {
                [JsonProperty("gender")]
                public string Gender { get; set; }

                [JsonProperty("max_age")]
                public string MaxAgeStr { get; set; }

                [JsonProperty("max_age_number")]
                public int MaxAgeInt { get; set; }

                [JsonProperty("max_age_unit")]
                public string MaxAgeUnits { get; set; }

                [JsonProperty("min_age")]
                public string MinAgeStr { get; set; }

                [JsonProperty("min_age_number")]
                public int MinAgeInt { get; set; }

                [JsonProperty("min_age_unit")]
                public string MinAgeUnits { get; set; }
            }

            /// <summary>
            /// Represents an unstructured eligibility criterion of a trial
            /// </summary>
            public class UnstructuredCriterion
            {
                /// <summary>
                /// Gets or sets a bool indicating if this criterion indications inclusion for the trial.
                /// If JSON property is set to null, set to default value (false).
                /// </summary>
                [DefaultValue(false)]
                [JsonProperty("inclusion_indicator", NullValueHandling = NullValueHandling.Ignore)]
                public bool IsInclusionCriterion { get; set; }

                /// <summary>
                /// Gets or sets the content of this criterion
                /// </summary>
                [JsonProperty("description")]
                public string Description { get; set; }
            }

            /// <summary>
            /// Gets or sets the Structured Eligibility Information for this trial (e.g. Age and Gender)
            /// </summary>
            [JsonProperty("structured")]
            public StructuredCriteriaInformation StructuredCriteria { get; set; }

            /// <summary>
            /// Gets and sets a list of unstructured eligibility criteria for this trial
            /// </summary>
            [JsonProperty("unstructured")]
            public List<UnstructuredCriterion> UnstructuredCriteria { get; set; }

            //Expose some helper properties and methods.  (Age, Gender, Inclusion and exclusion

            /// <summary>
            /// Gets the Gender Criterion
            /// </summary>
            public string Gender
            {
                get { return StructuredCriteria.Gender; }
            }
        }

        public class Intervention
        {
            [JsonProperty("")]
            public string intervention_name { get; set; }
            [JsonProperty("")]
            public string intervention_type { get; set; }
            [JsonProperty("")]
            public object intervention_description { get; set; }
            [JsonProperty("")]
            public string date_created_intervention { get; set; }
            [JsonProperty("")]
            public string date_updated_intervention { get; set; }
        }

        public class Arm
        {
            [JsonProperty("")]
            public string arm_name { get; set; }
            [JsonProperty("")]
            public object arm_type { get; set; }
            [JsonProperty("")]
            public string arm_description { get; set; }
            [JsonProperty("")]
            public string date_created_arm { get; set; }
            [JsonProperty("")]
            public string date_updated_arm { get; set; }
            [JsonProperty("")]
            public List<Intervention> interventions { get; set; }
        }

        #endregion


        #region Trial Identifiers

        /// <summary>
        /// Gets or sets the NCI ID for this trial
        /// </summary>
        [JsonProperty("nci_id")]
        public string NCIID { get; set; }

        /// <summary>
        /// Gets or sets the ClinicalTrials.gov ID
        /// </summary>
        [JsonProperty("nct_id")]
        public string NCTID { get; set; }

        /// <summary>
        /// Gets or sets the primary protocol ID of this trial
        /// </summary>
        [JsonProperty("protocol_id")]
        public string ProtocolID { get; set; }

        /// <summary>
        /// Gets or sets the NCI Center for Cancer Research identifier of this trial, if it exists
        /// </summary>
        [JsonProperty("ccr_id")]
        public string CCRID { get; set; }

        /// <summary>
        /// Gets or sets the NCI Cancer Therapy Evaluation Program identifier of this trial, if it exists
        /// </summary>
        [JsonProperty("ctep_id")]
        public string CTEPID { get; set; }

        /// <summary>
        /// Gets or sets the NCI Division of Cancer Prevention identifier of this trial, if it exists
        /// </summary>
        [JsonProperty("dcp_id")]
        public string DCPID { get; set; }

        /// <summary>
        /// Gets or sets a collection of additional unspecified trial identifiers
        /// </summary>
        [JsonProperty("other_ids")]
        public List<OtherId> OtherTrialIDs { get; set; }

        /// <summary>
        /// Gets or sets the Phase object
        /// </summary>
        [JsonProperty("phase")]
        public Phase TrialPhase { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the shorter title for this trial
        /// </summary>
        [JsonProperty("brief_title")]
        public string BriefTitle { get; set; }

        /// <summary>
        /// Gets or sets the official title of this trial
        /// </summary>
        [JsonProperty("official_title")]
        public string OfficialTitle { get; set; }

        /// <summary>
        /// Gets or sets a brief summary of this trial
        /// </summary>
        [JsonProperty("brief_summary")]
        public string BriefSummary { get; set; }

        /// <summary>
        /// Gets or sets the detailed description for this trial
        /// <remarks>This will contain newline characters (\r\n) to indicate a new line
        /// </remarks>
        /// </summary>
        [JsonProperty("detail_description")]
        public string DetailedDescription { get; set; }

        /// <summary>
        /// Gets or sets the eligibility information for this trial
        /// </summary>
        [JsonProperty("eligibility")]
        public EligibilityInformation EligibilityInfo { get; set; }

        /// <summary>
        /// Gets or sets the primary purpose of this trial (Treatment, Screening, Prevention, etc.)
        /// </summary>
        [JsonProperty("primary_purpose")]
        public PrimaryPurposeInformation PrimaryPurpose { get; set; }

        /// <summary>
        /// Gets or sets the status of this trial
        /// </summary>
        [JsonProperty("current_trial_status")]
        public string CurrentTrialStatus { get; set; }


        #region lead organization and sponsor information

        /// <summary>
        /// Gets or sets the name of the lead organization of this trial
        /// </summary>
        [JsonProperty("lead_org")]
        public string LeadOrganizationName { get; set; }

        /// <summary>
        /// Gets or sets a list of the collaborators for this trial
        /// </summary>
        [JsonProperty("collaborators")]
        public List<Collaborator> Collaborators { get; set; }

        /// <summary>
        /// Gets or sets the name of the overall principal investigator of this trial
        /// </summary>
        [JsonProperty("principal_investigator")]
        public string PrincipalInvestigator { get; set; }

        /// <summary>
        /// Gets or sets the overall contact information for this trial
        /// </summary>
        [JsonProperty("central_contact")]
        public CentralContactInformation CentralContact { get; set; }


        #endregion

        #region Study Site Specific Classes and Properties

        /// <summary>
        /// Class Representing a Study Site where the trials are being held
        /// </summary>
        public class StudySite
        {
            /// <summary>
            /// Represents a GeoLocation as returned by the API
            /// </summary>
            public class GeoLocation
            {
                /// <summary>
                /// Gets or sets the latitude for this location
                /// </summary>
                [JsonProperty("lat")]
                public double Latitude { get; set; }

                /// <summary>
                /// Gets or sets the longitude for this location
                /// </summary>
                [JsonProperty("lon")]
                public double Longitude { get; set; }
            }

            /// <summary>
            /// Gets or sets the first line of an address for this trial site
            /// </summary>
            [JsonProperty("org_address_line_1")]
            public string AddressLine1 { get; set; }

            /// <summary>
            /// Gets or sets the second line of an address for this trial site
            /// </summary>
            [JsonProperty("org_address_line_2")]
            public string AddressLine2 { get; set; }

            /// <summary>
            /// Gets or sets the postal code for this trial site
            /// </summary>
            [JsonProperty("org_postal_code")]
            public string PostalCode { get; set; }

            /// <summary>
            /// Gets or sets the coordinates for this trial site
            /// </summary>
            [JsonProperty("org_coordinates")]
            public GeoLocation Coordinates { get; set; }

            /// <summary>
            /// Gets or sets the city for this trial site
            /// </summary>
            [JsonProperty("org_city")]
            public string City { get; set; }

            /// <summary>
            /// Gets or sets the state/province of this trial site
            /// </summary>
            [JsonProperty("org_state_or_province")]
            public string StateOrProvinceAbbreviation { get; set; }

            /// <summary>
            /// Gets or sets the country of this trial site
            /// </summary>
            [JsonProperty("org_country")]
            public string Country { get; set; }

            /// <summary>
            /// Gets or sets a flag indicating if this organization is a Dept. of Veterans Affairs Center/Hospital/Clinic
            /// </summary>
            [JsonProperty("org_va")]
            public bool IsVA { get; set; }

            /// <summary>
            /// Gets or sets the name of this organization
            /// </summary>
            [JsonProperty("org_name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the name of the parent organization for this site.  (e.g. Albert Einstein Cancer Center)
            /// </summary>
            [JsonProperty("org_family")]
            public string Family { get; set; }

            /// <summary>
            /// Gets or sets the relationship of this organization to its parent organization. (Sub-Organization, Affiliated Organization
            /// </summary>
            [JsonProperty("org_to_family_relationship")]
            public string OrgToFamilyRelationship { get; set; }

            /// <summary>
            /// Gets or sets the email address of this site
            /// </summary>
            [JsonProperty("org_email")]
            public string OrgEmail { get; set; }

            /// <summary>
            /// Gets or sets the fax number of this site
            /// </summary>
            [JsonProperty("org_fax")]
            public string OrgFax { get; set; }

            /// <summary>
            /// Gets or sets the phone number of this site
            /// </summary>
            [JsonProperty("org_phone")]
            public string OrgPhone { get; set; }

            /// <summary>
            /// Gets or sets the TTY number of this site
            /// </summary>
            [JsonProperty("org_tty")]
            public string OrgTTY { get; set; }

            /// <summary>
            /// Gets or sets the email address used to contact this site.
            /// </summary>
            [JsonProperty("contact_email")]
            public string ContactEmail { get; set; }

            /// <summary>
            /// Gets or sets the name, if any, of the contact person at this site.
            /// </summary>
            [JsonProperty("contact_name")]
            public string ContactName { get; set; }

            /// <summary>
            /// Gets or sets the phone number used to contact this site.
            /// </summary>
            [JsonProperty("contact_phone")]
            public string ContactPhone { get; set; }

            // Unknown for now what this property provides.
            //[JsonProperty("generic_contact")]
            //public object GenericContact { get; set; }

            /// <summary>
            /// Gets or sets the recruitment status for this site.
            /// </summary>
            [JsonProperty("recruitment_status")]
            public string RecruitmentStatus { get; set; }

            // What exactlyh is the recruitment_status_date indicate?
            //[JsonProperty("recruitment_status_date")]
            //public string RecruitmentStatusDate { get; set; }

            /// <summary>
            /// Gets or sets the ID of the trial at this site.  (as each site can have its own ID for the trial)
            /// </summary>
            [JsonProperty("local_site_identifier")]
            public string LocalSiteIdentifier { get; set; }

            /// <summary>
            /// Gets the full spelling of the state or province
            /// </summary>
            [JsonIgnore()]
            public string StateOrProvince
            {
                get
                {
                    return StateMapping(this.StateOrProvinceAbbreviation);
                }
            }

            /// <summary>
            /// Checks abbreviation and returns full state/provice/territory name if there is a match.
            /// TODO: Clean up or move into a flat file or config
            /// </summary>
            /// <param name="state">string</param>
            /// <returns>String state</returns>
            public string StateMapping(string state)
            {
                switch (state)
                {
                    case "AB": return "Alberta";
                    case "AK": return "Alaska";
                    case "AL": return "Alabama";
                    case "AR": return "Arkansas";
                    case "AS": return "American Samoa";
                    case "AZ": return "Arizona";
                    case "BC": return "British Columbia";
                    case "CA": return "California";
                    case "CO": return "Colorado";
                    case "CT": return "Connecticut";
                    case "DC": return "District of Columbia";
                    case "DE": return "Delaware";
                    case "FL": return "Florida";
                    case "GA": return "Georgia";
                    case "GU": return "Guam";
                    case "HI": return "Hawaii";
                    case "IA": return "Iowa";
                    case "ID": return "Idaho";
                    case "IL": return "Illinois";
                    case "IN": return "Indiana";
                    case "KS": return "Kansas";
                    case "KY": return "Kentucky";
                    case "LA": return "Louisiana";
                    case "MA": return "Massachusetts";
                    case "MB": return "Manitoba";
                    case "MD": return "Maryland";
                    case "ME": return "Maine";
                    case "MI": return "Michigan";
                    case "MN": return "Minnesota";
                    case "MO": return "Missouri";
                    case "MP": return "Northern Mariana Islands";
                    case "MS": return "Mississippi";
                    case "MT": return "Montana";
                    case "NB": return "New Brunswick";
                    case "NC": return "North Carolina";
                    case "ND": return "North Dakota";
                    case "NE": return "Nebraska";
                    case "NH": return "New Hampshire";
                    case "NJ": return "New Jersey";
                    case "NL": return "Newfoundland and Labrador";
                    case "NM": return "New Mexico";
                    case "NS": return "Nova Scotia";
                    case "NV": return "Nevada";
                    case "NY": return "New York";
                    case "OH": return "Ohio";
                    case "OK": return "Oklahoma";
                    case "ON": return "Ontario";
                    case "OR": return "Oregon";
                    case "PA": return "Pennsylvania";
                    case "PE": return "Prince Edward Island";
                    case "PR": return "Puerto Rico";
                    case "QC": return "Quebec";
                    case "RI": return "Rhode Island";
                    case "SC": return "South Carolina";
                    case "SD": return "South Dakota";
                    case "SK": return "Saskatchewan";
                    case "TN": return "Tennessee";
                    case "TX": return "Texas";
                    case "UM": return "U.S. Minor Outlying Islands";
                    case "UT": return "Utah";
                    case "VA": return "Virginia";
                    case "VI": return "U.S. Virgin Islands";
                    case "VT": return "Vermont";
                    case "WA": return "Washington";
                    case "WI": return "Wisconsin";
                    case "WV": return "West Virginia";
                    case "WY": return "Wyoming";
                }
                return state;
            }
        }

        /// <summary>
        /// Gets or sets a list of trials sites and thier locations for this trial
        /// </summary>
        [JsonProperty("sites")]
        public List<StudySite> Sites { get; set; }

        #endregion

        /// <summary>
        /// Constructs a new instance of a ClinicalTrial
        /// </summary>
        public ClinicalTrial()
        {
            //Ensure we have one of these in case the trial does not
            Collaborators = new List<Collaborator>();
            OtherTrialIDs = new List<OtherId>();
            Sites = new List<StudySite>();

        }

        #region Composite Properties to Bubble Up information from embedded objects

        /// <summary>
        /// Gets the primary purpose code for this trial
        /// </summary>
        [JsonIgnore()]
        public string TrialType
        {
            get
            {
                return this.PrimaryPurpose.Code;
            }
        }

        #endregion



        /*
        [JsonProperty("")]
        public object associated_studies { get; set; }

        [JsonProperty("")]
        public string amendment_date { get; set; }

        [JsonProperty("")]
        public string date_last_created { get; set; }

        [JsonProperty("")]
        public object date_last_updated { get; set; }

        [JsonProperty("")]
        public string current_trial_status_date { get; set; }

        [JsonProperty("")]
        public string start_date { get; set; }

        [JsonProperty("")]
        public string start_date_type_code { get; set; }

        [JsonProperty("")]
        public object completion_date { get; set; }

        [JsonProperty("")]
        public object completion_date_type_code { get; set; }

        [JsonProperty("")]
        public object acronym { get; set; }

        [JsonProperty("")]
        public object keywords { get; set; }

        [JsonProperty("")]
        public object classification_code { get; set; }

        [JsonProperty("")]
        public object interventional_model { get; set; }

        [JsonProperty("")]
        public string accepts_healthy_volunteers_indicator { get; set; }

        [JsonProperty("")]
        public string study_protocol_type { get; set; }

        [JsonProperty("")]
        public string study_subtype_code { get; set; }

        [JsonProperty("")]
        public string study_population_description { get; set; }

        [JsonProperty("")]
        public string study_model_code { get; set; }

        [JsonProperty("")]
        public string study_model_other_text { get; set; }

        [JsonProperty("")]
        public string sampling_method_code { get; set; }

        [JsonProperty("")]
        public Phase phase { get; set; }

        [JsonProperty("")]
        public Masking masking { get; set; }

        [JsonProperty("")]
        public List<string> anatomic_sites { get; set; }

        [JsonProperty("")]
        public List<Disease> diseases { get; set; }

        [JsonProperty("")]
        public int minimum_target_accrual_number { get; set; }

        [JsonProperty("")]
        public object number_of_arms { get; set; }

        [JsonProperty("")]
        public List<Arm> arms { get; set; }

        [JsonProperty("")]
        public string date_last_updated_anything { get; set; }
         */
    }
}
