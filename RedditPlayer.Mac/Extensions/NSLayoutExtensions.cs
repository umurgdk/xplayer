using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using System.Text;
namespace RedditPlayer.Mac.Extensions
{
    public enum StackOrientation
    {
        Horizontal,
        Vertical
    }

    public static class NSLayoutExtensions
    {
        public static NSLayoutConstraint [] FillHorizontal (NSView view, bool standardSpacing)
        {
            if (standardSpacing) {
                return NSLayoutConstraint.FromVisualFormat ("|-[view]-|", NSLayoutFormatOptions.None, "view", view);
            }

            return NSLayoutConstraint.FromVisualFormat ("|[view]|", NSLayoutFormatOptions.None, "view", view);
        }

        public static NSLayoutConstraint [] FillHorizontal (bool standardSpacing, params NSView [] views)
        {
            return views.SelectMany (view => FillHorizontal (view, standardSpacing)).ToArray ();
        }

        public static NSLayoutConstraint [] FillHorizontal (NSView view, float spacing)
        {
            return NSLayoutConstraint.FromVisualFormat ("|-(s)-[view]-(s)-|", NSLayoutFormatOptions.None, "view", view, "s", spacing);
        }

        public static NSLayoutConstraint [] FillVertical (NSView view, bool standardSpacing)
        {
            if (standardSpacing) {
                return NSLayoutConstraint.FromVisualFormat ("V:|-[view]-|", NSLayoutFormatOptions.None, "view", view);
            }

            return NSLayoutConstraint.FromVisualFormat ("V:|[view]|", NSLayoutFormatOptions.None, "view", view);
        }

        public static NSLayoutConstraint [] FillVertical (bool standardSpacing, params NSView [] views)
        {
            return views.SelectMany (view => FillVertical (view, standardSpacing)).ToArray ();
        }

        public static NSLayoutConstraint [] FillVertical (NSView view, float spacing)
        {
            return NSLayoutConstraint.FromVisualFormat ("V:|-(s)-[view]-(s)-|", NSLayoutFormatOptions.None, "view", view, "s", spacing);
        }

        public static NSLayoutConstraint [] StackOld (StackOrientation orientation, bool toSuperView, bool leadingSpace, bool trailingSpace, params NSView [] views)
        {
            var vflBuilder = new StringBuilder ();
            vflBuilder.Append (orientation == StackOrientation.Horizontal ? "" : "V:");
            vflBuilder.Append (toSuperView ? "|" : "");
            vflBuilder.Append (leadingSpace ? "-" : "");

            var viewNames = views.Select ((v, index) => {
                var identifier = v.Identifier;
                identifier = string.IsNullOrEmpty (identifier) ? v.Class.Name : identifier;

                return string.Format ("{0}_{1}", identifier, index);
            }).ToList ();

            vflBuilder.Append ("[");
            vflBuilder.Append (string.Join ("]-[", viewNames));
            vflBuilder.Append ("]");

            vflBuilder.Append (trailingSpace ? "-" : "");

            if (trailingSpace && toSuperView)
                vflBuilder.Append ("(>=20)-|");

            //else if (trailingSpace)
            //    vflBuilder.Append ("(>=8)-|");

            var viewObjects = new List<object> (views.Length * 2);
            for (var i = 0; i < views.Length; i++) {
                viewObjects.Add (viewNames [i]);
                viewObjects.Add (views [i]);
            }

            return NSLayoutConstraint.FromVisualFormat (vflBuilder.ToString (), NSLayoutFormatOptions.None, viewObjects.ToArray ());
        }

