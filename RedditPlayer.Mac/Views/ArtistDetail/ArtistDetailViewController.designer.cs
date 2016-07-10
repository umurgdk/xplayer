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
	[Register ("ArtistDetailViewController")]
	partial class ArtistDetailViewController
	{
		[Outlet]
		RedditPlayer.Mac.Views.ViewPlaceholder albumListViewPlaceholder { get; set; }

		[Outlet]
		RedditPlayer.Mac.Views.ViewPlaceholder headerViewPlaceholder { get; set; }

		[Outlet]
		RedditPlayer.Mac.Views.ViewPlaceholder playlistViewPlaceholder { get; set; }

		[Outlet]
		RedditPlayer.Mac.Views.ViewPlaceholder songListViewPlaceholder { get; set; }

		[Outlet]
		AppKit.NSTabView tabView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (albumListViewPlaceholder != null) {
				albumListViewPlaceholder.Dispose ();
				albumListViewPlaceholder = null;
			}

			if (headerViewPlaceholder != null) {
				headerViewPlaceholder.Dispose ();
				headerViewPlaceholder = null;
			}

			if (songListViewPlaceholder != null) {
				songListViewPlaceholder.Dispose ();
				songListViewPlaceholder = null;
			}

			if (playlistViewPlaceholder != null) {
				playlistViewPlaceholder.Dispose ();
				playlistViewPlaceholder = null;
			}

			if (tabView != null) {
				tabView.Dispose ();
				tabView = null;
			}
		}
	}
}
