using System;
using AppKit;
using CoreGraphics;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using RedditPlayer.Mac.Extensions;

namespace RedditPlayer.Mac.Views.Player
{
    public class PlayerView : NSView
    {
        public const float DefaultHeight = 60.0f;
        public const float SafeHeight = 50.0f;

        public readonly NSImageView CoverImage;
        public readonly PlayerControlsView PlayerControls;
        public readonly SoundControlView SoundControl;
        public readonly NSTextField SongTitle;
        public readonly PlayerProgress Progress;

        public override CGSize IntrinsicContentSize => new CGSize (300, DefaultHeight);

        public PlayerView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            WantsLayer = true;
            Layer.MasksToBounds = false;

            CoverImage = new NSImageView ();
            CoverImage.Image = NSImage.ImageNamed ("EmptyCover");
            CoverImage.TranslatesAutoresizingMaskIntoConstraints = false;

            PlayerControls = new PlayerControlsView ();
            SoundControl = new SoundControlView ();

            SongTitle = NSLabel.CreateWithFont ("SF UI Display Regular", 12);
            SongTitle.StringValue = "Artist - Song Title";
            SongTitle.Alignment = NSTextAlignment.Center;
            SongTitle.TextColor = NSColor.FromRgb (84, 84, 84);

            Progress = new PlayerProgress ();

            AddSubview (Progress);
            AddSubview (CoverImage);
            AddSubview (PlayerControls);
            AddSubview (SongTitle);
            AddSubview (SoundControl);

            BuildConstraints ();
        }

        public override void DrawRect (CGRect dirtyRect)
        {
            NSColor.FromRgb (245, 245, 245).Set ();
            NSBezierPath.FillRect (new CGRect (0, 0, dirtyRect.Width, 50));
        }

        void BuildConstraints ()
        {
            AddConstraint (FixedHeight (this, DefaultHeight));

            // Song title
            AddConstraint (PinCenterY (SongTitle));

            // Cover image layout
            AddConstraint (FixedHeight (CoverImage, SafeHeight));
            AddConstraint (WidthEqualToHeight (CoverImage));
            AddConstraints (PinBottomLeft (CoverImage));

            // Player controls
            AddConstraint (FixedHeight (PlayerControls, SafeHeight));
            AddConstraint (PinBottom (PlayerControls));

            // Sound control
            AddConstraint (FixedHeight (SoundControl, SafeHeight));
            AddConstraint (PinRight (SoundControl));
            AddConstraint (PinBottom (SoundControl));

            // Horizontal alignment
            AddConstraints (StackHorizontal (8, CoverImage, PlayerControls, SongTitle, SoundControl));

            AddConstraint (PinTop (Progress));
            AddConstraints (FillHorizontal (Progress, false));
            AddConstraint (FixedHeight (Progress, 20.0f));
        }
    }
}

