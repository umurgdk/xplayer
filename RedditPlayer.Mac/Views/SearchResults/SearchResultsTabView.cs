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
                NeedsDisplay = true;
            }
        }

        public SearchResultsTabView ()
        {
            WantsLayer = true;
            TranslatesAutoresizingMaskIntoConstraints = false;

            ImageView = new NSImageView ();
            ImageView.TranslatesAutoresizingMaskIntoConstraints = false;

            AddSubview (ImageView);

            TextField = NSLabel.CreateWithFont ("SF UI Text", 14);

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
            } else {
                NSColor.FromRgb (245, 245, 245).Set ();
                NSBezierPath.FillRect (dirtyRect);
            }

            NSColor.FromRgb (233, 233, 233).Set ();

            // Draw bottom border
            //if (!Selected) {
            NSBezierPath.FillRect (new CGRect (dirtyRect.X, 0, dirtyRect.Width, 1));
            //}

            // Draw right border
            NSBezierPath.FillRect (new CGRect (dirtyRect.Right - 1, 0, 1, dirtyRect.Height));
        }
    }
}

