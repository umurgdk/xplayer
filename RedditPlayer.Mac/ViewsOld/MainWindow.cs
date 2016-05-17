using System;

using Foundation;
using AppKit;

namespace RedditPlayer.Mac
{
    public partial class MainWindow : NSWindow
    {
        public MainWindow (IntPtr handle) : base (handle)
        {
        }

        [Export ("initWithCoder:")]
        public MainWindow (NSCoder coder) : base (coder)
        {
        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();

            //ContentView.WantsLayer = true;
            //ContentView.Layer.Contents = NSImage.ImageNamed ("windowbg.jpg").CGImage;
        }
    }
}
