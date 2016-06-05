using System;
using AppKit;
using Foundation;
using CoreText;
namespace RedditPlayer.Mac.Extensions
{
    public static class NSButtonExtensions
    {
        public static NSButton ImageButton (string imageName)
        {
            var button = new NSButton ();
            button.Bordered = false;
            button.Image = NSImage.ImageNamed (imageName);
            button.ImagePosition = NSCellImagePosition.ImageOnly;
            button.SetButtonType (NSButtonType.MomentaryChange);
            button.TranslatesAutoresizingMaskIntoConstraints = false;

            return button;
        }

        public static NSButton ClearButton (string label)
        {
            var button = new NSButton ();
            button.Bordered = false;
            button.TranslatesAutoresizingMaskIntoConstraints = false;
            button.AttributedTitle = new NSAttributedString (label, new CTStringAttributes (new NSDictionary (NSStringAttributeKey.ForegroundColor, NSColor.FromRgb (43, 169, 255))));
            button.AttributedAlternateTitle = new NSAttributedString (label, new CTStringAttributes (new NSDictionary (NSStringAttributeKey.ForegroundColor, NSColor.FromRgb (13, 139, 225))));
            button.SetButtonType (NSButtonType.MomentaryChange);

            return button;
        }
    }
}

