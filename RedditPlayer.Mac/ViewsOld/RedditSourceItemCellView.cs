using System;
using AppKit;
using Foundation;
using CoreGraphics;
using RedditPlayer.Mac.Extensions;
namespace RedditPlayer.Mac.Views
{
    [Register ("RedditSourceItemViewCell")]
    public class RedditSourceItemCellView : NSView
    {
        [Outlet]
        public AppKit.NSTextField Title { get; set; }

        [Outlet]
        public AppKit.NSImageView ImageView { get; set; }

        private bool selected;
        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
                UpdateSelectedStyle ();
                NeedsDisplay = true;
            }
        }

        public NSColor SelectedColor = NSColor.FromRgb (255, 199, 0);
        public NSColor HeaderColor = NSColor.FromRgb (102, 102, 102);
        public NSColor ItemColor = NSColor.FromRgb (170, 170, 170);

        RedditsSourceItem item;

        public RedditSourceItemCellView (IntPtr handle) : base (handle)
        {
        }

        public RedditSourceItemCellView ()
        {
        }

        public RedditSourceItemCellView (Foundation.NSCoder coder) : base (coder)
        {
        }

        public RedditSourceItemCellView (Foundation.NSObjectFlag t) : base (t)
        {
        }

        public void SetItem (RedditsSourceItem item)
        {
            this.item = item;

            if (item.IsCategory) {
                HideImageView ();
                SetCategoryStyle ();
            } else {
                ShowImageView ();
                SetItemStyle ();
                ImageView.Image = NSImage.ImageNamed (item.Icon);
            }

            Title.StringValue = item.Title;
        }

        void SetItemStyle ()
        {
            var font = NSFont.FromFontName ("SF UI Display Regular", 12);
            Title.Font = font;
            Title.TextColor = ItemColor;
            Title.Frame = new CGRect (new CGPoint (23 + 22, 0), new CGSize (Frame.Width - (23 + 22), Frame.Height));
            ImageView.Frame = new CGRect (new CGPoint (23, ImageView.Frame.Y), ImageView.Frame.Size);
        }

        void SetCategoryStyle ()
        {
            var font = NSFont.FromFontName ("SF UI Text Regular", 11);
            Title.Font = font;
            Title.TextColor = HeaderColor;
            Title.Frame = new CGRect (new CGPoint (14, 0), new CGSize (Frame.Width - 14, Frame.Height));
        }

        void HideImageView ()
        {
            if (Subviews.Length > 1) {
                ImageView.RemoveFromSuperview ();
            }
        }

        void ShowImageView ()
        {
            if (Subviews.Length == 1) {
                var left = ImageView.Frame.Width + 7;
                AddSubview (ImageView);
            }
        }

        void UpdateSelectedStyle ()
        {
            if (Selected) {
                ImageView.Image = ImageView.Image.Tint (SelectedColor);
            } else {
                ImageView.Image = NSImage.ImageNamed (item.Icon);
            }

            var textNormalColor = item.IsCategory ? HeaderColor : ItemColor;
            Title.TextColor = Selected ? SelectedColor : textNormalColor;
        }
    }
}

