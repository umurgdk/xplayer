using System;
using AppKit;
using ReactiveUI;
using RedditPlayer.Mac.Views.Player;
using RedditPlayer.Mac.Views.SearchBar;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using RedditPlayer.Mac.Views.SongsList;

namespace RedditPlayer.Mac.Views.Detail
{
    public class DetailView : ReactiveView, IDisposable
    {
        public readonly SearchBarView SearchBarView;

        public readonly PlayerView PlayerView;

        NSStackView contentStack;
        NSStackView outerStack;

        public DetailView (SearchBarView searchBarView, PlayerView playerView)
        {
            contentStack = new NSStackView ();
            contentStack.TranslatesAutoresizingMaskIntoConstraints = false;
            contentStack.Orientation = NSUserInterfaceLayoutOrientation.Vertical;
            contentStack.SetHuggingPriority ((float)NSLayoutPriority.WindowSizeStayPut, NSLayoutConstraintOrientation.Horizontal);
            contentStack.Spacing = 0;

            outerStack = new NSStackView ();
            outerStack.TranslatesAutoresizingMaskIntoConstraints = false;
            outerStack.Orientation = NSUserInterfaceLayoutOrientation.Vertical;
            outerStack.SetHuggingPriority ((float)NSLayoutPriority.WindowSizeStayPut, NSLayoutConstraintOrientation.Horizontal);
            outerStack.Spacing = 0;


            SearchBarView = searchBarView;
            contentStack.AddArrangedSubview (searchBarView);

            PlayerView = playerView;
            PlayerView.SetContentHuggingPriorityForOrientation ((float)NSLayoutPriority.DefaultHigh, NSLayoutConstraintOrientation.Vertical);

            outerStack.AddArrangedSubview (contentStack);

            AddSubview (outerStack);

            AddDefaultLayoutConstraints ();
        }

        public void ShowPlayerView ()
        {
            outerStack.AddArrangedSubview (PlayerView);
        }

        public void HidePlayerView ()
        {
            outerStack.RemoveArrangedSubview (PlayerView);
        }

        public override void ViewDidMoveToWindow ()
        {
            base.ViewDidMoveToWindow ();
        }

        public void SetContentView (NSView view)
        {
            if (contentStack.ArrangedSubviews.Length > 1) {
                contentStack.RemoveArrangedSubview (contentStack.ArrangedSubviews [1]);
            }

            contentStack.AddArrangedSubview (view);
        }

        void AddDefaultLayoutConstraints ()
        {
            AddConstraints (FillHorizontal (outerStack, false));
            AddConstraints (FillVertical (outerStack, false));
        }
    }
}

