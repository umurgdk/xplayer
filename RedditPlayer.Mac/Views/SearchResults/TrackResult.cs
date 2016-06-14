using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using RedditPlayer.Mac.Views.SongsList;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public partial class TrackResult : NSTableCellView
    {
        #region Constructors

        // Called when created from unmanaged code
        public TrackResult (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public TrackResult (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
        }

        #endregion

        public NSTextField TitleLabel
        {
            get
            {
                return titleLabel;
            }

            protected set
            {
                titleLabel = value;
            }
        }

        public NSTextField DurationLabel
        {
            get
            {
                return durationLabel;
            }

            protected set
            {
                durationLabel = value;
            }
        }

        public SongThumbnailView ThumbnailView
        {
            get
            {
                return songThumbnail;
            }

            protected set
            {
                songThumbnail = value;
            }
        }
    }
}
