using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Mac.Views.SongsList
{
    public partial class SongListViewController : NSViewController, INSTableViewDelegate, INSTableViewDataSource
    {
        const string SongImageIdentifier = "Image";
        const string TitleIdentifier = "Title";
        const string DurationIdentifier = "Duration";

        public IList<Track> Tracks { get; protected set; }

        public delegate void SongDoubleClickedHandler (Track track);
        public event SongDoubleClickedHandler SongDoubleClicked;

        NSLayoutConstraint scrollViewConstraint;

        public SongListViewController (IList<Track> tracks) : base ("SongListView", NSBundle.MainBundle)
        {
            Tracks = tracks;
        }

        //strongly typed view accessor
        public new SongListView View
        {
            get
            {
                return (SongListView)base.View;
            }
        }

        public override void ViewDidLoad ()
        {
            tableView.DoubleClick += OnSongDoubleClicked;
        }

        public void DisableScroll ()
        {
            if (scrollViewConstraint == null) {
                scrollViewConstraint = NSLayoutConstraint.Create (View, NSLayoutAttribute.Height, NSLayoutRelation.Equal, tableView, NSLayoutAttribute.Height, 1.0f, 0.0f);
            }

            View.AddConstraint (scrollViewConstraint);
        }

        public void EnableScroll ()
        {
            if (scrollViewConstraint != null) {
                View.RemoveConstraint (scrollViewConstraint);
            }
        }

        #region NSTableViewSource

        [Export ("numberOfRowsInTableView:")]
        public nint GetRowCount (NSTableView tableView)
        {
            return Tracks.Count;
        }

        #endregion

        #region NSTableViewDelegate

        [Export ("tableView:viewForTableColumn:row:")]
        public NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var track = Tracks [(int)row];

            if (tableColumn.Identifier == SongImageIdentifier) {
                var view = tableView.MakeView (SongImageIdentifier, this) as SongThumbnailView;
                view.SetImageAsync (track.CoverUrl);
                return view;
            }

            if (tableColumn.Identifier == TitleIdentifier) {
                var view = tableView.MakeView (TitleIdentifier, this) as NSTableCellView;
                view.TextField.StringValue = track.Title;
                return view;
            }

            if (tableColumn.Identifier == DurationIdentifier) {
                var view = tableView.MakeView (DurationIdentifier, this) as NSTableCellView;
                view.TextField.StringValue = track.Duration.ToString ("m:s");
                return view;
            }

            return null;
        }

        #endregion

        void OnSongDoubleClicked (object sender, EventArgs e)
        {
            var index = (int)tableView.ClickedRow;

            if (index < Tracks.Count) {
                SongDoubleClicked?.Invoke (Tracks [index]);
            }
        }
    }
}
