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

        public static NSLayoutConstraint [] FillVertical (NSView view, float spacing)
        {
            return NSLayoutConstraint.FromVisualFormat ("V:|-(s)-[view]-(s)-|", NSLayoutFormatOptions.None, "view", view, "s", spacing);
        }

        public static NSLayoutConstraint [] Stack (StackOrientation orientation, bool toSuperView, bool leadingSpace, bool trailingSpace, params NSView [] views)
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
    }
}

