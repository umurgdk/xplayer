using System;
using AppKit;

using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views
{
    public class WelcomeView : NSView
    {
        NSImageView redditIcon;
        NSTextField applicationName;
        NSTextField helperText;

        public WelcomeView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            redditIcon = new NSImageView ();
            redditIcon.TranslatesAutoresizingMaskIntoConstraints = false;
            redditIcon.Image = NSImage.ImageNamed ("RedditIconBig");

            applicationName = NSLabel.CreateWithFont ("SF UI Display Medium", 23);
            applicationName.TextColor = NSColor.FromRgb (34, 34, 34);
            applicationName.StringValue = "Reddit Player";

            helperText = NSLabel.CreateWithFont ("SF UI Display Regular", 14);
            helperText.TextColor = NSColor.FromRgb (170, 170, 170);
            helperText.StringValue = "Welcome to reddit player. You can start listening by selecting one of the featured genres from the sidebar or you can use search bar to find your favorite genre subreddit.";
            helperText.Alignment = NSTextAlignment.Center;
            //helperText.UsesSingleLineMode = false;
            helperText.LineBreakMode = NSLineBreakMode.ByWordWrapping;

            AddSubview (redditIcon);
            AddSubview (applicationName);
            AddSubview (helperText);

            BuildDefaultContstraints ();
        }

        void BuildDefaultContstraints ()
        {
            AddConstraint (PinCenterX (redditIcon));
            AddConstraint (PinCenterY (redditIcon, this, -80));
            AddConstraints (StackVertical (14, NSLayoutFormatOptions.AlignAllCenterX, redditIcon, applicationName, helperText));
            AddConstraint (MaximumWidth (helperText, 380));

        }
    }
}

