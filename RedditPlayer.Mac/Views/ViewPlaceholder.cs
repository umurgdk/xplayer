using System;
using AppKit;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using Foundation;
namespace RedditPlayer.Mac.Views
{
    [Register ("ViewPlaceholder")]
    public class ViewPlaceholder : NSView
    {
        NSLayoutConstraint [] horizontalConstraints;
        NSLayoutConstraint [] verticalConstraints;

        public ViewPlaceholder ()
        {
            Initialize ();
        }

        public ViewPlaceholder (Foundation.NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        public ViewPlaceholder (Foundation.NSObjectFlag t) : base (t)
        {
            Initialize ();
        }

        public ViewPlaceholder (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        public ViewPlaceholder (CoreGraphics.CGRect frameRect) : base (frameRect)
        {
            Initialize ();
        }

        void Initialize ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public void PresentView (NSView view)
        {
            if (Subviews.Length > 0 && view == Subviews [0])
                return;

            if (horizontalConstraints != null)
                RemoveConstraints (horizontalConstraints);

            if (verticalConstraints != null)
                RemoveConstraints (verticalConstraints);

            if (Subviews.Length > 0)
                Subviews [0].RemoveFromSuperview ();

            AddSubview (view);

            horizontalConstraints = FillHorizontal (view, false);
            verticalConstraints = FillVertical (view, false);

            AddConstraints (horizontalConstraints);
            AddConstraints (verticalConstraints);
        }

        public void PresentViewController (NSViewController viewController)
        {
            PresentView (viewController.View);
        }
    }
}

