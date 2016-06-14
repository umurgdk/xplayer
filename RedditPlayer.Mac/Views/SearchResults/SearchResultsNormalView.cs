using AppKit;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultsNormalView : NSView
    {
        public readonly NSTableView TableView;
        public readonly NSView Container;
        public readonly NSScrollView ScrollView;

        public SearchResultsNormalView ()
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

            var imageColumn = new NSTableColumn ("ImageColumn");
            var titleColumn = new NSTableColumn ("TitleColumn");
            var durationColumn = new NSTableColumn ("DurationColumn");

            TableView = new NSTableView ();
            //TableView.TranslatesAutoresizingMaskIntoConstraints = false;
            //TableView.FloatsGroupRows = false;
            //TableView.HeaderView = null;
            TableView.AddColumn (imageColumn);
            TableView.AddColumn (titleColumn);
            TableView.AddColumn (durationColumn);
            //TableView.GridStyleMask = NSTableViewGridStyle.SolidHorizontalLine;
            //TableView.GridColor = NSColor.FromRgb (235, 235, 235);
            //TableView.WantsLayer = true;

            var tracksHeader = CreateSearchResultHeader ("Tracks");

            flipped.AddSubview (tracksHeader);
            flipped.AddSubview (TableView);

            ScrollView.DocumentView = TableView;

            AddSubview (ScrollView);

            AddConstraints (FillVertical (ScrollView, false));
            AddConstraints (FillHorizontal (ScrollView, false));

            //AddConstraints (FillHorizontal (flipped, false));
            //AddConstraint (PinTop (flipped, ScrollView.ContentView));
            //AddConstraint (PinBottom (flipped, ScrollView.ContentView, 0, NSLayoutRelation.GreaterThanOrEqual));
            //AddConstraint (PinTop (flipped));

            AddConstraint (MinimumWidth (ScrollView, 400));
            AddConstraint (MinimumHeight (ScrollView, 400));

            //AddConstraint (PinTop (tracksHeader, flipped, 30));
            //AddConstraints (FillHorizontal (tracksHeader, true));
            //AddConstraints (StackVertical (tracksHeader, TableView));
            //AddConstraints (FillHorizontal (TableView, false));
            //AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:[list]-(>=70)-|", NSLayoutFormatOptions.None, new object [] { "list", TableView }));
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

