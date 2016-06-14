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

namespace RedditPlayer.Mac.Views
{
    public class Navigator : INavigator
    {
        PlayerWindowController windowController;
        DetailViewController detailViewController;
        MainViewController mainViewController;
        PlaylistsViewController playlistViewController;
        SearchResultsNormalViewController searchResultsViewController;

        ApplicationViewModel appModel;

        public Navigator (ApplicationViewModel appModel, PlayerWindowController windowController)
        {
            this.appModel = appModel;
            this.windowController = windowController;
        }

        public void PresentWelcomeScreen ()
        {
            if (detailViewController == null) {
                var searchBarViewController = new SearchBarViewController (appModel.Search);
                var playerViewController = new PlayerViewController (appModel.Player);

                detailViewController = new DetailViewController (appModel, searchBarViewController, playerViewController);
            }

            detailViewController.SetContentView (new WelcomeView ());
            windowController.PresentView (detailViewController.View);
        }

        public void PresentSearchResults ()
        {
            if (searchResultsViewController == null) {
                searchResultsViewController = new SearchResultsNormalViewController (appModel);
            }

            var controller = new SearchResultsXibController (appModel);
            detailViewController.SetContentView (controller.View);

            //var nibController = new DetailNibController ();


            windowController.PresentView (detailViewController.View);
        }

        public void PresentPlaylist ()
        {
            throw new NotImplementedException ();
        }

        public void ShowWindow (object sender)
        {
            windowController.ShowWindow (NSObject.FromObject (sender));
        }
    }
}

