using HtmlRendererCore.Adapters;
using PdfSharpCore.Drawing;

namespace HtmlRendererCore.PdfSharp.Adapters
{
    /// <summary>
    /// Adapter for WinForms Image object for core.
    /// </summary>
    internal sealed class ImageAdapter : RImage
    {
        /// <summary>
        /// the underline win-forms image.
        /// </summary>
        private readonly XImage _image;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ImageAdapter(XImage image)
        {
            _image = image;
        }

        /// <summary>
        /// the underline win-forms image.
        /// </summary>
        public XImage Image
        {
            get { return _image; }
        }

        public override double Width
        {
            get { return _image.PixelWidth; }
        }

        public override double Height
        {
            get { return _image.PixelHeight; }
        }

        public override void Dispose()
        {
            _image.Dispose();
        }
    }
}