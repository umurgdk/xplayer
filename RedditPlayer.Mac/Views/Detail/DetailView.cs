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

        NSLayoutConstraint [] playerViewConstraints;
        NSLayoutConstraint [] contentViewConstraints;

        public DetailView (SearchBarView searchBarView, PlayerView playerView)
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            SearchBarView = searchBarView;
            SearchBarView.SetContentHuggingPriorityForOrientation (251, NSLayoutConstraintOrientation.Vertical);

            PlayerView = playerView;
            PlayerView.SetContentHuggingPriorityForOrientation (251, NSLayoutConstraintOrientation.Vertical);

            AddSubview (SearchBarView);

            AddDefaultLayoutConstraints ();
        }

        public void ShowPlayerView ()
        {
            if (PlayerView.Superview == null) {
                AddSubview (PlayerView);

                if (playerViewConstraints == null) {
                    CreatePlayerViewConstraints ();
                }

                AddConstraints (playerViewConstraints);
            }
        }

        public void HidePlayerView ()
        {
            if (PlayerView.Superview == this) {
                PlayerView.RemoveFromSuperview ();
                RemoveConstraints (playerViewConstraints);
            }
        }

        public override void ViewDidMoveToWindow ()
        {
            base.ViewDidMoveToWindow ();
        }

        public void SetContentView (NSView view)
        {
            if (ContentView != null)
                ContentView.RemoveFromSuperview ();

            if (contentViewConstraints != null) {
                RemoveConstraints (contentViewConstraints);
            }

            AddSubview (view);

            CreateContentViewConstraints (view);
            AddConstraints (contentViewConstraints);
        }

        void AddDefaultLayoutConstraints ()
        {
            AddConstraint (PinTop (SearchBarView));
            AddConstraints (FillHorizontal (SearchBarView, false));
        }

        void CreatePlayerViewConstraints ()
        {
            var constraints = new List<NSLayoutConstraint> ();
            constraints.AddRange (FillHorizontal (PlayerView, false));
            constraints.Add (PinBottom (PlayerView));

            playerViewConstraints = constraints.ToArray ();
        }

        void CreateContentViewConstraints (NSView view)
        {
            var constraints = new List<NSLayoutConstraint> ();
            constraints.AddRange (FillHorizontal (view, false));
            constraints.AddRange (StackVertical (0, SearchBarView, view));
            constraints.Add (PinBottom (view));

            contentViewConstraints = constraints.ToArray ();
        }
    }
}

