using AppKit;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultsView : NSView
    {
        public readonly RPTableView ResultsList;
        public readonly NSView Container;
        public readonly NSScrollView ScrollView;

        public SearchResultsView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            WantsLayer = true;
            Layer.ZPosition = 0;
            NeedsDisplay = true;

            ScrollView = new NSScrollView ();
            ScrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            ScrollView.WantsLayer = true;
            ScrollView.ContentView = new FlippedClipView ();

            var flipped = new NSView ();
            flipped.TranslatesAutoresizingMaskIntoConstraints = false;
            Container = flipped;

            var songColumn = new NSTableColumn ("SongColumn");
            songColumn.MinWidth = 400;

            ResultsList = new RPTableView ();
            ResultsList.FloatsGroupRows = false;
            ResultsList.HeaderView = null;
            ResultsList.AddColumn (songColumn);
            ResultsList.GridStyleMask = NSTableViewGridStyle.SolidHorizontalLine;
            ResultsList.GridColor = NSColor.FromRgb (235, 235, 235);

            var tracksHeader = CreateSearchResultHeader ("Tracks");

            flipped.AddSubview (tracksHeader);
            flipped.AddSubview (ResultsList);

            ScrollView.DocumentView = flipped;

            AddSubview (ScrollView);

            AddConstraints (FillVertical (ScrollView, false));
            AddConstraints (FillHorizontal (ScrollView, false));

            AddConstraints (FillHorizontal (flipped, false));
            AddConstraint (PinTop (flipped, ScrollView.ContentView));
            AddConstraint (PinBottom (flipped, ScrollView.ContentView, 0, NSLayoutRelation.GreaterThanOrEqual));
            //AddConstraint (PinTop (flipped));

            AddConstraint (MinimumWidth (ScrollView, 400));
            AddConstraint (MinimumHeight (ScrollView, 400));

            AddConstraint (PinTop (tracksHeader, flipped, 30));
            AddConstraints (FillHorizontal (tracksHeader, true));
            AddConstraints (StackVertical (tracksHeader, ResultsList));
            AddConstraints (FillHorizontal (ResultsList, false));
            AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:[list]-(>=70)-|", NSLayoutFormatOptions.None, new object [] { "list", ResultsList }));
        }

        NSTextField CreateSearchResultHeader (string title)
        {
            var label = NSLabel.CreateWithFont ("SF UI Text Light", 20);
            label.StringValue = title;
            label.SetContentHuggingPriorityForOrientation (251, NSLayoutConstraintOrientation.Vertical);

            return label;
        }
    }
}

