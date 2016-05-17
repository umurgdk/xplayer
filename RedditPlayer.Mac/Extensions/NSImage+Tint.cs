using System;
using AppKit;
using CoreGraphics;
namespace RedditPlayer.Mac.Extensions
{
    public static class NSImage_Tint
    {
        public static NSImage Tint (this NSImage image, NSColor color)
        {
            var copy = image.Copy () as NSImage;

            copy.LockFocus ();
            {
                color.Set ();
                var imageRect = new CGRect (CGPoint.Empty, image.Size);
                NSGraphics.RectFill (imageRect, NSCompositingOperation.SourceAtop);
            }
            copy.UnlockFocus ();

            return copy;
        }
    }
}

