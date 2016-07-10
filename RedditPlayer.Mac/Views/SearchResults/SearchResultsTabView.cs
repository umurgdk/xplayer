using System;
using AppKit;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System.Reactive.Linq;
using CoreGraphics;
using System.Diagnostics;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultsTabView : NSView
    {
        public NSImageView ImageView { get; protected set; }
        public NSTextField TextField { get; protected set; }

        bool selected;
        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
                if (selected && darkTheme)
                    TextField.TextColor = NSColor.Black;
                if (!selected && darkTheme)
                    TextField.TextColor = NSColor.FromDeviceWhite (1.0f, 1.0f);

                NeedsDisplay = true;
            }
        }

        bool darkTheme;

        public SearchResultsTabView () : this (false)
        {
        }

        public SearchResultsTabView (bool darkTheme)
        {
            this.darkTheme = darkTheme;

            TranslatesAutoresizingMaskIntoConstraints = false;

            ImageView = new NSImageView ();
            ImageView.TranslatesAutoresizingMaskIntoConstraints = false;

            AddSubview (ImageView);

            TextField = NSLabel.CreateWithFont ("SF UI Text", 14);
            TextField.TextColor = darkTheme ? NSColor.FromDeviceWhite (1.0f, 0.0f) : NSColor.FromDeviceWhite (0, 1);

            AddSubview (TextField);

            var hFormat = "H:|-(16)-[image(16)]-[text]-(16)-|";

            var objects = new object [] {
                "image", ImageView,
                "text", TextField
            };

            AddConstraints (NSLayoutConstraint.FromVisualFormat (hFormat, NSLayoutFormatOptions.AlignAllCenterY, objects));
            AddConstraint (NSLayoutConstraint.Create (ImageView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1.0f, 0.0f));
        }

        public override void DrawRect (CGRect dirtyRect)
        {
            if (Selected) {
                NSColor.White.SetFill ();
                NSBezierPath.FillRect (dirtyRect);
            } else if (!darkTheme) {
                NSColor.FromRgb (245, 245, 245).Set ();
                NSBezierPath.FillRect (dirtyRect);
            }

            if (!darkTheme) {
                NSColor.FromRgb (233, 233, 233).Set ();
                NSBezierPath.FillRect (new CGRect (dirtyRect.X, 0, dirtyRect.Width, 1));
                NSBezierPath.FillRect (new CGRect (dirtyRect.Right - 1, 0, 1, dirtyRect.Height));
            } else if (!Selected) {
                NSColor.FromWhite (1.0f, 0.21f).Set ();
                NSBezierPath.FillRect (new CGRect (dirtyRect.Right - 1, 0, 1, dirtyRect.Height));
            }
        }
    }
}

