using System;
using Foundation;
using AppKit;
using RedditPlayer.Mac.Views.SongsList;
using RedditPlayer.Domain.Media;
using RedditPlayer.ViewModels;
using Splat;
using RedditPlayer.Services;

namespace RedditPlayer.Mac.Views.SearchResults
{
    public partial class ArtistItemView : NSCollectionViewItem
    {
        Artist artist;
        public Artist Artist
        {
            get
            {
                return artist;
            }

            set
            {
                artist = value;
                UpdateView ();
            }
        }

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

        public override bool AcceptsFirstResponder () => true;

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

        public override void MouseDown (NSEvent theEvent)
        {
            var location = theEvent.LocationInWindow;

            if (!View.ConvertRectToView (View.Bounds, View.Window.ContentView).Contains (location))
                return;

            var artistViewModel = new ArtistDetailViewModel (artist);
            var navigator = Locator.CurrentMutable.GetService<INavigator> ();
            navigator.PresentArtist (artistViewModel);
        }

        void UpdateView ()
        {
            thumbnailView.SetImageAsync (artist.PictureUrl);
            TextField.StringValue = artist.Name;
        }
    }
}
