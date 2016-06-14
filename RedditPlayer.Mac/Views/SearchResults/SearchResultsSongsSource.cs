using System;
using AppKit;
using System.Collections.Generic;
using RedditPlayer.Domain.Media;
using Foundation;
using RedditPlayer.Mac.Extensions;
using CoreGraphics;
namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultsSongsSource : NSTableViewDataSource, INSTableViewDelegate
    {
        IList<Track> tracks;

        public SearchResultsSongsSource (IList<Track> tracks)
        {
            this.tracks = tracks;
        }

        public override nint GetRowCount (NSTableView tableView)
        {
            return tracks.Count;
        }

        [Export ("tableView:viewForTableColumn:row:")]
        public NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var track = tracks [(int)row];

            switch (tableColumn.Identifier) {

            case "ImageColumn": {
                    var view = tableView.MakeView (tableColumn.Identifier, this) as NSImageView;
                    if (view == null) {
                        view = new NSImageView ();
                        view.WantsLayer = true;
                    }

                    view.SetImageAsync (track.CoverUrl);
                    return view;
                }

            case "TitleColumn": {
                    var view = tableView.MakeView (tableColumn.Identifier, this) as NSTextField;
                    if (view == null) {
                        view = NSLabel.Create ();
                        view.WantsLayer = true;
                    }

                    view.StringValue = track.Title;
                    return view;
                }

            case "DurationColumn": {
                    var view = tableView.MakeView (tableColumn.Identifier, this) as NSTextField;
                    if (view == null) {
                        view = NSLabel.Create ();
                        view.WantsLayer = true;
                    }

                    view.StringValue = track.Duration.ToString ("g");
                    return view;
                }

            default:
                return null;
            }
        }
    }
}

