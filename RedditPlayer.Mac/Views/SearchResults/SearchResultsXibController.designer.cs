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
	[Register ("SearchResultsXibController")]
	partial class SearchResultsXibController
	{
		[Outlet]
		RedditPlayer.Mac.Views.SearchResults.SearchResultsTabGroup tabGroup { get; set; }

		[Outlet]
		AppKit.NSTableView tableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}

			if (tabGroup != null) {
				tabGroup.Dispose ();
				tabGroup = null;
			}
		}
	}
}
