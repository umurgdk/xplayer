using System;
using RedditPlayer.Services;
using RedditPlayer.ViewModels;
using AppKit;
using RedditPlayer.Mac.Views.Detail;
using ReactiveUI;
using RedditPlayer.Mac.Views.Playlists;
using Foundation;
using RedditPlayer.Mac.Views.SearchBar;
using RedditPlayer.Mac.Views.Player;
using RedditPlayer.Mac.Views.SearchResults;
using RedditPlayer.Mac.Views.ArtistDetail;
using System.Collections.Generic;

namespace RedditPlayer.Mac.Views
{
	struct RouteItem
	{
		public string Identifier;
		public NSView View;
	}

	public class Navigator : INavigator
	{
		PlayerWindowController windowController;
		DetailViewController detailViewController;
		MainViewController mainViewController;
		PlaylistsViewController playlistViewController;
		SearchResultsXibController searchResultsViewController;

		ApplicationViewModel appModel;

		BreadcrumbView breadcrumbView;
		Stack<RouteItem> history;

		public Navigator (ApplicationViewModel appModel, PlayerWindowController windowController, MainViewController mainViewController)
		{
			this.appModel = appModel;
			this.windowController = windowController;
			this.mainViewController = mainViewController;

			history = new Stack<RouteItem> ();
		}

		public void PresentWelcomeScreen ()
		{
			if (detailViewController == null) {
				var searchBarViewController = new SearchBarViewController (appModel.Search);
				var playerViewController = new PlayerViewController (appModel.Player);

				detailViewController = new DetailViewController (appModel, searchBarViewController, playerViewController);
				breadcrumbView = detailViewController.View.BreadcrumbView;
				breadcrumbView.NavigationRequested += NavigateTo;
			}

			PushView ("welcomeScreen", "Welcome Screen", new WelcomeView ());
		}

		public void PresentSearchResults ()
		{
			if (searchResultsViewController == null) {
				searchResultsViewController = new SearchResultsXibController (appModel);
			}

			ResetHistory ();
			PushView ("searchResults", "Search Results", searchResultsViewController.View);
		}

		public void PresentArtist (ArtistDetailViewModel artistViewModel)
		{
			var viewController = new ArtistDetailViewController (artistViewModel, appModel.Player);
			PushView ("artist", artistViewModel.Artist.Name, viewController.View);
		}

		public void PresentPlaylist ()
		{
			throw new NotImplementedException ();
		}

		public void ShowWindow (object sender)
		{
			windowController.ShowWindow (NSObject.FromObject (sender));
		}

		void PushView (string identifier, string title, NSView view)
		{
			history.Push (new RouteItem {
				Identifier = identifier,
				View = view
			});

			breadcrumbView.AddItem (identifier, title);

			detailViewController.SetContentView (view);
			windowController.PresentView (detailViewController.View);

			if (history.Count > 1) {
				detailViewController.ShowBreadcrumb ();
			}
		}

		void PopView ()
		{
			if (history.Count < 2)
				return;

			var item = history.Pop ();
			breadcrumbView.RemoveItem (item.Identifier);

			var previousItem = history.Peek ();
			detailViewController.SetContentView (previousItem.View);

			if (history.Count <= 1) {
				detailViewController.HideBreadcrumb ();
			}
		}

		void ResetHistory ()
		{
			foreach (var route in history) {
				breadcrumbView.RemoveItem (route.Identifier);
			}

			history.Clear ();
			detailViewController.HideBreadcrumb ();
		}

		void NavigateTo (string identifier)
		{
			RouteItem route;

			while (true) {
				route = history.Peek ();

				if (route.Identifier != identifier) {
					history.Pop ();
					breadcrumbView.RemoveItem (route.Identifier);
				} else {
					break;
				}
			}

			detailViewController.SetContentView (route.View);

			if (history.Count < 2) {
				detailViewController.HideBreadcrumb ();
			}
		}
	}
}

