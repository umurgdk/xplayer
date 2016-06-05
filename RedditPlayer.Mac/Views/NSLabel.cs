using System;
using AppKit;
namespace RedditPlayer.Mac.Views
{
    public static class NSLabel
    {
        public static NSTextField Create ()
        {
            return Create ("");
        }

        public static NSTextField Create (string defaultText)
        {
            var label = new NSTextField ();
            label.Bezeled = false;
            label.Editable = false;
            label.Selectable = false;
            label.DrawsBackground = false;
            label.TranslatesAutoresizingMaskIntoConstraints = false;
            label.StringValue = defaultText;

            return label;
        }

        public static NSTextField CreateWithFont (string fontFamily, float fontSize)
        {
            var label = new NSTextField ();
            label.Bezeled = false;
            label.Editable = false;
            label.Selectable = false;
            label.DrawsBackground = false;
            label.TranslatesAutoresizingMaskIntoConstraints = false;
            label.Font = NSFont.FromFontName (fontFamily, fontSize);

            return label;
        }
    }
}

