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
    [Register ("ArtistItemView")]
    partial class ArtistItemView
    {
        [Outlet]
        RedditPlayer.Mac.Views.SongsList.SongThumbnailView thumbnailView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (thumbnailView != null) {
                thumbnailView.Dispose ();
                thumbnailView = null;
            }
        }
    }
}