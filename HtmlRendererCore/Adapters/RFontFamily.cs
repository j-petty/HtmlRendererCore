namespace HtmlRendererCore.Adapters
{
    /// <summary>
    /// Adapter for platform specific font family object - define the available font families to use.<br/>
    /// Required for custom fonts handling: fonts that are not installed on the system.
    /// </summary>
    public abstract class RFontFamily
    {
        /// <summary>
        /// Gets the name of this Font Family.
        /// </summary>
        public abstract string Name { get; }
    }
}