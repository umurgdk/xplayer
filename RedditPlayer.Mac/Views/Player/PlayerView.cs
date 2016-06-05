using System;
using AppKit;
using CoreGraphics;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using RedditPlayer.Mac.Extensions;

namespace RedditPlayer.Mac.Views.Player
{
    public class PlayerView : NSView
    {
        public const float DefaultHeight = 50.0f;

        public readonly NSImageView CoverImage;
        public readonly PlayerControlsView PlayerControls;
        public readonly SoundControlView SoundControl;
        public readonly NSTextField SongTitle;

        public override CGSize IntrinsicContentSize => new CGSize (300, DefaultHeight);

        public PlayerView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            CoverImage = new NSImageView ();
            CoverImage.Image = NSImage.ImageNamed ("EmptyCover");
            CoverImage.TranslatesAutoresizingMaskIntoConstraints = false;

            PlayerControls = new PlayerControlsView ();
            SoundControl = new SoundControlView ();

            SongTitle = NSLabel.CreateWithFont ("SF UI Display Regular", 12);
            SongTitle.StringValue = "Artist - Song Title";
            SongTitle.Alignment = NSTextAlignment.Center;
            SongTitle.TextColor = NSColor.FromRgb (84, 84, 84);

            AddSubview (CoverImage);
            AddSubview (PlayerControls);
            AddSubview (SongTitle);
            AddSubview (SoundControl);

            BuildConstraints ();
        }

        public override void DrawRect (CGRect dirtyRect)
        {
            NSColor.FromRgb (245, 245, 245).Set ();
            NSBezierPath.FillRect (dirtyRect);

            NSColor.FromRgb (219, 219, 219).Set ();
            NSBezierPath.StrokeLine (new CGPoint (dirtyRect.X, Bounds.Height), new CGPoint (dirtyRect.X + dirtyRect.Width, Bounds.Height));
        }

        public override void ViewDidMoveToWindow ()
        {
            base.ViewDidMoveToWindow ();
            //Window.VisualizeConstraints (PlayerControls.Constraints);
        }

        void BuildConstraints ()
        {
            AddConstraint (FixedHeight (this, DefaultHeight));

            // Song title
            AddConstraint (PinCenterY (SongTitle, this, -2));

            // Cover image layout
            AddConstraint (EqualHeights (CoverImage, this, -1));
            AddConstraint (WidthEqualToHeight (CoverImage));
            AddConstraints (PinBottomLeft (CoverImage));

            // Player controls
            AddConstraint (EqualHeights (PlayerControls, this));
            AddConstraint (PinCenterY (PlayerControls));

            // Sound control
            AddConstraint (EqualHeights (SoundControl, this));
            AddConstraint (PinRight (SoundControl));
            AddConstraint (PinCenterY (SoundControl));

            // Horizontal alignment
            AddConstraints (StackHorizontal (8, CoverImage, PlayerControls, SongTitle, SoundControl));
        }
    }
}

