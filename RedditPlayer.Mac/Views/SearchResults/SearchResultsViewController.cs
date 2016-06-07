using AppKit;
using ReactiveUI;
using RedditPlayer.Mac.DataAdapters;
using Foundation;
using RedditPlayer.ViewModels;
using RedditPlayer.Domain.Media;
using System;
using System.Diagnostics;
using Splat;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultsViewController : ReactiveViewController
    {
        new SearchResultsView View
        {
            get
            {
                return (SearchResultsView)base.View;
            }
            set
            {
                base.View = value;
            }
        }

        public SearchResultsViewController (ApplicationViewModel viewModel)
        {
            View = new SearchResultsView ();

            var source = new GenericTableViewSource<Track> (
                View.ResultsList,
                viewModel.Search.Tracks,
                (t, c) => "SearchResultSongView",
                (t, i) => new SearchResultSongView(),
                (track, identifier, view) => {
                    var songView = view as SearchResultSongView;
                    songView.Title.StringValue = track.Title;
                    songView.Duration.StringValue = track.Duration.ToString ("g");
                    songView.Thumbnail.Image = new NSImage (NSUrl.FromString (track.CoverUrl));
                }
            );

            source.DoubleClicked += PlayTrack;

            View.ResultsList.Source = source;
            View.ResultsList.Delegate = source;
            View.ResultsList.ReloadData ();
        }

        void PlayTrack (Track track)
        {
            var appModel = Locator.CurrentMutable.GetService<ApplicationViewModel> ();
            appModel.Player.Play (track);
        }
   }
}

