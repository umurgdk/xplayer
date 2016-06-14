using System;
using AppKit;
using RedditPlayer.Mac.Views.SongsList;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using RedditPlayer.Mac.DataAdapters;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public class SearchResultSongView : NSView, ISelectableView
    {
        public readonly SongThumbnailView Thumbnail;
        public readonly NSTextField Title;
        public readonly NSTextField Duration;

        NSFont defaultFont = NSFont.FromFontName ("SF UI Display Regular", 12);
        NSColor defaultColor = NSColor.FromRgb (89, 89, 89);

        public SearchResultSongView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            Identifier = "SearchResultSongView";
            WantsLayer = true;

            Thumbnail = new SongThumbnailView ();
            Thumbnail.Identifier = "Thumbnail";
            Thumbnail.CornerRadius = 2;

            Title = NSLabel.Create ("Song Title");
            Title.Font = defaultFont;
            Title.TextColor = defaultColor;
            Title.Identifier = "SongTitle";
            //Title.AllowsDefaultTighteningForTruncation = true;
            Title.LineBreakMode = NSLineBreakMode.TruncatingTail;

            Duration = NSLabel.Create ("00:00");
            Duration.Font = defaultFont;
            Duration.TextColor = defaultColor;
            Duration.Identifier = "Duration";
            Duration.SetContentHuggingPriorityForOrientation (251, NSLayoutConstraintOrientation.Horizontal);
            Duration.SetContentCompressionResistancePriority (1000, NSLayoutConstraintOrientation.Horizontal);


            AddSubview (Thumbnail);
            AddSubview (Title);
            AddSubview (Duration);

            BuildDefaultConstraints ();
        }

        void BuildDefaultConstraints ()
        {
            // |-[Thumbnail]-(13)-[Title]-(>=0)-[Duration]-|

            AddConstraint (PinLeft (Thumbnail, this, 20));
            AddConstraints (FillVertical (Thumbnail, 4));
            AddConstraints (StackHorizontal (13, Thumbnail, Title, Duration));
            AddConstraint (PinCenterY (Title, Thumbnail, -2.0f));
            AddConstraint (PinFirstBaseline (Title, Duration));
            AddConstraint (PinRight (Duration, this, -20));

            AddConstraint (MinimumWidth (Title, 100));

            AddConstraint (FixedWidth (Thumbnail, 27));
            AddConstraint (FixedHeight (Thumbnail, 20));
        }

        public void DidSelectionChanged (bool isSelected)
        {
            //Layer.BackgroundColor =
            if (isSelected) {
                Title.TextColor = NSColor.Black;
                Duration.TextColor = NSColor.Black;
            } else {
                Title.TextColor = defaultColor;
                Duration.TextColor = defaultColor;
            }

            NeedsDisplay = true;
        }
    }
}

