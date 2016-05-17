using System;
using AppKit;
using Foundation;
using AVFoundation;
using RedditPlayer.Mac.Extensions;

using CoreGraphics;
namespace RedditPlayer.Mac.Views
{
    [Register ("WindowContentView")]
    public class WindowContentView : NSView
    {
        private CGRect PreviousRect;
        private NSImage ScaledImageCache;

        private NSImage backgroundImage;

        public WindowContentView (IntPtr handle) : base (handle)
        {
            backgroundImage = NSImage.ImageNamed ("windowbg.jpg");
        }

        public override void DrawRect (CGRect dirtyRect)
        {
            //NSGraphicsContext.GlobalSaveGraphicsState ();

            //if (PreviousRect != Frame || ScaledImageCache == null) {
            //    RenderBackgroundImage ();
            //}

            //ScaledImageCache.DrawInRect (dirtyRect, dirtyRect, NSCompositingOperation.SourceOver, 1.0f);
            //PreviousRect = Frame;

            //NSGraphicsContext.GlobalRestoreGraphicsState ();
        }

        void RenderBackgroundImage ()
        {
            var resizedImage = backgroundImage.Resize (Frame.Size, NSImageScalingMode.ScaleAspectFill);

            var colors = new NSColor []
            {
                NSColor.FromWhite(0.0f, 0.95f),
                NSColor.FromWhite (0.0f, 0.75f)
            };

            var locations = new float []
            {
                0.0f,
                0.3f
            };

            resizedImage.LockFocus ();
            var gradient = new NSGradient (colors, locations);
            gradient.DrawInRect (new CGRect (new CGPoint (0, 0), resizedImage.Size), -90.0f);
            resizedImage.UnlockFocus ();

            ScaledImageCache = resizedImage;
        }
    }
}

