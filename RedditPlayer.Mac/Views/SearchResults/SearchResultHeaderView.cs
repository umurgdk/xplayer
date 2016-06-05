using System;
using AppKit;
using RedditPlayer.Mac.Extensions;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using Foundation;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultHeaderView : NSView, INSCopying
    {
        public readonly NSImageView IconView;
        public readonly NSTextField TitleLabel;

        NSTextField totalSongsLabel;
        public readonly NSTextField TotalSongs;

        NSTextField lastEntryLabel;
        public readonly NSTextField LastEntry;

        public readonly NSButton AddToLibraryButton;

        public SearchResultHeaderView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            var labelFont = NSFont.FromFontName ("SF UI Display Medium", 11);
            var labelColor = NSColor.FromRgb (162, 162, 162);
            var valueColor = NSColor.FromRgb (90, 90, 90);

            // Header Title
            IconView = new NSImageView ();
            IconView.TranslatesAutoresizingMaskIntoConstraints = false;
            IconView.Image = NSImage.ImageNamed ("Playlist");
            //IconView.SetContentHuggingPriorityForOrientation ((float)NSLayoutPriority.DefaultHigh, NSLayoutConstraintOrientation.Horizontal);

            TitleLabel = NSLabel.CreateWithFont ("SF UI Text Medium", 14);
            TitleLabel.TextColor = NSColor.Black;
            TitleLabel.StringValue = "Playlist";
            TitleLabel.SetContentCompressionResistancePriority (1000, NSLayoutConstraintOrientation.Horizontal);

            // Total Songs
            totalSongsLabel = NSLabel.Create ();
            totalSongsLabel.TextColor = labelColor;
            totalSongsLabel.Font = labelFont;
            totalSongsLabel.StringValue = "TOTAL SONGS:";

            TotalSongs = NSLabel.Create ();
            TotalSongs.Font = labelFont;
            TotalSongs.TextColor = valueColor;
            TotalSongs.StringValue = "0";

            // Last Entry
            lastEntryLabel = NSLabel.Create ();
            lastEntryLabel.Font = labelFont;
            lastEntryLabel.TextColor = labelColor;
            lastEntryLabel.StringValue = "LAST ENTRY:";

            LastEntry = NSLabel.Create ();
            LastEntry.Font = labelFont;
            LastEntry.TextColor = valueColor;
            LastEntry.StringValue = "12/12/2016";

            // Add To Library
            AddToLibraryButton = NSButtonExtensions.ClearButton ("ADD TO LIBRARY");
            AddToLibraryButton.SetContentHuggingPriorityForOrientation (251f, NSLayoutConstraintOrientation.Horizontal);

            AddSubview (IconView);
            AddSubview (TitleLabel);
            AddSubview (totalSongsLabel);
            AddSubview (TotalSongs);
            AddSubview (lastEntryLabel);
            AddSubview (LastEntry);
            AddSubview (AddToLibraryButton);

            AddDefaultConstraints ();
        }

        void AddDefaultConstraints ()
        {
            AddConstraints (PinTopLeft (IconView, this, 20));

            AddConstraint (PinCenterY (TitleLabel, IconView, -2));
            AddConstraints (StackHorizontal (IconView, TitleLabel));

            AddConstraints (StackVertical (IconView, totalSongsLabel));
            AddConstraint (PinLeft (totalSongsLabel, IconView));

            AddConstraints (StackHorizontal (4, NSLayoutFormatOptions.AlignAllFirstBaseline, totalSongsLabel, TotalSongs));
            AddConstraints (StackHorizontal (32, NSLayoutFormatOptions.AlignAllFirstBaseline, TotalSongs, lastEntryLabel));
            AddConstraints (StackHorizontal (4, NSLayoutFormatOptions.AlignAllFirstBaseline, lastEntryLabel, LastEntry));
            AddConstraints (HorizontalSpaceBetween (LastEntry, AddToLibraryButton));

            AddConstraint (PinRight (AddToLibraryButton, this, -20));
            AddConstraint (PinFirstBaseline (LastEntry, AddToLibraryButton));

            AddConstraint (PinBottom (AddToLibraryButton, this, -8));
            AddConstraint (PinBottom (totalSongsLabel, this, -8));
        }

        //static bool didShowWindow = false;

        //public override void ViewDidMoveToWindow ()
        //{
        //    Window.VisualizeConstraints (Constraints);

        //    if (didShowWindow)
        //        return;

        //    didShowWindow = true;
        //    base.ViewDidMoveToWindow ();
        //    Window.VisualizeConstraints (Constraints);

        //    var testWindow = new NSWindow ();
        //    testWindow.StyleMask = NSWindowStyle.Resizable;
        //    NSView view = (NSView)Copy ();

        //    testWindow.ContentView.AddSubview (view);
        //    testWindow.ContentView.AddConstraints (FillVertical (view, false));
        //    testWindow.ContentView.AddConstraints (FillHorizontal (view, false));

        //    testWindow.MakeKeyAndOrderFront (this);
        //}

        public NSObject Copy (NSZone zone)
        {
            var copy = new SearchResultHeaderView ();
            copy.TotalSongs.StringValue = TotalSongs.StringValue;
            copy.LastEntry.StringValue = LastEntry.StringValue;
            copy.TitleLabel.StringValue = TitleLabel.StringValue;

            return copy;
        }
    }
}

