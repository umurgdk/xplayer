using AppKit;
using CoreGraphics;
using System;
using CoreAnimation;
using Foundation;
using System.Diagnostics;
using static RedditPlayer.Mac.Extensions.NSBezierPathExtensions;
using RedditPlayer.Mac.Extensions;

namespace RedditPlayer.Mac.Views.Player
{
    public class PlayerProgress : NSView, ICALayerDelegate
    {
        float progress = 0.0f;
        public float Progress
        {
            get
            {
                return progress;
            }

            set
            {
                progress = value;
                NeedsDisplay = true;
            }
        }

        NSColor fillColor = NSColor.FromRgb (56, 176, 255);
        public NSColor FillColor
        {
            get
            {
                return fillColor;
            }

            set
            {
                fillColor = value;
                NeedsDisplay = true;
            }
        }

        NSColor backgroundColor = NSColor.FromRgb (206, 236, 255);
        public NSColor BackgroundColor
        {
            get
            {
                return backgroundColor;
            }

            set
            {
                backgroundColor = value;
                NeedsDisplay = true;
            }
        }

        public override bool WantsUpdateLayer => true;

        public override CGSize IntrinsicContentSize => new CGSize (5, 3);

        bool showProgressHandle;

        NSTrackingArea trackingArea;

        CAShapeLayer progressHandleLayer;
        CALayer backgroundLayer;
        CALayer filledLayer;

        CABasicAnimation scaleUp;
        CABasicAnimation scaleDown;

        public PlayerProgress ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            WantsLayer = true;

            progressHandleLayer = new CAShapeLayer ();
            progressHandleLayer.Path = CGPath.EllipseFromRect (new CGRect (0, 0, 10, 10));
            progressHandleLayer.FillColor = fillColor.CGColor;
            progressHandleLayer.Bounds = new CGRect (0, 0, 10, 10);
            progressHandleLayer.AnchorPoint = new CGPoint (0.5, 0.5);
            progressHandleLayer.Opacity = 0;

            backgroundLayer = new CALayer ();
            backgroundLayer.BackgroundColor = backgroundColor.CGColor;

            filledLayer = new CALayer ();
            filledLayer.BackgroundColor = fillColor.CGColor;

            Layer.AddSublayer (backgroundLayer);
            Layer.AddSublayer (filledLayer);
            Layer.AddSublayer (progressHandleLayer);
        }

        public override void MouseEntered (NSEvent theEvent)
        {
            showProgressHandle = true;
            NeedsDisplay = true;
        }

        public override void MouseExited (NSEvent theEvent)
        {
            showProgressHandle = false;
            NeedsDisplay = true;
        }

        public override void UpdateTrackingAreas ()
        {
            if (trackingArea != null)
                RemoveTrackingArea (trackingArea);

            var trackingRect = Bounds;
            trackingRect.Inflate (4, 4);

            trackingArea = new NSTrackingArea (trackingRect, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.MouseMoved | NSTrackingAreaOptions.ActiveInKeyWindow, this, null);
            AddTrackingArea (trackingArea);
        }

        public override void UpdateLayer ()
        {
            var width = Layer.Bounds.Width;
            var progressX = Layer.Bounds.Width * progress;
            var midY = Layer.Bounds.GetMidY ();

            backgroundLayer.Frame = new CGRect (0, midY - 1, Layer.Bounds.Width, 3.0f);
            filledLayer.Frame = new CGRect (0, midY - 1, progressX, 3.0f);

            progressHandleLayer.Opacity = (showProgressHandle ? 1 : 0);

            progressHandleLayer.Position = new CGPoint (progressX, midY);
        }
    }
}

