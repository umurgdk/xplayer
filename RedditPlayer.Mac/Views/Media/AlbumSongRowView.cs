using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace RedditPlayer.Mac.Views.Media
{
    [Register ("AlbumSongRowView")]
    public class AlbumSongRowView : NSTableCellView
    {
        int order;
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
                orderView.StringValue = order.ToString ();
            }
        }

        public string SongTitle
        {
            get
            {
                return titleView.StringValue;
            }

            set
            {
                titleView.StringValue = value;
            }
        }

        TimeSpan duration;
        public TimeSpan SongDuration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
                durationView.StringValue = duration.ToString (duration.TotalHours >= 1 ? @"hh\:mm\:ss" : @"mm\:ss");
            }
        }

        #region Constructors

        // Called when created from unmanaged code
        public AlbumSongRowView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public AlbumSongRowView (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        public AlbumSongRowView ()
        {
        }

        // Shared initialization code
        void Initialize ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        #endregion

        [Outlet]
        public NSTextField durationView { get; set; }

        [Outlet]
        public NSTextField orderView { get; set; }

        [Outlet]
        public NSTextField titleView { get; set; }
    }
}

