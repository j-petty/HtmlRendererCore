﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using HtmlRendererCore.Adapters;
using HtmlRendererCore.Adapters.Entities;
using HtmlRendererCore.Core.Parse;
using HtmlRendererCore.Core.Utils;

namespace HtmlRendererCore.Core.Dom
{
    /// <summary>
    /// Base class for css box to handle the css properties.<br/>
    /// Has field and property for every css property that can be set, the properties add additional parsing like
    /// setting the correct border depending what border value was set (single, two , all four).<br/>
    /// Has additional fields to control the location and size of the box and 'actual' css values for some properties
    /// that require additional calculations and parsing.<br/>
    /// </summary>
    internal abstract class CssBoxProperties
    {
        #region CSS Fields

        private string _borderTopWidth = "medium";
        private string _borderRightWidth = "medium";
        private string _borderBottomWidth = "medium";
        private string _borderLeftWidth = "medium";
        private string _borderTopColor = "black";
        private string _borderRightColor = "black";
        private string _borderBottomColor = "black";
        private string _borderLeftColor = "black";
        private string _bottom;
        private string _color = "black";
        private string _cornerRadius = "0";
        private string _fontSize = "medium";
        private string _left = "auto";
        private string _lineHeight = "normal";
        private string _paddingLeft = "0";
        private string _paddingBottom = "0";
        private string _paddingRight = "0";
        private string _paddingTop = "0";
        private string _right;
        private string _textIndent = "0";
        private string _top = "auto";
        private string _wordSpacing = "normal";

        #endregion


        #region Fields

        /// <summary>
        /// Gets or sets the location of the box
        /// </summary>
        private RPoint _location;

        /// <summary>
        /// Gets or sets the size of the box
        /// </summary>
        private RSize _size;

        private double _actualCornerNw = double.NaN;
        private double _actualCornerNe = double.NaN;
        private double _actualCornerSw = double.NaN;
        private double _actualCornerSe = double.NaN;
        private RColor _actualColor = RColor.Empty;
        private double _actualBackgroundGradientAngle = double.NaN;
        private double _actualHeight = double.NaN;
        private double _actualWidth = double.NaN;
        private double _actualPaddingTop = double.NaN;
        private double _actualPaddingBottom = double.NaN;
        private double _actualPaddingRight = double.NaN;
        private double _actualPaddingLeft = double.NaN;
        private double _actualMarginTop = double.NaN;
        private double _collapsedMarginTop = double.NaN;
        private double _actualMarginBottom = double.NaN;
        private double _actualMarginRight = double.NaN;
        private double _actualMarginLeft = double.NaN;
        private double _actualBorderTopWidth = double.NaN;
        private double _actualBorderLeftWidth = double.NaN;
        private double _actualBorderBottomWidth = double.NaN;
        private double _actualBorderRightWidth = double.NaN;

        /// <summary>
        /// the width of whitespace between words
        /// </summary>
        private double _actualLineHeight = double.NaN;

        private double _actualWordSpacing = double.NaN;
        private double _actualTextIndent = double.NaN;
        private double _actualBorderSpacingHorizontal = double.NaN;
        private double _actualBorderSpacingVertical = double.NaN;
        private RColor _actualBackgroundGradient = RColor.Empty;
        private RColor _actualBorderTopColor = RColor.Empty;
        private RColor _actualBorderLeftColor = RColor.Empty;
        private RColor _actualBorderBottomColor = RColor.Empty;
        private RColor _actualBorderRightColor = RColor.Empty;
        private RColor _actualBackgroundColor = RColor.Empty;
        private RFont _actualFont;

        #endregion


        #region CSS Properties

        public string BorderBottomWidth
        {
            get { return _borderBottomWidth; }
            set
            {
                _borderBottomWidth = value;
                _actualBorderBottomWidth = float.NaN;
            }
        }

        public string BorderLeftWidth
        {
            get { return _borderLeftWidth; }
            set
            {
                _borderLeftWidth = value;
                _actualBorderLeftWidth = float.NaN;
            }
        }

        public string BorderRightWidth
        {
            get { return _borderRightWidth; }
            set
            {
                _borderRightWidth = value;
                _actualBorderRightWidth = float.NaN;
            }
        }

        public string BorderTopWidth
        {
            get { return _borderTopWidth; }
            set
            {
                _borderTopWidth = value;
                _actualBorderTopWidth = float.NaN;
            }
        }

        public string BorderBottomStyle { get; set; } = "none";

