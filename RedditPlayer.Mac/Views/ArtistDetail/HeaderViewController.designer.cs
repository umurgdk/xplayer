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
	[Register ("HeaderViewController")]
	partial class HeaderViewController
	{
		[Outlet]
		AppKit.NSTextField artistBioView { get; set; }

		[Outlet]
		AppKit.NSTextField artistNameView { get; set; }

		[Outlet]
		RedditPlayer.Mac.Views.SearchResults.SearchResultsTabGroup tabGroup { get; set; }

		[Outlet]
		RedditPlayer.Mac.Views.SongsList.SongThumbnailView thumbnailView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (artistNameView != null) {
				artistNameView.Dispose ();
				artistNameView = null;
			}

			if (artistBioView != null) {
				artistBioView.Dispose ();
				artistBioView = null;
			}

			if (tabGroup != null) {
				tabGroup.Dispose ();
				tabGroup = null;
			}

			if (thumbnailView != null) {
				thumbnailView.Dispose ();
				thumbnailView = null;
			}
		}
	}
}
