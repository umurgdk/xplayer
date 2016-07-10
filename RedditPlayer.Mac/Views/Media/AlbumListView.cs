using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace RedditPlayer.Mac.Views.Media
{
    public partial class AlbumListView : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public AlbumListView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public AlbumListView (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
        }

        #endregion
    }
}
