// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace API.Settings
{
    /// <summary>
    /// Audience Settings
    /// </summary>
    public class AudienceSettings
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the iss.
        /// </summary>
        /// <value>
        /// The iss.
        /// </value>
        public string Iss { get; set; }

        /// <summary>
        /// Gets or sets the aud.
        /// </summary>
        /// <value>
        /// The aud.
        /// </value>
        public string Aud { get; set; }

        /// <summary>
        /// Gets or sets the token expiry time in minutes.
        /// </summary>
        /// <value>
        /// The token expiry time in minutes.
        /// </value>
        public int TokenExpiryMinutes { get; set; }
    }
}