using System;
using System.Reactive.Linq;
using Foundation;
using AppKit;
using ReactiveUI;
using ReactiveUI.Legacy;
using RedditPlayer;
using System.Diagnostics;
using RedditPlayer.Mac.Views;

namespace RedditPlayer.Mac
{
    public partial class MainWindowController : ReactiveWindowController, IViewFor<ApplicationViewModel>
    {
        RedditMediasTableViewSource redditMediasSource;
        RedditsOutlineViewDataSource redditsOutlineSource;

        public MainWindowController (ApplicationViewModel viewModel, PlayerWindow window) : base (window)
        {
            ViewModel = viewModel;
        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();

            //redditMediasSource = new RedditMediasTableViewSource (ViewModel.RedditMedias);
            //RedditMediaTableView.Source = redditMediasSource;

            //redditsOutlineSource = new RedditsOutlineViewDataSource (RedditsOutlineView, ViewModel.Reddits);

            //ViewModel.Reddits.CollectionChanged += (sender, e) => RedditsOutlineView.ReloadData ();
            //ViewModel.RedditMedias.CollectionChanged += (sender, e) => RedditMediaTableView.ReloadData ();

            //NewRedditText.Changed += (sender, e) => ViewModel.NewSubRedditName = NewRedditText.StringValue;

            //this.BindCommand (ViewModel, vm => vm.AddSubreddit, window => window.AddButton);
            //this.Bind (ViewModel, vm => vm.CanEditSubRedditName, window => window.NewRedditText.Enabled);

            //ViewModel.AddSubreddit.Subscribe (_ => NewRedditText.StringValue = "");

            //ViewModel.WhenAnyValue (vm => vm.SelectedSubReddit)
            //         .Where (sr => sr != null)
            //         .Subscribe (sr => RedditDescription.StringValue = sr.Description);

            //redditsOutlineSource = new RedditsOutlineViewDataSource (RedditsOutlineView, ViewModel.Reddits);
            //RedditsOutlineView.DataSource = redditsOutlineSource;

            //var redditsOutlineDelegate = new RedditsOutlineViewDelegate (RedditsOutlineView, redditsOutlineSource);
            //RedditsOutlineView.Delegate = redditsOutlineDelegate;
            //redditsOutlineDelegate.ItemSelected += RedditItemSelected;

            //RedditsOutlineView.ReloadData ();
            //RedditsOutlineView.ExpandItem (null, true);
        }

        void RedditItemSelected (RedditsSourceItem item)
        {
            //if (item == null) {
            //    return;
            //}

            //ViewModel.SelectedSubReddit = item.SubReddit;

            //if (ViewModel.LoadMedia.CanExecute (null)) {
            //    Debug.WriteLine ("Loading medias");
            //    ViewModel.LoadMedia.ExecuteAsync ().Subscribe ();
            //} else {
            //    Debug.WriteLine ("Can't load medias");
            //}
        }

        public new PlayerWindow Window
        {
            get { return (PlayerWindow)base.Window; }
        }

        public ApplicationViewModel ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get
            {
                return ViewModel;
            }

            set
            {
                ViewModel = (ApplicationViewModel)value;
            }
        }
    }
}
