// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace API.Settings
{
    /// <summary>
    /// App Configuration
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// Gets or sets the frontend end points.
        /// </summary>
        /// <value>
        /// The frontend end points.
        /// </value>
        public string FrontendEndPoints { get; set; }


        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>
        /// The audience.
        /// </value>
        public AudienceSettings Audience { get; set; }
    }
}
