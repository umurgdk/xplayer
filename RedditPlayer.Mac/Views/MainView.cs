using System;
using AppKit;
using RedditPlayer.Mac.Views.Detail;
using RedditPlayer.Mac.Views.Playlists;
using RedditPlayer.Mac.Views.SearchResults;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using System.Collections.Generic;

namespace RedditPlayer.Mac.Views
{
    public class MainView : NSSplitView
    {
        NSLayoutConstraint [] sidebarConstraints;
        NSLayoutConstraint [] contentConstraints;

        public MainView ()
        {
            // Create splitview
            IsVertical = true;
            DividerStyle = NSSplitViewDividerStyle.Thin;
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public NSView Sidebar
        {
            get
            {
                if (ArrangedSubviews.Length > 0)
                    return ArrangedSubviews [0];

                return null;
            }

            set
            {
                if (ArrangedSubviews.Length >= 1)
                    RemoveArrangedSubview (ArrangedSubviews [0]);

                //if (sidebarConstraints != null)
                //    RemoveConstraints (sidebarConstraints);

                InsertArrangedSubview (value, 0);

                //var constraints = new List<NSLayoutConstraint> ();
                //constraints.AddRange (FillHorizontal (false, value));
                //constraints.AddRange (FillVertical (false, value));

                //sidebarConstraints = constraints.ToArray ();
                //AddConstraints (sidebarConstraints);

                //value.SetContentHuggingPriorityForOrientation (500, NSLayoutConstraintOrientation.Horizontal);
            }
        }

        public NSView Content
        {
            get
            {
                if (ArrangedSubviews.Length > 1)
                    return ArrangedSubviews [1];

                return null;
            }

            set
            {
                if (ArrangedSubviews.Length > 1)
                    RemoveArrangedSubview (ArrangedSubviews [1]);

                //if (contentConstraints != null)
                //    RemoveConstraints (contentConstraints);

                InsertArrangedSubview (value, 1);

                //var constraints = new List<NSLayoutConstraint> ();
                //constraints.AddRange (FillHorizontal (false, value));
                //constraints.AddRange (FillVertical (false, value));

                //contentConstraints = constraints.ToArray ();

                //AddConstraints (contentConstraints);

                //value.SetContentHuggingPriorityForOrientation (250, NSLayoutConstraintOrientation.Horizontal);
                SetHoldingPriority (1, 1);
            }
        }
    }
}

