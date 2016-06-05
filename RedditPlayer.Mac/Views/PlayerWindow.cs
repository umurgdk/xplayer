using System;
using AppKit;
using CoreGraphics;
using RedditPlayer.Mac.Views.Detail;
using RedditPlayer.Mac.Views.Playlists;
using RedditPlayer.Mac.Views.SearchResults;
namespace RedditPlayer.Mac.Views
{
    public class PlayerWindow : NSWindow
    {
        public PlayerWindow ()
        {
            var screenFrame = NSScreen.MainScreen.VisibleFrame;
            SetFrame (new CGRect (screenFrame.GetMidX () - 400, screenFrame.GetMidY () - 300, 800, 600), false);
            StyleMask = NSWindowStyle.Resizable | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Titled;
            BackgroundColor = NSColor.White;
        }
    }
}

