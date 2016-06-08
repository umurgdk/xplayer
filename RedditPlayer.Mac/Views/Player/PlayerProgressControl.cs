using AppKit;
using CoreGraphics;
using System;
using CoreAnimation;
using Foundation;
using System.Diagnostics;
using static RedditPlayer.Mac.Extensions.NSBezierPathExtensions;
using RedditPlayer.Mac.Extensions;
using ReactiveUI;
using System.ComponentModel;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;

namespace RedditPlayer.Mac.Views.Player
{
    public class PlayerProgressControl : NSControl, ICALayerDelegate, IReactiveObject
    {
        [Reactive]
        public bool IsDragging { get; protected set; }

        public event ReactiveUI.PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        float progress = 0.0f;
        public float Progress
        {
            get
            {
                return progress;
            }

            set
            {
                this.RaiseAndSetIfChanged (ref progress, value);
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

        public override CGRect Frame
        {
            get
            {
                return base.Frame;
            }
            set
            {
                base.Frame = value;
                NeedsDisplay = true;
                animateChanges = false;
            }
        }

        public override bool WantsUpdateLayer => true;

        public override CGSize IntrinsicContentSize => new CGSize (5, 3);

        public override bool AcceptsFirstMouse (NSEvent theEvent) => true;

        bool showProgressHandle;
        bool animateChanges = true;

        NSTrackingArea trackingArea;

        CAShapeLayer progressHandleLayer;
        CALayer backgroundLayer;
        CALayer filledLayer;

        public PlayerProgressControl ()
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

            this.WhenAnyValue (control => control.Progress)
                .DistinctUntilChanged ()
                .Subscribe (_ => NeedsDisplay = true);
        }

        public override void MouseEntered (NSEvent theEvent)
        {
            if (!IsDragging) {
                showProgressHandle = true;
                NeedsDisplay = true;
            }
        }

        public override void MouseExited (NSEvent theEvent)
        {
            if (!IsDragging) {
                showProgressHandle = false;
                NeedsDisplay = true;
            }
        }

        float CalculateProgressFromMouseEvent (NSEvent theEvent)
        {
            var dragLocation = ConvertPointFromView (theEvent.LocationInWindow, null);
            var fromLeft = Math.Min (dragLocation.X - Frame.Left, Frame.Width);
            var calculatedProgress = (float)(fromLeft / Frame.Width);

            return calculatedProgress;
        }

        public override void MouseDown (NSEvent theEvent)
        {
            var loop = true;
            while (loop) {
                var localEvent = Window.NextEventMatchingMask (NSEventMask.LeftMouseUp | NSEventMask.LeftMouseDragged);

                switch (localEvent.Type) {
                case NSEventType.LeftMouseDragged:
                    IsDragging = true;

                    animateChanges = false;
                    Progress = CalculateProgressFromMouseEvent (localEvent);

                    break;

                case NSEventType.LeftMouseUp:
                    loop = false;
                    showProgressHandle = IsMouseInRect (localEvent.LocationInWindow, Frame);

                    IsDragging = false;
                    Progress = CalculateProgressFromMouseEvent (localEvent);
                    break;

                default:
                    IsDragging = false;
                    break;
                }
            }
        }

        public override void UpdateTrackingAreas ()
        {
            if (trackingArea != null)
                RemoveTrackingArea (trackingArea);

            var trackingRect = Bounds;
            trackingRect.Inflate (4, 4);

            trackingArea = new NSTrackingArea (trackingRect, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.ActiveInKeyWindow, this, null);
            AddTrackingArea (trackingArea);
        }

        public override void UpdateLayer ()
        {
            if (!animateChanges) {
                CATransaction.Begin ();
                CATransaction.DisableActions = true;
            }

            var width = Layer.Bounds.Width;

            var progressX = Layer.Bounds.Width * progress;
            var midY = Layer.Bounds.GetMidY ();

            backgroundLayer.Frame = new CGRect (0, midY - 1, Layer.Bounds.Width, 3.0f);
            filledLayer.Frame = new CGRect (0, midY - 1, progressX, 3.0f);

            progressHandleLayer.Opacity = (showProgressHandle || IsDragging ? 1 : 0);

            progressHandleLayer.Position = new CGPoint (progressX, midY);

            if (!animateChanges) {
                CATransaction.Commit ();
            }

            animateChanges = true;
        }

        public void RaisePropertyChanging (ReactiveUI.PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke (this, args);
        }

        public void RaisePropertyChanged (PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke (this, args);
        }
    }
}

