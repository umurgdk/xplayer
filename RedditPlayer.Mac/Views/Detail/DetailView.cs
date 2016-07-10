using System;
using AppKit;
using ReactiveUI;
using RedditPlayer.Mac.Views.Player;
using RedditPlayer.Mac.Views.SearchBar;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
//using RedditPlayer.Mac.Views.SongsList;
using System.Collections.Generic;

namespace RedditPlayer.Mac.Views.Detail
{
    public class DetailView : ReactiveView, IDisposable
    {
        public readonly SearchBarView SearchBarView;

        public readonly PlayerView PlayerView;

        public NSView ContentView { get; protected set; }

        NSLayoutConstraint [] contentViewConstraints;

        public BreadcrumbView BreadcrumbView { get; protected set; }

        NSLayoutConstraint [] breadcrumbViewConstraints;

        public DetailView (SearchBarView searchBarView, PlayerView playerView)
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            WantsLayer = true;

            SearchBarView = searchBarView;
            SearchBarView.SetContentHuggingPriorityForOrientation (251, NSLayoutConstraintOrientation.Vertical);

            PlayerView = playerView;
            PlayerView.SetContentHuggingPriorityForOrientation (251, NSLayoutConstraintOrientation.Vertical);

            BreadcrumbView = new BreadcrumbView ();

            AddSubview (SearchBarView);
            AddSubview (PlayerView);

            AddDefaultLayoutConstraints ();
        }

        public override void ViewDidMoveToWindow ()
        {
            base.ViewDidMoveToWindow ();
        }

        public void SetContentView (NSView view)
        {
            if (view == ContentView)
                return;

            if (ContentView != null)
                ContentView.RemoveFromSuperview ();

            if (contentViewConstraints != null) {
                RemoveConstraints (contentViewConstraints);
            }

            ContentView = view;

            AddSubview (view, NSWindowOrderingMode.Below, SearchBarView);

            CreateContentViewConstraints (view);
            AddConstraints (contentViewConstraints);
        }

        void AddDefaultLayoutConstraints ()
        {
            AddConstraint (PinTop (SearchBarView));
            AddConstraints (FillHorizontal (SearchBarView, false));

            AddConstraints (FillHorizontal (PlayerView, false));
            AddConstraint (PinBottom (PlayerView));
            AddConstraint (MinimumWidth (this, 400));
        }

        void CreateContentViewConstraints (NSView view)
        {
            var constraints = new List<NSLayoutConstraint> ();
            constraints.AddRange (FillHorizontal (view, false));
            constraints.AddRange (StackVertical (0, SearchBarView, view));
            constraints.Add (PinBottom (view));

            contentViewConstraints = constraints.ToArray ();
        }

        void ShowBreadcrumb ()
        {
            if (BreadcrumbView.Superview == this)
                return;

            AddSubview (BreadcrumbView);

            var breadcrumbConstraints = new List<NSLayoutConstraint> ();
            breadcrumbConstraints.AddRange (FillHorizontal (BreadcrumbView, false));
            breadcrumbConstraints.Add (PinTop (BreadcrumbView));
        }
    }
}

