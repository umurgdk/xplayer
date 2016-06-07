using System;
using AppKit;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultsView : NSView
    {
        public readonly RPTableView ResultsList;

        public SearchResultsView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            var scrollView = new NSScrollView ();
            scrollView.TranslatesAutoresizingMaskIntoConstraints = false;

            var flipped = new FlippedView ();

            var stack = new NSStackView ();
			stack.TranslatesAutoresizingMaskIntoConstraints = false;
            stack.Orientation = NSUserInterfaceLayoutOrientation.Vertical;
            stack.Spacing = 15;
            stack.Distribution = NSStackViewDistribution.Fill;
            stack.Alignment = NSLayoutAttribute.Leading;
            stack.SetHuggingPriority ((float)NSLayoutPriority.WindowSizeStayPut, NSLayoutConstraintOrientation.Horizontal);

            var songColumn = new NSTableColumn ("SongColumn");
            songColumn.MinWidth = 400;

            ResultsList = new RPTableView ();
            ResultsList.FloatsGroupRows = false;
            ResultsList.HeaderView = null;
            ResultsList.AddColumn (songColumn);
            ResultsList.GridStyleMask = NSTableViewGridStyle.SolidHorizontalLine;
            ResultsList.GridColor = NSColor.FromRgb (235, 235, 235);

            var tracksHeader = CreateSearchResultHeader ("Tracks");

            stack.AddArrangedSubview (tracksHeader);
            stack.AddArrangedSubview (ResultsList);

            flipped.AddSubview (stack);

            scrollView.DocumentView = flipped;

            AddSubview (scrollView);

            AddConstraints (FillVertical (scrollView, false));
            AddConstraints (FillHorizontal (scrollView, false));

            AddConstraints (FillHorizontal (flipped, false));
            AddConstraints (FillVertical (flipped, false));

            AddConstraints (FillHorizontal (stack, false));
            AddConstraint (PinTop (stack, flipped, 20));

            AddConstraints (FillHorizontal (tracksHeader, 20));

            AddConstraint (MinimumWidth (scrollView, 400));
            AddConstraint (MinimumHeight (scrollView, 400));
        }

        NSTextField CreateSearchResultHeader (string title)
        {
            var label = NSLabel.CreateWithFont ("SF UI Text Light", 20);
            label.StringValue = title;

            return label;
        }
    }
}

