using System;
using AppKit;
namespace RedditPlayer.Mac.Views
{
    public class FlippedClipView : NSClipView
    {
        public override bool IsFlipped => true;
    }
}

