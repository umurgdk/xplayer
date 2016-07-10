// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RedditPlayer.Mac.Views.ArtistDetail
{
    partial class ArtistDetailHeaderViewController
    {
        [Outlet]
        AppKit.NSTextField artistBioView { get; set; }

        [Outlet]
        AppKit.NSTextField artistNameView { get; set; }

        [Outlet]
        RedditPlayer.Mac.Views.SongsList.SongThumbnailView backgroundImage { get; set; }

        [Outlet]
        RedditPlayer.Mac.Views.SearchResults.SearchResultsTabGroup tabGroup { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (artistBioView != null) {
                artistBioView.Dispose ();
                artistBioView = null;
            }

            if (artistNameView != null) {
                artistNameView.Dispose ();
                artistNameView = null;
            }

            if (backgroundImage != null) {
                backgroundImage.Dispose ();
                backgroundImage = null;
            }

            if (tabGroup != null) {
                tabGroup.Dispose ();
                tabGroup = null;
            }
        }
    }
}
