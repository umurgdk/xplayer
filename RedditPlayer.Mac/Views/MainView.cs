using System;
using AppKit;
using RedditPlayer.Mac.Views.Detail;
using RedditPlayer.Mac.Views.Playlists;
using RedditPlayer.Mac.Views.SearchResults;

namespace RedditPlayer.Mac.Views
{
	public class MainView : NSSplitView
	{
		public MainView ()
		{
			// Create splitview
			IsVertical = true;
			DividerStyle = NSSplitViewDividerStyle.Thin;
			TranslatesAutoresizingMaskIntoConstraints = false;
		}

		public NSView Sidebar
		{
			get
			{
				if (ArrangedSubviews.Length > 0)
					return ArrangedSubviews [0];

				return null;
			}

			set
			{
				if (ArrangedSubviews.Length >= 1)
					RemoveArrangedSubview (ArrangedSubviews [0]);

				InsertArrangedSubview (value, 0);
			}
		}

		public NSView Content
		{
			get
			{
				if (ArrangedSubviews.Length > 1)
					return ArrangedSubviews [1];

				return null;
			}

			set
			{
				if (ArrangedSubviews.Length > 1)
					RemoveArrangedSubview (ArrangedSubviews [1]);

				InsertArrangedSubview (value, 1);
			}
		}
	}
}

