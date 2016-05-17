using System;
using AppKit;
using RedditPlayer.Mac.Extensions;
using CoreGraphics;
using System.Diagnostics;
namespace RedditPlayer.Mac.Views.Detail
{
    public class SearchBarView : NSView
    {
        const float DefaultHeight = 38.5f;

        NSImageView searchIcon;
        NSTextField searchField;

        public override CGSize IntrinsicContentSize => new CGSize (100, DefaultHeight);

        public SearchBarView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            searchIcon = new NSImageView ();
            searchIcon.Image = NSImage.ImageNamed ("Search");
            searchIcon.TranslatesAutoresizingMaskIntoConstraints = false;

            searchField = new NSTextField ();
            searchField.DrawsBackground = false;
            searchField.FocusRingType = NSFocusRingType.None;
            searchField.Bordered = false;
            searchField.Activated += (sender, e) => Debug.WriteLine ("Search activated!");
            searchField.TranslatesAutoresizingMaskIntoConstraints = false;

            AddSubview (searchIcon);
            AddSubview (searchField);

            BuildConstraints ();
        }

        void BuildConstraints ()
        {
            AddConstraint (NSLayoutConstraint.Create (this, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1.0f, DefaultHeight));

            AddConstraint (NSLayoutConstraint.Create (searchIcon, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1.0f, 0.0f));
            AddConstraint (NSLayoutConstraint.Create (searchIcon, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this, NSLayoutAttribute.Left, 1.0f, 20.0f));
            AddConstraint (NSLayoutConstraint.Create (searchIcon, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this, NSLayoutAttribute.Height, 1.0f, 0.0f));
            AddConstraint (NSLayoutConstraint.Create (searchIcon, NSLayoutAttribute.Width, NSLayoutRelation.Equal, searchIcon, NSLayoutAttribute.Height, 1.0f, 0.0f));

            AddConstraint (NSLayoutConstraint.Create (searchField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1.0f, 0));
            AddConstraint (NSLayoutConstraint.Create (searchField, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, searchIcon, NSLayoutAttribute.CenterY, 1.0f, 0));
            AddConstraint (NSLayoutConstraint.Create (searchField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, searchIcon, NSLayoutAttribute.Right, 1.0f, 0));
            AddConstraint (NSLayoutConstraint.Create (searchField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this, NSLayoutAttribute.Right, 1.0f, -20));
        }
    }
}

