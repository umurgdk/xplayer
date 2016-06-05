using System;
using AppKit;
namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultsView : NSScrollView
    {
        public readonly RPTableView ResultsList;

        public SearchResultsView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            ResultsList = new RPTableView ();
            ResultsList.FloatsGroupRows = false;
            ResultsList.HeaderView = null;
            ResultsList.AddColumn (new NSTableColumn ("SongColumn"));
            ResultsList.GridStyleMask = NSTableViewGridStyle.SolidHorizontalLine;
            ResultsList.GridColor = NSColor.FromRgb (235, 235, 235);

            DocumentView = ResultsList;
        }
    }
}

