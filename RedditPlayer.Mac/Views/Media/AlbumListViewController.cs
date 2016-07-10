using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using ReactiveUI;
using RedditPlayer.Domain.Media;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using RedditPlayer.Mac.DataAdapters;
using RedditPlayer.ViewModels;

namespace RedditPlayer.Mac.Views.Media
{
    public partial class AlbumListViewController : ReactiveViewController, INSTableViewDataSource, INSTableViewDelegate
    {
        [Reactive]
        public IList<TrackListViewModel> TrackLists { get; set; }

        int? cachedRowCount;
        Dictionary<nint, bool> cachedGroupRowIndexes;
        Dictionary<nint, TrackListViewModel> albumsByRow;
        Dictionary<nint, Track> tracksByRow;

        public delegate void SongDoubleClickedHandler (Track track);
        public event SongDoubleClickedHandler SongDoubleClicked;

        uint? floatingRow;

        #region Constructors

        // Called when created from unmanaged code
        public AlbumListViewController (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public AlbumListViewController (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Call to load from the XIB/NIB file
        public AlbumListViewController () : base ("AlbumListView", NSBundle.MainBundle)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
            cachedGroupRowIndexes = new Dictionary<nint, bool> ();
            albumsByRow = new Dictionary<nint, TrackListViewModel> ();
            tracksByRow = new Dictionary<nint, Track> ();

            this.WhenAnyValue (c => c.TrackLists)
                .Where (albums => albums != null)
                .Subscribe (albums => {
                    tableView.ReloadData ();
                });
        }

        #endregion

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            View.TranslatesAutoresizingMaskIntoConstraints = false;

            tableView.DoubleClick += DidDoubleClick;
        }

        public void ReloadData ()
        {
            tableView.ReloadData ();
        }

        //strongly typed view accessor
        public new AlbumListView View
        {
            get
            {
                return (AlbumListView)base.View;
            }
        }

        void DidDoubleClick (object sender, EventArgs e)
        {
            var index = tableView.SelectedRow;
            var track = GetTrackByRowIndex (index);

            SongDoubleClicked?.Invoke (track);
        }

        [Export ("numberOfRowsInTableView:")]
        public nint GetRowCount (NSTableView tableView)
        {
            if (TrackLists == null)
                return 0;

            if (cachedRowCount.HasValue)
                return cachedRowCount.Value;

            cachedRowCount = TrackLists.Aggregate (0, (acc, album) => acc + album.Tracks.Count) + TrackLists.Count;
            return cachedRowCount.Value;
        }

        [Export ("tableView:isGroupRow:")]
        public bool IsGroupRow (NSTableView tableView, nint row)
        {
            if (cachedGroupRowIndexes.ContainsKey (row)) {
                return cachedGroupRowIndexes [row];
            }

            int index = 0;
            foreach (var album in TrackLists) {
                cachedGroupRowIndexes [index] = true;

                if (index == row) {
                    return true;
                }

                foreach (var track in album.Tracks) {
                    index += 1;
                    cachedGroupRowIndexes [index] = false;

                    if (index == row) {
                        return false;
                    }
                }

                index += 1;
            }

            return false;
        }

        TrackListViewModel GetTracklistByRowIndex (nint rowIndex)
        {
            if (albumsByRow.ContainsKey (rowIndex))
                return albumsByRow [rowIndex];

            int index = 0;
            foreach (var trackList in TrackLists) {
                albumsByRow [index] = trackList;

                if (index == rowIndex)
                    return trackList;

                index += trackList.Tracks.Count + 1;
            }

            throw new Exception (string.Format ("Album with index {0} not found!", rowIndex));
        }

        Track GetTrackByRowIndex (nint rowIndex)
        {
            if (tracksByRow.ContainsKey (rowIndex))
                return tracksByRow [rowIndex];

            int index = 0;
            foreach (var album in TrackLists) {
                foreach (var track in album.Tracks) {
                    index += 1;

                    tracksByRow [index] = track;

                    if (index == rowIndex) {
                        return track;
                    }
                }

                index += 1;
            }

            throw new Exception (string.Format ("Tracks with index {0} not found!", rowIndex));
        }

        [Export ("tableView:viewForTableColumn:row:")]
        public NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var isGroup = IsGroupRow (tableView, row);

            if (isGroup) {
                var tracklist = GetTracklistByRowIndex (row);
                var nsview = tableView.MakeView ("AlbumGroupRowView", this);
                var view = (AlbumGroupRowView)nsview;

                view.SetAlbumCoverFromUrl (tracklist.ImageUrl);
                view.ReleaseDate = tracklist.CreatedAt;
                view.AlbumName = tracklist.Title;

                return view;
            } else {
                var track = GetTrackByRowIndex (row);
                var view = tableView.MakeView ("AlbumSongRowView", this) as AlbumSongRowView;

                view.Order = track.OrderInAlbum;
                view.SongTitle = track.Title;
                view.SongDuration = track.Duration;

                return view;
            }
        }

        [Export ("tableView:heightOfRow:")]
        public nfloat GetRowHeight (NSTableView tableView, nint row)
        {
            return IsGroupRow (tableView, row) ? 102 : 32;
        }

        [Export ("tableView:rowViewForRow:")]
        public NSTableRowView CoreGetRowView (NSTableView tableView, nint row)
        {
            var rowView = new GrayRowView ();
            return rowView;
        }
    }
}
