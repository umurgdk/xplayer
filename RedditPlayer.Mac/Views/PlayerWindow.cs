using System;
using AppKit;
using CoreGraphics;
using RedditPlayer.Mac.Views.Detail;
using RedditPlayer.Mac.Views.Playlists;
namespace RedditPlayer.Mac.Views
{
    public class PlayerWindow : NSWindow
    {
        NSSplitView splitView;

        DetailViewController detailViewController;
        PlaylistsViewController playlistsViewController;

        public PlayerWindow (ApplicationViewModel viewModel)
        {
            var screenFrame = NSScreen.MainScreen.VisibleFrame;
            SetFrame (new CGRect (screenFrame.GetMidX () - 400, screenFrame.GetMidY () - 300, 800, 600), false);
            StyleMask = NSWindowStyle.Resizable | NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable;
            BackgroundColor = NSColor.White;

            // Create splitview
            splitView = new NSSplitView ();
            splitView.IsVertical = true;
            splitView.DividerStyle = NSSplitViewDividerStyle.Thin;
            splitView.TranslatesAutoresizingMaskIntoConstraints = false;

            playlistsViewController = new PlaylistsViewController (viewModel);
            detailViewController = new DetailViewController (viewModel);

            splitView.AddArrangedSubview (playlistsViewController.View);
            splitView.AddArrangedSubview (detailViewController.View);
            splitView.SetHoldingPriority ((float)NSLayoutPriority.DefaultLow + 1, 0);

            ContentView.AddSubview (splitView);

            var horizontalConstraints = NSLayoutConstraint.FromVisualFormat ("|[splitView]|", NSLayoutFormatOptions.None, "splitView", splitView);
            var verticalConstraints = NSLayoutConstraint.FromVisualFormat ("V:|[splitView]|", NSLayoutFormatOptions.None, "splitView", splitView);

            var sidebarWidthConstraint = NSLayoutConstraint.Create (playlistsViewController.View, NSLayoutAttribute.Width, NSLayoutRelation.GreaterThanOrEqual, 1.0f, 170.0f);

            ContentView.AddConstraint (sidebarWidthConstraint);
            ContentView.AddConstraints (horizontalConstraints);
            ContentView.AddConstraints (verticalConstraints);
        }
    }
}

