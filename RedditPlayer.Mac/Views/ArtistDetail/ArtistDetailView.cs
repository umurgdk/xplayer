using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace RedditPlayer.Mac.Views.ArtistDetail
{
    public partial class ArtistDetailView : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public ArtistDetailView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public ArtistDetailView (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
            Identifier = "ArtistDetailView";
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        #endregion
    }
}
