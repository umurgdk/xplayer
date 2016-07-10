// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RedditPlayer.Mac.Views.Media
{
	[Register ("AlbumListViewController")]
	partial class AlbumListViewController
	{
		[Outlet]
		AppKit.NSScrollView scrollView { get; set; }

		[Outlet]
		AppKit.NSTableView tableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}
		}
	}
}
