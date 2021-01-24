using HtmlRendererCore.Core.Entities;

namespace HtmlRendererCore.Core.Dom
{
    /// <summary>
    /// CSS boxes that have ":hover" selector on them.
    /// </summary>
    internal sealed class HoverBoxBlock
    {
        /// <summary>
        /// the box that has :hover css on
        /// </summary>
        private readonly CssBox _cssBox;

        /// <summary>
        /// the :hover style block data
        /// </summary>
        private readonly CssBlock _cssBlock;

        /// <summary>
        /// Init.
        /// </summary>
        public HoverBoxBlock(CssBox cssBox, CssBlock cssBlock)
        {
            _cssBox = cssBox;
            _cssBlock = cssBlock;
        }

        /// <summary>
        /// the box that has :hover css on
        /// </summary>
        public CssBox CssBox
        {
            get { return _cssBox; }
        }

        /// <summary>
        /// the :hover style block data
        /// </summary>
        public CssBlock CssBlock
        {
            get { return _cssBlock; }
        }
    }
}