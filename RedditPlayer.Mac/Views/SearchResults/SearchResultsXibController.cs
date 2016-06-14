using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using RedditPlayer.Mac.Extensions;
using ReactiveUI;
using System.Threading.Tasks;
using RedditPlayer.Mac.Views.SongsList;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public partial class SearchResultsXibController : ReactiveViewController, INSTableViewDelegate, INSTableViewDataSource
    {
        ApplicationViewModel viewModel;

        #region Constructors

        // Called when created from unmanaged code
        public SearchResultsXibController (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public SearchResultsXibController (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Call to load from the XIB/NIB file
        public SearchResultsXibController (ApplicationViewModel viewModel) : base ("SearchResultsXib", NSBundle.MainBundle)
        {
            Initialize ();
            this.viewModel = viewModel;
        }

        // Shared initialization code
        void Initialize ()
        {
        }

        #endregion

        //strongly typed view accessor
        public new SearchResultsXib View
        {
            get
            {
                return (SearchResultsXib)base.View;
            }
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            View.Identifier = "SearchResultsXibView";
            View.TranslatesAutoresizingMaskIntoConstraints = false;

            tableView.ReloadData ();
            tableView.DoubleClick += TableDoubleClicked;

            viewModel.Search.Tracks.Changed.Subscribe (_ => tableView.ReloadData ());

            tabGroup.AddTab ("songs", "Playlist", "Tracks");
            tabGroup.AddTab ("videos", "Video", "Videos");
            tabGroup.AddTab ("artists", "Artist", "Artists");
            tabGroup.ActivateTab ("songs");
        }

        [Export ("numberOfRowsInTableView:")]
        public nint GetRowCount (NSTableView tableView)
        {
            return viewModel.Search.Tracks.Count;
        }

        [Export ("tableView:viewForTableColumn:row:")]
        public NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var track = viewModel.Search.Tracks [(int)row];

            if (tableColumn.Identifier == "Image") {
                var view = tableView.MakeView (tableColumn.Identifier, this) as SongThumbnailView;
                if (track.CoverUrl == null) {
                    view.Image = NSImage.ImageNamed ("EmptyCover");
                } else {
                    view.SetImageAsync (track.CoverUrl);
                }

                return view;
            }

            if (tableColumn.Identifier == "Title") {
                var view = tableView.MakeView (tableColumn.Identifier, this) as NSTableCellView;
                view.TextField.StringValue = track.Title;
                return view;
            }

            if (tableColumn.Identifier == "Duration") {
                var view = tableView.MakeView (tableColumn.Identifier, this) as NSTableCellView;
                view.TextField.StringValue = track.Duration.ToString ("g");
                return view;
            }

            return null;
        }

        void TableDoubleClicked (object sender, EventArgs e)
        {
            var row = (int)tableView.ClickedRow;

            if (row < viewModel.Search.Tracks.Count) {
                viewModel.Player.Play (viewModel.Search.Tracks [row]);
            }
        }
    }
}
