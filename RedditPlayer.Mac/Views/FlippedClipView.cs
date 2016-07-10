using System;
using AppKit;
using Foundation;
namespace RedditPlayer.Mac.Views
{
    [Register ("XPFlippedClipView")]
    public class FlippedClipView : NSClipView
    {
        public override bool IsFlipped => true;

        public FlippedClipView ()
        {
        }

        public FlippedClipView (NSCoder coder) : base (coder)
        {
        }

        public FlippedClipView (NSObjectFlag t) : base (t)
        {
        }

        public FlippedClipView (IntPtr handle) : base (handle)
        {
        }

        public FlippedClipView (CoreGraphics.CGRect frameRect) : base (frameRect)
        {
        }
    }
}

