using System;
using AppKit;
using RedditPlayer.Mac.Extensions;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
namespace RedditPlayer.Mac.Views.Player
{
    public class SoundControlView : NSView
    {
        public readonly NSButton MuteButton;
        public readonly NSSlider VolumeSlider;
        public readonly NSButton LoudButton;

        bool muted;
        public bool Muted
        {
            get
            {
                return muted;
            }

            set
            {
                muted = value;

                if (muted) {
                    ShowMutedIcon ();
                } else {
                    ShowUnmutedIcon ();
                }
            }
        }

        public SoundControlView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            MuteButton = NSButtonExtensions.ImageButton ("Speaker");
            LoudButton = NSButtonExtensions.ImageButton ("SpeakerLoud");

            VolumeSlider = new NSSlider ();
            VolumeSlider.TranslatesAutoresizingMaskIntoConstraints = false;

            AddSubview (MuteButton);
            AddSubview (VolumeSlider);
            AddSubview (LoudButton);

            BuildConstraints ();
        }

        void BuildConstraints ()
        {
            AddConstraint (PinCenterY (MuteButton));
            AddConstraint (FixedHeight (MuteButton, 30));
            AddConstraint (WidthEqualToHeight (MuteButton));

            AddConstraint (FixedHeight (LoudButton, 30));
            AddConstraint (WidthEqualToHeight (LoudButton));

            AddConstraint (MaximumWidth (VolumeSlider, 120));
            AddConstraint (MinimumWidth (VolumeSlider, 80));

            AddConstraint (PinLeft (MuteButton));
            AddConstraints (StackHorizontal (0, NSLayoutFormatOptions.AlignAllCenterY, MuteButton, VolumeSlider, LoudButton));
            AddConstraint (PinRight (LoudButton, this, -8));
        }

        void ShowMutedIcon ()
        {
            MuteButton.Image = NSImage.ImageNamed ("SpeakerMuted");
        }

        void ShowUnmutedIcon ()
        {
            MuteButton.Image = NSImage.ImageNamed ("Speaker");
        }
    }
}

