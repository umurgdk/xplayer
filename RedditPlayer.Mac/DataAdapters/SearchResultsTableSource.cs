using System;
using System.Linq;
using System.Collections.Generic;
using AppKit;
using Foundation;
using RedditPlayer.Mac.Views.SearchResults;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Mac.DataAdapters
{
    class SearchResult
    {
        public string Title { get; set; } = "";
        public int TotalSongs { get; set; } = 0;
        public DateTime LastEntry { get; set; } = DateTime.Now;
        public IList<Track> Tracks { get; set; } = new List<Track> ();
    }

    class SearchResultsTableSource : NSTableViewSource, INSTableViewDelegate
    {
        nfloat headerHeight;
        nfloat rowHeight;

        List<SearchResult> searchResults;
        Dictionary<int, bool> isHeaderCache;

        public SearchResultsTableSource (List<SearchResult> searchResults)
        {
            this.searchResults = searchResults;

            isHeaderCache = new Dictionary<int, bool> ();

            headerHeight = new SearchResultHeaderView ().FittingSize.Height;
            rowHeight = new SearchResultSongView ().FittingSize.Height;
        }

        public override nint GetRowCount (NSTableView tableView)
        {
            var resultsCount = searchResults.Count;
            return resultsCount + searchResults.Select (r => r.Tracks.Count).Sum ();
        }

        bool IsHeader (int row)
        {
            if (isHeaderCache.ContainsKey (row)) {
                return isHeaderCache [row];
            }

            var totalRows = (int)GetRowCount (null);
            var resultIndex = 0;
            for (var i = 0; i < totalRows; i = i + searchResults [resultIndex - 1].Tracks.Count + 1) {
                if (row == i) {
                    isHeaderCache [row] = true;
                    return true;
                }

                resultIndex += 1;
            }

            isHeaderCache [row] = false;

            return false;
        }

        SearchResult GetSearchResultByRow (int row)
        {
            var resultIndex = 0;
            var totalRows = (int)GetRowCount (null);
            for (var i = 0; i < totalRows; i = i + searchResults [i].Tracks.Count + 1) {
                if (row == i) {
                    return searchResults [resultIndex];
                }

                resultIndex += 1;
            }

            return null;
        }

        Track GetTrackByRowId (int row)
        {
            var searchIndex = 1;
            for (var i = 0; i < searchResults.Count; i++) {
                for (var j = 0; j < searchResults [i].Tracks.Count; j++) {
                    if (searchIndex == row) {
                        return searchResults [i].Tracks [j];
                    }

                    searchIndex += 1;
                }

                searchIndex += 1;
            }

            return null;
        }

        public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var isHeader = IsHeader ((int)row);

            if (isHeader) {
                var result = GetSearchResultByRow ((int)row);
                var view = tableView.MakeView ("HeaderView", this) as SearchResultHeaderView;

                if (view == null) {
                    view = new SearchResultHeaderView ();
                }

                view.TitleLabel.StringValue = result.Title;
                view.TotalSongs.StringValue = result.TotalSongs.ToString ();
                view.LastEntry.StringValue = result.LastEntry.ToString ();

                return view;
            } else {
                var track = GetTrackByRowId ((int)row);
                var view = tableView.MakeView ("SongView", this) as SearchResultSongView;

                if (view == null) {
                    view = new SearchResultSongView ();
                }

                view.Title.StringValue = track.Title;
                view.Duration.StringValue = track.Duration.ToString ();

                var image = new NSImage (NSUrl.FromString (track.CoverUrl));
                view.Thumbnail.Image = image ?? NSImage.ImageNamed ("EmptyCover");

                return view;
            }
        }

        public override bool ShouldSelectRow (NSTableView tableView, nint row)
        {
            return !IsHeader ((int)row);
        }

        public override NSTableRowView GetRowView (NSTableView tableView, nint row)
        {
            var rowView = new GrayRowView ();

            if (!IsHeader ((int)row)) {
                rowView.BorderColor = NSColor.FromRgb (232, 0, 0);
                rowView.BorderLocations = BorderLocation.Top;
            }

            return rowView;
        }

        public override bool IsGroupRow (NSTableView tableView, nint row)
        {
            return IsHeader ((int)row);
        }

        public override nfloat GetRowHeight (NSTableView tableView, nint row)
        {
            return IsHeader ((int)row) ? headerHeight : rowHeight;
        }
    }
}
