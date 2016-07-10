using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using RedditPlayer.Mac.Views.SearchResults;

namespace RedditPlayer.Mac.Views.ArtistDetail
{
    public partial class HeaderViewController : AppKit.NSViewController
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
                return thumbnailView.Image;
            }

            set
            {
                thumbnailView.Image = value;
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

        #region Constructors

        // Called when created from unmanaged code
        public HeaderViewController (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public HeaderViewController (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Call to load from the XIB/NIB file
        public HeaderViewController () : base ("HeaderView", NSBundle.MainBundle)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
        }

        #endregion

        //strongly typed view accessor
        public new HeaderView View
        {
            get
            {
                return (HeaderView)base.View;
            }
        }

        public void SetBackgroundImageUrl (string url)
        {
            thumbnailView.SetImageAsync (url);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            tabGroup.DarkTheme = true;
            thumbnailView.OverlayAlpha = 0.5f;
        }
    }
}
