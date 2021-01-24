using System;

namespace HtmlRendererCore.Adapters.Entities
{
    /// <summary>
    /// Specifies style information applied to text.
    /// </summary>
    [Flags]
    public enum RFontStyle
    {
        Regular = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Strikeout = 8,
    }
}