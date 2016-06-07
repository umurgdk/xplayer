using System;
using AppKit;
namespace RedditPlayer.Mac.Views
{
    public class FlippedView : NSView
    {
        public override bool IsFlipped => true;

        public FlippedView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
        }
    }
}

