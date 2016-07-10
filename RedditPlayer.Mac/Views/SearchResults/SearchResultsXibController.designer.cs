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
		AppKit.NSCollectionView artistsCollectionView { get; set; }

		[Outlet]
		RedditPlayer.Mac.Views.SearchResults.SearchResultsTabGroup tabGroup { get; set; }

		[Outlet]
		AppKit.NSTabView tabView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (artistsCollectionView != null) {
				artistsCollectionView.Dispose ();
				artistsCollectionView = null;
			}

			if (tabGroup != null) {
				tabGroup.Dispose ();
				tabGroup = null;
			}

			if (tabView != null) {
				tabView.Dispose ();
				tabView = null;
			}
		}
	}
}
