using System;
using AppKit;
using Foundation;
using RedditPlayer.Mac.Views.SearchResults;
namespace RedditPlayer.Mac.Views.ArtistDetail
{
    [Register ("ArtistDetailHeaderViewController")]
    public partial class ArtistDetailHeaderViewController : NSViewController
    {
        public string ArtistName
        {
            get
            {
                return artistNameView.StringValue;
            }

            set
            {
                artistNameView.StringValue = value;
            }
        }

        public string ArtistBio
        {
            get
            {
                return artistBioView.StringValue;
            }

            set
            {
                artistBioView.StringValue = value;
            }
        }

        public NSImage BackgroundImage
        {
            get
            {
                return backgroundImage.Image;
            }

            set
            {
                backgroundImage.Image = value;
            }
        }

        public SearchResultsTabGroup TabGroup
        {
            get
            {
                return tabGroup;
            }

            set
            {
                tabGroup = value;
            }
        }

        public ArtistDetailHeaderViewController (NSCoder coder) : base (coder)
        {
        }

        public ArtistDetailHeaderViewController (IntPtr handle) : base (handle)
        {
        }

        public ArtistDetailHeaderViewController () : base ("ArtistDetailHeaderView", NSBundle.MainBundle)
        {
        }

        public void SetBackgroundImageUrl (string url)
        {
            backgroundImage.SetImageAsync (url);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            View.TranslatesAutoresizingMaskIntoConstraints = false;
            tabGroup.DarkTheme = true;
        }
    }
}

