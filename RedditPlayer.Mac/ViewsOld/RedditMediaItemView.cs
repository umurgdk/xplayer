using AppKit;
using Foundation;
namespace RedditPlayer.Mac
{
    [Register ("RedditMediaItemView")]
    class RedditMediaItemView : NSTableCellView
    {
        [Outlet]
        public NSImageView Image { get; set; }

        [Outlet]
        public NSTextField Title { get; set; }

        [Outlet]
        public NSButton PlayButton { get; set; }

        [Outlet]
        public NSTextField Url { get; set; }

        public RedditMediaItemView ()
        {
        }

        [Export ("initWithCoder:")]
        public RedditMediaItemView (NSCoder coder) : base (coder)
        {
        }

        public RedditMediaItemView (NSObjectFlag t) : base (t)
        {
        }

        public RedditMediaItemView (System.IntPtr handle) : base (handle)
        {
        }
    }
}