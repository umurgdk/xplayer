using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using RedditPlayer.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using RedditPlayer.Mac.Views.SongsList;
using RedditPlayer.Domain.Media;
using RedditPlayer.Mac.Views.Media;
using RedditPlayer.Mac.Extensions;

namespace RedditPlayer.Mac.Views.ArtistDetail
{
    public partial class ArtistDetailViewController : ReactiveViewController
    {
        [Reactive]
        public ArtistDetailViewModel ViewModel { get; set; }

        PlayerViewModel playerViewModel;

        HeaderViewController headerViewController;
        SongListViewController songListViewController;
        AlbumListViewController albumListViewController;
        AlbumListViewController playlistsViewController;

        #region Constructors

        // Called when created from unmanaged code
        public ArtistDetailViewController (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public ArtistDetailViewController (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Call to load from the XIB/NIB file
        public ArtistDetailViewController (ArtistDetailViewModel viewModel, PlayerViewModel playerViewModel) : base ("ArtistDetailView", NSBundle.MainBundle)
        {
            ViewModel = viewModel;
            this.playerViewModel = playerViewModel;

            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
        }

        #endregion

        //strongly typed view accessor
        public new ArtistDetailView View
        {
            get
            {
                return (ArtistDetailView)base.View;
            }
        }

        NSTextField CreateDetailTitle (string title)
        {
            var textField = NSLabel.CreateWithFont ("SF UI Text Light", 18);
            textField.StringValue = title;

            return textField;
        }

        public override void ViewDidLoad ()
        {
            headerViewController = new HeaderViewController ();
            songListViewController = new SongListViewController ();

            albumListViewController = new AlbumListViewController ();
            albumListViewController.SongDoubleClicked += PlaySong;
            albumListViewPlaceholder.PresentView (albumListViewController.View);

            playlistsViewController = new AlbumListViewController ();
            playlistsViewController.SongDoubleClicked += PlaySong;
            playlistViewPlaceholder.PresentView (playlistsViewController.View);

            songListViewController.SongDoubleClicked += PlaySong;
            songListViewController.DisableScroll ();

            songListViewPlaceholder.PresentView (songListViewController.TableView);
            songListViewPlaceholder.InvalidateIntrinsicContentSize ();

            headerViewPlaceholder.PresentView (headerViewController.View);
            headerViewPlaceholder.InvalidateIntrinsicContentSize ();

            headerViewController.TabGroup.AddTab ("popularSongs", "Playlist", "Popular Songs");
            headerViewController.TabGroup.AddTab ("albums", "Star", "Albums");
            headerViewController.TabGroup.AddTab ("playlists", "Playlist", "Playlists");

            headerViewController.TabGroup.ActivateTab ("popularSongs");
            headerViewController.TabGroup.ActiveTabChanged += DidActiveTabChanged;

            this.WhenAnyValue (val => val.ViewModel)
                .Where (val => val != null)
                .DistinctUntilChanged ()
                .Subscribe (vm => BindViewModel ());
        }

        void BindViewModel ()
        {
            headerViewController.SetBackgroundImageUrl (ViewModel.Artist.PictureUrl);
            headerViewController.ArtistName = ViewModel.Artist.Name;
            headerViewController.ArtistBio = ViewModel.Artist.Bio;

            ViewModel.LoadPopularSongs.ExecuteAsyncTask ();
            ViewModel.LoadAlbums.ExecuteAsyncTask ();
            ViewModel.LoadPlaylists.ExecuteAsyncTask ();

            ViewModel.LoadPopularSongs.Subscribe (popularSongs => {
                if (popularSongs.Count > 0) {
                    headerViewController.TabGroup.AddTab ("popularSongs", "Playlist", "Popular Songs");
                } else {
                    headerViewController.TabGroup.RemoveTab ("popularSongs");
                }
            });

            ViewModel.LoadAlbums.Subscribe (albums => {
                if (albums.Count > 0) {
                    headerViewController.TabGroup.AddTab ("albums", "Album", "Albums");
                } else {
                    headerViewController.TabGroup.RemoveTab ("albums");
                }
            });

            ViewModel.LoadPlaylists.Subscribe (playlists => {
                if (playlists.Count > 0) {
                    headerViewController.TabGroup.AddTab ("playlists", "Playlist", "Playlists");
                } else {
                    headerViewController.TabGroup.RemoveTab ("playlists");
                }
            });

            songListViewController.Tracks = ViewModel.PopularSongs;
            albumListViewController.TrackLists = ViewModel.Albums.Select (TrackListViewModel.FromAlbum).ToList ();
            playlistsViewController.TrackLists = ViewModel.Playlists.Select (TrackListViewModel.FromPlaylist).ToList ();

            ViewModel.PopularSongs.Changed
                     .Subscribe (_ => songListViewController.Tracks = ViewModel.PopularSongs);

            ViewModel.Albums.Changed
                     .Subscribe (_ => {
                         albumListViewController.TrackLists = ViewModel.Albums.Select (TrackListViewModel.FromAlbum).ToList ();
                         albumListViewController.ReloadData ();
                     });

            ViewModel.Playlists.Changed
                     .Subscribe (_ => playlistsViewController.TrackLists = ViewModel.Playlists.Select (TrackListViewModel.FromPlaylist).ToList ());
        }

        void PlaySong (Track track)
        {
            playerViewModel.Play (track);
        }

        void DidActiveTabChanged (string identifier)
        {
            tabView.Select (new NSString (identifier));
        }
    }
}
