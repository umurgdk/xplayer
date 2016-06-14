// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RedditPlayer.Mac.Views.SearchResults
{
    [Register ("TrackResult")]
    partial class TrackResult
    {
        [Outlet]
        AppKit.NSTextField durationLabel { get; set; }

        [Outlet]
        RedditPlayer.Mac.Views.SongsList.SongThumbnailView songThumbnail { get; set; }

        [Outlet]
        AppKit.NSTextField titleLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (durationLabel != null) {
                durationLabel.Dispose ();
                durationLabel = null;
            }

            if (titleLabel != null) {
                titleLabel.Dispose ();
                titleLabel = null;
            }

            if (songThumbnail != null) {
                songThumbnail.Dispose ();
                songThumbnail = null;
            }
        }
    }
}
