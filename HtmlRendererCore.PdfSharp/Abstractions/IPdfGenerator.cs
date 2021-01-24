using HtmlRendererCore.Core;
using HtmlRendererCore.Core.Entities;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System;

namespace HtmlRendererCore.PdfSharp.Abstractions
{
    public interface IPdfGenerator
    {
        /// <summary>
        /// Adds a font mapping from <paramref name="fromFamily"/> to <paramref name="toFamily"/> iff the <paramref name="fromFamily"/> is not found.<br/>
        /// When the <paramref name="fromFamily"/> font is used in rendered html and is not found in existing 
        /// fonts (installed or added) it will be replaced by <paramref name="toFamily"/>.<br/>
        /// </summary>
        /// <remarks>
        /// This fonts mapping can be used as a fallback in case the requested font is not installed in the client system.
        /// </remarks>
        /// <param name="fromFamily">the font family to replace</param>
        /// <param name="toFamily">the font family to replace with</param>
        public void AddFontFamilyMapping(string fromFamily, string toFamily);

        /// <summary>
        /// Parse the given stylesheet to <see cref="CssData"/> object.<br/>
        /// If <paramref name="combineWithDefault"/> is true the parsed css blocks are added to the 
        /// default css data (as defined by W3), merged if class name already exists. If false only the data in the given stylesheet is returned.
        /// </summary>
        /// <seealso cref="http://www.w3.org/TR/CSS21/sample.html"/>
        /// <param name="stylesheet">the stylesheet source to parse</param>
        /// <param name="combineWithDefault">true - combine the parsed css data with default css data, false - return only the parsed css data</param>
        /// <returns>the parsed css data</returns>
        public CssData ParseStyleSheet(string stylesheet, bool combineWithDefault = true);

        /// <summary>
        /// Create PDF document from given HTML.<br/>
        /// </summary>
        /// <param name="html">HTML source to create PDF from</param>
        /// <param name="pageSize">the page size to use for each page in the generated pdf </param>
        /// <param name="margin">the margin to use between the HTML and the edges of each page</param>
        /// <param name="cssData">optional: the style to use for html rendering (default - use W3 default style)</param>
        /// <param name="stylesheetLoad">optional: can be used to overwrite stylesheet resolution logic</param>
        /// <param name="imageLoad">optional: can be used to overwrite image resolution logic</param>
        /// <returns>the generated image of the html</returns>
        public PdfDocument GeneratePdf(string html, PageSize pageSize, int margin = 20, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null);

        /// <summary>
        /// Create PDF document from given HTML.<br/>
        /// </summary>
        /// <param name="html">HTML source to create PDF from</param>
        /// <param name="config">the configuration to use for the PDF generation (page size/page orientation/margins/etc.)</param>
        /// <param name="cssData">optional: the style to use for html rendering (default - use W3 default style)</param>
        /// <param name="stylesheetLoad">optional: can be used to overwrite stylesheet resolution logic</param>
        /// <param name="imageLoad">optional: can be used to overwrite image resolution logic</param>
        /// <returns>the generated image of the html</returns>
        public PdfDocument GeneratePdf(string html, PdfGenerateConfig config, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null);

        /// <summary>
        /// Create PDF pages from given HTML and appends them to the provided PDF document.<br/>
        /// </summary>
        /// <param name="document">PDF document to append pages to</param>
        /// <param name="html">HTML source to create PDF from</param>
        /// <param name="pageSize">the page size to use for each page in the generated pdf </param>
        /// <param name="margin">the margin to use between the HTML and the edges of each page</param>
        /// <param name="cssData">optional: the style to use for html rendering (default - use W3 default style)</param>
        /// <param name="stylesheetLoad">optional: can be used to overwrite stylesheet resolution logic</param>
        /// <param name="imageLoad">optional: can be used to overwrite image resolution logic</param>
        /// <returns>the generated image of the html</returns>
        public void AddPdfPages(PdfDocument document, string html, PageSize pageSize, int margin = 20, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null);

        /// <summary>
        /// Create PDF pages from given HTML and appends them to the provided PDF document.<br/>
        /// </summary>
        /// <param name="document">PDF document to append pages to</param>
        /// <param name="html">HTML source to create PDF from</param>
        /// <param name="config">the configuration to use for the PDF generation (page size/page orientation/margins/etc.)</param>
        /// <param name="cssData">optional: the style to use for html rendering (default - use W3 default style)</param>
        /// <param name="stylesheetLoad">optional: can be used to overwrite stylesheet resolution logic</param>
        /// <param name="imageLoad">optional: can be used to overwrite image resolution logic</param>
        /// <returns>the generated image of the html</returns>
        public void AddPdfPages(PdfDocument document, string html, PdfGenerateConfig config, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null);
    }
}
