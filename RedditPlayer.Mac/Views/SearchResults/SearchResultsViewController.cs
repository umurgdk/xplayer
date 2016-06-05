using System;
using System.Linq;
using System.Collections.Generic;
using AppKit;
using ReactiveUI;
using RedditPlayer.Mac.DataAdapters;
using RedditPlayer.Domain.Reddit;
using Foundation;
using RedditPlayer.ViewModels;

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

        public SearchResultsViewController (SearchViewModel viewModel)
        {
            View = new SearchResultsView ();

            var results = new List<SearchResult> {
                new SearchResult {
                    Title = "/r/krautrock",
                    TotalSongs = 12200,
                    Tracks = viewModel.Tracks
                }
            };

            var source = new SearchResultsTableSource (results);
            View.ResultsList.Source = source;
            View.ResultsList.Delegate = source;
            View.ResultsList.ReloadData ();
        }
    }
}

