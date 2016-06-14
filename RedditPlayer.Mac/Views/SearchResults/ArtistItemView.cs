using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using RedditPlayer.Mac.Views.SongsList;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public partial class ArtistItemView : NSCollectionViewItem
    {
        public SongThumbnailView ThumbnailView
        {
            get
            {
                return thumbnailView;
            }

            protected set
            {
                thumbnailView = value;
            }
        }

        #region Constructors

        // Called when created from unmanaged code
        public ArtistItemView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public ArtistItemView (NSCoder coder) : base (coder)
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
