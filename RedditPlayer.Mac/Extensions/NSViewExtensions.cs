using System;
using AppKit;
using CoreGraphics;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
namespace RedditPlayer.Mac.Extensions
{
    public static class NSViewExtensions
    {
        public static void ShowInWindow (this NSView view)
        {
            var window = new NSWindow (
                new CGRect (0, 0, 200, 200),
                NSWindowStyle.Titled | NSWindowStyle.Resizable | NSWindowStyle.Closable,
                NSBackingStore.Buffered,
                false
            );

            if (view.Superview != null) {
                view.RemoveFromSuperview ();
            }

            window.ContentView.AddSubview (view);

            window.ContentView.AddConstraints (FillHorizontal (view, false));
            window.ContentView.AddConstraints (FillVertical (view, false));

            window.MakeKeyAndOrderFront (view);
        }
    }
}

