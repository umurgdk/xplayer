using System;
using Foundation;
using AppKit;
using RedditPlayer.Mac.Views.SongsList;

namespace RedditPlayer.Mac.Views.Media
{
    [Register ("AlbumGroupRowView")]
    public partial class AlbumGroupRowView : NSTableCellView
    {
        DateTime releaseDate;

        public DateTime ReleaseDate
        {
            get
            {
                return releaseDate;
            }
            set
            {
                releaseDate = value;
                releaseDateView.StringValue = releaseDate.ToString ("yyyy");
            }
        }

        public string AlbumName
        {
            get
            {
                return nameView.StringValue;
            }

            set
            {
                nameView.StringValue = value;
            }
        }

        public NSImage AlbumCover
        {
            get
            {
                return thumbnailView.Image;
            }

            set
            {
                thumbnailView.Image = value;
            }
        }

        public void SetAlbumCoverFromUrl (string url)
        {
            thumbnailView.SetImageAsync (url);
        }

        #region Constructors

        // Called when created from unmanaged code
        public AlbumGroupRowView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public AlbumGroupRowView (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        public AlbumGroupRowView ()
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
            Identifier = "AlbumGroupRowView";
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        #endregion

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();
            thumbnailView.BorderWidth = 1;
            thumbnailView.BorderColor = NSColor.FromRgb (230, 230, 230).CGColor;
        }

        [Outlet]
        public SongThumbnailView thumbnailView { get; set; }

        [Outlet]
        public NSTextField nameView { get; set; }

        [Outlet]
        public NSTextField releaseDateView { get; set; }

        [Outlet]
        public NSButton addToPlaylistButton { get; set; }

        [Outlet]
        public NSLayoutConstraint thumbnailHeightConstraint { get; set; }
    }
}
