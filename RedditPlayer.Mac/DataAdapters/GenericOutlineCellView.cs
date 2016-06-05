using System;
using AppKit;
using RedditPlayer.Mac.Views;
using RedditPlayer.Mac.Extensions;
using System.Collections.Generic;
using CoreImage;

namespace RedditPlayer.Mac.DataAdapters
{
    public class GenericOutlineCellView : NSView, ISelectableView
    {
        public const string KIdentifier = "GenericOutlineCellView";
        public NSImageView ImageView { get; protected set; }
        public NSTextField TextField { get; protected set; }
        public NSButton CollapseButton { get; protected set; }

        bool selected = false;
        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
                DidSelectionChanged (value);
            }
        }

        bool cachedIsCategory;
        GenericOutlineItemWrapper item;

        public GenericOutlineCellView ()
        {
            ImageView = new NSImageView ();
            ImageView.TranslatesAutoresizingMaskIntoConstraints = false;

            TextField = NSLabel.CreateWithFont ("SF UI Display Bold", 13);
            TextField.DrawsBackground = false;

            CollapseButton = new NSButton ();
            CollapseButton.Bordered = false;
            CollapseButton.BezelStyle = NSBezelStyle.Disclosure;

            AddSubview (ImageView);
            AddSubview (TextField);

            RebuildConstraints ();
        }

        public void SetItem (GenericOutlineItemWrapper item)
        {
            this.item = item;

            if (!item.IsCategory) {
                ImageView.Image = NSImage.ImageNamed (item.Icon).Tint (NSColor.FromRgb (45, 45, 45));
                TextField.Font = NSFont.FromFontName ("SF UI Display Regular", 14);
                TextField.TextColor = NSColor.FromRgb (45, 45, 45);
                AddSubview (ImageView);
            } else {
                ImageView.RemoveFromSuperview ();
                TextField.Font = NSFont.FromFontName ("SF UI Display Medium", 13);
                TextField.TextColor = NSColor.FromRgb (135, 135, 135);
            }

            TextField.StringValue = item.Text;

            if (cachedIsCategory != item.IsCategory) {
                cachedIsCategory = item.IsCategory;
                RebuildConstraints ();
            }
        }

        void RebuildConstraints ()
        {
            RemoveConstraints (Constraints);

            if (cachedIsCategory) {
                AddConstraints (NSLayoutExtensions.FillHorizontal (TextField, false));
                AddConstraints (NSLayoutExtensions.FillVertical (TextField, false));
            } else {
                AddConstraints (NSLayoutExtensions.FillVertical (ImageView, false));
                AddConstraint (NSLayoutConstraint.Create (ImageView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, ImageView, NSLayoutAttribute.Height, 1.0f, 0.0f));
                AddConstraints (NSLayoutExtensions.StackOld (StackOrientation.Horizontal, true, false, true, ImageView, TextField));
                AddConstraint (NSLayoutConstraint.Create (TextField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, ImageView, NSLayoutAttribute.Height, 1.0f, 0.0f));
                AddConstraint (NSLayoutConstraint.Create (TextField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ImageView, NSLayoutAttribute.Top, 1.0f, 0.0f));
            }
        }

        public void DidSelectionChanged (bool isSelected)
        {
        }
    }
}

