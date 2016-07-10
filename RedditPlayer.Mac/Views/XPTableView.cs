using System;
using AppKit;
using CoreGraphics;
using Foundation;
namespace RedditPlayer.Mac.Views
{
    [Register ("XPTableView")]
    public class XPTableView : NSTableView
    {
        public XPTableView ()
        {
        }

        public XPTableView (NSCoder coder) : base (coder)
        {
        }

        public XPTableView (NSObjectFlag t) : base (t)
        {
        }

        public XPTableView (IntPtr handle) : base (handle)
        {
        }

        public XPTableView (CGRect frameRect) : base (frameRect)
        {
        }

        public override CoreGraphics.CGSize IntrinsicContentSize
        {
            get
            {
                var height = Math.Max (RowCount, 1) * RowHeight;

                return new CGSize (1, height);
            }
        }
    }
}

