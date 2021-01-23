namespace HtmlRendererCore.Core.Entities
{
    /// <summary>
    /// Controls the way styles are generated when html is generated.
    /// </summary>
    public enum HtmlGenerationStyle
    {
        /// <summary>
        /// styles are not generated at all
        /// </summary>
        None = 0,

        /// <summary>
        /// style are inserted in style attribute for each html tag
        /// </summary>
        Inline = 1,

        /// <summary>
        /// style section is generated in the head of the html
        /// </summary>
        InHeader = 2
    }
}