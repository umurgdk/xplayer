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
		AppKit.NSTextField ArtistBio { get; set; }

		[Outlet]
		RedditPlayer.Mac.Views.SongsList.SongThumbnailView ArtistImageView { get; set; }

		[Outlet]
		AppKit.NSTextField ArtistName { get; set; }

		[Outlet]
		AppKit.NSStackView ContentStack { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ArtistBio != null) {
				ArtistBio.Dispose ();
				ArtistBio = null;
			}

			if (ArtistImageView != null) {
				ArtistImageView.Dispose ();
				ArtistImageView = null;
			}

			if (ArtistName != null) {
				ArtistName.Dispose ();
				ArtistName = null;
			}

			if (ContentStack != null) {
				ContentStack.Dispose ();
				ContentStack = null;
			}
		}
	}
}
