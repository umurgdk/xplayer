using System;
using AppKit;
using System.Collections.Generic;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
namespace RedditPlayer.Mac.Views
{
    public class PlayerWindowController : NSWindowController
    {
        List<NSLayoutConstraint> contentConstraints;

        public PlayerWindowController ()
        {
            Window = new PlayerWindow ();

            contentConstraints = new List<NSLayoutConstraint> ();
        }

        public void PresentView (NSView view)
        {
            if (Window.ContentView.Subviews.Length > 0)
                Window.ContentView.Subviews [0].RemoveFromSuperview ();

            Window.ContentView.AddSubview (view);

            if (contentConstraints.Count > 0)
                Window.ContentView.RemoveConstraints (contentConstraints.ToArray ());

            contentConstraints = new List<NSLayoutConstraint> ();
            contentConstraints.AddRange (FillHorizontal (false, view));
            contentConstraints.AddRange (FillVertical (false, view));

            Window.ContentView.AddConstraints (contentConstraints.ToArray ());
        }

        public new PlayerWindow Window
        {
            get
            {
                return (PlayerWindow)base.Window;
            }
            set
            {
                base.Window = value;
            }
        }
    }
}

