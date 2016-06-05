using System;
using AppKit;
using RedditPlayer.Mac.Extensions;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views.SongsList
{
    public class SongView : NSView
    {
        public readonly SongThumbnailView Thumbnail;
        public readonly NSImageView MediaProviderIcon;
        public readonly NSTextField SongTitle;
        public readonly NSTextField Duration;
        public readonly NSImageView PlayingIndicator;

        readonly NSColor DefaultTitleColor = NSColor.FromRgb (51, 51, 51);
        readonly NSColor DefaultDurationColor = NSColor.FromRgb (126, 126, 126);

        readonly NSColor SelectedTitleColor = NSColor.White;
        readonly NSColor SelectedDurationColor = NSColor.White;

        public SongView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            WantsLayer = true;

            Thumbnail = new SongThumbnailView (3.0f);
            Thumbnail.TranslatesAutoresizingMaskIntoConstraints = false;

            SongTitle = NSLabel.CreateWithFont ("SF UI Display Regular", 14);
            SongTitle.TextColor = DefaultTitleColor;

            Duration = NSLabel.CreateWithFont ("SF UI Display Regular", 12);
            Duration.TextColor = DefaultDurationColor;
            Duration.Alignment = NSTextAlignment.Right;
            Duration.StringValue = "0:00";

            AddSubview (Thumbnail);
            AddSubview (SongTitle);
            AddSubview (Duration);

            BuildConstraints ();
        }

        void BuildConstraints ()
        {
            // Thumbnail
            AddConstraint (FixedWidth (Thumbnail, 40));
            AddConstraint (FixedHeight (Thumbnail, 30));
            AddConstraint (PinCenterY (Thumbnail, this, -1));
            AddConstraint (PinLeft (Thumbnail, this, 20));

            AddConstraint (FixedWidth (Duration, 50));
            AddConstraint (PinRight (Duration, this, -20));

            AddConstraints (StackHorizontal (18, NSLayoutFormatOptions.AlignAllCenterY, Thumbnail, SongTitle, Duration));
        }

        public void DidSelectionChange (bool selected)
        {
            if (selected) {
                SongTitle.TextColor = SelectedTitleColor;
                Duration.TextColor = SelectedDurationColor;
            } else {
                SongTitle.TextColor = DefaultTitleColor;
                Duration.TextColor = DefaultDurationColor;
            }

            NeedsDisplay = true;
        }
    }
}

