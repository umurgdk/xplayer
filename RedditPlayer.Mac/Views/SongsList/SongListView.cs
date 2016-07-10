using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace RedditPlayer.Mac.Views.SongsList
{
    public partial class SongListView : NSScrollView
    {
        #region Constructors

        // Called when created from unmanaged code
        public SongListView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public SongListView (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        public SongListView ()
        {
            Initialize ();
        }

        public SongListView (NSObjectFlag t) : base (t)
        {
            Initialize ();
        }

        public SongListView (CoreGraphics.CGRect frameRect) : base (frameRect)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
            Identifier = "SongListView";
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        #endregion
    }
}
