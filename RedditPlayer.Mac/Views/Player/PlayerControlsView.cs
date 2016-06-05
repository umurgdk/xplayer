using System;
using AppKit;
using RedditPlayer.Mac.Extensions;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
namespace RedditPlayer.Mac.Views.Player
{
    public class PlayerControlsView : NSView
    {
        public readonly NSButton PlayButton;
        public readonly NSButton PauseButton;
        public readonly NSButton RewindButton;
        public readonly NSButton ForwardButton;

        public PlayerControlsView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            RewindButton = NSButtonExtensions.ImageButton ("Rewind");

            PlayButton = NSButtonExtensions.ImageButton ("Play");
            PlayButton.Activated += (s, e) => ShowPauseButton ();

            PauseButton = NSButtonExtensions.ImageButton ("Pause");
            PauseButton.Activated += (s, e) => ShowPlayButton ();

            ForwardButton = NSButtonExtensions.ImageButton ("Forward");

            AddSubview (RewindButton);
            AddSubview (PlayButton);
            AddSubview (ForwardButton);

            BuildConstraints ();
        }

        public void ShowPauseButton ()
        {
            PlayButton.RemoveFromSuperview ();
            AddSubview (PauseButton);
            BuildConstraints ();
        }

        public void ShowPlayButton ()
        {
            PauseButton.RemoveFromSuperview ();
            AddSubview (PlayButton);
            BuildConstraints ();
        }

        public override void ViewDidMoveToWindow ()
        {
            base.ViewDidMoveToWindow ();
        }

        void BuildConstraints ()
        {
            //RemoveConstraints (Constraints);

            // Rewind button layout
            AddConstraint (FixedHeight (RewindButton, 30));
            AddConstraint (WidthEqualToHeight (RewindButton));
            AddConstraint (PinCenterY (RewindButton));
            AddConstraint (PinLeft (RewindButton));

            // Play button layout
            if (PlayButton.Superview != null) {
                AddConstraint (FixedHeight (PlayButton, 30));
                AddConstraint (WidthEqualToHeight (PlayButton));
                AddConstraint (PinCenterY (PlayButton));

                AddConstraints (StackHorizontal (2, RewindButton, PlayButton, ForwardButton));
            }

            // Pause button layout
            if (PauseButton.Superview != null) {
                AddConstraint (FixedHeight (PauseButton, 30));
                AddConstraint (WidthEqualToHeight (PauseButton));
                AddConstraint (PinCenterY (PauseButton));

                AddConstraints (StackHorizontal (2, RewindButton, PauseButton, ForwardButton));
            }

            // Forward button layout
            AddConstraint (FixedHeight (ForwardButton, 30));
            AddConstraint (WidthEqualToHeight (ForwardButton));
            AddConstraint (PinCenterY (ForwardButton));
            AddConstraint (PinRight (ForwardButton));
        }
    }
}

