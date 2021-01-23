using HtmlRendererCore.Core;

namespace HtmlRendererCore.Adapters.Entities
{
    /// <summary>
    /// Even class for handling keyboard events in <see cref="HtmlContainerInt"/>.
    /// </summary>
    public sealed class RMouseEvent
    {
        /// <summary>
        /// Is the left mouse button participated in the event
        /// </summary>
        private readonly bool _leftButton;

        /// <summary>
        /// Init.
        /// </summary>
        public RMouseEvent(bool leftButton)
        {
            _leftButton = leftButton;
        }

        /// <summary>
        /// Is the left mouse button participated in the event
        /// </summary>
        public bool LeftButton
        {
            get { return _leftButton; }
        }
    }
}