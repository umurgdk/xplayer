using System;
using AppKit;
using RedditPlayer.Mac.Extensions;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using CoreGraphics;
using System.Diagnostics;
using System.Collections.Generic;
using Foundation;

namespace RedditPlayer.Mac.Views.SearchBar
{
    public delegate void SearchActivatedEventHandle (NSTextField textField, string text);

    public class SearchBarView : NSView, INSTextFieldDelegate
    {
        const float DefaultHeight = 38.5f;
        readonly NSColor BorderColor = NSColor.FromRgb (238, 238, 238);

        public NSTextField SearchField { get; protected set; }
        public NSProgressIndicator ProgressIndicator { get; protected set; }

        NSImageView searchIcon;
        NSTrackingArea trackingArea;

        public override CGSize IntrinsicContentSize => new CGSize (100, DefaultHeight);

        NSLayoutConstraint [] progressConstraints;

        public event SearchActivatedEventHandle SearchActivated;

        public SearchBarView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            searchIcon = new NSImageView ();
            searchIcon.Image = NSImage.ImageNamed ("Search");
            searchIcon.TranslatesAutoresizingMaskIntoConstraints = false;

            SearchField = new NSTextField ();
            SearchField.DrawsBackground = false;
            SearchField.FocusRingType = NSFocusRingType.None;
            SearchField.Bordered = false;
            SearchField.TranslatesAutoresizingMaskIntoConstraints = false;
            SearchField.Font = NSFont.FromFontName ("SF UI Display Regular", 14);
            SearchField.TextColor = NSColor.FromRgb (100, 100, 100);
            SearchField.PlaceholderString = "Search music here...";

            ProgressIndicator = new NSProgressIndicator ();
            ProgressIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
            ProgressIndicator.Style = NSProgressIndicatorStyle.Spinning;
            //ProgressIndicator.Hidden = true;

            AddSubview (searchIcon);
            AddSubview (SearchField);
            AddSubview (ProgressIndicator);

            ProgressIndicator.StartAnimation (this);

            BuildConstraints ();
        }

        void BuildConstraints ()
        {
            AddConstraint (NSLayoutConstraint.Create (this, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1.0f, DefaultHeight));

            AddConstraint (NSLayoutConstraint.Create (searchIcon, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1.0f, 0.0f));
            AddConstraint (NSLayoutConstraint.Create (searchIcon, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this, NSLayoutAttribute.Left, 1.0f, 20.0f));
            AddConstraint (NSLayoutConstraint.Create (searchIcon, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1.0f, 11.0f));
            AddConstraint (NSLayoutConstraint.Create (searchIcon, NSLayoutAttribute.Width, NSLayoutRelation.Equal, searchIcon, NSLayoutAttribute.Height, 1.0f, 0.0f));

            AddConstraint (NSLayoutConstraint.Create (SearchField, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, searchIcon, NSLayoutAttribute.CenterY, 1.0f, 0));
            AddConstraint (NSLayoutConstraint.Create (SearchField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, searchIcon, NSLayoutAttribute.Right, 1.0f, 10));
            AddConstraint (NSLayoutConstraint.Create (SearchField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this, NSLayoutAttribute.Right, 1.0f, -20));

            var constraints = new List<NSLayoutConstraint> ();
            constraints.Add (PinCenterY (ProgressIndicator));
            constraints.Add (FixedHeight (ProgressIndicator, 15));
            constraints.Add (WidthEqualToHeight (ProgressIndicator));
            constraints.Add (PinRight (ProgressIndicator, this, -20));

            progressConstraints = constraints.ToArray ();
            AddConstraints (progressConstraints);
        }

        public override void MouseEntered (NSEvent theEvent)
        {
            base.MouseEntered (theEvent);
            NSCursor.IBeamCursor.Set ();
        }

        public override void MouseMoved (NSEvent theEvent)
        {
            base.MouseMoved (theEvent);

            if (NSCursor.CurrentCursor != NSCursor.IBeamCursor) {
                NSCursor.IBeamCursor.Set ();
            }
        }

        public override void MouseExited (NSEvent theEvent)
        {
            base.MouseExited (theEvent);
            NSCursor.ArrowCursor.Set ();
        }

        public override void MouseDown (NSEvent theEvent)
        {
            base.MouseDown (theEvent);
            SearchField.BecomeFirstResponder ();
        }

        public override void DrawRect (CGRect dirtyRect)
        {
            if (dirtyRect.Y <= 0) {
                BorderColor.Set ();
                NSBezierPath.FillRect (new CGRect (dirtyRect.X, 0, dirtyRect.Width, 1));
            }
        }

        public override void UpdateTrackingAreas ()
        {
            if (trackingArea != null) {
                RemoveTrackingArea (trackingArea);
            }

            trackingArea = new NSTrackingArea (Bounds, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.MouseMoved | NSTrackingAreaOptions.ActiveInActiveApp, this, null);
            AddTrackingArea (trackingArea);
        }
    }
}

