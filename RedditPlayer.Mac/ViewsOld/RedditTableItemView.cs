using System;
using AppKit;
using Foundation;

namespace RedditPlayer.Mac
{
    [Register ("RedditTableItemView")]
    public partial class RedditTableItemView : NSTableCellView
    {
        [Outlet]
        public NSTextField Title { get; set; }

        public RedditTableItemView ()
        {
        }

        [Export ("initWithCoder:")]
        public RedditTableItemView (Foundation.NSCoder coder) : base (coder)
        {
        }

        public RedditTableItemView (Foundation.NSObjectFlag t) : base (t)
        {
        }

        public RedditTableItemView (IntPtr handle) : base (handle)
        {
        }
    }
}