        public string BorderLeftStyle { get; set; } = "none";

        public string BorderRightStyle { get; set; } = "none";

        public string BorderTopStyle { get; set; } = "none";

        public string BorderBottomColor
        {
            get { return _borderBottomColor; }
            set
            {
                _borderBottomColor = value;
                _actualBorderBottomColor = RColor.Empty;
            }
        }

        public string BorderLeftColor
        {
            get { return _borderLeftColor; }
            set
            {
                _borderLeftColor = value;
                _actualBorderLeftColor = RColor.Empty;
            }
        }

        public string BorderRightColor
        {
            get { return _borderRightColor; }
            set
            {
                _borderRightColor = value;
                _actualBorderRightColor = RColor.Empty;
            }
        }

        public string BorderTopColor
        {
            get { return _borderTopColor; }
            set
            {
                _borderTopColor = value;
                _actualBorderTopColor = RColor.Empty;
            }
        }

        public string BorderSpacing { get; set; } = "0";

        public string BorderCollapse { get; set; } = "separate";

        public string CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                MatchCollection r = RegexParserUtils.Match(RegexParserUtils.CssLength, value);

                switch (r.Count)
                {
                    case 1:
                        CornerNeRadius = r[0].Value;
                        CornerNwRadius = r[0].Value;
                        CornerSeRadius = r[0].Value;
                        CornerSwRadius = r[0].Value;
                        break;
                    case 2:
                        CornerNeRadius = r[0].Value;
                        CornerNwRadius = r[0].Value;
                        CornerSeRadius = r[1].Value;
                        CornerSwRadius = r[1].Value;
                        break;
                    case 3:
                        CornerNeRadius = r[0].Value;
                        CornerNwRadius = r[1].Value;
                        CornerSeRadius = r[2].Value;
                        break;
                    case 4:
                        CornerNeRadius = r[0].Value;
                        CornerNwRadius = r[1].Value;
                        CornerSeRadius = r[2].Value;
                        CornerSwRadius = r[3].Value;
                        break;
                }

