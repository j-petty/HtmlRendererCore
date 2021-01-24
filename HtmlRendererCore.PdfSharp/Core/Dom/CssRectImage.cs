using HtmlRendererCore.Adapters;
using HtmlRendererCore.Adapters.Entities;

namespace HtmlRendererCore.Core.Dom
{
    /// <summary>
    /// Represents a word inside an inline box
    /// </summary>
    internal sealed class CssRectImage : CssRect
    {
        #region Fields and Consts

        /// <summary>
        /// the image object if it is image word (can be null if not loaded)
        /// </summary>
        private RImage _image;

        /// <summary>
        /// the image rectangle restriction as returned from image load event
        /// </summary>
        private RRect _imageRectangle;

        #endregion


        /// <summary>
        /// Creates a new BoxWord which represents an image
        /// </summary>
        /// <param name="owner">the CSS box owner of the word</param>
        public CssRectImage(CssBox owner)
            : base(owner)
        { }

        /// <summary>
        /// Gets the image this words represents (if one exists)
        /// </summary>
        public override RImage Image
        {
            get { return _image; }
            set { _image = value; }
        }

        /// <summary>
        /// Gets if the word represents an image.
        /// </summary>
        public override bool IsImage
        {
            get { return true; }
        }

        /// <summary>
        /// the image rectange restriction as returned from image load event
        /// </summary>
        public RRect ImageRectangle
        {
            get { return _imageRectangle; }
            set { _imageRectangle = value; }
        }

        /// <summary>
        /// Represents this word for debugging purposes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Image";
        }
    }
}