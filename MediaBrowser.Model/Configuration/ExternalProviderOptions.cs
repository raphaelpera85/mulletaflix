namespace MediaBrowser.Model.Configuration
{
    /// <summary>
    /// Configuration options for external metadata providers.
    /// </summary>
    public class ExternalProviderOptions
    {
        /// <summary>
        /// Gets or sets the TMDB API key.
        /// </summary>
        public string TmdbApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the AudioDB API key.
        /// </summary>
        public string AudioDbApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the OMDB API key.
        /// </summary>
        public string OmdbApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Fanart API key.
        /// </summary>
        public string FanartApiKey { get; set; } = string.Empty;
    }
}
