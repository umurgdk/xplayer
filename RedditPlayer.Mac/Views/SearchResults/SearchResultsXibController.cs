using System;
using Foundation;
using AppKit;
using ReactiveUI;
using RedditPlayer.Mac.Views.SongsList;
using RedditPlayer.Domain.Media;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public partial class SearchResultsXibController : ReactiveViewController, INSTableViewDelegate, INSTableViewDataSource, INSCollectionViewDataSource, INSCollectionViewDelegate
    {
        ApplicationViewModel viewModel;

        SongListViewController songListViewController;

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

            viewModel.Search.Tracks.Changed.Subscribe (_ => songListViewController.Tracks = viewModel.Search.Tracks);
            viewModel.Search.Artists.Changed.Subscribe (_ => {
                artistsCollectionView.ReloadData ();
            });
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

            songListViewController = new SongListViewController ();

            var itemIndex = tabView.IndexOf (new NSString ("songs"));
            var tabViewItem = tabView.Items [itemIndex];
            tabViewItem.View.AddSubview (songListViewController.View);

            tabViewItem.View.AddConstraints (FillHorizontal (songListViewController.View, false));
            tabViewItem.View.AddConstraints (FillVertical (songListViewController.View, false));

            songListViewController.Tracks = viewModel.Search.Tracks;
            songListViewController.SongDoubleClicked += DidSongDoubleClicked;

            tabGroup.AddTab ("songs", "Playlist", "Tracks");
            tabGroup.AddTab ("videos", "Video", "Videos");
            tabGroup.AddTab ("artists", "Artist", "Artists");

            tabGroup.ActiveTabChanged += OnActiveTabChanged;

            tabGroup.ActivateTab ("songs");

            artistsCollectionView.Delegate = this;
            artistsCollectionView.Selectable = true;
            artistsCollectionView.RegisterNib (new NSNib ("ArtistItemView", NSBundle.MainBundle), "ArtistItemView");
            artistsCollectionView.RegisterClassForItem (typeof (ArtistItemView), "ArtistItemView");

            artistsCollectionView.ReloadData ();
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

        void DidSongDoubleClicked (Track track)
        {
            viewModel.Player.Play (track);
        }

        void OnActiveTabChanged (string identifier)
        {
            tabView.Select (new NSString (identifier));

            if (identifier == "artists")
                artistsCollectionView.ReloadData ();
        }

        [Export ("numberOfSectionsInCollectionView:")]
        public nint GetNumberOfSections (NSCollectionView collectionView)
        {
            return 1;
        }

        [Export ("collectionView:numberOfItemsInSection:")]
        public nint GetNumberofItems (NSCollectionView collectionView, nint section)
        {
            return viewModel.Search.Artists.Count;
        }

        [Export ("collectionView:itemForRepresentedObjectAtIndexPath:")]
        public NSCollectionViewItem GetItem (NSCollectionView collectionView, NSIndexPath indexPath)
        {
            var item = collectionView.MakeItem ("ArtistItemView", indexPath) as ArtistItemView;

            var artist = viewModel.Search.Artists [(int)indexPath.Item];
            item.Artist = artist;

            return item;
        }

        [Export ("collectionView:shouldSelectItemsAtIndexPaths:")]
        public NSSet ShouldSelectItems (NSCollectionView collectionView, NSSet indexPaths)
        {
            return indexPaths;
        }
    }
}
