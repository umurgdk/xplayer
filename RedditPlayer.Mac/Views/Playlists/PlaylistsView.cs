using System;
using AppKit;
using RedditPlayer.Mac.Views.Detail;
using RedditPlayer.Mac.Extensions;
namespace RedditPlayer.Mac.Views.Playlists
{
    public class PlaylistsView : NSView
    {
        public readonly NSOutlineView OutlineView;

        public PlaylistsView ()
        {
            var scrollView = new NSScrollView ();
            scrollView.TranslatesAutoresizingMaskIntoConstraints = false;

            var clipView = new NSClipView ();

            OutlineView = new NSOutlineView ();
            OutlineView.HeaderView = null;
            OutlineView.FloatsGroupRows = false;
            OutlineView.BackgroundColor = NSColor.FromRgb (245, 245, 245);
            OutlineView.IndentationPerLevel = 4;

            var outlineColumn = new NSTableColumn ();
            outlineColumn.Editable = false;
            outlineColumn.MinWidth = 100;

            OutlineView.AddColumn (outlineColumn);
            OutlineView.OutlineTableColumn = outlineColumn;
            outlineColumn.Dispose ();
            outlineColumn = null;

            scrollView.AddSubview (clipView);
            scrollView.DocumentView = OutlineView;

            AddSubview (scrollView);

            AddConstraints (NSLayoutExtensions.FillHorizontal (scrollView, false));
            AddConstraints (NSLayoutExtensions.FillVertical (scrollView, false));
        }
    }
}

