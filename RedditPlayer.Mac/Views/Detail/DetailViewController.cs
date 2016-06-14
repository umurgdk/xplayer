using System;
using System.Reactive.Linq;
using ReactiveUI;
using AppKit;
using RedditPlayer.ViewModels;
using RedditPlayer.Mac.Views.SearchBar;
using RedditPlayer.Mac.Views.Player;

namespace RedditPlayer.Mac.Views.Detail
{
    public class DetailViewController : ReactiveViewController, IDisposable
    {
        public new DetailView View
        {
            get { return (DetailView)base.View; }
            set { base.View = value; }
        }

        public DetailViewController (ApplicationViewModel appModel, SearchBarViewController searchBarViewController, PlayerViewController playerViewController)
        {
            View = new DetailView (searchBarViewController.View, playerViewController.View);
        }

        public void SetContentView (NSView contentView)
        {
            View.SetContentView (contentView);
        }
    }
}

