using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using RedditPlayer.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;

namespace RedditPlayer.Mac.Views.ArtistDetail
{
    public partial class ArtistDetailViewController : ReactiveViewController, INSTableViewDataSource, INSTableViewDelegate
    {
        [Reactive]
        public ArtistDetailViewModel ViewModel { get; set; }

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
        public ArtistDetailViewController (ArtistDetailViewModel viewModel) : base ("ArtistDetailView", NSBundle.MainBundle)
        {
            Initialize ();

            ViewModel = viewModel;
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
            var popularSongs = CreateDetailTitle ("Popular Songs");

            ContentStack.AddArrangedSubview (popularSongs);

            var popularSongsTableView = new NSTableView ();
            popularSongsTableView.AddColumn (new NSTableColumn ("Song"));
            popularSongsTableView.DataSource = this;

            ContentStack.AddArrangedSubview (popularSongsTableView);

            var albums = CreateDetailTitle ("Albums");

            ContentStack.AddArrangedSubview (albums);

            var albumsCollection = new NSCollectionView ();
            ContentStack.AddArrangedSubview (albumsCollection);

            this.WhenAnyValue (val => val.ViewModel)
                .Where (val => val != null)
                .DistinctUntilChanged ()
                .Subscribe (vm => BindViewModel ());
        }

        void BindViewModel ()
        {
            ArtistImageView.SetImageAsync (ViewModel.Artist.PictureUrl);
            ArtistName.StringValue = ViewModel.Artist.Name;
            ArtistBio.StringValue = ViewModel.Artist.Bio;
        }
    }
}
