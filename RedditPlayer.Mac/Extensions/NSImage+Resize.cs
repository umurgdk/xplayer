using System;
using AppKit;
using CoreGraphics;
namespace RedditPlayer.Mac.Extensions
{
    public enum NSImageScalingMode
    {
        ScaleToFill,
        ScaleAspectFit,
        ScaleAspectFill
    }

    [Flags]
    public enum NSImageSizing
    {
        Up,
        Down
    }

    public static class NSImage_Resize
    {
        public static NSImage Resize (this NSImage image, CGSize destSize, NSImageScalingMode scalingMode)
        {
            return Resize (image, destSize, scalingMode, NSImageSizing.Down | NSImageSizing.Up);
        }

        public static NSImage Resize (this NSImage image, CGSize destSize, NSImageScalingMode scalingMode, NSImageSizing sizingMode)
        {
            var sourceSize = image.Size;

            var wantToScaleUp = (destSize.Height > sourceSize.Height || destSize.Width > sourceSize.Width);
            var canScaleUp = (NSImageSizing.Up & sizingMode) == NSImageSizing.Up;
            var wantToScaleDown = (destSize.Height < sourceSize.Height || destSize.Width < sourceSize.Width);
            var canScaleDown = (NSImageSizing.Down & sizingMode) == NSImageSizing.Down;

            if (!canScaleDown && wantToScaleDown) {
                sourceSize.Width = destSize.Width;
                sourceSize.Height = destSize.Height;
            }

            if (!canScaleUp && wantToScaleUp) {
                destSize.Width = sourceSize.Width;
                destSize.Height = sourceSize.Height;
            }

            var destRect = new CGRect (0, 0, destSize.Width, destSize.Height);
            var sourceRect = new CGRect (0, 0, sourceSize.Width, sourceSize.Height);

            var targetImage = new NSImage (destSize);

            if (scalingMode != NSImageScalingMode.ScaleToFill) {
                float ratioH = (float)(destSize.Height / sourceSize.Height);
                float ratioW = (float)(destSize.Width / sourceSize.Width);

                var fill = scalingMode == NSImageScalingMode.ScaleAspectFill;
                var h = ratioH >= ratioW;
                if ((fill && !h) || (h && !fill)) {
                    var newSize = new CGSize (sourceSize.Width, destSize.Height / ratioW);
                    sourceRect = new CGRect (sourceRect.Location, newSize);
                } else {
                    var newSize = new CGSize (Math.Floor (destSize.Width / ratioH), sourceSize.Height);
                    sourceRect = new CGRect (sourceRect.Location, newSize);
                }
            }

            var newX = Math.Floor ((sourceSize.Width - sourceRect.Size.Width) / 2);
            var newY = Math.Floor ((sourceSize.Height - sourceRect.Size.Height) / 2);

            sourceRect.Location = new CGPoint (newX, newY);

            targetImage.LockFocus ();
            image.DrawInRect (destRect, sourceRect, NSCompositingOperation.Copy, 1.0f);
            targetImage.UnlockFocus ();

            return targetImage;
        }
    }
}

