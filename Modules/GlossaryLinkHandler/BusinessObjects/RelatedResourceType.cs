namespace GlossaryLinkHandler
{
    /// <summary>
    /// RelatedResourceType enum
    /// </summary>
    public enum RelatedResourceType
    {
        /// <summary>
        /// The resource is a PDQ Drug Information Summary.
        /// </summary>
        DrugSummary,

        /// <summary>
        /// The resource is a PDQ Cancer Information Summary.
        /// </summary>
        Summary,

        /// <summary>
        /// The resource is an external link.
        /// </summary>
        External,

        /// <summary>
        /// The resource is a PDQ GlossaryTerm.
        /// </summary>
        GlossaryTerm
    }
}