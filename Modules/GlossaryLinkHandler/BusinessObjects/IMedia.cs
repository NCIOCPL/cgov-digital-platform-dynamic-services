namespace GlossaryLinkHandler
{
    /// <summary>
    /// Notes a media item, type determined by the Type property.
    /// </summary>
    public interface IMedia
    {
        /// <summary>
        /// type of media to be used
        /// </summary>
        MediaType Type { get; }
    }
}