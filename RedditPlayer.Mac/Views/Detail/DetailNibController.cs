using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace RedditPlayer.Mac.Views.Detail
{
    public partial class DetailNibController : AppKit.NSViewController
    {
        #region Constructors

        // Called when created from unmanaged code
        public DetailNibController (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public DetailNibController (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Call to load from the XIB/NIB file
        public DetailNibController () : base ("DetailNib", NSBundle.MainBundle)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
        }

        #endregion

        //strongly typed view accessor
        public new DetailNib View
        {
            get
            {
                return (DetailNib)base.View;
            }
        }
    }
}