                _cornerRadius = value;
            }
        }

        public string CornerNwRadius { get; set; } = "0";

        public string CornerNeRadius { get; set; } = "0";

        public string CornerSeRadius { get; set; } = "0";

        public string CornerSwRadius { get; set; } = "0";

        public string MarginBottom { get; set; } = "0";

        public string MarginLeft { get; set; } = "0";

        public string MarginRight { get; set; } = "0";

        public string MarginTop { get; set; } = "0";

        public string PaddingBottom
        {
            get { return _paddingBottom; }
            set
            {
                _paddingBottom = value;
                _actualPaddingBottom = double.NaN;
            }
        }

        public string PaddingLeft
        {
            get { return _paddingLeft; }
            set
            {
                _paddingLeft = value;
                _actualPaddingLeft = double.NaN;
            }
        }

        public string PaddingRight
        {
            get { return _paddingRight; }
            set
            {
                _paddingRight = value;
                _actualPaddingRight = double.NaN;
            }
        }

        public string PaddingTop
        {
            get { return _paddingTop; }
            set
            {
                _paddingTop = value;
                _actualPaddingTop = double.NaN;
            }
        }

        public string PageBreakInside { get; set; } = CssConstants.Auto;

        public string Left
        {
            get { return _left; }
            set
            {
                _left = value;

                if (Position == CssConstants.Fixed)
                {
                    _location = GetActualLocation(Left, Top);
                }
            }
        }

        public string Top
        {
            get { return _top; }
            set {
                _top = value;

                if (Position == CssConstants.Fixed)
                {
                    _location = GetActualLocation(Left, Top);
                }

            }
        }

        public string Width { get; set; } = "auto";

        public string MaxWidth { get; set; } = "none";

        public string Height { get; set; } = "auto";

        public string BackgroundColor { get; set; } = "transparent";

        public string BackgroundImage { get; set; } = "none";

        public string BackgroundPosition { get; set; } = "0% 0%";

        public string BackgroundRepeat { get; set; } = "repeat";

        public string BackgroundGradient { get; set; } = "none";

        public string BackgroundGradientAngle { get; set; } = "90";

        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _actualColor = RColor.Empty;
            }
        }

        public string Content { get; set; } = "normal";

        public string Display { get; set; } = "inline";

        public string Direction { get; set; } = "ltr";

        public string EmptyCells { get; set; } = "show";

        public string Float { get; set; } = "none";

        public string Position { get; set; } = "static";

        public string LineHeight
        {
            get { return _lineHeight; }
            set { _lineHeight = string.Format(NumberFormatInfo.InvariantInfo, "{0}px", CssValueParser.ParseLength(value, Size.Height, this, CssConstants.Em)); }
        }

        public string VerticalAlign { get; set; } = "baseline";

        public string TextIndent
        {
            get { return _textIndent; }
            set { _textIndent = NoEms(value); }
        }

        public string TextAlign { get; set; } = string.Empty;

        public string TextDecoration { get; set; } = string.Empty;

        public string WhiteSpace { get; set; } = "normal";

        public string Visibility { get; set; } = "visible";

        public string WordSpacing
        {
            get { return _wordSpacing; }
            set { _wordSpacing = NoEms(value); }
        }

        public string WordBreak { get; set; } = "normal";

        public string FontFamily { get; set; }

        public string FontSize
        {
            get { return _fontSize; }
            set
            {
                string length = RegexParserUtils.Search(RegexParserUtils.CssLength, value);

                if (length != null)
                {
                    string computedValue;
                    CssLength len = new CssLength(length);

                    if (len.HasError)
                    {
                        computedValue = "medium";
                    }
                    else if (len.Unit == CssUnit.Ems && GetParent() != null)
                    {
                        computedValue = len.ConvertEmToPoints(GetParent().ActualFont.Size).ToString();
                    }
                    else
                    {
                        computedValue = len.ToString();
                    }

                    _fontSize = computedValue;
                }
                else
                {
                    _fontSize = value;
                }
            }
        }

        public string FontStyle { get; set; } = "normal";

        public string FontVariant { get; set; } = "normal";

        public string FontWeight { get; set; } = "normal";

        public string ListStyle { get; set; } = string.Empty;

        public string Overflow { get; set; } = "visible";

        public string ListStylePosition { get; set; } = "outside";

        public string ListStyleImage { get; set; } = string.Empty;

        public string ListStyleType { get; set; } = "disc";

        #endregion CSS Propertier

        /// <summary>
        /// Gets or sets the location of the box
        /// </summary>
        public RPoint Location
        {
            get {
                if (_location.IsEmpty && Position == CssConstants.Fixed)
                {
                    _location = GetActualLocation(Left, Top);
                }
                return _location;
            }
            set {
                _location = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the box
        /// </summary>
        public RSize Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Gets the bounds of the box
        /// </summary>
        public RRect Bounds
        {
            get { return new RRect(Location, Size); }
        }

        /// <summary>
        /// Gets the width available on the box, counting padding and margin.
        /// </summary>
        public double AvailableWidth
        {
            get { return Size.Width - ActualBorderLeftWidth - ActualPaddingLeft - ActualPaddingRight - ActualBorderRightWidth; }
        }

        /// <summary>
        /// Gets the right of the box. When setting, it will affect only the width of the box.
        /// </summary>
        public double ActualRight
        {
            get { return Location.X + Size.Width; }
            set { Size = new RSize(value - Location.X, Size.Height); }
        }

        /// <summary>
        /// Gets or sets the bottom of the box. 
        /// (When setting, alters only the Size.Height of the box)
        /// </summary>
        public double ActualBottom
        {
            get { return Location.Y + Size.Height; }
            set { Size = new RSize(Size.Width, value - Location.Y); }
        }

        /// <summary>
        /// Gets the left of the client rectangle (Where content starts rendering)
        /// </summary>
        public double ClientLeft
        {
            get { return Location.X + ActualBorderLeftWidth + ActualPaddingLeft; }
        }

        /// <summary>
        /// Gets the top of the client rectangle (Where content starts rendering)
        /// </summary>
        public double ClientTop
        {
            get { return Location.Y + ActualBorderTopWidth + ActualPaddingTop; }
        }

        /// <summary>
        /// Gets the right of the client rectangle
        /// </summary>
        public double ClientRight
        {
            get { return ActualRight - ActualPaddingRight - ActualBorderRightWidth; }
        }

        /// <summary>
        /// Gets the bottom of the client rectangle
        /// </summary>
        public double ClientBottom
        {
            get { return ActualBottom - ActualPaddingBottom - ActualBorderBottomWidth; }
        }

        /// <summary>
        /// Gets the client rectangle
        /// </summary>
        public RRect ClientRectangle
        {
            get { return RRect.FromLTRB(ClientLeft, ClientTop, ClientRight, ClientBottom); }
        }

        /// <summary>
        /// Gets the actual height
        /// </summary>
        public double ActualHeight
        {
            get
            {
                if (double.IsNaN(_actualHeight))
                {
                    _actualHeight = CssValueParser.ParseLength(Height, Size.Height, this);
                }
                return _actualHeight;
            }
        }

        /// <summary>
        /// Gets the actual height
        /// </summary>
        public double ActualWidth
        {
            get
            {
                if (double.IsNaN(_actualWidth))
                {
                    _actualWidth = CssValueParser.ParseLength(Width, Size.Width, this);
                }
                return _actualWidth;
            }
        }

        /// <summary>
        /// Gets the actual top's padding
        /// </summary>
        public double ActualPaddingTop
        {
            get
            {
                if (double.IsNaN(_actualPaddingTop))
                {
                    _actualPaddingTop = CssValueParser.ParseLength(PaddingTop, Size.Width, this);
                }
                return _actualPaddingTop;
            }
        }

        /// <summary>
        /// Gets the actual padding on the left
        /// </summary>
        public double ActualPaddingLeft
        {
            get
            {
                if (double.IsNaN(_actualPaddingLeft))
                {
                    _actualPaddingLeft = CssValueParser.ParseLength(PaddingLeft, Size.Width, this);
                }
                return _actualPaddingLeft;
            }
        }

        /// <summary>
        /// Gets the actual Padding of the bottom
        /// </summary>
        public double ActualPaddingBottom
        {
            get
            {
                if (double.IsNaN(_actualPaddingBottom))
                {
                    _actualPaddingBottom = CssValueParser.ParseLength(PaddingBottom, Size.Width, this);
                }
                return _actualPaddingBottom;
            }
        }

        /// <summary>
        /// Gets the actual padding on the right
        /// </summary>
        public double ActualPaddingRight
        {
            get
            {
                if (double.IsNaN(_actualPaddingRight))
                {
                    _actualPaddingRight = CssValueParser.ParseLength(PaddingRight, Size.Width, this);
                }
                return _actualPaddingRight;
            }
        }

        /// <summary>
        /// Gets the actual top's Margin
        /// </summary>
        public double ActualMarginTop
        {
            get
            {
                if (double.IsNaN(_actualMarginTop))
                {
                    if (MarginTop == CssConstants.Auto)
                        MarginTop = "0";
                    var actualMarginTop = CssValueParser.ParseLength(MarginTop, Size.Width, this);
                    if (MarginLeft.EndsWith("%"))
                        return actualMarginTop;
                    _actualMarginTop = actualMarginTop;
                }
                return _actualMarginTop;
            }
        }

        /// <summary>
        /// The margin top value if was effected by margin collapse.
        /// </summary>
        public double CollapsedMarginTop
        {
            get { return double.IsNaN(_collapsedMarginTop) ? 0 : _collapsedMarginTop; }
            set { _collapsedMarginTop = value; }
        }

        /// <summary>
        /// Gets the actual Margin on the left
        /// </summary>
        public double ActualMarginLeft
        {
            get
            {
                if (double.IsNaN(_actualMarginLeft))
                {
                    if (MarginLeft == CssConstants.Auto)
                        MarginLeft = "0";
                    var actualMarginLeft = CssValueParser.ParseLength(MarginLeft, Size.Width, this);
                    if (MarginLeft.EndsWith("%"))
                        return actualMarginLeft;
                    _actualMarginLeft = actualMarginLeft;
                }
                return _actualMarginLeft;
            }
        }

        /// <summary>
        /// Gets the actual Margin of the bottom
        /// </summary>
        public double ActualMarginBottom
        {
            get
            {
                if (double.IsNaN(_actualMarginBottom))
                {
                    if (MarginBottom == CssConstants.Auto)
                        MarginBottom = "0";
                    var actualMarginBottom = CssValueParser.ParseLength(MarginBottom, Size.Width, this);
                    if (MarginLeft.EndsWith("%"))
                        return actualMarginBottom;
                    _actualMarginBottom = actualMarginBottom;
                }
                return _actualMarginBottom;
            }
        }

        /// <summary>
        /// Gets the actual Margin on the right
        /// </summary>
        public double ActualMarginRight
        {
            get
            {
                if (double.IsNaN(_actualMarginRight))
                {
                    if (MarginRight == CssConstants.Auto)
                        MarginRight = "0";
                    var actualMarginRight = CssValueParser.ParseLength(MarginRight, Size.Width, this);
                    if (MarginLeft.EndsWith("%"))
                        return actualMarginRight;
                    _actualMarginRight = actualMarginRight;
                }
                return _actualMarginRight;
            }
        }

        /// <summary>
        /// Gets the actual top border width
        /// </summary>
        public double ActualBorderTopWidth
        {
            get
            {
                if (double.IsNaN(_actualBorderTopWidth))
                {
                    _actualBorderTopWidth = CssValueParser.GetActualBorderWidth(BorderTopWidth, this);
                    if (string.IsNullOrEmpty(BorderTopStyle) || BorderTopStyle == CssConstants.None)
                    {
                        _actualBorderTopWidth = 0f;
                    }
                }
                return _actualBorderTopWidth;
            }
        }

        /// <summary>
        /// Gets the actual Left border width
        /// </summary>
        public double ActualBorderLeftWidth
        {
            get
            {
                if (double.IsNaN(_actualBorderLeftWidth))
                {
                    _actualBorderLeftWidth = CssValueParser.GetActualBorderWidth(BorderLeftWidth, this);
                    if (string.IsNullOrEmpty(BorderLeftStyle) || BorderLeftStyle == CssConstants.None)
                    {
                        _actualBorderLeftWidth = 0f;
                    }
                }
                return _actualBorderLeftWidth;
            }
        }

        /// <summary>
        /// Gets the actual Bottom border width
        /// </summary>
        public double ActualBorderBottomWidth
        {
            get
            {
                if (double.IsNaN(_actualBorderBottomWidth))
                {
                    _actualBorderBottomWidth = CssValueParser.GetActualBorderWidth(BorderBottomWidth, this);
                    if (string.IsNullOrEmpty(BorderBottomStyle) || BorderBottomStyle == CssConstants.None)
                    {
                        _actualBorderBottomWidth = 0f;
                    }
                }
                return _actualBorderBottomWidth;
            }
        }

        /// <summary>
        /// Gets the actual Right border width
        /// </summary>
        public double ActualBorderRightWidth
        {
            get
            {
                if (double.IsNaN(_actualBorderRightWidth))
                {
                    _actualBorderRightWidth = CssValueParser.GetActualBorderWidth(BorderRightWidth, this);
                    if (string.IsNullOrEmpty(BorderRightStyle) || BorderRightStyle == CssConstants.None)
                    {
                        _actualBorderRightWidth = 0f;
                    }
                }
                return _actualBorderRightWidth;
            }
        }

        /// <summary>
        /// Gets the actual top border Color
        /// </summary>
        public RColor ActualBorderTopColor
        {
            get
            {
                if (_actualBorderTopColor.IsEmpty)
                {
                    _actualBorderTopColor = GetActualColor(BorderTopColor);
                }
                return _actualBorderTopColor;
            }
        }

        protected abstract RPoint GetActualLocation(string X, string Y);

        protected abstract RColor GetActualColor(string colorStr);

        /// <summary>
        /// Gets the actual Left border Color
        /// </summary>
        public RColor ActualBorderLeftColor
        {
            get
            {
                if ((_actualBorderLeftColor.IsEmpty))
                {
                    _actualBorderLeftColor = GetActualColor(BorderLeftColor);
                }
                return _actualBorderLeftColor;
            }
        }

        /// <summary>
        /// Gets the actual Bottom border Color
        /// </summary>
        public RColor ActualBorderBottomColor
        {
            get
            {
                if ((_actualBorderBottomColor.IsEmpty))
                {
                    _actualBorderBottomColor = GetActualColor(BorderBottomColor);
                }
                return _actualBorderBottomColor;
            }
        }

        /// <summary>
        /// Gets the actual Right border Color
        /// </summary>
        public RColor ActualBorderRightColor
        {
            get
            {
                if ((_actualBorderRightColor.IsEmpty))
                {
                    _actualBorderRightColor = GetActualColor(BorderRightColor);
                }
                return _actualBorderRightColor;
            }
        }

        /// <summary>
        /// Gets the actual length of the north west corner
        /// </summary>
        public double ActualCornerNw
        {
            get
            {
                if (double.IsNaN(_actualCornerNw))
                {
                    _actualCornerNw = CssValueParser.ParseLength(CornerNwRadius, 0, this);
                }
                return _actualCornerNw;
            }
        }

        /// <summary>
        /// Gets the actual length of the north east corner
        /// </summary>
        public double ActualCornerNe
        {
            get
            {
                if (double.IsNaN(_actualCornerNe))
                {
                    _actualCornerNe = CssValueParser.ParseLength(CornerNeRadius, 0, this);
                }
                return _actualCornerNe;
            }
        }

        /// <summary>
        /// Gets the actual length of the south east corner
        /// </summary>
        public double ActualCornerSe
        {
            get
            {
                if (double.IsNaN(_actualCornerSe))
                {
                    _actualCornerSe = CssValueParser.ParseLength(CornerSeRadius, 0, this);
                }
                return _actualCornerSe;
            }
        }

        /// <summary>
        /// Gets the actual length of the south west corner
        /// </summary>
        public double ActualCornerSw
        {
            get
            {
                if (double.IsNaN(_actualCornerSw))
                {
                    _actualCornerSw = CssValueParser.ParseLength(CornerSwRadius, 0, this);
                }
                return _actualCornerSw;
            }
        }

        /// <summary>
        /// Gets a value indicating if at least one of the corners of the box is rounded
        /// </summary>
        public bool IsRounded
        {
            get { return ActualCornerNe > 0f || ActualCornerNw > 0f || ActualCornerSe > 0f || ActualCornerSw > 0f; }
        }

        /// <summary>
        /// Gets the actual width of whitespace between words.
        /// </summary>
        public double ActualWordSpacing
        {
            get { return _actualWordSpacing; }
        }

        /// <summary>
        /// 
        /// Gets the actual color for the text.
        /// </summary>
        public RColor ActualColor
        {
            get
            {
                if (_actualColor.IsEmpty)
                {
                    _actualColor = GetActualColor(Color);
                }

                return _actualColor;
            }
        }

        /// <summary>
        /// Gets the actual background color of the box
        /// </summary>
        public RColor ActualBackgroundColor
        {
            get
            {
                if (_actualBackgroundColor.IsEmpty)
                {
                    _actualBackgroundColor = GetActualColor(BackgroundColor);
                }

                return _actualBackgroundColor;
            }
        }

        /// <summary>
        /// Gets the second color that creates a gradient for the background
        /// </summary>
        public RColor ActualBackgroundGradient
        {
            get
            {
                if (_actualBackgroundGradient.IsEmpty)
                {
                    _actualBackgroundGradient = GetActualColor(BackgroundGradient);
                }
                return _actualBackgroundGradient;
            }
        }

        /// <summary>
        /// Gets the actual angle specified for the background gradient
        /// </summary>
        public double ActualBackgroundGradientAngle
        {
            get
            {
                if (double.IsNaN(_actualBackgroundGradientAngle))
                {
                    _actualBackgroundGradientAngle = CssValueParser.ParseNumber(BackgroundGradientAngle, 360f);
                }

                return _actualBackgroundGradientAngle;
            }
        }

        /// <summary>
        /// Gets the actual font of the parent
        /// </summary>
        public RFont ActualParentFont
        {
            get { return GetParent() == null ? ActualFont : GetParent().ActualFont; }
        }

        /// <summary>
        /// Gets the font that should be actually used to paint the text of the box
        /// </summary>
        public RFont ActualFont
        {
            get
            {
                if (_actualFont == null)
                {
                    if (string.IsNullOrEmpty(FontFamily))
                    {
                        FontFamily = CssConstants.DefaultFont;
                    }
                    if (string.IsNullOrEmpty(FontSize))
                    {
                        FontSize = CssConstants.FontSize.ToString(CultureInfo.InvariantCulture) + "pt";
                    }

                    RFontStyle st = RFontStyle.Regular;

                    if (FontStyle == CssConstants.Italic || FontStyle == CssConstants.Oblique)
                    {
                        st |= RFontStyle.Italic;
                    }

                    if (FontWeight != CssConstants.Normal && FontWeight != CssConstants.Lighter && !string.IsNullOrEmpty(FontWeight) && FontWeight != CssConstants.Inherit)
                    {
                        st |= RFontStyle.Bold;
                    }

                    double fsize;
                    double parentSize = CssConstants.FontSize;

                    if (GetParent() != null)
                        parentSize = GetParent().ActualFont.Size;

                    switch (FontSize)
                    {
                        case CssConstants.Medium:
                            fsize = CssConstants.FontSize;
                            break;
                        case CssConstants.XXSmall:
                            fsize = CssConstants.FontSize - 4;
                            break;
                        case CssConstants.XSmall:
                            fsize = CssConstants.FontSize - 3;
                            break;
                        case CssConstants.Small:
                            fsize = CssConstants.FontSize - 2;
                            break;
                        case CssConstants.Large:
                            fsize = CssConstants.FontSize + 2;
                            break;
                        case CssConstants.XLarge:
                            fsize = CssConstants.FontSize + 3;
                            break;
                        case CssConstants.XXLarge:
                            fsize = CssConstants.FontSize + 4;
                            break;
                        case CssConstants.Smaller:
                            fsize = parentSize - 2;
                            break;
                        case CssConstants.Larger:
                            fsize = parentSize + 2;
                            break;
                        default:
                            fsize = CssValueParser.ParseLength(FontSize, parentSize, parentSize, null, true, true);
                            break;
                    }

                    if (fsize <= 1f)
                    {
                        fsize = CssConstants.FontSize;
                    }

                    _actualFont = GetCachedFont(FontFamily, fsize, st);
                }
                return _actualFont;
            }
        }

        protected abstract RFont GetCachedFont(string fontFamily, double fsize, RFontStyle st);

        /// <summary>
        /// Gets the line height
        /// </summary>
        public double ActualLineHeight
        {
            get
            {
                if (double.IsNaN(_actualLineHeight))
                {
                    _actualLineHeight = .9f * CssValueParser.ParseLength(LineHeight, Size.Height, this);
                }
                return _actualLineHeight;
            }
        }

        /// <summary>
        /// Gets the text indentation (on first line only)
        /// </summary>
        public double ActualTextIndent
        {
            get
            {
                if (double.IsNaN(_actualTextIndent))
                {
                    _actualTextIndent = CssValueParser.ParseLength(TextIndent, Size.Width, this);
                }

                return _actualTextIndent;
            }
        }

        /// <summary>
        /// Gets the actual horizontal border spacing for tables
        /// </summary>
        public double ActualBorderSpacingHorizontal
        {
            get
            {
                if (double.IsNaN(_actualBorderSpacingHorizontal))
                {
                    MatchCollection matches = RegexParserUtils.Match(RegexParserUtils.CssLength, BorderSpacing);

                    if (matches.Count == 0)
                    {
                        _actualBorderSpacingHorizontal = 0;
                    }
                    else if (matches.Count > 0)
                    {
                        _actualBorderSpacingHorizontal = CssValueParser.ParseLength(matches[0].Value, 1, this);
                    }
                }


                return _actualBorderSpacingHorizontal;
            }
        }

        /// <summary>
        /// Gets the actual vertical border spacing for tables
        /// </summary>
        public double ActualBorderSpacingVertical
        {
            get
            {
                if (double.IsNaN(_actualBorderSpacingVertical))
                {
                    MatchCollection matches = RegexParserUtils.Match(RegexParserUtils.CssLength, BorderSpacing);

                    if (matches.Count == 0)
                    {
                        _actualBorderSpacingVertical = 0;
                    }
                    else if (matches.Count == 1)
                    {
                        _actualBorderSpacingVertical = CssValueParser.ParseLength(matches[0].Value, 1, this);
                    }
                    else
                    {
                        _actualBorderSpacingVertical = CssValueParser.ParseLength(matches[1].Value, 1, this);
                    }
                }
                return _actualBorderSpacingVertical;
            }
        }

        /// <summary>
        /// Get the parent of this css properties instance.
        /// </summary>
        /// <returns></returns>
        protected abstract CssBoxProperties GetParent();

        /// <summary>
        /// Gets the height of the font in the specified units
        /// </summary>
        /// <returns></returns>
        public double GetEmHeight()
        {
            return ActualFont.Height;
        }

        /// <summary>
        /// Ensures that the specified length is converted to pixels if necessary
        /// </summary>
        /// <param name="length"></param>
        protected string NoEms(string length)
        {
            var len = new CssLength(length);
            if (len.Unit == CssUnit.Ems)
            {
                length = len.ConvertEmToPixels(GetEmHeight()).ToString();
            }
            return length;
        }

        /// <summary>
        /// Set the style/width/color for all 4 borders on the box.<br/>
        /// if null is given for a value it will not be set.
        /// </summary>
        /// <param name="style">optional: the style to set</param>
        /// <param name="width">optional: the width to set</param>
        /// <param name="color">optional: the color to set</param>
        protected void SetAllBorders(string style = null, string width = null, string color = null)
        {
            if (style != null)
                BorderLeftStyle = BorderTopStyle = BorderRightStyle = BorderBottomStyle = style;
            if (width != null)
                BorderLeftWidth = BorderTopWidth = BorderRightWidth = BorderBottomWidth = width;
            if (color != null)
                BorderLeftColor = BorderTopColor = BorderRightColor = BorderBottomColor = color;
        }

        /// <summary>
        /// Measures the width of whitespace between words (set <see cref="ActualWordSpacing"/>).
        /// </summary>
        protected void MeasureWordSpacing(RGraphics g)
        {
            if (double.IsNaN(ActualWordSpacing))
            {
                _actualWordSpacing = CssUtils.WhiteSpace(g, this);
                if (WordSpacing != CssConstants.Normal)
                {
                    string len = RegexParserUtils.Search(RegexParserUtils.CssLength, WordSpacing);
                    _actualWordSpacing += CssValueParser.ParseLength(len, 1, this);
                }
            }
        }

        /// <summary>
        /// Inherits inheritable values from specified box.
        /// </summary>
        /// <param name="everything">Set to true to inherit all CSS properties instead of only the ineritables</param>
        /// <param name="p">Box to inherit the properties</param>
        protected void InheritStyle(CssBox p, bool everything)
        {
            if (p != null)
            {
                BorderSpacing = p.BorderSpacing;
                BorderCollapse = p.BorderCollapse;
                _color = p._color;
                EmptyCells = p.EmptyCells;
                WhiteSpace = p.WhiteSpace;
                Visibility = p.Visibility;
                _textIndent = p._textIndent;
                TextAlign = p.TextAlign;
                VerticalAlign = p.VerticalAlign;
                FontFamily = p.FontFamily;
                _fontSize = p._fontSize;
                FontStyle = p.FontStyle;
                FontVariant = p.FontVariant;
                FontWeight = p.FontWeight;
                ListStyleImage = p.ListStyleImage;
                ListStylePosition = p.ListStylePosition;
                ListStyleType = p.ListStyleType;
                ListStyle = p.ListStyle;
                _lineHeight = p._lineHeight;
                WordBreak = p.WordBreak;
                Direction = p.Direction;

                if (everything)
                {
                    BackgroundColor = p.BackgroundColor;
                    BackgroundGradient = p.BackgroundGradient;
                    BackgroundGradientAngle = p.BackgroundGradientAngle;
                    BackgroundImage = p.BackgroundImage;
                    BackgroundPosition = p.BackgroundPosition;
                    BackgroundRepeat = p.BackgroundRepeat;
                    _borderTopWidth = p._borderTopWidth;
                    _borderRightWidth = p._borderRightWidth;
                    _borderBottomWidth = p._borderBottomWidth;
                    _borderLeftWidth = p._borderLeftWidth;
                    _borderTopColor = p._borderTopColor;
                    _borderRightColor = p._borderRightColor;
                    _borderBottomColor = p._borderBottomColor;
                    _borderLeftColor = p._borderLeftColor;
                    BorderTopStyle = p.BorderTopStyle;
                    BorderRightStyle = p.BorderRightStyle;
                    BorderBottomStyle = p.BorderBottomStyle;
                    BorderLeftStyle = p.BorderLeftStyle;
                    _bottom = p._bottom;
                    CornerNwRadius = p.CornerNwRadius;
                    CornerNeRadius = p.CornerNeRadius;
                    CornerSeRadius = p.CornerSeRadius;
                    CornerSwRadius = p.CornerSwRadius;
                    _cornerRadius = p._cornerRadius;
                    Display = p.Display;
                    Float = p.Float;
                    Height = p.Height;
                    MarginBottom = p.MarginBottom;
                    MarginLeft = p.MarginLeft;
                    MarginRight = p.MarginRight;
                    MarginTop = p.MarginTop;
                    _left = p._left;
                    _lineHeight = p._lineHeight;
                    Overflow = p.Overflow;
                    _paddingLeft = p._paddingLeft;
                    _paddingBottom = p._paddingBottom;
                    _paddingRight = p._paddingRight;
                    _paddingTop = p._paddingTop;
                    _right = p._right;
                    TextDecoration = p.TextDecoration;
                    _top = p._top;
                    Position = p.Position;
                    Width = p.Width;
                    MaxWidth = p.MaxWidth;
                    _wordSpacing = p._wordSpacing;
                }
            }
        }
    }
}