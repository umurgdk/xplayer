using AppKit;
using ReactiveUI;
using RedditPlayer.Mac.DataAdapters;
using Foundation;
using RedditPlayer.ViewModels;
using RedditPlayer.Domain.Media;
using System;
using System.Diagnostics;
using Splat;
using RedditPlayer.Mac.Extensions;
using RedditPlayer.Mac.Views.SongsList;

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
                (t, c) => {
                    return c.Identifier;
                },
                CreateColumnView,
                UpdateColumnView
            );

            viewModel.Search.Tracks.Changed.Subscribe (_ => {
                View.ResultsList.ReloadData ();
            });

            source.DoubleClicked += PlayTrack;

            //    View.ResultsList.Source = source;
            //    View.ResultsList.Delegate = source;
            //    View.ResultsList.ReloadData ();
        }

        NSView CreateColumnView (Track track, string identifier)
        {
            switch (identifier) {
            case "CoverColumn":
                return new SongThumbnailView ();
            case "TitleColumn":
                return NSLabel.CreateWithFont ("SF UI Display Regular", 12);
            case "DurationColumn":
                return NSLabel.CreateWithFont ("SF UI Display Regular", 12);
            default:
                throw new Exception (string.Format ("Undefined column for search results table view: {0}", identifier));
            }
        }

        void UpdateColumnView (Track track, string identifier, NSView view)
        {
            switch (identifier) {
            case "CoverColumn":
                var thumbnail = view as SongThumbnailView;
                thumbnail.SetImageAsync (track.CoverUrl);
                break;

            case "TitleColumn":
                var titleView = view as NSTextField;
                titleView.StringValue = track.Title;
                break;

            case "DurationColumn":
                var durationView = view as NSTextField;
                durationView.StringValue = track.Duration.ToString ("g");
                break;
            }
        }

        void PlayTrack (Track track)
        {
            var appModel = Locator.CurrentMutable.GetService<ApplicationViewModel> ();
            appModel.Player.Play (track);
        }
    }
}