        public static NSLayoutConstraint FixedWidth (NSView view, float width)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, 1.0f, width);
        }

        public static NSLayoutConstraint FixedHeight (NSView view, float height)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1.0f, height);
        }

        public static NSLayoutConstraint MinimumWidth (NSView view, float width)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Width, NSLayoutRelation.GreaterThanOrEqual, 1.0f, width);
        }

        public static NSLayoutConstraint MinimumHeight (NSView view, float height)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual, 1.0f, height);
        }

        public static NSLayoutConstraint MaximumWidth (NSView view, float width)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Width, NSLayoutRelation.LessThanOrEqual, 1.0f, width);
        }

        public static NSLayoutConstraint MaximumHeight (NSView view, float height)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Height, NSLayoutRelation.LessThanOrEqual, 1.0f, height);
        }

        #region EqualSizes

        public static NSLayoutConstraint EqualHeights (NSView view, NSView otherView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, otherView, NSLayoutAttribute.Height, 1.0f, padding);
        }

        public static NSLayoutConstraint EqualHeights (NSView view, NSView otherView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, otherView, NSLayoutAttribute.Height, 1.0f, 0.0f);
        }

        public static NSLayoutConstraint EqualWidths (NSView view, NSView otherView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, otherView, NSLayoutAttribute.Width, 1.0f, padding);
        }

        public static NSLayoutConstraint EqualWidths (NSView view, NSView otherView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, otherView, NSLayoutAttribute.Width, 1.0f, 0.0f);
        }

        public static NSLayoutConstraint WidthEqualToHeight (NSView view)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, view, NSLayoutAttribute.Height, 1.0f, 0.0f);
        }

        public static NSLayoutConstraint [] HorizontalSpaceBetween (NSView leftView, NSView rightView)
        {
            return HorizontalSpaceBetween (leftView, rightView, NSLayoutFormatOptions.None);
        }

        public static NSLayoutConstraint [] VerticalSpaceBetween (NSView topView, NSView bottomView)
        {
            return VerticalSpaceBetween (topView, bottomView, NSLayoutFormatOptions.None);
        }

        public static NSLayoutConstraint [] HorizontalSpaceBetween (NSView leftView, NSView rightView, NSLayoutFormatOptions options)
        {
            return NSLayoutConstraint.FromVisualFormat ("[left]-(>=0)-[right]", options, "left", leftView, "right", rightView);
        }

        public static NSLayoutConstraint [] VerticalSpaceBetween (NSView topView, NSView bottomView, NSLayoutFormatOptions options)
        {
            return NSLayoutConstraint.FromVisualFormat ("V:[top]-(>=0)-[bottom]", options, "top", topView, "bottom", bottomView);
        }

        public static NSLayoutConstraint [] VerticalSpaceBetween (NSView topView, NSView bottomView, NSLayoutFormatOptions options, NSLayoutPriority priority)
        {
            return NSLayoutConstraint.FromVisualFormat ($"V:[top]-(>=0@{(float)priority})-[bottom]", options, "top", topView, "bottom", bottomView);
        }

        #endregion

        static NSLayoutConstraint [] Stack (StackOrientation orientation, float padding, NSLayoutFormatOptions options, params NSView [] views)
        {
            var names = views.Select ((view, index) => $"{view.Identifier}__{index}");
            var orientIndicator = orientation == StackOrientation.Horizontal ? "H:" : "V:";
            var separator = padding < 0 ? "-" : $"-({padding})-";
            var vfl = string.Join (separator, names.Select (name => $"[{name}]"));
            var objects = new List<object> ();

            for (var i = 0; i < views.Length; i++) {
                objects.Add (names.ElementAt (i));
                objects.Add (views [i]);
            }

            return NSLayoutConstraint.FromVisualFormat (orientIndicator + vfl, options, objects.ToArray ());
        }

        #region StackHorizontal

        public static NSLayoutConstraint [] StackHorizontal (float padding, NSLayoutFormatOptions options, params NSView [] views)
        {
            return NSLayoutExtensions.Stack (StackOrientation.Horizontal, padding, options, views);
        }

        public static NSLayoutConstraint [] StackHorizontal (float padding, params NSView [] views)
        {
            return NSLayoutExtensions.Stack (StackOrientation.Horizontal, padding, NSLayoutFormatOptions.None, views);
        }

        public static NSLayoutConstraint [] StackHorizontal (params NSView [] views)
        {
            return NSLayoutExtensions.Stack (StackOrientation.Horizontal, -1, NSLayoutFormatOptions.None, views);
        }

        #endregion

        #region StackVertical

        public static NSLayoutConstraint [] StackVertical (float padding, NSLayoutFormatOptions options, params NSView [] views)
        {
            return NSLayoutExtensions.Stack (StackOrientation.Vertical, padding, options, views);
        }

        public static NSLayoutConstraint [] StackVertical (float padding, params NSView [] views)
        {
            return NSLayoutExtensions.Stack (StackOrientation.Vertical, padding, NSLayoutFormatOptions.None, views);
        }

        public static NSLayoutConstraint [] StackVertical (params NSView [] views)
        {
            return NSLayoutExtensions.Stack (StackOrientation.Vertical, -1, NSLayoutFormatOptions.None, views);
        }

        #endregion

        #region PinCenterX

        public static NSLayoutConstraint PinCenterX (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterX, relation, superView, NSLayoutAttribute.CenterX, 1.0f, padding);
        }

        public static NSLayoutConstraint PinCenterX (NSView view, NSView superView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, superView, NSLayoutAttribute.CenterX, 1.0f, padding);
        }

        public static NSLayoutConstraint PinCenterX (NSView view, NSView superView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, superView, NSLayoutAttribute.CenterX, 1.0f, 0);
        }

        public static NSLayoutConstraint PinCenterX (NSView view)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, view.Superview, NSLayoutAttribute.CenterX, 1.0f, 0);
        }

        #endregion

        #region PinCenterY

        public static NSLayoutConstraint PinCenterY (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterY, relation, superView, NSLayoutAttribute.CenterY, 1.0f, padding);
        }

        public static NSLayoutConstraint PinCenterY (NSView view, NSView superView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, superView, NSLayoutAttribute.CenterY, 1.0f, padding);
        }

        public static NSLayoutConstraint PinCenterY (NSView view, NSView superView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, superView, NSLayoutAttribute.CenterY, 1.0f, 0);
        }

        public static NSLayoutConstraint PinCenterY (NSView view)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, view.Superview, NSLayoutAttribute.CenterY, 1.0f, 0);
        }

        #endregion

        #region PinLeft

        public static NSLayoutConstraint PinLeft (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Left, relation, superView, NSLayoutAttribute.Left, 1.0f, padding);
        }

        public static NSLayoutConstraint PinLeft (NSView view, NSView superView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Left, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Left, 1.0f, padding);
        }

        public static NSLayoutConstraint PinLeft (NSView view, NSView superView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Left, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Left, 1.0f, 0);
        }

        public static NSLayoutConstraint PinLeft (NSView view)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Left, NSLayoutRelation.Equal, view.Superview, NSLayoutAttribute.Left, 1.0f, 0);
        }

        #endregion

        #region PinTop

        public static NSLayoutConstraint PinTop (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Top, relation, superView, NSLayoutAttribute.Top, 1.0f, padding);
        }

        public static NSLayoutConstraint PinTop (NSView view, NSView superView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Top, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Top, 1.0f, padding);
        }

        public static NSLayoutConstraint PinTop (NSView view, NSView superView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Top, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Top, 1.0f, 0);
        }

        public static NSLayoutConstraint PinTop (NSView view)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Top, NSLayoutRelation.Equal, view.Superview, NSLayoutAttribute.Top, 1.0f, 0);
        }

        #endregion

        #region PinBottom

        public static NSLayoutConstraint PinBottom (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Bottom, relation, superView, NSLayoutAttribute.Bottom, 1.0f, padding);
        }

        public static NSLayoutConstraint PinBottom (NSView view, NSView superView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Bottom, 1.0f, padding);
        }

        public static NSLayoutConstraint PinBottom (NSView view, NSView superView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Bottom, 1.0f, 0);
        }

        public static NSLayoutConstraint PinBottom (NSView view)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, view.Superview, NSLayoutAttribute.Bottom, 1.0f, 0);
        }

        #endregion

        #region PinRight

        public static NSLayoutConstraint PinRight (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Right, relation, superView, NSLayoutAttribute.Right, 1.0f, padding);
        }

        public static NSLayoutConstraint PinRight (NSView view, NSView superView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Right, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Right, 1.0f, padding);
        }

        public static NSLayoutConstraint PinRight (NSView view, NSView superView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Right, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Right, 1.0f, 0);
        }

        public static NSLayoutConstraint PinRight (NSView view)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.Right, NSLayoutRelation.Equal, view.Superview, NSLayoutAttribute.Right, 1.0f, 0);
        }

        #endregion

        #region PinFirstBaseline

        public static NSLayoutConstraint PinFirstBaseline (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.FirstBaseline, relation, superView, NSLayoutAttribute.FirstBaseline, 1.0f, padding);
        }

        public static NSLayoutConstraint PinFirstBaseline (NSView view, NSView superView, float padding)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.FirstBaseline, NSLayoutRelation.Equal, superView, NSLayoutAttribute.FirstBaseline, 1.0f, padding);
        }

        public static NSLayoutConstraint PinFirstBaseline (NSView view, NSView superView)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.FirstBaseline, NSLayoutRelation.Equal, superView, NSLayoutAttribute.FirstBaseline, 1.0f, 0);
        }

        public static NSLayoutConstraint PinFirstBaseline (NSView view)
        {
            return NSLayoutConstraint.Create (view, NSLayoutAttribute.FirstBaseline, NSLayoutRelation.Equal, view.Superview, NSLayoutAttribute.FirstBaseline, 1.0f, 0);
        }

        #endregion

        #region PinBottomLeft

        public static NSLayoutConstraint [] PinBottomLeft (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return new NSLayoutConstraint []
            {
                NSLayoutConstraint.Create (view, NSLayoutAttribute.Left, relation, superView, NSLayoutAttribute.Left, 1.0f, padding),
                NSLayoutConstraint.Create (view, NSLayoutAttribute.Bottom, relation, superView, NSLayoutAttribute.Bottom, 1.0f, padding)
            };
        }

        public static NSLayoutConstraint [] PinBottomLeft (NSView view, NSView superView, float padding)
        {
            return PinBottomLeft (view, superView, padding, NSLayoutRelation.Equal);
        }

        public static NSLayoutConstraint [] PinBottomLeft (NSView view, NSView superView)
        {
            return PinBottomLeft (view, superView, 0, NSLayoutRelation.Equal);
        }

        public static NSLayoutConstraint [] PinBottomLeft (NSView view)
        {
            return PinBottomLeft (view, view.Superview, 0, NSLayoutRelation.Equal);
        }

        #endregion

        #region PinBottomRight

        public static NSLayoutConstraint [] PinBottomRight (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return new NSLayoutConstraint []
            {
                NSLayoutConstraint.Create (view, NSLayoutAttribute.Right, relation, superView, NSLayoutAttribute.Right, 1.0f, padding),
                NSLayoutConstraint.Create (view, NSLayoutAttribute.Bottom, relation, superView, NSLayoutAttribute.Bottom, 1.0f, padding)
            };
        }

        public static NSLayoutConstraint [] PinBottomRight (NSView view, NSView superView, float padding)
        {
            return PinBottomRight (view, superView, padding, NSLayoutRelation.Equal);
        }

        public static NSLayoutConstraint [] PinBottomRight (NSView view, NSView superView)
        {
            return PinBottomRight (view, superView, 0, NSLayoutRelation.Equal);
        }

        public static NSLayoutConstraint [] PinBottomRight (NSView view)
        {
            return PinBottomRight (view, view.Superview, 0, NSLayoutRelation.Equal);
        }

        #endregion

        #region PinTopLeft

        public static NSLayoutConstraint [] PinTopLeft (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return new NSLayoutConstraint []
            {
                NSLayoutConstraint.Create (view, NSLayoutAttribute.Left, relation, superView, NSLayoutAttribute.Left, 1.0f, padding),
                NSLayoutConstraint.Create (view, NSLayoutAttribute.Top, relation, superView, NSLayoutAttribute.Top, 1.0f, padding)
            };
        }

        public static NSLayoutConstraint [] PinTopLeft (NSView view, NSView superView, float padding)
        {
            return PinTopLeft (view, superView, padding, NSLayoutRelation.Equal);
        }

        public static NSLayoutConstraint [] PinTopLeft (NSView view, NSView superView)
        {
            return PinTopLeft (view, superView, 0, NSLayoutRelation.Equal);
        }

        public static NSLayoutConstraint [] PinTopLeft (NSView view)
        {
            return PinTopLeft (view, view.Superview, 0, NSLayoutRelation.Equal);
        }

        #endregion

        #region PinTopRight

        public static NSLayoutConstraint [] PinTopRight (NSView view, NSView superView, float padding, NSLayoutRelation relation)
        {
            return new NSLayoutConstraint []
            {
                NSLayoutConstraint.Create (view, NSLayoutAttribute.Right, relation, superView, NSLayoutAttribute.Right, 1.0f, padding),
                NSLayoutConstraint.Create (view, NSLayoutAttribute.Top, relation, superView, NSLayoutAttribute.Top, 1.0f, padding)
            };
        }

        public static NSLayoutConstraint [] PinTopRight (NSView view, NSView superView, float padding)
        {
            return PinTopRight (view, superView, padding, NSLayoutRelation.Equal);
        }

        public static NSLayoutConstraint [] PinTopRight (NSView view, NSView superView)
        {
            return PinTopRight (view, superView, 0, NSLayoutRelation.Equal);
        }

        public static NSLayoutConstraint [] PinTopRight (NSView view)
        {
            return PinTopRight (view, view.Superview, 0, NSLayoutRelation.Equal);
        }

        #endregion
    }
}

